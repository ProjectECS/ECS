using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using ChiaraMail.Properties;
using Microsoft.Office.Interop.Outlook;
using Microsoft.Office.Core;
using Redemption;
using Exception = System.Exception;
using Timer = System.Threading.Timer;

namespace ChiaraMail.Wrappers
{
    internal class ExplWrap
    {
        private Explorer _explorer;
        private CommandBarButton _configButton;
        private CommandBarButton _helpButton;
        private CommandBarButton _supportButton;
        private const string CLASS_NAME = "ExplWrap.";
        private Timer _timer;
        private MailItem _mailItem;
        private string _sendUsingAddress;

        public ExplWrap(Explorer explorer)
        {
            const string SOURCE = CLASS_NAME + ".ctor";
            if (explorer == null) { return; }
            Key = Guid.NewGuid().ToString();
            Logger.Info(SOURCE,"initializing new ExplWrap with key " + Key);
            Explorer = explorer;
            var folder = Explorer.CurrentFolder;
            FolderId = folder.EntryID;
            StoreId = folder.StoreID;
            _timer = new Timer(TimerCallback);
            if (folder != null)
            {
                if (folder.DefaultItemType == OlItemType.olMailItem)
                {
                    //launch async process to update message class on ECS messages
                    _timer.Change(0, Settings.Default.TimerDelay);
                }
            }
            if (ThisAddIn.AppVersion >= 14) return;
            Logger.Info(SOURCE, "calling AddMenuOptions");
            //add Configuration option to menu
            AddMenuOptions();
        }

        ~ExplWrap()
        {
            //release object, which releases handlers
            if (_timer != null)
            {
                _timer.Dispose();
                _timer = null;
            }
            Explorer = null;
        }

        internal Explorer Explorer
        {
            get { return _explorer; }
            set
            {
                if (_explorer == value) return;
                if (_explorer != null)
                {
                    //release current handlers
                    ((ExplorerEvents_Event)_explorer).Close -=
                        ExplorerClose;
                    _explorer.SelectionChange -= ExplorerSelectionChange;
                }
                if (value != null)
                {
                    //assign
                    _explorer = value;
                    //add handler
                    ((ExplorerEvents_Event)_explorer).Close += 
                        ExplorerClose;
                    _explorer.SelectionChange += ExplorerSelectionChange;
                    _explorer.BeforeFolderSwitch += ExplorerBeforeFolderSwitch;
                    _explorer.FolderSwitch += ExplorerFolderSwitch;
                    if (ThisAddIn.AppVersion > 14)
                    {
                        _explorer.InlineResponse += ExplorerInlineResponse;
                        _explorer.InlineResponseClose += ExplorerInlineResponseClose;
                    }
                }
                else
                {
                    //release reference
                    if (_timer != null)
                    {
                        _timer.Dispose();
                        _timer = null;
                    }
                    _explorer = null;
                }
            }
        }

        internal string Key { get; set; }

        internal string FolderId { get; set; }

        internal string StoreId { get; set; }
        
        internal CommandBarButton ConfigButton
        {
            set
            {
                const string SOURCE = CLASS_NAME + "ConfigButton";
                if (_configButton == value) return;
                if (_configButton != null)
                {
                    Logger.Info(SOURCE, "clearing existing handler for ConfigButton");
                    //release existing handler (if any)
                     _configButton.Click -=ConfigButtonClick;
                }
                if (value != null)
                {
                    Logger.Info(SOURCE, "adding config button");
                    //assign 
                    _configButton = value;
                    //configure
                    _configButton.BeginGroup = false;
                    _configButton.Caption =
                        Resources.label_config_button;
                    _configButton.TooltipText =
                        Resources.tooltip_config_button;
                    _configButton.Parameter = Key;
                    _configButton.Visible = true;
                    //add handler
                    _configButton.Click += 
                            ConfigButtonClick;
                }
                else
                {
                    _configButton = null;
                }
            }
        }

        internal bool Dynamic { get; set; }

        internal bool Encrypted { get; set; }

        internal bool NoPlaceholder { get; set; }
        
        internal bool AllowForwarding { get; set; }

        internal MailItem MailItem
        {
            get { return _mailItem; }
            set
            {
                if (_mailItem == value) return;
                if (_mailItem != null)
                {
                    //release handlers
                }
                if (value != null)
                {
                    //assign
                    _mailItem = value;
                    //preset Dynamic based on SendUsingAccount
                    SetSendUsing();                    
                    var account = ThisAddIn.Accounts[_sendUsingAddress];
                    var config = account.Configurations[account.DefaultConfiguration];
                    if (!string.IsNullOrEmpty(config.Password))
                    {
                        Dynamic = config.DefaultOn;
                        if (Dynamic)
                        {
                            Encrypted = config.Encrypt;
                            NoPlaceholder = config.NoPlaceholder;
                        }
                    }
                    MailItem.PropertyChange += MailItemPropertyChange;
                    MailItem.Open += MailItemOpen;
                    if (MailItem.Sent)
                    {
                        var safItem = RedemptionLoader.new_SafeMailItem();
                        safItem.Item = MailItem;
                        //RecordKey = Utils.GetRecordKey(safItem);
                    }
                    ((ItemEvents_Event)MailItem).Send += MailItemSend;

                    //Ephemeral
                    if (!MailItem.Sent) return;
                    var sender = Utils.GetSenderAddress(MailItem);
                    if (sender == account.SMTPAddress) return;
                    var pointer = "";
                    var server = "";
                    var port = "";
                    var key = "";
                    var key2 = "";
                    var duration = "";
                    var user_agent = "";
                    var allow_forwarding = false;
                    Utils.ReadHeaders(MailItem, ref pointer, ref server, ref port,
                        ref key, ref key2, ref duration, ref user_agent, ref allow_forwarding);
                    if (!string.IsNullOrEmpty(duration) && duration != "0")
                    {
                        MailItem.Actions["Reply"].Enabled = false;
                        MailItem.Actions["ReplyAll"].Enabled = false;
                        MailItem.Actions["Forward"].Enabled = false;
                    }
                }
            }
        } 
        private NameSpace Session
        {
            get { return Explorer.Session; }
        }

        private CommandBarButton HelpButton
        {
            set
            {
                if (_helpButton == value) return;
                if (_helpButton != null)
                {
                    //release existing handler (if any)\
                    _helpButton.Click -= HelpButton_Click;
                }
                if (value != null)
                {
                    //add the button
                    _helpButton = value;
                    _helpButton.BeginGroup = false;
                    _helpButton.Caption = Resources.label_help;
                    _helpButton.TooltipText = Resources.description_help;
                    _helpButton.Parameter = Key;
                    _helpButton.Visible = true;
                    //add handler
                    _helpButton.Click += 
                            HelpButton_Click;
                }
                else
                {
                    _helpButton = null;
                }
            }
        }

        private CommandBarButton SupportButton
        {
            set
            {
                if (_supportButton == value) return;
                if (_supportButton != null)
                {
                    //release existing handler (if any)\
                    _supportButton.Click -= SupportButton_Click;
                }
                if (value != null)
                {
                    //add the button
                    _supportButton = value;
                    _supportButton.BeginGroup = false;
                    _supportButton.Caption = Resources.label_support;
                    _supportButton.TooltipText = Resources.description_support;
                    _supportButton.Parameter = Key;
                    _supportButton.Visible = true;
                    //add handler
                    _supportButton.Click +=
                            SupportButton_Click;
                }
                else
                {
                    _supportButton = null;
                }
            }

        }
       
        private void HelpButton_Click(CommandBarButton ctrl, ref bool cancelDefault)
        {
            Utils.OpenHelp();
        }

        private void SupportButton_Click(CommandBarButton ctrl, ref bool cancelDefault)
        {
            Globals.ThisAddIn.RequestSupport();
        }

        private void ConfigButtonClick(CommandBarButton ctrl, ref bool cancelDefault)
        {
            if (ctrl.Parameter != Key) return;
            //launch the Config form
            Globals.ThisAddIn.ShowConfig();
        }

        private void ExplorerClose()
        {
            if (_timer != null)
            {
                _timer.Dispose();
                _timer = null;
            }
            Globals.ThisAddIn.ReleaseExplWrap(Key);
        }

        private void ExplorerBeforeFolderSwitch(object newFolder, ref bool cancel)
        {
            //const string SOURCE = CLASS_NAME + "BeforeFolderSwitch";
            var folder = newFolder as Folder;
            if(folder == null) return;
            if (folder.DefaultItemType != OlItemType.olMailItem) return;
            //don't bother counting items now, wait until timer fires
            FolderId = folder.EntryID;
            StoreId = folder.StoreID;
            //fire in 1/2 second
            _timer.Change(500, Settings.Default.TimerDelay);
        }

        private void ExplorerFolderSwitch()
        {
               
        }

        private void ExplorerSelectionChange()
        {
            const string SOURCE = CLASS_NAME + "SelectionChange";
            var selection = _explorer.Selection;
            if (selection.Count <= 0) return;
            //don't bother if this isn't a valid store
            var folder = _explorer.CurrentFolder;
            var store = folder.Store;
            switch (store.ExchangeStoreType)
            {
                    case OlExchangeStoreType.olExchangeMailbox:
                    case OlExchangeStoreType.olExchangePublicFolder:
                    case OlExchangeStoreType.olAdditionalExchangeMailbox:
                        return;
            }
            var storeAddress = Globals.ThisAddIn.GetStoreAddress(folder.StoreID);
            foreach (var mail in selection.OfType<MailItem>().Select(item => item))
            {
                var hasHeader = Utils.HasChiaraHeader(mail);

                if (!hasHeader)
                    ThisAddIn.IsMailAllowForwarding = true;

                var change = false;
                if (hasHeader)
                {
                    if (mail.MessageClass != Resources.message_class_CM)
                    {
                        Logger.Info(SOURCE, string.Format(
                            "setting {0} on {1}",
                            Resources.message_class_CM,
                            mail.Subject));
                        mail.MessageClass = Resources.message_class_CM;
                        change = true;
                    }
                    //block forwards for ephemeral messages
                    var senderAddress = Utils.GetSenderAddress(mail);
                    //get account address of current store
                    var pointer = "";
                    var server = "";
                    var port = "";
                    var key = "";
                    var key2 = "";
                    var duration = "";
                    var user_agent = "";
                    var allow_forwarding = false;
                    Utils.ReadHeaders(mail, ref pointer, ref  server, ref port,
                        ref key, ref key2, ref duration, ref user_agent, ref allow_forwarding);
                    if (string.IsNullOrEmpty(storeAddress))
                        storeAddress = mail.InternetAccountName();
                    var editable = ThisAddIn.IsEditable(mail,senderAddress, server, port, storeAddress);
                    if (!string.IsNullOrEmpty(duration) && duration != "0" && !editable)
                    {
                        if (mail.Actions["Reply"].Enabled)
                        {
                            mail.Actions["Reply"].Enabled = false;
                            mail.Actions["Reply to All"].Enabled = false;
                            mail.Actions["Forward"].Enabled = false;
                            change = true;
                        }
                    }
                }
                else if (mail.MessageClass == Resources.message_class_CM)
                {
                    //no header but somehow got our message class (?) - change it back
                    Logger.Info(SOURCE, "changing back to IPM.Note on " + mail.Subject);
                    //account is not configured - reset to IPM.Note
                    mail.MessageClass = "IPM.Note";
                    change = true;
                }
                if (change) mail.Save();
            }
        }

        private void TimerCallback(object arg)
        {
            try
            {
                //stop the timer
                _timer.Change(Timeout.Infinite, 0);
                //launch the process
                UpdateMessageClass();
            }
            catch (Exception ex)
            {
                Logger.Error("TimerCallback",ex.Message);
            }
        }
       
        private void AddMenuOptions()
        {
            const int TOOLS_ID = 30007;
            const int TRUST_CENTER_ID = 14324;
            const int HELP_ID = 30010;
            const int HELP_BTN_ID = 984;
            const string SOURCE = CLASS_NAME + "AddMenuOptions";
            try
            {
                //get the main menu
                CommandBars commandBars = Explorer.CommandBars;
                CommandBar menuBar = commandBars.ActiveMenuBar;
                //find Tools
                var tools = (CommandBarPopup)
                    menuBar.FindControl(Type.Missing,TOOLS_ID,Type.Missing,true,true);                
                if (tools != null)
                {
                    //find Trust Center button
                    var toolsBar = tools.CommandBar;
                    var trustCenter = (CommandBarButton)
                        toolsBar.FindControl(MsoControlType.msoControlButton,
                        TRUST_CENTER_ID,Type.Missing,true,true);
                    var index = trustCenter.Index;
                    //insert before Trust Center Account Settings...
                    ConfigButton = (CommandBarButton)
                        tools.Controls.Add(MsoControlType.msoControlButton, 
                        Type.Missing, Key, index, true);
                }   
                //Help
                var help = (CommandBarPopup)
                    menuBar.FindControl(Type.Missing, HELP_ID, Type.Missing, true, true);
                if (help == null) return;
                var helpBar = help.CommandBar;
                //add right after Outlook Help - find the next item and get the index
                object idx = Type.Missing;
                bool next = false;
                for (var i = 1; i <= helpBar.Controls.Count; i++)
                {
                    if (!helpBar.Controls[i].Visible) continue;
                    if (next)
                    {
                        idx = helpBar.Controls[i].Index;
                        Logger.Info(SOURCE,string.Format(
                            "found index: {0}",idx));
                        break;
                    }
                    if (helpBar.Controls[i].Id == HELP_BTN_ID) next = true;
                }
                SupportButton = (CommandBarButton)
                                help.Controls.Add(MsoControlType.msoControlButton,
                                                  Type.Missing, Key, idx, true);
                HelpButton = (CommandBarButton)
                             help.Controls.Add(MsoControlType.msoControlButton,
                                               Type.Missing, Key,idx,true);                
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
        }

        private void UpdateMessageClass() 
        {
            const string SOURCE = CLASS_NAME + "UpdateMessageClass";
            try
            {
                var session = RedemptionLoader.new_RDOSession();
                session.MAPIOBJECT = Session.MAPIOBJECT;
                var folder = session.GetFolderFromID(FolderId, StoreId);
                if(folder == null) return;
                if(folder.DefaultItemType != (int)OlItemType.olMailItem) return;
                var items = folder.Items;
                if (items == null || items.Count == 0) return;
                Logger.Verbose(SOURCE, "updating items in " + folder.Name);
                var filter = string.Format(
                    "SELECT MessageClass, Subject FROM Folder WHERE \"{0}\" like '%{1}%' AND \"{2}\" = 'IPM.Note'" +
                    "ORDER BY ReceivedTime DESC",
                    "http://schemas.microsoft.com/mapi/proptag/0x007D001F",
                    Resources.content_header,
                    "http://schemas.microsoft.com/mapi/proptag/0x001A001F");
                var item = items.Find(filter);
                if (item == null) return;
                Logger.Verbose(SOURCE, "updating items in " + folder.Name);
                var counter = 0;
                var fireTimer = false;
                while (item != null)
                {
                    item.MessageClass = Resources.message_class_CM;
                    item.Save();
                    counter += 1;
                    item = items.FindNext();
                    if (item != null && counter == Settings.Default.UpdateBatch)
                    {
                        //bail out now and set the timer;
                        fireTimer = true;
                        break;
                    }
                }
                Logger.Verbose(SOURCE, string.Format(
                    "updated {0} items in {1}",
                    counter, folder.Name));
                if (fireTimer) 
                    _timer.Change(Settings.Default.TimerDelay, Settings.Default.TimerDelay);
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.Message);
            }
        }

        #region InlineResponse
        
        private void ExplorerInlineResponseClose()
        {
            //tear down the wrapper
            MailItem = null;
        }

        private void ExplorerInlineResponse(object item)
        {
            MailItem = item as MailItem;
        }

        private void MailItemOpen(ref bool cancel)
        {
            SetSendUsing();
        }

        private void MailItemPropertyChange(string name)
        {
            if (!name.Equals("SendUsingAccount")) return;
            SetSendUsing();
        }

        private void MailItemSend(ref bool cancel)
        {
            const string SOURCE = CLASS_NAME + "InlineMailItemSend";
            //kill the timer
            if (_timer != null) _timer.Dispose();
            //get path for a temp folder 
            var tempFolder = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            try
            {
                if (!Dynamic) return;
                Win32.SetCursor(Cursors.WaitCursor.Handle);
                Logger.Verbose(SOURCE, String.Format(
                    "assembling ECS content for {0}", MailItem.Subject));
                //assemble comma-delimited list of recipient addresses
                var recipients = MailItem.Recipients;
                var recipList = (from Recipient recip in recipients
                                 select recip.AddressEntry.GetPrimarySMTP()).ToList();
                //post the content first
                var contentPointer = "";
                string error;
                var recips = String.Join(",", recipList);
                var pointers = new List<string>();
                //just post the body, not the full HTML
                var originalHTML = MailItem.HTMLBody;
                var rawContent = Utils.GetBody(originalHTML);
                var originalContent = rawContent;
                //SendUsingAccount 
                if(string.IsNullOrEmpty(_sendUsingAddress))
                    SetSendUsing();
                Logger.Verbose(SOURCE,"attempting to retrieve account using " + _sendUsingAddress);
                var account = ThisAddIn.Accounts[_sendUsingAddress];
                Logger.Verbose(SOURCE, "retrieveing default configuration for " + account.UserName);
                var config = account.Configurations[account.DefaultConfiguration];
                var encryptKey = Encrypted
                                     ? Cryptography.GenerateKey()
                                     : "";
                var replaced = new List<int>();
                if (MailItem.BodyFormat == OlBodyFormat.olFormatHTML)
                {
                    //are there embedded images?                     
                    Logger.Verbose(SOURCE, "checking for embedded images");
                    if (Utils.HasEmbeddedImages(MailItem) && !NoPlaceholder)
                    {
                        Logger.Verbose(SOURCE,"found embedded images");
                        var embedded = Utils.GetEmbeddedImages(MailItem);
                        //replace the src link with the base64-encoded content
                        foreach (var attachment in embedded)
                        {
                            //build the replacement path

                            var data = Convert.ToBase64String(attachment.Content);
                            var imageType = Regex.Match(attachment.Name, @"\.(\S{3,4})").Groups[1].Value;
                            var src = string.Format("data:image/{0};base64,{1}",
                                imageType, data);
                            //replace the src path
                            var cidPath = "cid:" + attachment.ContentId;
                            while (rawContent.Contains(cidPath))
                                rawContent = rawContent.Replace(cidPath, src);
                            replaced.Add(attachment.Index);
                        }
                    }
                }

                //encode it (use JS compatible method for encryption)
                var content = ContentHandler.EncodeContent(rawContent, "", encryptKey);
                Logger.Verbose(SOURCE, "invoking PostContent");
                ContentHandler.PostContent(account.SMTPAddress, config,
                    content, recips, ref contentPointer, out error);
                if (String.IsNullOrEmpty(contentPointer))
                {
                    Logger.Warning(SOURCE, String.Format(
                        "unable to post content for {0}: {1}",
                        MailItem.Subject, error));
                    //we've got a problem - raise alert
                    MessageBox.Show(string.Format(Resources.error_storing_content,
                            Environment.NewLine,
                            error),
                        Resources.product_name,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    //cancel the send
                    cancel = true;
                    return;
                }
                pointers.Add(contentPointer);
                //post each attachment and replace with stub
                if (replaced.Count > 0)
                    Utils.DeleteReplaced(MailItem, replaced);
                var attachments = MailItem.Attachments;
                if (attachments.Count > 0)
                {
                    var win = new OutlookWin32Window(_explorer, false);
                    Logger.Verbose(SOURCE,"invoking PostAttachments");
                    if (!Utils.PostAttachments(MailItem, account, config, encryptKey, recips, 
                        ref pointers, win, NoPlaceholder))
                    {
                        Logger.Warning(SOURCE, String.Format(
                            "unable to post attachments for {0}",
                            MailItem.Subject));
                        //delete any pointers that we got
                        Utils.DeletePointers(pointers, account, config);
                        //cancel the send
                        cancel = true;
                        return;
                    }
                }
                if (!NoPlaceholder)
                {
                    //swap in placeholder to replace original body
                    MailItem.HTMLBody = MailItem.HTMLBody.Replace(
                        originalContent, Resources.placeholder_html);
                }
                //assign the headers
                Logger.Verbose(SOURCE,"calling Assign Headers");
                Utils.AssignHeaders(MailItem, account, pointers, encryptKey, Encrypted);

                //change message class
                _mailItem.MessageClass = Resources.message_class_CM;
                //save changes
                _mailItem.Save();
                //Outlook handles the Send from here
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
            finally
            {
                if (Directory.Exists(tempFolder))
                {
                    //clear out the temp directory and files if they exist
                    Directory.Delete(tempFolder, true);
                }
                Win32.SetCursor(Cursors.Default.Handle);
            }

        }
        
        private void SetSendUsing()
        {
            var olAccount = MailItem.SendUsingAccount;
            if(olAccount == null) return;
            _sendUsingAddress = olAccount.SmtpAddress;            
        }
        #endregion
        //private void SetViews(Outlook.MAPIFolder folder)
        //{
        //    if (folder == null) return;
        //    if (folder.DefaultMessageClass != "IPM.Note") return;
        //    var view = folder.CurrentView;
        //    if (view.ViewType != Outlook.OlViewType.olTableView) return;
        //    var tableView = view as Outlook.TableView;
        //    if (tableView == null) return;
        //    var formatRules = tableView.AutoFormatRules;
        //    //DC
        //    if (formatRules["DynamicContent"] == null)
        //    {
        //        var ruleDC = formatRules.Add("DynamicContent");
        //        ruleDC.Filter = string.Format(
        //            "\"http://schemas.microsoft.com/mapi/proptag/0x001A001E\" LIKE '{0}'",
        //            Properties.Resources.message_class_CM);
        //        ruleDC.Font.Color = Outlook.OlColor.olColorMaroon;
        //        ruleDC.Enabled = true;
        //    }
        //    //Registered
        //    const string REGISTERED = "RegisteredDC";
        //    var ruleRegistered = formatRules[REGISTERED];
        //    if (ruleRegistered!=null) formatRules.Remove(REGISTERED);            
        //    var filter = ThisAddIn.RegistrationFilter(folder);
        //    if (!string.IsNullOrEmpty(filter))
        //    {
        //    //    if (ruleRegistered != null && ruleRegistered.Enabled)
        //    //        ruleRegistered.Enabled = false;
        //    //}
        //    //else
        //    //{                
        //        Logger.Verbose("SetViews", "applying auto-format rule with filter " + filter);
        //        ruleRegistered = formatRules.Add(REGISTERED);
        //        //if (ruleRegistered == null) ruleRegistered = formatRules.Add("RegisteredDC");
        //        ruleRegistered.Filter = filter;
        //        ruleRegistered.Font.Color = Outlook.OlColor.olColorMaroon;
        //        ruleRegistered.Enabled = true;
        //    }
        //    formatRules.Save();
        //    view.Apply();
        //}
    }
}
