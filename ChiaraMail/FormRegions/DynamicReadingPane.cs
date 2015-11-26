using System;
using System.Collections.Generic;
using System.Linq;
using ChiaraMail.Controls;
using ChiaraMail.Forms;
using ChiaraMail.Properties;
using Microsoft.Office.Interop.Outlook;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using Redemption;
using System.Text;
using Exception = System.Exception;
using MAPIFolder = Microsoft.Office.Interop.Outlook.MAPIFolder;

namespace ChiaraMail.FormRegions
{
    partial class DynamicReadingPane
    {
        #region Form Region Factory

        private const string CLASS_NAME = "ReadingPane.";
        private string _pointerString;
        private string _serverName = "";
        private string _serverPort = "";
        private string _encryptKey = "";
        private string _encryptKey2 = "";
        private string _duration = "";
        private string _userAgent = "";
        private bool _allowForwarding = false;
        private Dictionary<string, Attachment> _attachList = new Dictionary<string, Attachment>();
        private string _recordKey = "";
        private string _content;
        private string _senderAddress = "";
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
        private int _lastKeyPress;
        private Dictionary<string, Redemption.Attachment> _embedded = 
            new Dictionary<string, Redemption.Attachment>();
        private AttachPanel _sourceBtn;
        private string _entryId;
        //private Timer _selfDestructTimer;

        //private delegate void SelfDestructHandler(string path);

        internal string[] Pointers
        {
            get
            {
                if (string.IsNullOrEmpty(_pointerString))
                {
                    return null;
                }
                return _pointerString.Split(' ');
            }
        }

        internal string Content
        {
            get { return _content; }
            set
            {   _content = value;
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

        [Microsoft.Office.Tools.Outlook.FormRegionMessageClass("IPM.Note.ChiaraMail")]
        [Microsoft.Office.Tools.Outlook.FormRegionName("ChiaraMail.DynamicReadingPane")]
        public partial class DynamicReadingPaneFactory
        {
            // Occurs before the form region is initialized.
            // To prevent the form region from appearing, set e.Cancel to true.
            // Use e.OutlookItem to get a reference to the current Outlook item.
            private void DynamicReadingPaneFactoryFormRegionInitializing(object sender, Microsoft.Office.Tools.Outlook.FormRegionInitializingEventArgs e)
            {

            }
        }

        #endregion

        // Occurs before the form region is displayed.
        // Use this.OutlookItem to get a reference to the current Outlook item.
        // Use this.OutlookFormRegion to get a reference to the form region.
        private void DynamicReadingPaneFormRegionShowing(object sender, EventArgs e)
        {
            const string SOURCE = CLASS_NAME + "Showing";
            Cursor = Cursors.WaitCursor;
            try
            {
                //need this event handler to capture space key
                htmlEditor1.KeyDown +=
                    HTMLEditor1KeyDown;
                
                btnEdit.Text = Resources.label_edit_content;
                btnDelete.Text = Resources.label_delete_content;
                var item = (MailItem)OutlookItem;
                Logger.Verbose(SOURCE, "loading " + item.Subject);
                var folder = (MAPIFolder)item.Parent;
                _outlookFolderName = folder.Name;                
                _plainText = (item.BodyFormat == OlBodyFormat.olFormatPlain);
                _senderName = item.SenderName;
                _sentDate = string.Format("{0} {1}",
                    item.SentOn.ToString("ddd"),
                    item.SentOn.ToString("g"));
                _toRecip = item.To;
                _ccRecip = item.CC;
                _subject = item.Subject;
                LoadMessageHeader();
                //hide preview handler
                previewHandlerControl.Visible = false;
                //read props from the header
                var safMail = RedemptionLoader.new_SafeMailItem();
                safMail.Item = item;
                _recordKey = Utils.GetRecordKey(safMail);
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
                if (Utils.IsSpoofed(_senderName, _senderAddress))
                {
                    Logger.Warning(SOURCE, string.Format("blocking content fetch for probable spoofed sender {0}:{1}",
                                                       _senderName, _senderAddress));

                        MessageBox.Show(this, Resources.spoofed_sender_address,
                                        Resources.product_name,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                    return;
                }

                Utils.ReadHeaders(item, ref _pointerString, ref _serverName,
                    ref _serverPort, ref _encryptKey, ref _encryptKey2, 
                    ref _duration, ref _userAgent, ref _allowForwarding);

                ThisAddIn.IsMailAllowForwarding = _allowForwarding;
                ThisAddIn._pointerString = _pointerString;
                
                // check for missing/incomplete configuration first
                //Logger.Verbose(SOURCE,string.Format("checking for store address; store: {0}, supplying {1}",
                //    store.DisplayName, store.StoreID));
                var storeAddress = Globals.ThisAddIn.GetStoreAddress(folder.StoreID);
                if (string.IsNullOrEmpty(storeAddress))
                {
                    Logger.Info(SOURCE, "using InternetAccountName as storeAddress");
                    storeAddress = item.InternetAccountName();
                }
                //Logger.Info(SOURCE,string.Format("store address:{0}, len(StoreID): {1}", 
                //    storeAddress, folder.StoreID.Length));
                _account = ThisAddIn.GetAccount(storeAddress);
                if (_account == null && !ThisAddIn.Initialized)
                {
                    //pause for 1 second and try again
                    Thread.Sleep(1000);
                    _account = ThisAddIn.GetAccount(storeAddress);
                }
                _configuration = ThisAddIn.GetMatchingConfiguration(storeAddress,
                    _serverName, _serverPort, true);
                if (_configuration == null)
                {
                    //no matching configuration
                    //only raise an alert if the reading pane is not visible
                    //otherwise it will just be a duplicate
                    MessageBox.Show(this, Resources.unknown_server,
                                    Resources.product_name,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    ShowContent("<body/>");
                    EnableEdits(false);
                    return;
                }
                else if (string.IsNullOrEmpty(_configuration.Password))
                {
                    Logger.Warning(SOURCE,string.Format("raising missing password alert for {0}; config.Key:{1}, server:{2}, port:{3}, account address: {4}",
                        _subject, _configuration.Key, _serverName, _serverPort,
                        _account == null ? "" : _account.SMTPAddress));
                    //incomplete configuration
                    MessageBox.Show(this, string.Format(
                            Resources.missing_settings,
                            Environment.NewLine, "Password"),
                        Resources.product_name,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    ShowContent("<body/>");
                    EnableEdits(false);
                    return;
                }

                //security checks next
                //sender registration
                string registrationAlert;
                var registered = ThisAddIn.CheckSenderRegistration(_senderAddress, _account, out registrationAlert);
                if (registered != ThisAddIn.RegistrationState.Registered)
                {
                    ShowContent("<body/>");
                    EnableEdits(false);
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
                    return;
                }

                
                //server licensed
                if (!ThisAddIn.IsServerLicensed(_serverName, _serverPort))
                {
                    Logger.Error(SOURCE, string.Format("blocking content fetch for invalid server/port {0}:{1}",
                        _serverName, _serverPort));
                    MessageBox.Show(this, Resources.invalid_server,
                                    Resources.product_name,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return;
                }
                _entryId = item.EntryID;
                
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
                Logger.Verbose(SOURCE, string.Format(
                    "found {0} attachments on {1}",
                    attachments.Count, item.Subject));
                
                //add handler for contextmenu.show event
                mnuAttach.Opening += MnuAttachOpening;
                _editable = ThisAddIn.IsEditable(item, _senderAddress, _serverName, _serverPort, storeAddress);
                //Ephemeral
                if (!string.IsNullOrEmpty(_duration) &&
                    _duration != "0" && !_editable)
                {
                    Content = Resources.ephemeral_content;
                    EnableEdits(false);
                    return;
                }

                if (_editable)
                {
                    _account = ThisAddIn.Accounts[_senderAddress];
                    openToolStripMenuItem.Text = Resources.open_for_editing;
                    useDefaultApplicationToolStripMenuItem.Visible = true;
                    browseForEditorToolStripMenuItem.Visible = true;
                }
                else
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
                    EnableEdits(false);
                    return;
                }
                if (_account == null)
                {
                    Logger.Warning(SOURCE, string.Format(
                        "unable to locate account to retrieve content for {0}",
                        item.Subject));
                    return;
                }

                _configuration = _account.Configurations.Values.
                                            First(config => config.Server == _serverName);
                if (_configuration == null)
                {
                    Logger.Warning(SOURCE, string.Format(
                        "unable to locate valid configuration to retrieve content for {0}",
                        item.Subject));
                    return;
                }

                Logger.Verbose(SOURCE, string.Format(
                    "found {0} pointers for {1}",
                    Pointers.Length, item.Subject));
                //body
                string content;
                string error;
                ContentHandler.FetchContent(_account.SMTPAddress, _configuration, _senderAddress,
                                            Pointers[0], ServerName, ServerPort,
                                            !string.IsNullOrEmpty(EncryptKey2), out content,
                                            out error);
                if (string.IsNullOrEmpty(content))
                {
                    ThisAddIn.IsCurrentItemHasContent = false;
                    Logger.Warning(SOURCE, string.Format(
                        "FetchContent request for {0} from {1} returned {2}",
                        Pointers[0], _senderAddress, error));
                    ShowContent("<body/>");
                    MessageBox.Show(this, string.Format(
                            Resources.error_fetching_content,
                            Environment.NewLine, error),
                        Resources.product_name,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }

                //self-destruct
                //if (!string.IsNullOrEmpty(_duration))
                //{
                //    Logger.Info(SOURCE, string.Format("found Duration: {0} on {1}",
                //        _duration, item.Subject));
                    //always instantiate timer
                    //_selfDestructTimer = new Timer(SelfDestructTimerTick,
                    //                                "content",
                    //                                Convert.ToInt32(_duration)*1000,
                    //                                Timeout.Infinite);

                    //if (!string.IsNullOrEmpty(content) &&  _senderAddress != _account.SMTPAddress)
                    //{
                    //    //post REMOVE RECIPIENT
                    //    ContentHandler.RemoveRecipient(_account.SMTPAddress, _configuration,
                    //        _senderAddress, Pointers[0], ServerName, ServerPort, out error);
                    //}
                //}
                var embeddedFileNames = new List<string>();
                if (!string.IsNullOrEmpty(content))
                {
                    ThisAddIn.IsCurrentItemHasContent = true;
                    if (string.IsNullOrEmpty(EncryptKey + EncryptKey2))
                    {
                        Logger.Verbose(SOURCE, string.Format(
                            "content length: {0}", content.Length));
                    }
                    else
                    {
                        Logger.Verbose(SOURCE, string.Format(
                            "raw content length: {0}", content.Length));
                        //decrypt content - use EncryptKey2 if we have it
                        if (!string.IsNullOrEmpty(EncryptKey2))
                        {
                            //content is still base64 - decode first
                            byte[] encrypted = Convert.FromBase64String(content);
                            
                            //if user-agent field have value then decrypt with CBC mode or decrypt with ECB mode (earlier solution)
                            if (!string.IsNullOrEmpty(UserAgent))
                            {
                                content = Encoding.UTF8.GetString(
                                AES_JS.DecryptCBC(encrypted, EncryptKey2));
                            }
                            else
                            {
                                content = Encoding.UTF8.GetString(
                                AES_JS.Decrypt(encrypted, EncryptKey2));
                            }
                        }
                        else
                        {
                            content = Cryptography.DecryptAES(content, EncryptKey);
                        }
                    }
                    htmlEditor1.BaseUrl = Path.Combine(
                        Path.GetTempPath(), "ChiaraMail", _recordKey);
                    var imageMap = Utils.MapAttachments(Pointers, attachments);
                    //fix any paths to embedded images
                    var imageLinks = Utils.GetImageFileLinks(content, "src");
                    
                    if (imageLinks.Count > 0)
                    {
                        content = Utils.FetchEmbeddedFileImages(content, imageLinks, imageMap,
                            htmlEditor1.BaseUrl, _account, _configuration, _senderAddress, 
                            ServerName, ServerPort, EncryptKey2, UserAgent, ref embeddedFileNames);
                    }
                    else
                    {
                        //if sent with 'include content' we need to handle regular cid: links
                        content = Utils.LoadEmbeddedImageAttachments(item, content);
                    }
                }
                if (attachments.Count.Equals(0))
                {
                    tableLayoutAttach.Visible = false;
                }
                else
                {
                    //create or get a temp folder to store the attachments
                    Utils.CreateTempFolder(_recordKey);
                    //int index = 0;
                    int upperWidth = 0;
                    int upperHeight = 0;
                    panelAttach.AutoScroll = false;
                    Utils.LoadAttachments(item, Pointers, htmlEditor1.BaseUrl, _account, _senderAddress,
                        ServerName, ServerPort, EncryptKey, EncryptKey2, embeddedFileNames,
                        ref _attachList, ref _embedded, ref panelAttach, ref upperWidth, ref upperHeight);
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
                    tableLayoutAttach.Visible = (panelAttach.Controls.Count > 1);
                    panelVertLine.Height = btnMessage.Height;
                    //adjust all AttachPanel buttons to same (upper) Width
                    for (var i = 1; i < panelAttach.Controls.Count; i++)
                    {
                        var btn = (AttachPanel)panelAttach.Controls[i];
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
                    //handle emnbedded video
                    //var videoLinks = Utils.GetVideoLinks(content);
                    //if (videoLinks.Count > 0)
                    //{
                    //    content = Utils.LoadEmbeddedVideos(content, videoLinks, _attachList,
                    //                                       htmlEditor1.BaseUrl, _account, _configuration,
                    //                                       _senderAddress, ServerName, ServerPort, _encryptKey2, _userAgent);
                    //}
                }
                if(!string.IsNullOrEmpty(content))
                    Content = content;
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

        // Occurs when the form region is closed.
        // Use this.OutlookItem to get a reference to the current Outlook item.
        // Use this.OutlookFormRegion to get a reference to the form region.
        private void DynamicReadingPaneFormRegionClosed(object sender, EventArgs e)
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
       
        private void HTMLEditor1KeyDown(object sender, SpiceLogic.WinHTMLEditor.EditorKeyEventArgs e)
        {
            //32 is space bar which for some reason doesn't convert to KeyPress
            //the event fires twice so only add space after second firing
            if (e.KeyCode.Equals(32) && _lastKeyPress.Equals(32))
            {
                //insert space
                htmlEditor1.InsertText(" ");
                //reset the buffer
                _lastKeyPress = 0;
                return;
            }
            _lastKeyPress = e.KeyCode;
        }

        private void TableLayoutPanel1Paint(object sender, PaintEventArgs e)
        {
            const string SOURCE = CLASS_NAME + "tableLayoutPanel_Paint";
            try
            {
                //position attachments
                var top = 0;
                if (tableLayoutAttach.Visible)
                {
                    var nextLeft = 1;
                    var rows = 1;
                    panelAttach.Height = btnMessage.Height;
                    for (var i = 1; i < panelAttach.Controls.Count; i++)
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
                //draw separators
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
                //draw line at top of visible control
                Control ctrl = htmlEditor1;
                if (previewHandlerControl.Visible) ctrl = previewHandlerControl;
                if (embeddedMsg1.Visible) ctrl = embeddedMsg1;
                top = ctrl.Top - 2;
                var left = ctrl.Left + 1;
                var width = Width - (2 * left);
                var start = new Point(left, top);
                var end = new Point(left + width, top);
                g.DrawLine(pen, start, end);
                if (tableLayoutAttach.Visible && !ThisAddIn.NoPreviewer)
                {
                    //if there are attachments then also draw a line at the top of that panel
                    top = tableLayoutAttach.Top - 1;
                    start = new Point(left, top);
                    end = new Point(left + width, top);
                    g.DrawLine(pen, start, end);
                }
                pen.Dispose();
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
        }

        private void SelectAttachment(object sender, EventArgs e)
        {
            var btn = (AttachPanel)sender;
            //clear all other button backgrounds
            btnMessage.Selected = false;
            ResetButtons(btn.Pointer);
            btn.Selected = true;
            if (ThisAddIn.NoPreviewer) return;
            try
            {
                if (!string.IsNullOrEmpty(_duration) && _duration != "0") return;
                Cursor = Cursors.WaitCursor;
                var pointer = btn.Pointer;
                var embedded = pointer.StartsWith("embedded:");
                if (embedded)
                {
                    //state = btn.Pointer;
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
                        previewHandlerControl.Visible = false;
                        embeddedMsg1.LoadMsg(message, _recordKey,
                                             btn.Pointer.Replace("embedded:", ""),
                                             _entryId, _account, _senderAddress);
                        embeddedMsg1.Visible = true;
                    }
                }
                else
                {
                    ShowPreview(true);
                    //do we already have the attachment?
                    var waitForm = new WaitForm
                                   {
                                       Pointer = pointer,
                                       AttachList = AttachList,
                                       RecordKey = _recordKey,
                                       CurrentAccount = _account,
                                       CurrentConfiguration = _configuration,
                                       SenderAddress = _senderAddress,
                                       ServerName = ServerName,
                                       ServerPort = ServerPort,
                                       EncryptKey = EncryptKey,
                                       EncryptKey2 = EncryptKey2,
                                       UserAgent =  UserAgent,
                                       CallType = DownloadUpload.Download
                                   };

                    waitForm.ShowDialog();
                    _currentFilePath = WaitForm.Path;
                    AttachList[pointer].Hash = WaitForm.Hash;
                    LoadAttachmentHeader(pointer, _currentFilePath, "", "");
                    if (File.Exists(_currentFilePath))
                    {
                        previewHandlerControl.Open(_currentFilePath);
                        //state = path;
                    }
                }
            }
            finally
            {
                Cursor = Cursors.Default;
            }
            //if (string.IsNullOrEmpty(_duration)) return;
            //if(string.IsNullOrEmpty(state)) return;
            //if (_senderAddress != _account.SMTPAddress)
            //{
            //    if (embedded)
            //    {
            //        _embedded[btn.Pointer] = null;
            //    }
            //    else
            //    {
            //        string error;
            //        ContentHandler.RemoveRecipient(_account.SMTPAddress, _configuration,
            //                _senderAddress, btn.Pointer, ServerName, ServerPort, out error);
            //    }
            //}
            //_selfDestructTimer.Change(0, Timeout.Infinite);
            //_selfDestructTimer = new Timer(SelfDestructTimerTick, 
            //    state, 
            //    Convert.ToInt32(_duration)*1000,
            //    Timeout.Infinite);
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
        //    Logger.Info(SOURCE,"handling " + path);
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
                if (btn.Pointer.StartsWith("embedded:")) return;
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

        private void BtnDeleteClick(object sender, EventArgs e)
        {
            const string SOURCE = CLASS_NAME + "Delete_Click";
            Cursor = Cursors.WaitCursor;
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
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void BtnEditClick(object sender, EventArgs e)
        {
            const string SOURCE = CLASS_NAME + "Edit_Click";
            Cursor = Cursors.WaitCursor;
            try
            {
                //grab the current content
                var content = htmlEditor1.BodyHtml;
                var updated = false;
                //has message changed?
                if (content != Content)
                {
                    //revert any embedded image links back to pointers
                    Utils.CollapseImgLinks(ref content);
                    var encodedContent = ContentHandler.EncodeContent(
                        content, EncryptKey, EncryptKey2);
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
                    string path = Utils.GetFilePath(
                        _recordKey, AttachList[pointer].Index, AttachList[pointer].Name); //GetFile(pointer);
                    if (File.Exists(path))
                    {
                        //get the content
                        byte[] buf = File.ReadAllBytes(path);
                        //has it changed?
                        string hash = Cryptography.GetHash(buf);
                        if (hash != AttachList[pointer].Hash)
                        {
                            var attach = new Attachment
                                             {
                                                 Name = Path.GetFileName(path),
                                                 Pointer = pointer,
                                                 Content = buf,
                                                 Hash = hash,
                                                 Type = (pointer == "0" ? 0 : 1)
                                             };
                            updateList.Add(attach);
                        }
                    }
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
                        foreach (var attach in updateList)
                        {
                            AttachList[attach.Pointer].Hash = attach.Hash;
                        }
                    }
                    else updated = false;
                }
                if (updated)
                {
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
            btnDelete.Enabled = enable;
            btnEdit.Enabled = enable;
            if (enable && _plainText)
            {
                //hide HTML formatting controls
                Utils.ConfigureEditorForPlainText(htmlEditor1.Toolbar1);
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
            //previewHandlerHost.Visible = preview;
            previewHandlerControl.Visible = preview;
            htmlEditor1.Visible = !preview;
            embeddedMsg1.Visible = false;
            tableLayoutPanelMain.RowStyles[6].Height = 0;
            if (preview)
            {
                tableLayoutPanelMain.RowStyles[4].Height = 0;
                tableLayoutPanelMain.RowStyles[5].SizeType = SizeType.Percent;
                tableLayoutPanelMain.RowStyles[5].Height = 100;
            }
            else
            {
                tableLayoutPanelMain.RowStyles[4].Height = 100;
                tableLayoutPanelMain.RowStyles[5].Height = 0;
            }            
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
                if (string.IsNullOrEmpty(path) || !File.Exists(path))
                {
                    Logger.Warning(SOURCE, "failed to retrieve file for " + pointer);
                    return;
                }
                AttachList[pointer].Hash = hash;
                Logger.Verbose(SOURCE, "opening " + Path.GetFileName(path));
                ThreadPool.QueueUserWorkItem(Utils.OpenFile, 
                    new[] {path});
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
                var waitForm = new WaitForm
                {
                    Pointer = pointer,
                    AttachList = AttachList,
                    RecordKey = _recordKey,
                    CurrentAccount = _account,
                    CurrentConfiguration = _configuration,
                    SenderAddress = _senderAddress,
                    ServerName = ServerName,
                    ServerPort = ServerPort,
                    EncryptKey = EncryptKey,
                    EncryptKey2 = EncryptKey2,
                    UserAgent = UserAgent,
                    CallType = DownloadUpload.Download
                };

                waitForm.ShowDialog();
                path = WaitForm.Path;
                hash = WaitForm.Hash;

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
                //string path = GetFile(pointer);
                string path;
                string hash;
                Utils.GetFile(pointer, AttachList[pointer].Name, AttachList[pointer].Index,
                    _recordKey, _account, _configuration, _senderAddress, ServerName, ServerPort,
                    EncryptKey, EncryptKey2, UserAgent, out path, out hash);
                if (string.IsNullOrEmpty(path))
                {
                    Logger.Warning(SOURCE, "failed to return path for " + pointer);
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
                messageHdr14.Visible = false;
                messageHdr15.Visible = true;
                messageHdr15.LoadMessage(_subject,_senderName, _sentDate, _toRecip,_ccRecip, true);
                messageHdr15.Reply += Reply;
                messageHdr15.ReplyAll += ReplyAll;
                messageHdr15.Forward += Forward;
            }
            else
            {
                messageHdr14.Visible = true;
                messageHdr15.Visible = false;
                messageHdr14.LoadMessage(_subject,_senderName, _sentDate, _toRecip,_ccRecip);
            }
        }

        private void Reply(object sender, EventArgs e)
        {
            Logger.Info("DynamicReadingPane.Reply","responding to Reply event");
            try
            {
                 var reply = ((MailItem) OutlookItem).Reply();
                reply.Display();
            }
            catch (Exception ex)
            {
                Logger.Error("Reply",ex.ToString());
            }
        }

        private void ReplyAll(object sender, EventArgs e)
        {
            Logger.Info("DynamicReadingPane.ReplyAll", "responding to ReplyAll event");
            try
            {
                var reply = ((MailItem)OutlookItem).ReplyAll();
                reply.Display();
            }
            catch (Exception ex)
            {
                Logger.Error("ReplyAll", ex.ToString());
            }
        }

        private void Forward(object sender, EventArgs e)
        {
            Logger.Info("DynamicReadingPane.Forward", "responding to Forward event");
            try
            {
                var forward = ((MailItem)OutlookItem).Forward();
                forward.Display();
            }
            catch (Exception ex)
            {
                Logger.Error("Forward", ex.ToString());
            }
        }      

        private void LoadAttachmentHeader(string pointer, string path, 
            string subject, string size)
        {
            //get the values
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
            if (ThisAddIn.AppVersion > 14)
            {
                messageHdr15.LoadAttachment(subject, size);
            }
            else
            {
                messageHdr14.LoadAttachment(subject, size);
            }            
        }

        private void ShowContent(string content)
        {
            htmlEditor1.BodyHtml = content;
            htmlEditor1.DisableEditorRightClick = true;
            tableLayoutAttach.Visible = false;
            previewHandlerControl.Visible = false;
        }
    }
    
}
