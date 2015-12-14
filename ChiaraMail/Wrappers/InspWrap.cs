using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Timers;
using System.Web;
using ChiaraMail.Forms;
using ChiaraMail.Properties;
using Microsoft.Office.Interop.Outlook;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Redemption;
using System.Runtime.InteropServices;
using Exception = System.Exception;
using Timer = System.Timers.Timer;

namespace ChiaraMail.Wrappers
{
    internal class InspWrap
    {
        private const string CLASS_NAME = "InspWrap.";
        private string _key;
        private Inspector _inspector;
        private MailItem _mailItem;
        private Timer _timer;
        private string _sendUsingAddress;

        public InspWrap(Inspector insp)
        {
            if (insp == null)
            {
                return;
            }
            Key = Guid.NewGuid().ToString();
            Inspector = insp;
        }

        ~InspWrap()
        {
            Teardown();
        }

        private void Teardown()
        {
            //release objects, which releases handlers
            if (_mailItem != null)
            {
                Marshal.FinalReleaseComObject(_mailItem);
                _mailItem = null;
            }
            if (_inspector != null)
            {
                Marshal.FinalReleaseComObject(_inspector);
                _inspector = null;
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        #region Properties

        internal string Key
        {
            get { return _key; }
            set { if (String.IsNullOrEmpty(_key)) _key = value; }
        }

        internal bool Dynamic { get; set; }

        internal bool Encrypted { get; set; }

        internal bool NoPlaceholder { get; set; }

        internal bool AllowForwarding { get; set; }

        internal string RecordKey { get; private set; }

        internal bool Inherited { get; set; }
        #endregion

        #region Outlook objects

        internal Inspector Inspector
        {
            get { return _inspector; }
            set
            {
                if (_inspector == value) return;
                if (value != null)
                {
                    _inspector = value;
                    ((InspectorEvents_Event)Inspector).Close += InspectorClose;
                    //assign
                    MailItem = (MailItem)_inspector.CurrentItem;
                }
            }
        }

        internal MailItem MailItem
        {
            get { return _mailItem; }
            set
            {
                if (_mailItem == value) return;

                if (value == null) return;
                try
                {
                    _mailItem = value;
                    //preset Dynamic based on SendUsingAccount
                    // that could be null on 2K7, so initialize with default account
                    var olAcct = _mailItem.SendUsingAccount;
                    var account = olAcct != null
                        ? ThisAddIn.Accounts[olAcct.SmtpAddress]
                        : GetStoreAccount();
                    var config = account.Configurations[account.DefaultConfiguration];
                    if (!String.IsNullOrEmpty(config.Password))
                    {
                        Dynamic = config.DefaultOn;
                        if (Dynamic && !Inherited)
                        {
                            Encrypted = config.Encrypt;
                            NoPlaceholder = config.NoPlaceholder;
                            AllowForwarding = config.AllowForwarding;
                        }
                    }
                    MailItem.PropertyChange += MailItemPropertyChange;
                    MailItem.Open += MailItemOpen;
                    if (MailItem.Sent)
                    {
                        var safItem = RedemptionLoader.new_SafeMailItem();
                        safItem.Item = MailItem;
                        RecordKey = Utils.GetRecordKey(safItem);
                    }
                    ((ItemEvents_Event)MailItem).Send += MailItemSend;
                    ((ItemEvents_Event)MailItem).Close += MailItemClose;
                    if (ThisAddIn.AppVersion < 14)
                    {
                        //fire timer every 1/2 second to check for Account change
                        _timer = new Timer(500);
                        _timer.Elapsed += TimerElapsed;
                    }
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
                catch
                {
                }
            }
        }

        #endregion

        #region Outlook event handlers

        private void InspectorClose()
        {
            Teardown();
        }

        private void MailItemClose(ref bool cancel)
        {
            //kill the timer
            if (_timer != null) _timer.Dispose();
            //release the wrapper
            Globals.ThisAddIn.ReleaseInspWrap(Key);
            Teardown();
        }

        private void MailItemSend(ref bool cancel)
        {
            const string SOURCE = CLASS_NAME + "MailItemSend";
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
                var rawContent = GetBody(originalHTML);
                var originalContent = rawContent;
                //SendUsingAccount could be null in 2K7
                var account = MailItem.SendUsingAccount != null
                                      ? ThisAddIn.Accounts[MailItem.SendUsingAccount.SmtpAddress]
                                      : GetStoreAccount();
                var config = account.Configurations[account.DefaultConfiguration];
                var encryptKey = Encrypted
                                     ? Cryptography.GenerateKey()
                                     : "";
                var replaced = new List<int>();
                if (MailItem.BodyFormat == OlBodyFormat.olFormatHTML)
                {
                    //are there embedded images?                     
                    if (HasEmbeddedImages() && !NoPlaceholder)
                    {
                        var embedded = GetEmbeddedImages();
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
                    DeleteReplaced(replaced);
                var attachments = MailItem.Attachments;
                if (attachments.Count > 0)
                {

                    if (!PostAttachments(account, config, encryptKey, recips, ref pointers))
                    {
                        Logger.Warning(SOURCE, String.Format(
                            "unable to post attachments for {0}",
                            MailItem.Subject));
                        //delete any pointers that we got
                        DeletePointers(pointers, account, config);
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
                AssignHeaders(account, pointers, encryptKey, AllowForwarding);

                Utils.UpdateAccountStorage(account);
                
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

        private void MailItemOpen(ref bool cancel)
        {
            const string SOURCE = CLASS_NAME + "MailItem_Open";
            Logger.Verbose(SOURCE, MailItem.Subject);
            _sendUsingAddress = (MailItem.SendUsingAccount == null)
                                    ? "null"
                                    : MailItem.SendUsingAccount.SmtpAddress;
            if (_timer != null && !_timer.Enabled)
            {
                _timer.Start();
            }
            else
            {
                EvalSendUsingAccount(MailItem.SendUsingAccount);
            }
        }

        private void MailItemPropertyChange(string name)
        {
            if (!name.Equals("SendUsingAccount")) return;
            //get the SendUsingAccount (will be null in 2K7 if using the default) 
            Microsoft.Office.Interop.Outlook.Account olAccount = MailItem.SendUsingAccount;
            string currentAddress = olAccount == null
                                        ? "null"
                                        : olAccount.SmtpAddress;
            if (currentAddress == _sendUsingAddress) return;
            EvalSendUsingAccount(olAccount);
            _sendUsingAddress = currentAddress;
        }

        #endregion

        #region Private methods

        private string GetBody(string body)
        {
            const string SOURCE = CLASS_NAME + "GetBody";
            string content = body;
            try
            {
                //find the body start tag
                content = Regex.Match(body, @"<body\b[^>]*>(.*?)</body>",
                                      RegexOptions.Singleline | RegexOptions.IgnoreCase).Groups[1].Value;
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
            return content;
        }

        private IEnumerable<Attachment> GetEmbeddedImages()
        {
            var list = new List<Attachment>();
            SafeMailItem safMail;
            try
            {
                safMail = RedemptionLoader.new_SafeMailItem();
            }
            catch (Exception ex)
            {
                Logger.Error("", String.Format(
                    "unable to work with attachments for {0}, failed to instantiate SafeMailItem: {1}",
                    MailItem.Subject, ex.Message));
                return list;
            }
            //need to save the item first before we can work with the SafeMailItem
            MailItem.Save();
            safMail.Item = MailItem;
            var colAttach = safMail.Attachments;
            //walk down the existing 
            foreach (Redemption.Attachment rdoAttach in colAttach)
            {
                string contentId;
                bool hidden;
                Utils.GetAttachProps(rdoAttach, out contentId, out hidden);
                if (string.IsNullOrEmpty(contentId)) continue;
                try
                {
                    //could fail if attachment is too big
                    var content = rdoAttach.AsArray != null
                        ? rdoAttach.AsArray as byte[]
                        : null; //.Fields[ThisAddIn.PR_ATTACH_DATA_BIN]);
                    if (content == null) continue;
                    {
                        list.Add(new Attachment
                                     {
                                         Content = content,
                                         ContentId = contentId,
                                         Name = rdoAttach.FileName,
                                         Index = rdoAttach.Index
                                     });
                    }
                }
                catch
                {
                    Logger.Warning("GetEmbeddedImages",
                                   "failed to retrieve content from embedded image");
                }
            }
            return list;
        }

        private void DeleteReplaced(List<int> replaced)
        {
            var attachments = MailItem.Attachments;
            for (var i = attachments.Count; i > 0; i--)
            {
                var index = attachments[i].Index;
                if (replaced.Contains(index))
                    attachments.Remove(index);
            }
        }

        private bool PostAttachments(Account account, EcsConfiguration configuration,
            string encryptKey, string recips, ref List<string> pointers)
        {
            var source = CLASS_NAME + "PostAttachments";
            try
            {
                //get the bytes for the placeholder text
                var placeholder = Encoding.UTF8.GetBytes(Resources.placeholder_text);
                SafeMailItem safMail;
                try
                {
                    safMail = RedemptionLoader.new_SafeMailItem();
                }
                catch (Exception ex)
                {
                    Logger.Error("", String.Format(
                        "unable to work with attachments for {0}, failed to instantiate SafeMailItem: {1}",
                        MailItem.Subject, ex.Message));
                    return false;
                }
                //need to save the item first before we can work with the SafeMailItem
                MailItem.Save();
                safMail.Item = MailItem;
                var colAttach = safMail.Attachments;
                /* Outlook will move any embedded images to the head of the attachments table
                * if that's the case then we need to remove and re-add the other attachments 
                * so that the pointer list will match the finished order
                */
                var hidden = false;
                string contentId;
                var savedAttach = new Dictionary<int, byte[]>();
                //do we have any embedded images?
                foreach (Redemption.Attachment rdoAttach in colAttach)
                {
                    Utils.GetAttachProps(rdoAttach, out contentId, out hidden);
                    if (hidden) break;
                }
                if (hidden)
                {
                    //walk through in reverse order
                    //delete and reattach each non-hidden attachment
                    for (var i = colAttach.Count; i > 0; i--)
                    {
                        Redemption.Attachment rdoAttach = colAttach[i];
                        Utils.GetAttachProps(rdoAttach, out contentId, out hidden);
                        if (hidden) continue;
                        if (rdoAttach.Type.Equals(5)) //embedded
                        {
                            var msg = rdoAttach.EmbeddedMsg;
                            rdoAttach.Delete();
                            colAttach.Add(msg, 5);
                        }
                        else
                        {
                            var path = Path.Combine(Path.GetTempPath(), "ChiaraMail", rdoAttach.FileName);
                            var displayName = rdoAttach.DisplayName;
                            if (File.Exists(path)) File.Delete(path);
                            rdoAttach.SaveAsFile(path);
                            rdoAttach.Delete();
                            rdoAttach = colAttach.Add(path, 1, Type.Missing, displayName);
                            //get the bytes and drop those in the dictionary, linked to the current index
                            savedAttach.Add(rdoAttach.Index, File.ReadAllBytes(path));
                            File.Delete(path);
                        }
                    }
                }

                //now loop through and collect the content (except for embedded messages)
                var attachList = new List<Attachment>();
                bool showForm = false;
                foreach (Redemption.Attachment rdoAttach in colAttach)
                {
                    var attach = new Attachment { Type = rdoAttach.Type };
                    switch (rdoAttach.Type)
                    {
                        case (int)OlAttachmentType.olEmbeddeditem:
                            //is this an ECS attachment?
                            var msg = rdoAttach.EmbeddedMsg;
                            if (Utils.HasChiaraHeader(msg))
                            {
                                ForwardEmbeddedECS(msg, recips, account);
                            }
                            //always add
                            attachList.Add(attach);
                            break;
                        case (int)OlAttachmentType.olByReference:
                        case (int)OlAttachmentType.olOLE:
                            attachList.Add(attach);
                            break;
                        case (int)OlAttachmentType.olByValue:
                            showForm = true;
                            //we may have already gotten the bytes
                            if (savedAttach.Count > 0 && savedAttach.ContainsKey(rdoAttach.Index))
                            {
                                attach.Content = savedAttach[rdoAttach.Index];
                            }
                            if (attach.Content == null || attach.Content.Length == 0)
                            {
                                //try just read the bytes from the binary property
                                //this could fail if the attachment is too big
                                try
                                {
                                    attach.Content = rdoAttach.AsArray != null
                                        ? rdoAttach.AsArray as byte[]
                                        : null;//.Fields[ThisAddIn.PR_ATTACH_DATA_BIN]);
                                }
                                catch
                                {
                                    attach.Content = null;
                                }
                            }
                            if (attach.Content == null)
                            {
                                //save to disk then get the bytes
                                var path = Path.Combine(Path.GetTempPath(), "ChiaraMail", rdoAttach.FileName);
                                if (File.Exists(path)) File.Delete(path);
                                rdoAttach.SaveAsFile(path);
                                attach.Content = File.ReadAllBytes(path);
                                File.Delete(path);
                            }
                            if (attach.Content != null)
                            {
                                attach.Index = rdoAttach.Index;
                                attach.Name = rdoAttach.DisplayName;
                                attachList.Add(attach);
                            }
                            else
                            {
                                Logger.Warning(source,
                                               "aborting: failed to retrieve content for " + rdoAttach.DisplayName);
                                MessageBox.Show(String.Format(
                                    "Unable to retrieve original content from {0}",
                                    rdoAttach.DisplayName), Resources.product_name,
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Error);
                                return false;
                            }
                            break;
                    }
                }
                if (!showForm)
                {
                    pointers.AddRange(attachList.Select(attach => attach.Pointer));
                    return true;
                }
                //use the WaitForm to upload the attachments
                var win = new OutlookWin32Window(Inspector, Inspector.IsWordMail());
                var form = new WaitForm
                               {
                                   Attachments = attachList,
                                   Account = account,
                                   Configuration = configuration,
                                   Recips = recips,
                                   EncryptKey2 = encryptKey
                               };
                //use encryptKey2 for new post
                if (form.ShowDialog(win) == DialogResult.OK)
                {
                    //post succeeded for all attachments
                    //get the pointers
                    pointers.AddRange(form.Attachments.Select(attach => attach.Pointer));
                    //don't replace attachment bytes if we are sending content
                    if (NoPlaceholder) return true;
                    //loop back through to replace the original content with the placeholder bytes
                    foreach (Redemption.Attachment rdoAttach in colAttach)
                    {
                        if (rdoAttach.Type.Equals(1)) //OlAttachmentType.olByValue)
                        {
                            rdoAttach.Fields[ThisAddIn.PR_ATTACH_DATA_BIN] = placeholder;
                        }
                    }
                    return true;
                }
                //get the pointer list anyway so we can delete the items that got posted
                pointers.AddRange(form.Attachments
                                      .TakeWhile(attach => !String.IsNullOrEmpty(attach.Pointer))
                                      .Select(attach => attach.Pointer));
            }
            catch (Exception ex)
            {
                Logger.Error(source, ex.ToString());
            }
            return false;
        }

        private void AssignHeaders(Account account, List<string> pointers, string encryptKey, bool allowForwarding)
        {
            string source = CLASS_NAME + "AssignHeaders";
            try
            {
                var accessor = MailItem.PropertyAccessor;
                //set each property individually 
                //accessor fails to set the content header if there are too many pointer
                var pointerString = String.Join(" ", pointers.ToArray());
                accessor.SetProperty(
                    ThisAddIn.MAIL_HEADER_GUID +
                    Resources.content_header,
                    pointerString);
                var config = account.Configurations[account.DefaultConfiguration];
                accessor.SetProperty(
                    ThisAddIn.MAIL_HEADER_GUID +
                    Resources.server_header,
                    config.Server);
                accessor.SetProperty(
                    ThisAddIn.MAIL_HEADER_GUID +
                    Resources.port_header,
                    config.Port);
                if (Encrypted && !String.IsNullOrEmpty(encryptKey))
                {
                    //only write to new encrypt key header
                    accessor.SetProperty(ThisAddIn.MAIL_HEADER_GUID +
                                         Resources.encrypt_key_header2,
                                         encryptKey);
                }
                accessor.SetProperty(
                    ThisAddIn.MAIL_HEADER_GUID +
                    Resources.user_agent_header,
                    Resources.label_help_group + " " + Utils.AssemblyFullVersion);
                accessor.SetProperty(
                    ThisAddIn.MAIL_HEADER_GUID +
                    Resources.user_allow_forwarding_header,
                    allowForwarding.ToString().ToLower());
            }
            catch (Exception ex)
            {
                Logger.Error(source, ex.ToString());
            }
        }

        private void DeletePointers(List<string> pointers, Account account, EcsConfiguration configuration)
        {
            var source = CLASS_NAME + "DeletePointers";
            Logger.Info(source, String.Format(
                "deleting {0} pointers after failed attachment upload",
                pointers.Count));
            foreach (var pointer in pointers)
            {
                string error;
                ContentHandler.DeleteContent(account.SMTPAddress,
                    configuration, pointer, out error, true);
            }
        }

        private bool HasEmbeddedImages()
        {
            var attachments = MailItem.Attachments;
            if (attachments == null || attachments.Count == 0) return false;
            return (from Microsoft.Office.Interop.Outlook.Attachment attachment in attachments
                    where attachment.Type == OlAttachmentType.olByValue
                    select attachment.PropertyAccessor
                        into pa
                        select pa.GetProperty(ThisAddIn.DASL_ATTACH_CONTENT_ID))
                .Any(cid => (cid != null && !string.IsNullOrEmpty(cid)));
        }

        private void ForwardEmbeddedECS(MessageItem msg, string recips, Account account)
        {
            string pointerString;
            string serverName;
            string serverPort;
            string encryptKey2;
            string userAgent;
            Utils.GetChiaraHeaders(msg, out pointerString, out serverName, out serverPort, out encryptKey2, out userAgent);
            var sender = msg.Sender.SMTPAddress;
            var config = account.Configurations.Values.
                First(cfg => cfg.Server.Equals(serverName,
                    StringComparison.CurrentCultureIgnoreCase));
            if (string.IsNullOrEmpty(sender))
            {
                Logger.Warning("ForwardEmbeddedECS", string.Format(
                    "failed to retrieve sender for {0}, skipping call to AddRecipients",
                    msg.Subject));
                return;
            }

            if (string.IsNullOrEmpty(pointerString))
            {
                Logger.Warning("ForwardEmbeddedECS", string.Format(
                    "failed to retrieve pointer(s) for {0}, skipping call to AddRecipients",
                    msg.Subject));
                return;
            }
            var pointers = pointerString.Split(new char[' ']);
            foreach (var pointer in pointers)
            {
                string error;
                ContentHandler.AddRecipients(account.SMTPAddress, config,
                    sender, pointer, serverName, serverPort, recips, out error);
            }
        }

        private Account GetStoreAccount()
        {
            var parent = MailItem.Parent as Folder;
            if (parent == null) return Globals.ThisAddIn.ActiveAccount;
            var storeAddress = Globals.ThisAddIn.GetStoreAddress(parent.StoreID);
            return ThisAddIn.Accounts[storeAddress];
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            //get the SendUsingAccount
            var currentAccount = MailItem.SendUsingAccount;
            var currentAddress =
                (currentAccount == null ? "null" : currentAccount.SmtpAddress);
            if (currentAddress != _sendUsingAddress)
            {
                EvalSendUsingAccount(currentAccount);
                _sendUsingAddress = currentAddress;
            }
        }

        private void EvalSendUsingAccount(Microsoft.Office.Interop.Outlook.Account sendUsingAccount)
        {
            var SOURCE = CLASS_NAME + "EvalSendUsingAccount";
            Logger.Verbose("EvalSendUsingAccount", "");
            try
            {
                //SendUsingAccount could be null in 2K7 so find the account associated with this store
                var account = GetStoreAccount();
                if (sendUsingAccount != null)
                {
                    var thisSendUsing = MailItem.SendUsingAccount;
                    account = ThisAddIn.Accounts[thisSendUsing.SmtpAddress];
                }
                if (account == null ||
                    String.IsNullOrEmpty(account.Configurations[account.DefaultConfiguration].Password))
                {
                    //not configured
                    Dynamic = false;
                    Encrypted = false;
                    NoPlaceholder = false;
                }
                else
                {
                    var config = account.Configurations[account.DefaultConfiguration];
                    //switch On if default is true
                    //but don't switch Off if it isn't
                    if (config.DefaultOn) Dynamic = true;
                    if (Dynamic && !Inherited)
                    {
                        Encrypted = config.Encrypt;
                        NoPlaceholder = config.NoPlaceholder;
                        AllowForwarding = config.AllowForwarding;
                    }
                }
                //invalidate the ribbon controls
                ThisAddIn.ResetInspButtons();
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
        }
        #endregion
    }
}