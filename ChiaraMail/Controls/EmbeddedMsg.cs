using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Redemption;
using System.Threading;
using System.IO;
using System.Runtime.InteropServices;

namespace ChiaraMail.Controls
{
    public partial class EmbeddedMsg : UserControl
    {
        private Attachments _attachments;
        private string _parentKey;
        public string Key;
        private Account _account;
        private EcsConfiguration _configuration;
        private string _serverName;
        private string _serverPort;
        private string _encryptKey2;
        private string _userAgent;
        private string[] _pointers;
        private string _sender;
        private string _className = "EmbeddedMsg.";

        public EmbeddedMsg()
        {
            InitializeComponent();
            tableLayoutPanelMain.Paint += TableLayoutPanelMainPaint;
            if (ThisAddIn.AppVersion < 14)
            {
                //labels are blue
                msgHdr15.Visible = false;
                lblAttach.ForeColor = Color.CornflowerBlue;
            }
            else if (ThisAddIn.AppVersion == 14)
            {
                //labels are gray
                msgHdr15.Visible = false;
                lblAttach.ForeColor = Color.DarkGray;
            }
            else
            {
                msgHdr14.Visible = false;
                msgHdr15.Visible = true;
                lblAttach.ForeColor = SystemColors.ControlDarkDark;
            }
        }

        public void LoadMsg(MessageItem item, string parentKey, string index, 
            string parentId, Account account, string parentSender)
        {
            string source = _className + "LoadMsg";
            _parentKey = parentKey;
            Key = index;
            _sender = item.Sender.SMTPAddress;
            _account = account;
            if (ThisAddIn.AppVersion < 15)
            {
                msgHdr14.LoadMessage(item.Subject,
                                     item.SenderName,
                                     string.Format("{0} {1}",
                                                   item.SentOn.ToString("ddd"),
                                                   item.SentOn.ToString("g")),
                                     item.To,
                                     item.CC);
            }
            else
            {
                msgHdr15.LoadMessage(item.Subject,
                                     item.SenderName,
                                     string.Format("{0} {1}",
                                                   item.SentOn.ToString("ddd"),
                                                   item.SentOn.ToString("g")),
                                     item.To,
                                     item.CC, false);
            }
            wb1.DocumentText = item.HTMLBody;
            if (Utils.HasChiaraHeader(item))
            {
                string pointers;               
                Utils.GetChiaraHeaders(item, out pointers, out _serverName, out _serverPort, out _encryptKey2, out _userAgent);
                _configuration = _account.Configurations.Values.First(config => config.Server == _serverName);
                if (!string.IsNullOrEmpty(pointers))
                {
                    _pointers = pointers.Split(new[] {" "}, StringSplitOptions.None);
                    string content;
                    string error;
                    ContentHandler.FetchContent(account.SMTPAddress, _configuration, _sender,
                        _pointers[0], _serverName, _serverPort, false, out content, out error);
                    if (string.IsNullOrEmpty(error) || error == "success")
                    {
                        if (!string.IsNullOrEmpty(_encryptKey2))
                        {
                            byte[] encrypted = Convert.FromBase64String(content);
                            content = Encoding.UTF8.GetString(
                                AES_JS.Decrypt(encrypted, _encryptKey2));
                        }
                        wb1.DocumentText = content;
                    }
                    else
                    {
                        Logger.Warning(source,string.Format(
                            "failed to retrieve content for {0} using pointer {1}, supplying sender {2}: {3}",
                            item.Subject, _pointers[0], _sender, error));
                    }
                }
            }
            _attachments = item.Attachments;
            if (_attachments.Count.Equals(0))
            {
                lblAttach.Visible = false;
                panelAttach.Visible = false;
            }
            else
            {
                //load them into the panel
                LoadAttachments();
            }
        }

        private void TableLayoutPanelMainPaint(object sender, PaintEventArgs e)
        {
            string source = _className + "tableLayoutPanel_Paint";
            try
            {
                //position attachments
                var top = 0;
                if (_attachments.Count>0)
                {
                    var nextLeft = 1;
                    var rows = 1;
                    //panelAttach.Height = btnMessage.Height;
                    for (var i = 1; i < panelAttach.Controls.Count; i++)
                    {
                        var btn = (AttachPanel)panelAttach.Controls[i];
                        if (i > 1 && (nextLeft + btn.Width) > panelAttach.Width)
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
                    //tableLayoutAttach.Height = panelAttach.Height;
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
                }
                else
                {
                    //dotted gray line
                    pen = new Pen(SystemColors.ControlDark, 1)
                              {
                                  DashStyle = System.Drawing.Drawing2D.DashStyle.Dash
                              };
                }
                //draw line at top of editor control
                top = wb1.Top - 2;
                var left = wb1.Left + 1;

                var width = Width - (2 * left);
                var start = new Point(left, top);
                var end = new Point(left + width, top);
                g.DrawLine(pen, start, end);
                pen.Dispose();
            }
            catch (Exception ex)
            {
                Logger.Error(source, ex.ToString());
            }
        }

        private void LoadAttachments()
        {
            var index = 0;
            var upperWidth = 0;
            var upperHeight = 0;
            panelAttach.Controls.Clear();
            foreach (Redemption.Attachment rdoAttach in _attachments)
            {
                index++;
                var btn = new AttachPanel
                              {
                                  Caption = rdoAttach.DisplayName, 
                                  Pointer = index.ToString(CultureInfo.InvariantCulture)
                              };
                switch (rdoAttach.Type)
                {
                    case 1: //byvalue
                        //is it hidden or have contentId
                        string contentId;
                        bool hidden;
                        Utils.GetAttachProps(rdoAttach, out contentId, out hidden);
                        if (!hidden)
                        {
                            //get the image
                            var container = ShellIcons.GetIconForFile(
                                rdoAttach.DisplayName, true, false);
                            btn.Picture = container.Icon.ToBitmap();
                        }
                        break;
                    case 5: //embedded
                        //use default envelope picture for button 
                        break;
                    default:
                        //skip other types
                        continue;
                }
                panelAttach.Controls.Add(btn);
                if (btn.Width > upperWidth) upperWidth = btn.Width;
                if (btn.Height > upperHeight) upperHeight = btn.Height;
            }
            //adjust all to same (upper) Width
            for (var i = 0; i < panelAttach.Controls.Count; i++)
            {
                var btn = (AttachPanel)panelAttach.Controls[i];
                //hook up the event handler
                btn.PanelDblClick += AttachmentDoubleClick;
                if (btn.Width < upperWidth)
                {
                    btn.AutoSize = false;
                    btn.Width = upperWidth;
                    btn.Height = upperHeight;
                }
            }
        }

        private void AttachmentDoubleClick(object sender, EventArgs e)
        {
            string source = _className + "AttachmentDoubleClick";
            try
            {
                //get the filename and path
                var btn = (AttachPanel)sender;
                DisplayAttach(btn.Pointer);
            }
            catch (Exception ex)
            {
                Logger.Error(source, ex.ToString());
            }
        }

        private void DisplayAttach(string pointer)
        {
            string source = _className + "DisplayAttach";           
            try
            {
                int index;
                string path;
                if (!Int32.TryParse(pointer, out index)) return;
                Redemption.Attachment attach = _attachments[index];
                if (attach == null) return;
                if (attach.Type == 5)//embedded
                {
                    //save as MSG/EML file
                    var item = attach.EmbeddedMsg;
                    if (item == null) return;
                    var fileName = attach.FileName;
                    path = GetTempPath(pointer, fileName);
                    if (!File.Exists(path)) item.SaveAs(path, Type.Missing);
                    Marshal.ReleaseComObject(item);
                    GC.Collect();
                }
                else
                {
                    //save to disk      
                    var isEcs = (_pointers != null && _pointers.Length > 0);
                    if(isEcs)
                    {
                        var ecsPointer = _pointers[index];
                        string hash;
                        Utils.GetFile(ecsPointer, attach.DisplayName, index, 
                            Path.Combine(_parentKey, Key), _account, _configuration,
                            _sender, _serverName, _serverPort, "", _encryptKey2, _userAgent, out path, out hash);

                    }
                    else
                    {
                        path = GetTempPath(pointer,attach.FileName);
                        if (!File.Exists(path))
                        {
                            attach.SaveAsFile(path);
                        }
                    }
                }
                if (!File.Exists(path))
                {
                    Logger.Warning(source,string.Format(
                        "failed to save {0} to disk",attach.FileName));
                    return;
                }
                //open in default app
                Logger.Verbose(source, "opening " + attach.FileName);
                ThreadPool.QueueUserWorkItem(Utils.OpenFile,
                    new[] { path });
            }
            catch (Exception ex)
            {
                Logger.Error(source,ex.ToString());
            }
        }

        private string GetTempPath(string pointer, string fileName)
        {
            string source = _className + "GetTempPath";
            string path = "";
            try
            {
                path = Path.Combine(new[] {
                    Path.GetTempPath(), 
                    "ChiaraMail",
                    _parentKey,
                    Key,
                    pointer});
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = Path.Combine(path, fileName);
            }
            catch (Exception ex)
            {
                Logger.Error(source, ex.ToString());
            }
            return path;
        }
    }
}
