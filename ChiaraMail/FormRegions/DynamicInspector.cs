using System;
using System.Collections.Generic;
using ChiaraMail.Controls;
using ChiaraMail.Forms;
using ChiaraMail.Properties;
using Microsoft.Office.Interop.Outlook;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using Redemption;
using System.Text;
using Exception = System.Exception;
using MAPIFolder = Microsoft.Office.Interop.Outlook.MAPIFolder;

namespace ChiaraMail.FormRegions
{
    partial class DynamicInspector
    {
        #region Form Region Factory

        private const string CLASS_NAME = "DynamicInspector.";
        private string _pointerString;
        private string _serverName = "";
        private string _serverPort = "";
        private string _encryptKey = "";
        private string _encryptKey2 = "";
        private string _duration = "";
        private string _userAgent = "";
        private Dictionary<string, Attachment> _attachList = new Dictionary<string, Attachment>();
        private string _recordKey = "";
        private string _content;
        private string _senderAddress;
        private string _senderName = "";
        private string _sentDate = "";
        private string _toRecip = "";
        private string _ccRecip = "";
        private string _subject = "";
        private string _outlookFolderName = "";
        private string _currentFilePath = "";
        private Account _account;
        private EcsConfiguration _configuration;
        private bool _editable;
        private bool _plainText;
        private Dictionary<string, Redemption.Attachment> _embedded = 
            new Dictionary<string, Redemption.Attachment>();
        private AttachPanel _sourceBtn;
        private string _entryId;
        //private Timer _selfDestructTimer;

        private delegate void SelfDestructHandler(string path);

        internal string[] Pointers
        {
            get
            {
                return string.IsNullOrEmpty(_pointerString) 
                    ? null 
                    : _pointerString.Split(' ');
            }
        }

        internal string Content
        {
            get { return _content; }
            set
            {
                _content = value;                
                htmlEditor1.DocumentHtml = value;            
            }
        }

        internal string ServerName
        {
            get { return _serverName; }
        }

        internal string ServerPort
        {
            get { return _serverPort; }
        }

        internal string EncryptKey
        {
            get { return _encryptKey; }
        }

        internal string EncryptKey2
        {
            get { return _encryptKey2; }
        }

        internal string UserAgent
        {
            get { return _userAgent; }
        }

        internal bool Editable 
        {
            get { return _editable; }
        }
        internal Dictionary<string, Attachment> AttachList
        {
            get { return _attachList; }
        }

        internal string RecordKey
        {
            get { return _recordKey; }
        }

        internal void RefreshContent(string content)
        {
            //a change was committed from another window
            //reload the content
            Content = content;
            if (!string.IsNullOrEmpty(_currentFilePath))
            {
                //refetch the current attachment
                previewHandlerControl.Open(_currentFilePath);
            }
        }

        internal void SaveChanges()
        {
            const string SOURCE = CLASS_NAME + "SaveChanges";
            try
            {
                var updated = false;
                //grab the current content
                var content = htmlEditor1.BodyHtml;
                //has message changed?
                if (content != Content)
                {
                    //revert any embedded image links back to pointers
                    Utils.CollapseImgLinks(ref content);
                    string encodedContent = ContentHandler.EncodeContent(content, EncryptKey, EncryptKey2);
                    string error;
                    ContentHandler.UpdateContent(_account.SMTPAddress,
                        _configuration, encodedContent, Pointers[0], out error);
                    if (!error.Equals("success"))
                    {
                        MessageBox.Show(string.Format(
                            Resources.error_updating_content,
                            Environment.NewLine, error),
                            Resources.product_name,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }
                    //save the change
                    Content = content;
                    updated = true;
                }
                //handle attachment updates - use WaitForm
                var updateList = new List<Attachment>();
                for (var i = 1; i < Pointers.Length; i++)
                {
                    var pointer = Pointers[i];
                    //is there a file copy?
                    var path = Utils.GetFilePath(
                        _recordKey, AttachList[pointer].Index, AttachList[pointer].Name);
                    if (!File.Exists(path)) continue;
                    //get the content
                    var buf = File.ReadAllBytes(path);
                    //has it changed?
                    var hash = Cryptography.GetHash(buf);
                    if (hash == AttachList[pointer].Hash) continue;
                    var attach = new Attachment
                                     {
                                         Name = Path.GetFileName(path),
                                         Pointer = pointer,
                                         Content = buf,
                                         Hash = hash,
                                         Type = (pointer == "0" ? 5 : 1)
                                     };
                    updateList.Add(attach);
                }
                if (updateList.Count > 0)
                {
                    var form = new WaitForm
                                   {
                                       Attachments = updateList,
                                       Account = _account,
                                       Configuration = _configuration,
                                       EncryptKey = EncryptKey,
                                       EncryptKey2 = EncryptKey2
                                   };
                    if (form.ShowDialog(this) == DialogResult.OK)
                    {
                        updated = true;
                        //update the stored hash values
                        foreach (Attachment attach in updateList)
                        {
                            AttachList[attach.Pointer].Hash = attach.Hash;
                        }
                    }
                    else updated = false;
                }
                if (!updated) return;
                MessageBox.Show(Resources.content_updated, Resources.product_name,
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                //relay this change to any Inspector windows for this object
                ThisAddIn.RelayContentChange(this, RecordKey, Content);
                if (!string.IsNullOrEmpty(_currentFilePath))
                {
                    //refetch the current attachment 
                    previewHandlerControl.Open(_currentFilePath);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
        }

        internal void DeleteContent()
        {
            const string SOURCE = CLASS_NAME + "DeleteContent";
            try
            {
                //prompt for confirmation
                if (MessageBox.Show(this, Resources.prompt_delete_content_confirm,
                                    Resources.product_name, MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question) != DialogResult.Yes) return;
                foreach (var pointer in Pointers)
                {
                    string error;
                    ContentHandler.DeleteContent(_account.SMTPAddress,
                                                 _configuration, pointer, out error, false);
                    if (error.Equals("success")) continue;
                    MessageBox.Show(string.Format(
                        Resources.error_deleting_content,
                        Environment.NewLine, error),
                                    Resources.product_name,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    return;
                }
                //clear the content
                Content = "";

                //relay this change to any other windows for this object
                ThisAddIn.RelayContentChange(this, RecordKey, Content);
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
        }

        [Microsoft.Office.Tools.Outlook.FormRegionMessageClass("IPM.Note.ChiaraMail")]
        [Microsoft.Office.Tools.Outlook.FormRegionName("ChiaraMail.DynamicInspector")]
        public partial class DynamicInspectorFactory
        {
            // Occurs before the form region is initialized.
            // To prevent the form region from appearing, set e.Cancel to true.
            // Use e.OutlookItem to get a reference to the current Outlook item.
            private void DynamicInspectorFactory_FormRegionInitializing(object sender, Microsoft.Office.Tools.Outlook.FormRegionInitializingEventArgs e)
            {
            }
        }

        #endregion

        // Occurs before the form region is displayed.
        // Use this.OutlookItem to get a reference to the current Outlook item.
        // Use this.OutlookFormRegion to get a reference to the form region.
        private void DynamicInspectorFormRegionShowing(object sender, EventArgs e)
        {
            const string SOURCE = CLASS_NAME + "Showing";
            try
            {
                Cursor = Cursors.WaitCursor;
                var item = (MailItem)OutlookItem;
                _entryId = item.EntryID;
                var folder = (MAPIFolder)item.Parent;
                _outlookFolderName = folder.Name;                
                htmlEditor1.DocumentHtml = item.HTMLBody;
                _plainText = (item.BodyFormat == OlBodyFormat.olFormatPlain);
                _senderName = item.SenderName;
                _sentDate = string.Format("{0} {1}",
                    item.SentOn.ToString("ddd"),
                    item.SentOn.ToString("g"));
                _toRecip = item.To;
                _ccRecip = item.CC;
                _subject = item.Subject;
                previewHandlerControl.Visible = false;
                var safMail = RedemptionLoader.new_SafeMailItem();
                safMail.Item = item;
                //get primary SMTP of sender
                _senderAddress = safMail.Sender.SMTPAddress;
                if (string.IsNullOrEmpty(_senderAddress))
                {
                    Logger.Warning(SOURCE, "unable to identify senderAddress for " + item.Subject);
                }
                else
                {
                    Logger.Verbose(SOURCE, "found senderAddress:" + _senderAddress);
                }
                var previewPane = Utils.PreviewPaneVisible(folder.CurrentView.XML);
                LoadMessageHeader();
                if (Utils.IsSpoofed(_senderName, _senderAddress))
                {
                    Logger.Warning(SOURCE, string.Format("blocking content fetch for probable spoofed sender {0}:{1}",
                                                       _senderName, _senderAddress));
                    //only raise an alert if the reading pane is not visible
                    //otherwise it will just be a duplicate
                    if (!previewPane)
                    {
                        MessageBox.Show(this, Resources.spoofed_sender_address,
                                        Resources.product_name,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                    }
                    ShowContent("<body/>");
                    EnableEdits(false);
                    return;   
                }

                //read props from the header
                Utils.ReadHeaders(item, ref _pointerString, ref _serverName,
                        ref _serverPort, ref _encryptKey, ref _encryptKey2, 
                        ref _duration, ref _userAgent);

                // first check for matching/complete configuration
                var storeAddress = Globals.ThisAddIn.GetStoreAddress(folder.StoreID);
                if(string.IsNullOrEmpty(storeAddress))
                    storeAddress = item.InternetAccountName();
                _account = ThisAddIn.GetAccount(storeAddress);
                _configuration = ThisAddIn.GetMatchingConfiguration(storeAddress,
                    _serverName, _serverPort, true);
                if (_configuration == null)
                {
                    //no matching configuration
                    //only raise an alert if the reading pane is not visible
                    //otherwise it will just be a duplicate
                    if (!previewPane)
                    {
                        MessageBox.Show(this, Resources.unknown_server,
                                        Resources.product_name,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                    }
                    ShowContent("<body/>");
                    EnableEdits(false);
                    return;
                }
                else if (string.IsNullOrEmpty(_configuration.Password))
                {
                    //incomplete configuration
                    //only raise an alert if the reading pane is not visible
                    //otherwise it will just be a duplicate
                    if (!previewPane)
                    {
                        MessageBox.Show(this, string.Format(
                                Resources.missing_settings,
                                Environment.NewLine, "Password"),
                            Resources.product_name,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                    ShowContent("<body/>");
                    EnableEdits(false);
                    return;
                }

                //security checks next
                //sender registration
                string registrationAlert;
                var registered = ThisAddIn.CheckSenderRegistration(_senderAddress, _account, out registrationAlert);
                if(registered != ThisAddIn.RegistrationState.Registered)
                {
                    ShowContent("<body/>");
                    EnableEdits(false);                    
                    //only raise an alert if the reading pane is not visible
                    //otherwise it will just be a duplicate                        
                    if (!previewPane)
                    {
                        string alert;
                        switch (registered)
                        {
                            case ThisAddIn.RegistrationState.NotRegistered:
                                alert = string.Format(
                                    Resources.sender_not_registered,
                                    _senderAddress);
                                break;
                            case ThisAddIn.RegistrationState.BadCredentials:
                                alert = string.Format(Resources.error_checking_registration,
                                    _senderAddress, Resources.invalid_password);
                                break;
                            default:
                                alert = string.Format(Resources.error_checking_registration,
                                    _senderAddress, registrationAlert);
                                break;
                        }
                        MessageBox.Show(this, alert,
                            Resources.product_name,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                    return;
                }
                //server licensed
                if (!ThisAddIn.IsServerLicensed(_serverName, _serverPort))
                {
                    Logger.Error(SOURCE, string.Format("blocking content fetch for invalid server/port {0}:{1}",
                                                       _serverName, _serverPort));
                    //only raise an alert if the reading pane is not visible
                    //otherwise it will just be a duplicate
                    if (!previewPane)
                    {
                        MessageBox.Show(this, Resources.invalid_server,
                                        Resources.product_name,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                    }
                    ShowContent("<body/>");
                    EnableEdits(false);
                    return;                    
                }

                btnMessage.PanelClick += BtnMessageClick;
                if (ThisAddIn.NoPreviewer)
                {
                    tableLayoutPanelMain.Click += BtnMessageClick;
                    foreach (Control ctl in tableLayoutPanelMain.Controls)
                    {
                        if (ctl is Label ||
                            ctl is TableLayoutPanel)
                        {
                            ctl.Click += BtnMessageClick;
                        }
                    }
                    htmlEditor1.Click += HTMLEditor1Click;
                    Click += BtnMessageClick;
                }
                var attachments = item.Attachments;
                    
                _recordKey = Utils.GetRecordKey(safMail);
                
                Logger.Info(SOURCE,"evaluating Editable for " + item.Subject);
                _editable = ThisAddIn.IsEditable(item, _senderAddress, _serverName, _serverPort, storeAddress);
                //Ephemeral
                if (!string.IsNullOrEmpty(_duration) &&
                    _duration != "0" && !_editable)
                {
                    Content = Resources.ephemeral_content;
                    _editable = false;
                    EnableEdits(false);
                    return;
                }
                //add handler for contextmenu.show event
                mnuAttach.Opening += MnuAttachOpening;
                
                //get the account we'll use to fetch/update the content
                if (_editable)
                {
                    _account = ThisAddIn.Accounts[_senderAddress];
                    openToolStripMenuItem.Text = Resources.open_for_editing;
                    useDefaultApplicationToolStripMenuItem.Visible = true;
                    browseForEditorToolStripMenuItem.Visible = true;
                }
                else //if (fetch)
                {
                    openToolStripMenuItem.Text = Resources.open_menu_option;
                    useDefaultApplicationToolStripMenuItem.Visible = false;
                    browseForEditorToolStripMenuItem.Visible = false;
                }

                if (Pointers == null)
                {
                    Logger.Warning(SOURCE, string.Format(
                        "failed to return pointers for {0}",
                        item.Subject));
                    _editable = false;
                    EnableEdits(false);
                    return;
                }
                if (_account == null)
                {
                    Logger.Warning(SOURCE, string.Format(
                        "unable to locate account to retrieve content for {0}",
                        item.Subject));
                    _editable = false;
                    EnableEdits(false);
                    return;
                }

                //body
                string content;
                string error;
                ContentHandler.FetchContent(_account.SMTPAddress, _configuration,
                            _senderAddress, Pointers[0], ServerName, ServerPort,
                            !string.IsNullOrEmpty(EncryptKey2), out content, out error);
                
                if (string.IsNullOrEmpty(content))
                {
                    Logger.Warning(SOURCE, string.Format(
                        "FetchContent request for {0} from {1} returned {2}",
                        _pointerString, _senderAddress, error));
                    _editable = false;
                    //hide placeholder
                    ShowContent("<body/>");
                    //only raise an alert if the reading pane is not visible
                    //otherwise it will just be a duplicate                        
                    if (!previewPane)
                    {
                        MessageBox.Show(this, string.Format(
                                Resources.error_fetching_content,
                                Environment.NewLine, error),
                            Resources.product_name,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                    }
                }
                //self-destruct
                //if (!string.IsNullOrEmpty(_duration))
                //{
                //    Logger.Info(SOURCE, string.Format("found Duration: {0} on {1}",
                //        _duration, item.Subject));
                //    //instantiate timer, set to fire one Duration from now
                //    _selfDestructTimer = new Timer(SelfDestructTimerTick, 
                //        "content", 
                //        Convert.ToInt32(_duration) * 1000,
                //        Timeout.Infinite);                                                    
                    //if (!string.IsNullOrEmpty(content) && _senderAddress != _account.SMTPAddress)
                    //{
                    //    //post REMOVE RECIPIENT
                    //    ContentHandler.RemoveRecipient(_account.SMTPAddress, _configuration,
                    //        _senderAddress, Pointers[0],ServerName, ServerPort, out error);
                    //}
                //}
                var embeddedFileNames = new List<string>();
                if(!string.IsNullOrEmpty(content)){
                    if (!string.IsNullOrEmpty(EncryptKey2))
                    {
                        //content is raw base64 - decode first
                        var encrypted = Convert.FromBase64String(content);
                        content = Encoding.UTF8.GetString(
                            AES_JS.Decrypt(encrypted, EncryptKey2));
                    }
                    else if (!string.IsNullOrEmpty(EncryptKey))
                    {
                        content = Cryptography.DecryptAES(content, EncryptKey);
                    }
                    htmlEditor1.BaseUrl = Path.Combine(
                        Path.GetTempPath(), "ChiaraMail", _recordKey);
                    var imageMap = Utils.MapAttachments(Pointers, attachments);
                    //fix any paths to embedded images
                    var imageLinks = Utils.GetImageFileLinks(content, "src");
                    if (imageLinks.Count > 0)
                    {
                        Content = Utils.FetchEmbeddedFileImages(content, imageLinks, imageMap,
                            htmlEditor1.BaseUrl, _account, _configuration, _senderAddress, ServerName, 
                            ServerPort, EncryptKey2, UserAgent, ref embeddedFileNames);
                    }
                    else
                    {
                        //if sent with 'include content' we need to handle regular cid: links
                        Content = Utils.LoadEmbeddedImageAttachments(item, content);
                    }
                }
                //}
                //add links for attachments
                if (attachments.Count.Equals(0))// || !fetch)
                {
                    tableLayoutAttach.Visible = false;
                }
                else 
                {
                    //create or get a temp folder to store the attachments
                    Utils.CreateTempFolder(_recordKey);
                    tableLayoutAttach.Visible = true;
                    if (ThisAddIn.NoPreviewer)
                    {
                        btnMessage.Caption = "Attachments:";
                        btnMessage.HideImage(ThisAddIn.AppVersion < 14
                            ? Color.CornflowerBlue
                            : Color.DarkGray);
                        panelVertLine.Visible = false;
                        previewToolStripMenuItem.Visible = false;
                        mnuSep1.Visible = false;
                    }
                    //int index = 0;
                    int upperWidth = 0;
                    int upperHeight = 0;
                    panelAttach.AutoScroll = false;
                    Utils.LoadAttachments(item, Pointers, htmlEditor1.BaseUrl, _account, _senderAddress,
                            ServerName, ServerPort, EncryptKey, EncryptKey2,embeddedFileNames,
                            ref _attachList, ref _embedded, ref panelAttach, ref upperWidth, ref upperHeight);
                    //adjust all to same (upper) Width
                    for (var i = 1; i < panelAttach.Controls.Count; i++)
                    {
                        var btn = (AttachPanel) panelAttach.Controls[i];
                        //hook up the event handlers
                        btn.PanelClick += SelectAttachment;
                        btn.PanelDblClick += AttachmentDoubleClick;
                        if (!btn.Pointer.StartsWith("embedded"))
                        {
                            btn.ContextMenuStrip = mnuAttach;
                        }
                        if (btn.Width >= upperWidth) continue;
                        btn.AutoSize = false;
                        btn.Width = upperWidth;
                        btn.Height = upperHeight;
                    }
                }
                //enable/disable edits
                EnableEdits(_editable);
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void DynamicInspectorFormRegionClosed(object sender, EventArgs e)
        {
            //release the loaded file, if there is one
            previewHandlerControl.Dispose();
            //if(_selfDestructTimer != null)
            //    _selfDestructTimer.Dispose();
            //delete any attachments in the temp directory
            string key = RecordKey;
            _recordKey = "";
            Utils.CleanTempFolder(key);
        }

        private void HTMLEditor1Click(object sender, SpiceLogic.WinHTMLEditor.EditorMouseEventArgs e)
        {
            OnClick(e);
        }

        //private void SelfDestructTimerTick(object state)
        //{
        //    if (!(state is string)) return;
        //    Invoke(new SelfDestructHandler(HandleSelfDestruct),
        //        Convert.ToString(state));
        //}

        //private void HandleSelfDestruct(string path)
        //{
        //    const string SOURCE = "HandleSelfDestruct";
        //    Logger.Info(SOURCE, "handling " + path);
        //    if (path == "content")
        //    {
        //        //clear content from this
        //        Content = string.Empty;
        //        //and any other windows
        //        ThisAddIn.RelayContentChange(this, RecordKey, Content);
        //    }
        //    else if (path.StartsWith("embedded"))
        //    {
        //        //embedded message
        //        if (!embeddedMsg1.Visible) return;
        //        //if it's displaying our message clear the content
        //        if (embeddedMsg1.Key == path.Replace("embedded:", ""))
        //            embeddedMsg1.wb1.DocumentText = string.Empty;
        //    }
        //    else
        //    {
        //        //try to delete the file at 'path'
        //        if (File.Exists(path))
        //        {
        //            try
        //            {
        //                File.Delete(path);
        //            }
        //            catch (Exception ex)
        //            {
        //                Logger.Warning(SOURCE, ex.ToString());
        //                //file could be locked - in that case fire timer again
        //                _selfDestructTimer.Change(Convert.ToInt32(_duration) * 1000, Timeout.Infinite);
        //            }
        //        }
        //        //kill preview if file is currently displayed
        //        if (path == _currentFilePath)
        //            previewHandlerControl.Open("");
        //    }
        //}

        private void TableLayoutPanelMainPaint(object sender, PaintEventArgs e)
        {
            const string SOURCE = CLASS_NAME + "tableLayoutPaneMain_Paint";
            try
            {
                //position attachments
                int top = 0;
                Point start;
                Point end;
                if (tableLayoutAttach.Visible)
                {
                    int nextLeft = 1;
                    int rows = 1;
                    panelAttach.Height = 28;
                    for (int i = 1; i < panelAttach.Controls.Count; i++)
                    {
                        var btn = (AttachPanel)panelAttach.Controls[i];
                        if (i>1 && (nextLeft + btn.Width) > panelAttach.Width)
                        {
                            rows++;
                            panelAttach.AutoScroll = true;
                            //drop down a row
                            top += btn.Height;
                            nextLeft = 1;
                            if (rows < 4)
                            {
                                panelAttach.Height += btn.Height;
                            }
                        }
                        btn.Top = top;
                        btn.Left = nextLeft;
                        nextLeft = btn.Left + btn.Width;
                    }
                    tableLayoutAttach.Height = panelAttach.Height;
                }
                Graphics g = e.Graphics;
                Pen pen;
                if (ThisAddIn.AppVersion < 14)
                {
                    //solid light blue line
                    pen = new Pen(Color.CornflowerBlue, 1)
                              {
                                  DashStyle = System.Drawing.Drawing2D.DashStyle.Solid
                              };
                    panelVertLine.BackColor = Color.CornflowerBlue;
                }
                else
                {
                    //dotted gray line
                    pen = new Pen(Color.DarkGray, 1)
                              {
                                  DashStyle = System.Drawing.Drawing2D.DashStyle.Dash
                              };
                    panelVertLine.BackColor = Color.DarkGray;
                }
                int left = htmlEditor1.Left + 1;
                int width = Width - (2 * left);
                if (tableLayoutAttach.Visible && !ThisAddIn.NoPreviewer)
                {
                    //if there are attachments then draw a line at the top of that panel
                    top = tableLayoutAttach.Top - 2;
                    start = new Point(left, top);
                    end = new Point(left + width, top);
                    g.DrawLine(pen, start, end);
                }
                //border around editor/preview handler
                Control ctrl = htmlEditor1;
                if (previewHandlerControl.Visible) ctrl = previewHandlerControl;
                if (embeddedMsg1.Visible) ctrl = embeddedMsg1;
                pen.Color = Color.DarkGray;
                pen.Width = 1;
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                top = ctrl.Top - 1;
                left = ctrl.Left - 1;
                //across top
                start = new Point(left, top);
                end = new Point(left + ctrl.Width + 2, top);
                g.DrawLine(pen, start, end);
                //down right
                start = new Point(end.X, top);
                end = new Point(start.X, top + ctrl.Height + 2);
                g.DrawLine(pen, start, end);
                //across bottom
                start = new Point(left, end.Y);
                g.DrawLine(pen, end, start);
                //up left
                end = new Point(left, top);
                g.DrawLine(pen, start, end);               
                pen.Dispose();
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE,ex.ToString());
            }
        }

        private void SelectAttachment(object sender, EventArgs e)
        {
            const string SOURCE = CLASS_NAME + "SelectAttachment";
            var btn = (AttachPanel)sender;
            //btn.Selected = true;
            //clear all other button backgrounds
            btnMessage.Selected = false;
            ResetButtons(btn.Pointer);
            btn.Selected = true;
            if (ThisAddIn.NoPreviewer) return;
            if (!string.IsNullOrEmpty(_duration)  && _duration != "0")
            {
                Logger.Info(SOURCE,string.Format("exiting, duration = {0}", _duration));
                return; 
            }
            try
            {
                Cursor = Cursors.WaitCursor;
                var pointer = btn.Pointer;
                var embedded = pointer.StartsWith("embedded:");
                if (embedded)
                {
                    //get the embedded attachment
                    Redemption.Attachment attach = _embedded[btn.Pointer];
                    var message = attach.EmbeddedMsg;
                    if (message != null)
                    {
                        LoadAttachmentHeader("", "", attach.DisplayName, Utils.FormatFileSize(attach.Size));
                        //hide the htmlEditor row
                        htmlEditor1.Visible = false;
                        tableLayoutPanelMain.RowStyles[4].Height = 0;
                        tableLayoutPanelMain.RowStyles[5].Height = 0;
                        tableLayoutPanelMain.RowStyles[6].Height = 100;
                        previewHandlerControl.Visible = false;
                        embeddedMsg1.LoadMsg(message, _recordKey, btn.Pointer.Replace("embedded:", ""),
                                             _entryId, _account, _senderAddress);
                        embeddedMsg1.Visible = true;
                    }
                }
                else
                {
                    ShowPreview(true);
                    //do we already have the attachment?
                    string path;
                    string hash;
                    Utils.GetFile(pointer, AttachList[pointer].Name, AttachList[pointer].Index,
                                  _recordKey, _account, _configuration, _senderAddress, ServerName, ServerPort,
                                  EncryptKey, EncryptKey2, UserAgent, out path, out hash);
                    _currentFilePath = path;
                    AttachList[pointer].Hash = hash;
                    LoadAttachmentHeader(btn.Pointer, _currentFilePath, "", "");
                    //load previewer or "no previewer" text/link
                    if (File.Exists(path))
                    {
                        Logger.Info(SOURCE, "invoking previewControl.Open for " + Path.GetFileName(path));
                        previewHandlerControl.Open(path);
                    }
                    else
                    {
                        Logger.Info(SOURCE,"no file at " + path);
                    }
                }
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void AttachmentClick(string pointer)
        {
            for (int i = 1; i < panelAttach.Controls.Count; i++)
            {
                var btn = (AttachPanel)panelAttach.Controls[i];
                if (!btn.Pointer.Equals(pointer)) continue;
                SelectAttachment(btn, null);
                return;
            }
        }

        private void AttachmentDoubleClick(object sender, EventArgs e)
        {
            const string SOURCE = CLASS_NAME + "AttachmentDoubleClick";
            try
            {
                //get the filename and path
                var btn = (AttachPanel)sender;
                if(btn.Pointer.StartsWith("embedded")) return;
                //var path = GetFile(btn.Pointer);
                string path;
                string hash;
                Utils.GetFile(btn.Pointer, AttachList[btn.Pointer].Name, AttachList[btn.Pointer].Index,
                    _recordKey, _account, _configuration, _senderAddress, ServerName, ServerPort,
                    EncryptKey, EncryptKey2, UserAgent, out path, out hash);
                if (string.IsNullOrEmpty(path)) return;
                AttachList[btn.Pointer].Hash = hash;
                var frm = new OpenSaveEditForm
                              {
                                  lblName =
                                      {
                                          Text = string.Format(
                                              "Attachment: {0} from {1}",
                                              Path.GetFileName(path), _outlookFolderName)
                                      }
                              };
                var result = frm.ShowDialog(this);
                switch (result)
                {
                    case DialogResult.OK: //Open
                        OpenFile(btn.Pointer);
                        break;
                    case DialogResult.Yes: //Save
                        SaveFile(btn.Pointer);
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
        }

        private void BtnMessageClick(object sender, EventArgs e)
        {
            LoadMessageHeader();
            Content = Content;
            _currentFilePath = "";
            ShowPreview(false);
            btnMessage.Selected = !ThisAddIn.NoPreviewer;
            ResetButtons("");
            EnableEdits(_editable);
        }

        private void EnableEdits(bool enable)
        {
            htmlEditor1.Toolbar1.Visible = enable;
            htmlEditor1.Toolbar2.Visible = enable && !_plainText;
            htmlEditor1.ReadOnly = !enable;
            if (enable)
            {
                openToolStripMenuItem.Text = Resources.open_for_editing;
                if (_plainText)
                {
                    Utils.ConfigureEditorForPlainText(htmlEditor1.Toolbar1);
                }
            }
        }
                
        private void ResetButtons(string pointer)
        {
            for (var i = 1; i < panelAttach.Controls.Count; i++)
            {
                var btn = (AttachPanel)panelAttach.Controls[i];
                if (btn.Pointer != pointer)
                {
                    btn.Selected = false;
                }
            }
        }

        private void ShowPreview(bool preview)
        {
            previewHandlerControl.Visible = preview;
            htmlEditor1.Visible = !preview;
            embeddedMsg1.Visible = false;
            tableLayoutPanelMain.RowStyles[6].Height = 0;
            if (preview)
            {
                //hide the htmlEditor row
                tableLayoutPanelMain.RowStyles[4].Height = 0;
                tableLayoutPanelMain.RowStyles[5].SizeType = SizeType.Percent;
                tableLayoutPanelMain.RowStyles[5].Height = 100;
            }
            else
            {
                //show the htmlEditor row
                tableLayoutPanelMain.RowStyles[4].Height = 100;
                tableLayoutPanelMain.RowStyles[5].Height = 0;
            }
            tableLayoutPanelMain.Update();
        }

        private void MnuAttachOpening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _sourceBtn = mnuAttach.SourceControl as AttachPanel;
        }

        private void PreviewToolStripMenuItemClick(object sender, EventArgs e)
        {
            //fire click to preview attachment
            if (_sourceBtn == null)
            {
                _sourceBtn = (AttachPanel)mnuAttach.SourceControl;
            }
            AttachmentClick(_sourceBtn.Pointer);
        }

        private void OpenToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (_sourceBtn == null)
            {
                _sourceBtn = (AttachPanel)mnuAttach.SourceControl;
            }
            OpenFile(_sourceBtn.Pointer);
        }

        private void SaveToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (_sourceBtn == null)
            {
                _sourceBtn = (AttachPanel)mnuAttach.SourceControl;
            }
            SaveFile(_sourceBtn.Pointer);
        }

        private void UseDefaultApplicationToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (_sourceBtn == null)
            {
                _sourceBtn = (AttachPanel)mnuAttach.SourceControl;
            }
            if (_sourceBtn == null) return;
            OpenFile(_sourceBtn.Pointer);
        }

        private void BrowseForEditorToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (_sourceBtn == null)
            {
                _sourceBtn = (AttachPanel)mnuAttach.SourceControl;
            }
            if (_sourceBtn == null) return;
            BrowseAndOpenFile(_sourceBtn.Pointer);
        }

        //use OpenFile to handle edits as well
        private void OpenFile(string pointer)
        {
            const string SOURCE = CLASS_NAME + "OpenFile";
            try
            {
                if (string.IsNullOrEmpty(pointer))
                {
                    Logger.Warning(SOURCE, "pointer is blank");
                    return;
                }
                //string path = GetFile(pointer);
                string path;
                string hash;
                Utils.GetFile(pointer, AttachList[pointer].Name, AttachList[pointer].Index,
                    _recordKey, _account, _configuration, _senderAddress, ServerName, ServerPort,
                    EncryptKey, EncryptKey2, UserAgent, out path, out hash);
                if (string.IsNullOrEmpty(path))
                {
                    Logger.Warning(SOURCE, "failed to retrieve file for " + pointer);
                    return;
                }
                AttachList[pointer].Hash = hash;
                Logger.Verbose(SOURCE,"opening " + Path.GetFileName(path));
                ThreadPool.QueueUserWorkItem(Utils.OpenFile, 
                    new[]{path});
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
        }

        private void SaveFile(string pointer)
        {
            const string SOURCE = CLASS_NAME + "SaveFile";
            try
            {
                if (string.IsNullOrEmpty(pointer))
                {
                    Logger.Warning(SOURCE, "pointer is blank");
                    return;
                }
                //get the current path
                //string path = GetFile(pointer);
                string path;
                string hash;
                Utils.GetFile(pointer, AttachList[pointer].Name, AttachList[pointer].Index,
                    _recordKey, _account, _configuration, _senderAddress, ServerName, ServerPort,
                    EncryptKey, EncryptKey2, UserAgent, out path, out hash);
                if (string.IsNullOrEmpty(path))
                {
                    return;
                }
                AttachList[pointer].Hash = hash;
                //raise Save Dialog
                saveFileDialog.FileName = Path.GetFileName(path);
                saveFileDialog.ShowDialog(this);
                string newPath = saveFileDialog.FileName;
                if (!string.IsNullOrEmpty(newPath))
                {
                    //copy the file to the new location
                    File.Copy(path, newPath);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
        }

        private void BrowseAndOpenFile(string pointer)
        {
            const string SOURCE = CLASS_NAME + "BrowseAndOpenFile";
            try
            {
                if (string.IsNullOrEmpty(pointer))
                {
                    Logger.Warning(SOURCE, "pointer is blank");
                    return;
                }
                //get the current path
                string path;
                string hash;
                Utils.GetFile(pointer, AttachList[pointer].Name, AttachList[pointer].Index,
                    _recordKey, _account, _configuration, _senderAddress, ServerName, ServerPort, 
                    EncryptKey, EncryptKey2, UserAgent, out path, out hash);
                if (string.IsNullOrEmpty(path))
                {
                    Logger.Warning(SOURCE,"failed to return path for " + pointer);
                    return;
                }
                AttachList[pointer].Hash = hash;
                //raise Open Dialog so the user can find the editor
                openFileDialog.FileName = "";
                openFileDialog.Multiselect = false;
                openFileDialog.Title = string.Format(
                    "Open editor for {0}", Path.GetFileName(path));
                openFileDialog.Filter = Resources.Application__exe;
                openFileDialog.CheckFileExists = true;
                openFileDialog.CheckPathExists = true;
                if (openFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    string editorPath = openFileDialog.FileName;
                    if (editorPath != null && File.Exists(editorPath))
                    {
                        Logger.Verbose(SOURCE, string.Format(
                            "using {0} to edit {1}",
                            Path.GetFileName(editorPath), Path.GetFileName(path)));
                        ThreadPool.QueueUserWorkItem(Utils.OpenFile,
                            new[]{editorPath, 
                                "\"" + path + "\""});
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
        }

        private void LoadMessageHeader()
        {
            if (ThisAddIn.AppVersion > 14)
            {
                inspectorHdr.Visible = false;
                msgHdr15.Visible = true;
                msgHdr15.LoadMessage(_subject,_senderName,_sentDate,_toRecip, _ccRecip,false);
            }
            else
            {
                msgHdr15.Visible = false;
                inspectorHdr.Visible = true;
                inspectorHdr.LoadMessage(_subject, _senderName, _sentDate, _toRecip, _ccRecip);
            }
        }

        private void LoadAttachmentHeader(string pointer, string path, 
            string subject, string size)
        { 
            //set values            
            if (string.IsNullOrEmpty(subject))
            {
                if (string.IsNullOrEmpty(path) || !File.Exists(path))
                {
                    subject = AttachList[pointer].Name;
                    size = "";
                }
                else
                {
                    subject = Path.GetFileName(path);
                    size = Utils.GetFileSize(path);
                }
            }
            msgHdr15.Visible = false;
            inspectorHdr.Visible = true;
            inspectorHdr.LoadAttachment(subject,size);
        }
        
        private void ShowContent(string content)
        {
            Content = content;
            htmlEditor1.DisableEditorRightClick = true;
            tableLayoutAttach.Visible = false;
            previewHandlerControl.Visible = false;
        }
    }
}
