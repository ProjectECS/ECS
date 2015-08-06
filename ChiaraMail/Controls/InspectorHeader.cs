using System.Drawing;
using System.Windows.Forms;
using ChiaraMail.Properties;

namespace ChiaraMail.Controls
{
    public partial class InspectorHeader : UserControl
    {
        public InspectorHeader()
        {
            InitializeComponent();
        }

        public void LoadMessage(string subject, string sender, string  sentDate, string toRecip, string ccRecip)
        {
            //set values
            lblSender.Text = sender;
            lblDate.Text = sentDate;
            lblToRecip.Text = toRecip;
            lblCcRecip.Text = ccRecip;
            lblSubjectField.Text = subject;
            //reset color
            lblFrom.ForeColor = SystemColors.ControlText;
            lblTo.ForeColor = SystemColors.ControlText;
            //set captions
            lblFrom.Text = Resources.from_label;
            lblTo.Text = Resources.to_label;
            lblCc.Text = Resources.cc_label;
            lblSubject.Text = Resources.subject_label;
            //set visibility
            lblDate.Visible = true;
            lblSent.Visible = true;
            lblFrom.Visible = true;
            lblSender.Visible = true;
            lblSubject.Visible = true;
            lblSubjectField.Visible = true;
            lblTo.Visible = true;
            lblToRecip.Visible = true;
            lblCc.Visible = !string.IsNullOrEmpty(ccRecip);
            lblCcRecip.Visible = lblCc.Visible;
            tableLayoutPanelMain.ColumnStyles[0].SizeType = SizeType.Absolute;
            tableLayoutPanelMain.ColumnStyles[0].Width = 80;
        }

        public void LoadAttachment(string name, string size)
        {
            //set captions + values
            const string FORMAT = "{0}    {1}";
            lblFrom.Text = string.Format(FORMAT, Resources.file_name_label, name);
            lblTo.Text = string.IsNullOrEmpty(size)
                             ? string.Empty
                             : string.Format(FORMAT, Resources.size_label, size);
            //font color
            if (ThisAddIn.AppVersion > 14)
            {
                lblFrom.ForeColor = SystemColors.ControlDarkDark;
                lblTo.ForeColor = SystemColors.ControlDarkDark;
            }
            else
            {
                lblFrom.ForeColor = SystemColors.ControlText;
                lblTo.ForeColor = SystemColors.ControlText;
            }
            tableLayoutPanelMain.ColumnStyles[0].SizeType = SizeType.AutoSize;
            //set visibility
            lblFrom.Visible = true;
            lblTo.Visible = true;
            lblCc.Visible = false;
            lblSender.Visible = false;
            lblToRecip.Visible = false;
            lblCcRecip.Visible = false;
            lblSubject.Visible = false;
            lblSubjectField.Visible = false;
            lblSender.Visible = false;
            lblSubjectField.Visible = false;
            lblDate.Visible = false;
            lblSent.Visible = false;            
        }
    }
}
