using System;
using System.Drawing;
using System.Windows.Forms;
using ChiaraMail.Properties;

namespace ChiaraMail.Controls
{
    public partial class AttachPanel : UserControl
    {
        private Image _picture;
        private string _caption;
        private bool _selected;

        public event EventHandler PanelDblClick = delegate { };
        public event EventHandler PanelClick = delegate { }; 
        public AttachPanel()
        {
            InitializeComponent();
            SetStyle(ControlStyles.ContainerControl,true);
            SetStyle(ControlStyles.StandardClick,true);
            SetStyle(ControlStyles.StandardDoubleClick, true);
            if (ThisAddIn.AppVersion > 14)
                pic.Image = Resources.Envelope2013;
            pic.Click += ControlClick;
            pic.DoubleClick += ControlDoubleClick;
            label1.Click += ControlClick;
            label1.DoubleClick+=ControlDoubleClick;
            Click += ControlClick;
            DoubleClick += ControlDoubleClick;
            Paint += AttachPanelPaint;
           
        }

        private void AttachPanelPaint(object sender, PaintEventArgs e)
        {
            try
            {
                if (_selected)
                {
                    if (ThisAddIn.NoPreviewer)
                    {
                        //just highlight the caption
                        label1.BackColor = SystemColors.GradientActiveCaption;
                    }
                    else
                    {
                        BackColor = SystemColors.GradientActiveCaption;
                        ControlPaint.DrawBorder(
                            e.Graphics, DisplayRectangle,
                            Color.Black,
                            ButtonBorderStyle.Dotted);
                    }
                }
                else
                {
                    BackColor = Color.Transparent;
                    label1.BackColor = Color.Transparent;
                    ControlPaint.DrawBorder(e.Graphics, DisplayRectangle,
                        BackColor, 
                        ButtonBorderStyle.None);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("AttachPanel.Paint", ex.ToString());
            }
        }

        private void ControlDoubleClick(object sender, EventArgs e)
        {
            PanelDblClick(this, e);            
        }

        private void ControlClick(object sender, EventArgs e)
        {
            Logger.Info("ControlClick","");
            PanelClick(this, e);
        }

        public Image Picture
        {
            get { return _picture; }
            set
            {
                if (_picture != value)
                {
                    pic.Image = value;
                    _picture = value;
                }
            }
        }

        public string Caption
        {
            get { return _caption; }
            set
            {
                if (_caption != value)
                {
                    label1.Text = value;
                    _caption = value;
                }
            }
        }

        public string Pointer { get; set; }

        public bool Selected
        {
            get { return _selected; }
            set
            {
                if (_selected != value)
                {
                    _selected = value;
                    label1.Invalidate();
                    Invalidate();
                }
            }
        }

        public void HideImage(Color foreColor)
        {
            Controls.Remove(pic);
            label1.Left = 0;
            label1.ForeColor = foreColor;
        }
    }
}
