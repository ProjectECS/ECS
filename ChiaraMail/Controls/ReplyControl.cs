using System;
using System.Drawing;
using System.Windows.Forms;

namespace ChiaraMail.Controls
{
    public partial class ReplyControl : UserControl
    {
        private Image _picture;
        private string _caption;

        public ReplyControl()
        {
            InitializeComponent();
            SetStyle(ControlStyles.ContainerControl,true);
            SetStyle(ControlStyles.StandardClick,true);
            SetStyle(ControlStyles.StandardDoubleClick, true);
            pic.Click += ControlClick;
            pic.DoubleClick += ControlClick;
            label1.Click += ControlClick;
            label1.DoubleClick+=ControlClick;
            DoubleClick += ControlClick;
            MouseEnter += HighlightBackground;
            MouseHover += HighlightBackground;
            pic.MouseEnter += HighlightBackground;
            pic.MouseHover += HighlightBackground;
            label1.MouseEnter += HighlightBackground;
            label1.MouseHover += HighlightBackground;
            MouseLeave += TransparentBackground;
            pic.MouseLeave += TransparentBackground;
            label1.MouseLeave += TransparentBackground;
        }

        void TransparentBackground(object sender, EventArgs e)
        {
            BackColor = Color.Transparent;
        }

        void HighlightBackground(object sender, EventArgs e)
        {
            BackColor = SystemColors.InactiveCaption;
        }

        private void ControlClick(object sender, EventArgs e)
        {
            Logger.Info("ReplyControl","raising OnClick");
            OnClick(e);
        }

        public Image Picture
        {
            get { return _picture; }
            set
            {
                if (_picture == value) return;
                pic.Image = value;
                _picture = value;
            }
        }

        public string Caption
        {
            get { return _caption; }
            set
            {
                if (_caption == value) return;
                label1.Text = value;
                _caption = value;
            }
        }
    }
}
