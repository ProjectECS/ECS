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
                    HandleNextAttach();
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

        private void SetCaption(int index)
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
    }
}
