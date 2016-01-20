using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ChiaraMail.Properties;
using System.IO;
using System.Text;
using System.Net;

namespace ChiaraMail.Forms
{
    public partial class WaitForm : Form
    {
        private const string CLASS_NAME = "WaitForm.";
        internal List<Attachment> Attachments;
        internal Account Account;
        internal EcsConfiguration Configuration;
        internal string Recips;
        internal string EncryptKey;
        internal string EncryptKey2;
        internal bool CheckRegistration;
        internal string RegistrationResponse; 
        private int _index;        
        private int intProgressIncrement = 0;
        internal string Pointer;
        internal Dictionary<string, Attachment> AttachList;
        internal string RecordKey;
        internal Account CurrentAccount;
        internal EcsConfiguration CurrentConfiguration;
        internal string SenderAddress;
        internal string ServerName;
        internal string ServerPort;
        internal string UserAgent;
        public static string Path;
        public static string Hash;
        public DownloadUpload CallType = DownloadUpload.Upload;

        public WaitForm()
        {
            InitializeComponent();
            bw.DoWork += BwDoWork;
            bw.RunWorkerCompleted += 
                BwRunWorkerCompleted;
            Shown += WaitFormShown;
            lblSize.Text = "";
            SetProgress(0);
            AppConstants.CurrentChunk = 0;
        }

        private void SetProgress(int per)
        {
            progressBar.Value = per;
            lblPercentage.Text = per + "%";
            lblProgress.Text = per + "/100";
        }

        private void WaitFormShown(object sender, EventArgs e)
        {
            SetProgress(0);
            if (CallType == DownloadUpload.Upload)
            {
                tmrProgressBar.Enabled = true;
                if (CheckRegistration)
                {
                    lblWait.Text = Resources.wait_check_registered;
                    bw.RunWorkerAsync();
                }
                else
                {
                    if (Attachments == null || Attachments.Count == 0)
                    {
                        //nothing to do
                        DialogResult = DialogResult.Abort;
                    }
                    _index = 0;
                    HandleNextAttach();
                }
            }
            else
            {
                //lblWait.Text = Resources.wait_check_registered;
                bw.RunWorkerAsync();
            }
        }

        private void HandleNextAttach()
        {
            //only show dialog for attachments where type is ByValue
            while (_index < Attachments.Count)
            {
                while (Attachments[_index].Type != 1) _index++;
                break;
            }
            //do the next attachment if we have one             
            if (_index < Attachments.Count)
            {
                SetCaption(_index);
                bw.RunWorkerAsync();
            }
            else
            {
                //we're done - close the form
                DialogResult = DialogResult.OK;
            }
        }

        private const int BUFFER_SIZE = 1024;

        private void BwDoWork(object sender, DoWorkEventArgs e)
        {
            const string SOURCE = CLASS_NAME + "DoWork";
            try
            {
                string error;
                if (CallType == DownloadUpload.Upload)
                {
                    if (CheckRegistration)
                    {
                        e.Result = ContentHandler.CheckRegistered(Account.SMTPAddress, Configuration, Recips, out error);
                        return;
                    }
                    //get the current attachment
                    Attachment attach = Attachments[_index];

                    string pointer = null;
                    string encodedContent = ContentHandler.EncodeAttachBytes(attach.Content,
                        EncryptKey, EncryptKey2);
                    if (string.IsNullOrEmpty(attach.Pointer) || attach.Pointer.Equals("0"))
                    {
                        ContentHandler.PostContent(Account.SMTPAddress, Configuration, encodedContent,
                            Recips, ref bw, ref e, ref pointer, out error);
                        Attachments[_index].Pointer = pointer;
                    }
                    else
                    {
                        ContentHandler.UpdateContent(Account.SMTPAddress, Configuration, encodedContent,
                            attach.Pointer, ref bw, ref e, out error);
                    }
                    e.Result = error;
                }
                else
                {
                    string path;
                    string hash;
                    string content;

                    ContentHandler.FetchSegment(CurrentAccount.SMTPAddress, CurrentConfiguration, SenderAddress,
                            Pointer, ServerName, ServerPort, false, Pointer, out content, out error);

                    this.Invoke((MethodInvoker)delegate()
                    {
                        SetCaption(_index, DownloadUpload.Download);
                    });

                    double size;
                    Double.TryParse(error, out size);

                    path = "";
                    hash = "";
                    if (string.IsNullOrEmpty(RecordKey)) return;
                    try
                    {
                        path = Utils.GetFilePath(RecordKey, AttachList[Pointer].Index, AttachList[Pointer].Name);
                        //if (!File.Exists(path))
                        //{
                            //fetch the content
                            var post = ContentHandler.AssembleLoginParams(CurrentAccount.SMTPAddress, CurrentConfiguration.Password) +
                            string.Format("{0}&parms={1} {2}",
                            "FETCH%20CONTENT",
                            SenderAddress,
                            Pointer);
                            byte[] postData = Encoding.UTF8.GetBytes(post);
                            if (string.IsNullOrEmpty(ServerName)) ServerName = CurrentConfiguration.Server;
                            if (string.IsNullOrEmpty(ServerPort)) ServerPort = CurrentConfiguration.Port;
                            HttpWebRequest request = ContentHandler.CreateRequest(ServerName, ServerPort, postData);
                            if (request == null)
                            {
                                Logger.Error(SOURCE, "failed to create HttpWebRequest object");
                                error = "Internal error";
                                return;
                            }
                            var response = request.GetResponse() as HttpWebResponse;
                            if (!request.HaveResponse)
                            {
                                Logger.Error(SOURCE, "server did not respond");
                                request.Abort();
                                error = "Internal error";
                                return;
                            }
                            if (response == null)
                            {
                                Logger.Error(SOURCE, "response is null");
                                request.Abort();
                                error = "Internal error";
                                return;
                            }
                            var stream = response.GetResponseStream();
                            if (stream == null)
                            {
                                Logger.Error(SOURCE, "ResponseStream == null");
                                request.Abort();
                                error = "Internal error";
                                return;
                            }
                            
                            int byteSize;
                            byte[] downBuffer = new byte[BUFFER_SIZE];

                            var bytes = new MemoryStream();
                            while ((byteSize = stream.Read(downBuffer, 0, downBuffer.Length)) > 0)
                            {
                                bytes.Write(downBuffer, 0, byteSize);
                                int intPerValue = Convert.ToInt16(bytes.Length * 100 / size);

                                if (intPerValue > 1)
                                {
                                    progressBar.Invoke(
                                        (MethodInvoker)delegate()
                                        {
                                            SetProgress(intPerValue);
                                        }
                                    );
                                }
                            }

                            progressBar.Invoke(
                                (MethodInvoker)delegate()
                                {
                                    SetProgress(100);
                                }
                            );
                            System.Threading.Thread.Sleep(500);

                            byte[] result = bytes.ToArray();
                            string responseText = System.Text.Encoding.UTF8.GetString(result);

                            //------------



                            ContentHandler.ParseFetchResponse(responseText, true, ref content, out error);      

                            if (string.IsNullOrEmpty(content))
                            {
                                Logger.Warning(SOURCE, string.Format(
                                    "failed to retrieve content for {0} using pointer {1} from {2}: {3}",
                                    AttachList[Pointer].Name, Pointer, SenderAddress, error));
                                return;
                            }
                            ContentHandler.SaveAttachment(
                                content, EncryptKey, EncryptKey2, UserAgent, path);
                        //}
                        //return the hash
                        byte[] buf = File.ReadAllBytes(path);
                        hash = Cryptography.GetHash(buf);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(SOURCE, ex.ToString());
                    }




                    Path = path;
                    Hash = hash;

                    DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
                e.Result = ex.Message;
            }
        }

        private void BwRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            const string SOURCE = CLASS_NAME + "WorkerCompleted";
            try
            {
                if (e.Cancelled)
                {
                    if (CheckRegistration)
                    {
                        Logger.Info(SOURCE,
                            "user cancelled registration check");
                    }
                    else
                    {
                        Logger.Info(SOURCE, string.Format(
                            "user cancelled during upload of {0}",
                            Attachments[_index].Name));
                    }
                    DialogResult = DialogResult.Cancel;
                    return;
                }
                if (e.Error != null)
                {
                    if (CheckRegistration)
                    {
                        Logger.Error(SOURCE, string.Format(
                            "BackgroundWorker threw exception during registration check: {0}",
                            e.Error.Message));
                    }
                    else
                    {
                        Logger.Error(SOURCE, string.Format(
                            "BackgroundWorker threw exception during {0}: {1}",
                            Attachments[_index].Name,
                            e.Error.Message));
                    }
                    DialogResult = DialogResult.Abort;
                    return;
                }
                if (CheckRegistration)
                {
                    RegistrationResponse = Convert.ToString(e.Result);
                    DialogResult = DialogResult.OK;
                    return;
                }
                
                if (e.Result==null || 
                    e.Result.Equals(string.Empty) ||
                    e.Result.Equals("success"))
                {
                    //increment the index
                    _index++;
                    //do the next attachment if we have one                         
                    if (CallType == DownloadUpload.Upload)
                    {
                        HandleNextAttach();
                    }
                }
                else
                {
                    MessageBox.Show(string.Format(
                        "There was a problem posting {0}{1}{2}",
                        Attachments[_index].Name,
                        Environment.NewLine,
                        e.Result),
                        Resources.product_name,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    DialogResult=DialogResult.Abort;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
            }
        }

        private void BtnCancelClick(object sender, EventArgs e)
        {
            if (bw.IsBusy)
            {
                bw.CancelAsync();
            }
            //unload
            DialogResult = DialogResult.Cancel;
        }

        private void SetCaption(int index, DownloadUpload callType = DownloadUpload.Upload)
        {
            if (callType == DownloadUpload.Upload)
            {
                string action = "Updating file on content server:";
                string pointer = Attachments[_index].Pointer;
                if (string.IsNullOrEmpty(pointer) || pointer.Equals("0"))
                {
                    action = "Uploading file to content server:";
                }
                lblWait.Text = string.Format(
                    "{0}{2}{1}",
                    action, Attachments[index].Name,
                    Environment.NewLine);
                lblSize.Text = Utils.FormatFileSize(Attachments[index].Content.Length);
            }
            else
            {
                lblWait.Text = string.Format(
                "{0}{2}{1}",
                "Downloading file from content server:", AttachList[Pointer].Name,
                Environment.NewLine);
            }
        }

        private void tmrProgressBar_Tick(object sender, EventArgs e)
        {
            decimal perValue = (AppConstants.CurrentChunk / AppConstants.TotalChunks) * 100;

            if (perValue > 0)
            {
                if (AppConstants.CurrentChunk < AppConstants.TotalChunks && perValue <= 100)
                {
                    SetProgress(Convert.ToInt16(perValue));
                }
                else
                {
                    SetProgress(100);
                    System.Threading.Thread.Sleep(500);
                    tmrProgressBar.Enabled = false;
                }
            }
        }
    }
}