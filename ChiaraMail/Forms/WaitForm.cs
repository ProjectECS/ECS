using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ChiaraMail.Properties;

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
        }

        private void WaitFormShown(object sender, EventArgs e)
        {
            if (CallType == DownloadUpload.Upload)
            {
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
                lblWait.Text = Resources.wait_check_registered;
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

                    try
                    {
                        double fileSizeInKB = (Attachments[_index].Content.Length * 8) / 1024;
                        double speed = Properties.Settings.Default.UploadSpeed; //kb

                        if (speed != 0)
                        {
                            int second = Convert.ToInt16(fileSizeInKB / speed);

                            if (second != 0)
                            {
                                intProgressIncrement = ((100 / second) * Attachments.Count);

                                this.Invoke((MethodInvoker)delegate()
                                {
                                    tmr.Enabled = true;
                                });
                            }
                            else
                            {
                                this.Invoke((MethodInvoker)delegate()
                                {
                                    progressBar.Value = 100;
                                });
                            }
                        }
                        else
                        {
                            this.Invoke((MethodInvoker)delegate()
                            {
                                progressBar.Value = 100;
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(SOURCE, ex.ToString());
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

                    this.Invoke((MethodInvoker)delegate()
                    {
                        progressBar.Value = 10;
                        lblWait.Text = "Analyzing attachment...";
                    });

                    ContentHandler.FetchSegment(CurrentAccount.SMTPAddress, CurrentConfiguration, SenderAddress,
                            Pointer, ServerName, ServerPort, false, Pointer, out content, out error);

                    this.Invoke((MethodInvoker)delegate()
                    {
                        SetCaption(_index, DownloadUpload.Download);
                    });

                    double size;
                    Double.TryParse(error, out size);

                    double fileSizeInKB = (size) / 1024;
                    double speed = Properties.Settings.Default.DownloadSpeed; //kb

                    if (speed != 0)
                    {
                        int second = Convert.ToInt16(fileSizeInKB / speed);

                        if (speed != 0 && second > 0)
                        {
                            intProgressIncrement = (100 / second);

                            this.Invoke((MethodInvoker)delegate()
                            {
                                tmr.Enabled = true;
                            });
                        }
                        else
                        {
                            this.Invoke((MethodInvoker)delegate()
                            {
                                progressBar.Value = 100;
                            });
                        }
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate()
                        {
                            progressBar.Value = 100;
                        });
                    }

                    Utils.GetFile(Pointer, AttachList[Pointer].Name, AttachList[Pointer].Index,
                                  RecordKey, CurrentAccount, CurrentConfiguration, SenderAddress, ServerName, ServerPort,
                                  EncryptKey, EncryptKey2, UserAgent, out path, out hash);

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

        void tmr_Tick(object sender, EventArgs e)
        {
            const string SOURCE = CLASS_NAME + "tmr_Tick";
            try
            {
                if ((progressBar.Value + intProgressIncrement) > 100)
                {
                    progressBar.Value = 100;
                    tmr.Enabled = false;
                    return;
                }

                progressBar.Value += intProgressIncrement;
            }
            catch (Exception ex)
            {
                Logger.Error(SOURCE, ex.ToString());
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
    }
}
