using System;
using System.Drawing;
using System.Windows.Forms;
using ChiaraMail.Properties;

namespace ChiaraMail.Controls
{
    public partial class MessageHeader2013 : UserControl
    {
        public event EventHandler Reply = delegate { };
        public event EventHandler ReplyAll = delegate { };
        public event EventHandler Forward = delegate { };

        public MessageHeader2013()
        {
            InitializeComponent();
            //set captions
            lblTo.Text = Resources.to_label;
            lblCc.Text = Resources.cc_label;
            //set colors
            lblTo.ForeColor = Color.DarkGray;
            lblCc.ForeColor = Color.DarkGray;
            
            btnReply.Click+=BtnReplyClick;
            btnReplyAll.Click += BtnReplyAllClick;
            btnForward.Click += BtnForwardClick;
        }

        public void LoadMessage(string subject, string sender, string date, string toRecip,
            string ccRecip, bool showButtons)
        {
            for (var i = 1; i < 5; i++)
            {
                tableLayoutPanel1.RowStyles[i].SizeType = SizeType.AutoSize;
            }
            panelButtons.Visible = showButtons;
            lblSubjectField.Text = subject;
            lblSender.Text = sender;
            lblDate.Text = date;
            lblToRecip.Text = toRecip;
            lblCcRecip.Text = ccRecip;
            //set captions
            lblTo.Text = Resources.to_label.Replace(":","");
            lblCc.Text = Resources.cc_label.Replace(":", "");
            //set visibility
            pictureBox1.Visible = true;
            lblDate.Visible = true;
            lblSubjectField.Visible = true;
            //force long subject to wrap
            //lblSubjectField.MaximumSize = new Size((int)tableLayoutPanel1.ColumnStyles[3].Width-2,0);
            lblSender.Visible = true;
            lblTo.Visible = true;
            lblToRecip.Visible = true;
            lblCc.Visible = !string.IsNullOrEmpty(ccRecip);
            lblCcRecip.Visible = lblCc.Visible;
            lblAttachName.Visible = false;
            
        }

        public void LoadAttachment(string name, string size)
        {
            panelButtons.Visible = false;
            pictureBox1.Visible = false;
            lblDate.Visible = false;
            lblSender.Visible = false;
            lblSubjectField.Visible = false;
            lblCc.Visible = false;
            lblCcRecip.Visible = false;
            lblAttachName.Visible = true;
            lblTo.Text = Resources.size_label;
            lblAttachName.Text = name;
            lblToRecip.Text = size;
            for (var i= 1;i<5;i++)
            {
                tableLayoutPanel1.RowStyles[i].SizeType= SizeType.Absolute;
                tableLayoutPanel1.RowStyles[i].Height = 0;
            }
        }
        private void BtnReplyClick(object sender, EventArgs e)
        {
            Logger.Info("BtnReplyClick","fired");
            Reply(this, e);
        }

        private void BtnReplyAllClick(object sender, EventArgs e)
        {
            ReplyAll(this, e);
        }

        private void BtnForwardClick(object sender, EventArgs e)
        {
            Forward(this, e);
        }

    }
}
