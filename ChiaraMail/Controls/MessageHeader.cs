using System.Drawing;
using System.Windows.Forms;
using ChiaraMail.Properties;

namespace ChiaraMail.Controls
{
    public partial class MessageHeader : UserControl
    {
      
        public MessageHeader()
        {
            InitializeComponent();
            if (ThisAddIn.AppVersion < 14)
            {
                //labels are blue
                lblSent.ForeColor = Color.CornflowerBlue;
                lblTo.ForeColor = Color.CornflowerBlue;
                lblCc.ForeColor = Color.CornflowerBlue;
            }
            else
            {
                //labels are gray
                lblSent.ForeColor = Color.DarkGray;
                lblTo.ForeColor = Color.DarkGray;
                lblCc.ForeColor = Color.DarkGray;
            }
        }

        public void LoadMessage(string subject, string sender, string date, string toRecip, 
            string ccRecip)
        {
            lblSubjectField.Text = subject;
            lblSender.Text = sender;
            lblDate.Text = date;
            lblToRecip.Text = toRecip;
            lblCcRecip.Text = ccRecip;
            //set captions
            lblSent.Text = Resources.sent_label;
            lblTo.Text = Resources.to_label;
            lblCc.Text = Resources.cc_label;
            //set visibility
            lblSent.Visible = true;
            lblDate.Visible = true;
            lblTo.Visible = true;
            lblToRecip.Visible = true;
            lblCc.Visible = !string.IsNullOrEmpty(ccRecip);
            lblCcRecip.Visible = lblCc.Visible;
            lblSubjectField.Visible = true;
        }

        public void LoadAttachment(string name, string size)
        {
            //set captions + values
            lblSender.Text = name;
            if (string.IsNullOrEmpty(size))
            {
                lblSent.Visible = false;                
            }
            else
            {
                lblSent.Visible = true;
                lblSent.Text = Resources.size_label;
                lblDate.Text = "   " + size;
            }
            //set visibility
            lblDate.Visible = lblSent.Visible;
            lblTo.Visible = false;
            lblToRecip.Visible = false;
            lblCc.Visible = false;
            lblCcRecip.Visible = false;
            lblSubjectField.Visible = false;
        }
    }
}
