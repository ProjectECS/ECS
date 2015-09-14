using ChiaraMail.Controls;
using ChiaraMail.FormRegions;

namespace ChiaraMail.FormRegions
{
    [System.ComponentModel.ToolboxItemAttribute(false)]
    partial class DynamicReadingPane : Microsoft.Office.Tools.Outlook.FormRegionBase
    {
        public DynamicReadingPane(Microsoft.Office.Interop.Outlook.FormRegion formRegion)
            : base(Globals.Factory, formRegion)
        {
            Logger.Verbose("DynamicReadingPane", "InitializeComponent");
            this.InitializeComponent();
        }

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DynamicReadingPane));
            SpiceLogic.HtmlEditorControl.Domain.DesignTime.DictionaryFileInfo dictionaryFileInfo2 = new SpiceLogic.HtmlEditorControl.Domain.DesignTime.DictionaryFileInfo();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.previewHandlerControl = new ChiaraMail.PreviewHandlerControl();
            this.embeddedMsg1 = new ChiaraMail.Controls.EmbeddedMsg();
            this.tableLayoutAttach = new System.Windows.Forms.TableLayoutPanel();
            this.btnMessage = new ChiaraMail.Controls.AttachPanel();
            this.panelAttach = new System.Windows.Forms.Panel();
            this.panelVertLine = new System.Windows.Forms.Panel();
            this.messageHdr14 = new ChiaraMail.Controls.MessageHeader();
            this.messageHdr15 = new ChiaraMail.Controls.MessageHeader2013();
            this.mnuAttach = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.previewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.useDefaultApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.browseForEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.winFormHtmlEditor1 = new SpiceLogic.WinHTMLEditor.WinForm.WinFormHtmlEditor();
            this.tableLayoutPanelMain.SuspendLayout();
            this.tableLayoutAttach.SuspendLayout();
            this.panelAttach.SuspendLayout();
            this.mnuAttach.SuspendLayout();
            this.winFormHtmlEditor1.Toolbar1.SuspendLayout();
            this.winFormHtmlEditor1.Toolbar2.SuspendLayout();
            this.winFormHtmlEditor1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 4;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 68F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 68F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelMain.Controls.Add(this.winFormHtmlEditor1, 0, 4);
            this.tableLayoutPanelMain.Controls.Add(this.btnEdit, 1, 0);
            this.tableLayoutPanelMain.Controls.Add(this.btnDelete, 2, 0);
            this.tableLayoutPanelMain.Controls.Add(this.previewHandlerControl, 0, 5);
            this.tableLayoutPanelMain.Controls.Add(this.embeddedMsg1, 0, 6);
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutAttach, 0, 3);
            this.tableLayoutPanelMain.Controls.Add(this.messageHdr14, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.messageHdr15, 0, 2);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 7;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 2F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(737, 428);
            this.tableLayoutPanelMain.TabIndex = 0;
            this.tableLayoutPanelMain.Paint += new System.Windows.Forms.PaintEventHandler(this.TableLayoutPanel1Paint);
            // 
            // btnEdit
            // 
            this.btnEdit.AutoSize = true;
            this.btnEdit.Enabled = false;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Image = global::ChiaraMail.Properties.Resources.edit_content;
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnEdit.Location = new System.Drawing.Point(599, 3);
            this.btnEdit.Name = "btnEdit";
            this.tableLayoutPanelMain.SetRowSpan(this.btnEdit, 3);
            this.btnEdit.Size = new System.Drawing.Size(62, 80);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.Text = "Update Content";
            this.btnEdit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.BtnEditClick);
            // 
            // btnDelete
            // 
            this.btnDelete.AutoSize = true;
            this.btnDelete.Enabled = false;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Image = global::ChiaraMail.Properties.Resources.delete_content;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnDelete.Location = new System.Drawing.Point(667, 3);
            this.btnDelete.Name = "btnDelete";
            this.tableLayoutPanelMain.SetRowSpan(this.btnDelete, 3);
            this.btnDelete.Size = new System.Drawing.Size(62, 80);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Delete Content";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.BtnDeleteClick);
            // 
            // previewHandlerControl
            // 
            this.previewHandlerControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.previewHandlerControl.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanelMain.SetColumnSpan(this.previewHandlerControl, 3);
            this.previewHandlerControl.Location = new System.Drawing.Point(3, 416);
            this.previewHandlerControl.Name = "previewHandlerControl";
            this.previewHandlerControl.Size = new System.Drawing.Size(726, 1);
            this.previewHandlerControl.TabIndex = 19;
            this.previewHandlerControl.Visible = false;
            // 
            // embeddedMsg1
            // 
            this.embeddedMsg1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.embeddedMsg1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanelMain.SetColumnSpan(this.embeddedMsg1, 3);
            this.embeddedMsg1.Location = new System.Drawing.Point(3, 423);
            this.embeddedMsg1.Name = "embeddedMsg1";
            this.embeddedMsg1.Size = new System.Drawing.Size(726, 2);
            this.embeddedMsg1.TabIndex = 20;
            this.embeddedMsg1.Visible = false;
            // 
            // tableLayoutAttach
            // 
            this.tableLayoutAttach.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutAttach.ColumnCount = 2;
            this.tableLayoutPanelMain.SetColumnSpan(this.tableLayoutAttach, 3);
            this.tableLayoutAttach.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutAttach.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutAttach.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutAttach.Controls.Add(this.btnMessage, 0, 0);
            this.tableLayoutAttach.Controls.Add(this.panelAttach, 1, 0);
            this.tableLayoutAttach.Location = new System.Drawing.Point(2, 217);
            this.tableLayoutAttach.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutAttach.Name = "tableLayoutAttach";
            this.tableLayoutAttach.RowCount = 2;
            this.tableLayoutAttach.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutAttach.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutAttach.Size = new System.Drawing.Size(728, 22);
            this.tableLayoutAttach.TabIndex = 17;
            // 
            // btnMessage
            // 
            this.btnMessage.AutoSize = true;
            this.btnMessage.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnMessage.BackColor = System.Drawing.Color.Transparent;
            this.btnMessage.Caption = null;
            this.btnMessage.Location = new System.Drawing.Point(0, 0);
            this.btnMessage.Margin = new System.Windows.Forms.Padding(0);
            this.btnMessage.Name = "btnMessage";
            this.btnMessage.Picture = null;
            this.btnMessage.Pointer = null;
            this.btnMessage.Selected = false;
            this.btnMessage.Size = new System.Drawing.Size(75, 20);
            this.btnMessage.TabIndex = 0;
            // 
            // panelAttach
            // 
            this.panelAttach.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelAttach.AutoScroll = true;
            this.panelAttach.Controls.Add(this.panelVertLine);
            this.panelAttach.Location = new System.Drawing.Point(75, 0);
            this.panelAttach.Margin = new System.Windows.Forms.Padding(0);
            this.panelAttach.Name = "panelAttach";
            this.panelAttach.Size = new System.Drawing.Size(653, 20);
            this.panelAttach.TabIndex = 1;
            // 
            // panelVertLine
            // 
            this.panelVertLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelVertLine.BackColor = System.Drawing.Color.DarkGray;
            this.panelVertLine.Location = new System.Drawing.Point(0, 0);
            this.panelVertLine.Margin = new System.Windows.Forms.Padding(0);
            this.panelVertLine.Name = "panelVertLine";
            this.panelVertLine.Size = new System.Drawing.Size(1, 20);
            this.panelVertLine.TabIndex = 0;
            // 
            // messageHdr14
            // 
            this.messageHdr14.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messageHdr14.AutoSize = true;
            this.messageHdr14.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.messageHdr14.Location = new System.Drawing.Point(3, 5);
            this.messageHdr14.MinimumSize = new System.Drawing.Size(400, 60);
            this.messageHdr14.Name = "messageHdr14";
            this.messageHdr14.Size = new System.Drawing.Size(590, 81);
            this.messageHdr14.TabIndex = 21;
            // 
            // messageHdr15
            // 
            this.messageHdr15.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messageHdr15.AutoSize = true;
            this.messageHdr15.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.messageHdr15.Location = new System.Drawing.Point(3, 92);
            this.messageHdr15.MinimumSize = new System.Drawing.Size(400, 0);
            this.messageHdr15.Name = "messageHdr15";
            this.messageHdr15.Size = new System.Drawing.Size(590, 120);
            this.messageHdr15.TabIndex = 22;
            // 
            // mnuAttach
            // 
            this.mnuAttach.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.previewToolStripMenuItem,
            this.mnuSep1,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.mnuAttach.Name = "mnuAttach";
            this.mnuAttach.Size = new System.Drawing.Size(116, 76);
            // 
            // previewToolStripMenuItem
            // 
            this.previewToolStripMenuItem.Name = "previewToolStripMenuItem";
            this.previewToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.previewToolStripMenuItem.Text = "Preview";
            this.previewToolStripMenuItem.Click += new System.EventHandler(this.PreviewToolStripMenuItemClick);
            // 
            // mnuSep1
            // 
            this.mnuSep1.Name = "mnuSep1";
            this.mnuSep1.Size = new System.Drawing.Size(112, 6);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.useDefaultApplicationToolStripMenuItem,
            this.browseForEditorToolStripMenuItem});
            this.openToolStripMenuItem.Image = global::ChiaraMail.Properties.Resources.Open;
            this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItemClick);
            // 
            // useDefaultApplicationToolStripMenuItem
            // 
            this.useDefaultApplicationToolStripMenuItem.Name = "useDefaultApplicationToolStripMenuItem";
            this.useDefaultApplicationToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.useDefaultApplicationToolStripMenuItem.Text = "Use default application";
            this.useDefaultApplicationToolStripMenuItem.Click += new System.EventHandler(this.UseDefaultApplicationToolStripMenuItemClick);
            // 
            // browseForEditorToolStripMenuItem
            // 
            this.browseForEditorToolStripMenuItem.Name = "browseForEditorToolStripMenuItem";
            this.browseForEditorToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.browseForEditorToolStripMenuItem.Text = "Browse for editor...";
            this.browseForEditorToolStripMenuItem.Click += new System.EventHandler(this.BrowseForEditorToolStripMenuItemClick);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = global::ChiaraMail.Properties.Resources.SaveAs;
            this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.saveToolStripMenuItem.Text = "Save As";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItemClick);
            // 
            // winFormHtmlEditor1
            // 
            this.winFormHtmlEditor1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.winFormHtmlEditor1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.winFormHtmlEditor1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.winFormHtmlEditor1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            // 
            // winFormHtmlEditor1.BtnAlignCenter
            // 
            this.winFormHtmlEditor1.BtnAlignCenter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnAlignCenter.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnAlignCenter.Image")));
            this.winFormHtmlEditor1.BtnAlignCenter.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnAlignCenter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnAlignCenter.Name = "_factoryBtnAlignCenter";
            this.winFormHtmlEditor1.BtnAlignCenter.Size = new System.Drawing.Size(26, 26);
            this.winFormHtmlEditor1.BtnAlignCenter.Text = "Align Centre";
            // 
            // winFormHtmlEditor1.BtnAlignLeft
            // 
            this.winFormHtmlEditor1.BtnAlignLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnAlignLeft.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnAlignLeft.Image")));
            this.winFormHtmlEditor1.BtnAlignLeft.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnAlignLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnAlignLeft.Name = "_factoryBtnAlignLeft";
            this.winFormHtmlEditor1.BtnAlignLeft.Size = new System.Drawing.Size(26, 26);
            this.winFormHtmlEditor1.BtnAlignLeft.Text = "Align Left";
            // 
            // winFormHtmlEditor1.BtnAlignRight
            // 
            this.winFormHtmlEditor1.BtnAlignRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnAlignRight.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnAlignRight.Image")));
            this.winFormHtmlEditor1.BtnAlignRight.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnAlignRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnAlignRight.Name = "_factoryBtnAlignRight";
            this.winFormHtmlEditor1.BtnAlignRight.Size = new System.Drawing.Size(26, 26);
            this.winFormHtmlEditor1.BtnAlignRight.Text = "Align Right";
            // 
            // winFormHtmlEditor1.BtnBodyStyle
            // 
            this.winFormHtmlEditor1.BtnBodyStyle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnBodyStyle.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnBodyStyle.Image")));
            this.winFormHtmlEditor1.BtnBodyStyle.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnBodyStyle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnBodyStyle.Name = "_factoryBtnBodyStyle";
            this.winFormHtmlEditor1.BtnBodyStyle.Size = new System.Drawing.Size(27, 26);
            this.winFormHtmlEditor1.BtnBodyStyle.Text = "Document Style ";
            // 
            // winFormHtmlEditor1.BtnBold
            // 
            this.winFormHtmlEditor1.BtnBold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnBold.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnBold.Image")));
            this.winFormHtmlEditor1.BtnBold.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnBold.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnBold.Name = "_factoryBtnBold";
            this.winFormHtmlEditor1.BtnBold.Size = new System.Drawing.Size(23, 26);
            this.winFormHtmlEditor1.BtnBold.Text = "Bold";
            // 
            // winFormHtmlEditor1.BtnCopy
            // 
            this.winFormHtmlEditor1.BtnCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnCopy.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnCopy.Image")));
            this.winFormHtmlEditor1.BtnCopy.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnCopy.Name = "_factoryBtnCopy";
            this.winFormHtmlEditor1.BtnCopy.Size = new System.Drawing.Size(23, 26);
            this.winFormHtmlEditor1.BtnCopy.Text = "Copy";
            // 
            // winFormHtmlEditor1.BtnCut
            // 
            this.winFormHtmlEditor1.BtnCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnCut.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnCut.Image")));
            this.winFormHtmlEditor1.BtnCut.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnCut.Name = "_factoryBtnCut";
            this.winFormHtmlEditor1.BtnCut.Size = new System.Drawing.Size(23, 26);
            this.winFormHtmlEditor1.BtnCut.Text = "Cut";
            // 
            // winFormHtmlEditor1.BtnFontColor
            // 
            this.winFormHtmlEditor1.BtnFontColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnFontColor.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnFontColor.Image")));
            this.winFormHtmlEditor1.BtnFontColor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnFontColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnFontColor.Name = "_factoryBtnFontColor";
            this.winFormHtmlEditor1.BtnFontColor.Size = new System.Drawing.Size(23, 26);
            this.winFormHtmlEditor1.BtnFontColor.Text = "Apply Font Color";
            // 
            // winFormHtmlEditor1.BtnFormatRedo
            // 
            this.winFormHtmlEditor1.BtnFormatRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnFormatRedo.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnFormatRedo.Image")));
            this.winFormHtmlEditor1.BtnFormatRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnFormatRedo.Name = "_factoryBtnRedo";
            this.winFormHtmlEditor1.BtnFormatRedo.Size = new System.Drawing.Size(23, 26);
            this.winFormHtmlEditor1.BtnFormatRedo.Text = "Redo";
            // 
            // winFormHtmlEditor1.BtnFormatReset
            // 
            this.winFormHtmlEditor1.BtnFormatReset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnFormatReset.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnFormatReset.Image")));
            this.winFormHtmlEditor1.BtnFormatReset.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnFormatReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnFormatReset.Name = "_factoryBtnFormatReset";
            this.winFormHtmlEditor1.BtnFormatReset.Size = new System.Drawing.Size(34, 26);
            this.winFormHtmlEditor1.BtnFormatReset.Text = "Remove Format";
            // 
            // winFormHtmlEditor1.BtnFormatUndo
            // 
            this.winFormHtmlEditor1.BtnFormatUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnFormatUndo.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnFormatUndo.Image")));
            this.winFormHtmlEditor1.BtnFormatUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnFormatUndo.Name = "_factoryBtnUndo";
            this.winFormHtmlEditor1.BtnFormatUndo.Size = new System.Drawing.Size(23, 26);
            this.winFormHtmlEditor1.BtnFormatUndo.Text = "Undo";
            // 
            // winFormHtmlEditor1.BtnHighlightColor
            // 
            this.winFormHtmlEditor1.BtnHighlightColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnHighlightColor.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnHighlightColor.Image")));
            this.winFormHtmlEditor1.BtnHighlightColor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnHighlightColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnHighlightColor.Name = "_factoryBtnHighlightColor";
            this.winFormHtmlEditor1.BtnHighlightColor.Size = new System.Drawing.Size(27, 26);
            this.winFormHtmlEditor1.BtnHighlightColor.Text = "Apply Highlight Color";
            // 
            // winFormHtmlEditor1.BtnHorizontalRule
            // 
            this.winFormHtmlEditor1.BtnHorizontalRule.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnHorizontalRule.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnHorizontalRule.Image")));
            this.winFormHtmlEditor1.BtnHorizontalRule.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnHorizontalRule.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnHorizontalRule.Name = "_factoryBtnHorizontalRule";
            this.winFormHtmlEditor1.BtnHorizontalRule.Size = new System.Drawing.Size(24, 26);
            this.winFormHtmlEditor1.BtnHorizontalRule.Text = "Insert Horizontal Rule";
            // 
            // winFormHtmlEditor1.BtnHyperlink
            // 
            this.winFormHtmlEditor1.BtnHyperlink.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnHyperlink.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnHyperlink.Image")));
            this.winFormHtmlEditor1.BtnHyperlink.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnHyperlink.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnHyperlink.Name = "_factoryBtnHyperlink";
            this.winFormHtmlEditor1.BtnHyperlink.Size = new System.Drawing.Size(23, 26);
            this.winFormHtmlEditor1.BtnHyperlink.Text = "Hyperlink";
            // 
            // winFormHtmlEditor1.BtnImage
            // 
            this.winFormHtmlEditor1.BtnImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnImage.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnImage.Image")));
            this.winFormHtmlEditor1.BtnImage.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnImage.Name = "_factoryBtnImage";
            this.winFormHtmlEditor1.BtnImage.Size = new System.Drawing.Size(23, 26);
            this.winFormHtmlEditor1.BtnImage.Text = "Image";
            // 
            // winFormHtmlEditor1.BtnIndent
            // 
            this.winFormHtmlEditor1.BtnIndent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnIndent.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnIndent.Image")));
            this.winFormHtmlEditor1.BtnIndent.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnIndent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnIndent.Name = "_factoryBtnIndent";
            this.winFormHtmlEditor1.BtnIndent.Size = new System.Drawing.Size(27, 26);
            this.winFormHtmlEditor1.BtnIndent.Text = "Indent";
            // 
            // winFormHtmlEditor1.BtnInsertYouTubeVideo
            // 
            this.winFormHtmlEditor1.BtnInsertYouTubeVideo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnInsertYouTubeVideo.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnInsertYouTubeVideo.Image")));
            this.winFormHtmlEditor1.BtnInsertYouTubeVideo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnInsertYouTubeVideo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnInsertYouTubeVideo.Name = "_factoryBtnInsertYouTubeVideo";
            this.winFormHtmlEditor1.BtnInsertYouTubeVideo.Size = new System.Drawing.Size(23, 26);
            this.winFormHtmlEditor1.BtnInsertYouTubeVideo.Text = "Insert YouTube Video";
            // 
            // winFormHtmlEditor1.BtnItalic
            // 
            this.winFormHtmlEditor1.BtnItalic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnItalic.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnItalic.Image")));
            this.winFormHtmlEditor1.BtnItalic.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnItalic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnItalic.Name = "_factoryBtnItalic";
            this.winFormHtmlEditor1.BtnItalic.Size = new System.Drawing.Size(23, 26);
            this.winFormHtmlEditor1.BtnItalic.Text = "Italic";
            // 
            // winFormHtmlEditor1.BtnNew
            // 
            this.winFormHtmlEditor1.BtnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnNew.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnNew.Image")));
            this.winFormHtmlEditor1.BtnNew.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnNew.Name = "_factoryBtnNew";
            this.winFormHtmlEditor1.BtnNew.Size = new System.Drawing.Size(23, 26);
            this.winFormHtmlEditor1.BtnNew.Text = "New";
            // 
            // winFormHtmlEditor1.BtnOpen
            // 
            this.winFormHtmlEditor1.BtnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnOpen.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnOpen.Image")));
            this.winFormHtmlEditor1.BtnOpen.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnOpen.Name = "_factoryBtnOpen";
            this.winFormHtmlEditor1.BtnOpen.Size = new System.Drawing.Size(23, 26);
            this.winFormHtmlEditor1.BtnOpen.Text = "Open";
            // 
            // winFormHtmlEditor1.BtnOrderedList
            // 
            this.winFormHtmlEditor1.BtnOrderedList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnOrderedList.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnOrderedList.Image")));
            this.winFormHtmlEditor1.BtnOrderedList.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnOrderedList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnOrderedList.Name = "_factoryBtnOrderedList";
            this.winFormHtmlEditor1.BtnOrderedList.Size = new System.Drawing.Size(24, 26);
            this.winFormHtmlEditor1.BtnOrderedList.Text = "Numbered List";
            // 
            // winFormHtmlEditor1.BtnOutdent
            // 
            this.winFormHtmlEditor1.BtnOutdent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnOutdent.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnOutdent.Image")));
            this.winFormHtmlEditor1.BtnOutdent.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnOutdent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnOutdent.Name = "_factoryBtnOutdent";
            this.winFormHtmlEditor1.BtnOutdent.Size = new System.Drawing.Size(27, 26);
            this.winFormHtmlEditor1.BtnOutdent.Text = "Outdent";
            // 
            // winFormHtmlEditor1.BtnPaste
            // 
            this.winFormHtmlEditor1.BtnPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnPaste.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnPaste.Image")));
            this.winFormHtmlEditor1.BtnPaste.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnPaste.Name = "_factoryBtnPaste";
            this.winFormHtmlEditor1.BtnPaste.Size = new System.Drawing.Size(23, 26);
            this.winFormHtmlEditor1.BtnPaste.Text = "Paste";
            // 
            // winFormHtmlEditor1.BtnPasteFromMSWord
            // 
            this.winFormHtmlEditor1.BtnPasteFromMSWord.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnPasteFromMSWord.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnPasteFromMSWord.Image")));
            this.winFormHtmlEditor1.BtnPasteFromMSWord.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnPasteFromMSWord.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnPasteFromMSWord.Name = "_factoryBtnPasteFromMSWord";
            this.winFormHtmlEditor1.BtnPasteFromMSWord.Size = new System.Drawing.Size(23, 26);
            this.winFormHtmlEditor1.BtnPasteFromMSWord.Text = "Paste the Content that you Copied from MS Word";
            // 
            // winFormHtmlEditor1.BtnPrint
            // 
            this.winFormHtmlEditor1.BtnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnPrint.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnPrint.Image")));
            this.winFormHtmlEditor1.BtnPrint.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnPrint.Name = "_factoryBtnPrint";
            this.winFormHtmlEditor1.BtnPrint.Size = new System.Drawing.Size(23, 26);
            this.winFormHtmlEditor1.BtnPrint.Text = "Print";
            // 
            // winFormHtmlEditor1.BtnSave
            // 
            this.winFormHtmlEditor1.BtnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnSave.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnSave.Image")));
            this.winFormHtmlEditor1.BtnSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnSave.Name = "_factoryBtnSave";
            this.winFormHtmlEditor1.BtnSave.Size = new System.Drawing.Size(23, 26);
            this.winFormHtmlEditor1.BtnSave.Text = "Save";
            // 
            // winFormHtmlEditor1.BtnSearch
            // 
            this.winFormHtmlEditor1.BtnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnSearch.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnSearch.Image")));
            this.winFormHtmlEditor1.BtnSearch.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnSearch.Name = "_factoryBtnSearch";
            this.winFormHtmlEditor1.BtnSearch.Size = new System.Drawing.Size(24, 26);
            this.winFormHtmlEditor1.BtnSearch.Text = "Search";
            // 
            // winFormHtmlEditor1.BtnSpellCheck
            // 
            this.winFormHtmlEditor1.BtnSpellCheck.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnSpellCheck.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnSpellCheck.Image")));
            this.winFormHtmlEditor1.BtnSpellCheck.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnSpellCheck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnSpellCheck.Name = "_factoryBtnSpellCheck";
            this.winFormHtmlEditor1.BtnSpellCheck.Size = new System.Drawing.Size(26, 26);
            this.winFormHtmlEditor1.BtnSpellCheck.Text = "Check Spelling";
            // 
            // winFormHtmlEditor1.BtnStrikeThrough
            // 
            this.winFormHtmlEditor1.BtnStrikeThrough.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnStrikeThrough.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnStrikeThrough.Image")));
            this.winFormHtmlEditor1.BtnStrikeThrough.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnStrikeThrough.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnStrikeThrough.Name = "_factoryBtnStrikeThrough";
            this.winFormHtmlEditor1.BtnStrikeThrough.Size = new System.Drawing.Size(24, 26);
            this.winFormHtmlEditor1.BtnStrikeThrough.Text = "Strike Thru";
            // 
            // winFormHtmlEditor1.BtnSubscript
            // 
            this.winFormHtmlEditor1.BtnSubscript.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnSubscript.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnSubscript.Image")));
            this.winFormHtmlEditor1.BtnSubscript.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnSubscript.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnSubscript.Name = "_factoryBtnSubscript";
            this.winFormHtmlEditor1.BtnSubscript.Size = new System.Drawing.Size(27, 26);
            this.winFormHtmlEditor1.BtnSubscript.Text = "Subscript";
            // 
            // winFormHtmlEditor1.BtnSuperScript
            // 
            this.winFormHtmlEditor1.BtnSuperScript.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnSuperScript.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnSuperScript.Image")));
            this.winFormHtmlEditor1.BtnSuperScript.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnSuperScript.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnSuperScript.Name = "_factoryBtnSuperScript";
            this.winFormHtmlEditor1.BtnSuperScript.Size = new System.Drawing.Size(27, 26);
            this.winFormHtmlEditor1.BtnSuperScript.Text = "Superscript";
            // 
            // winFormHtmlEditor1.BtnSymbol
            // 
            this.winFormHtmlEditor1.BtnSymbol.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnSymbol.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnSymbol.Image")));
            this.winFormHtmlEditor1.BtnSymbol.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnSymbol.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnSymbol.Name = "_factoryBtnSymbol";
            this.winFormHtmlEditor1.BtnSymbol.Size = new System.Drawing.Size(23, 26);
            this.winFormHtmlEditor1.BtnSymbol.Text = "Insert Symbols";
            // 
            // winFormHtmlEditor1.BtnTable
            // 
            this.winFormHtmlEditor1.BtnTable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnTable.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnTable.Image")));
            this.winFormHtmlEditor1.BtnTable.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnTable.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnTable.Name = "_factoryBtnTable";
            this.winFormHtmlEditor1.BtnTable.Size = new System.Drawing.Size(24, 26);
            this.winFormHtmlEditor1.BtnTable.Text = "Table";
            // 
            // winFormHtmlEditor1.BtnUnderline
            // 
            this.winFormHtmlEditor1.BtnUnderline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnUnderline.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnUnderline.Image")));
            this.winFormHtmlEditor1.BtnUnderline.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnUnderline.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnUnderline.Name = "_factoryBtnUnderline";
            this.winFormHtmlEditor1.BtnUnderline.Size = new System.Drawing.Size(23, 26);
            this.winFormHtmlEditor1.BtnUnderline.Text = "Underline";
            // 
            // winFormHtmlEditor1.BtnUnOrderedList
            // 
            this.winFormHtmlEditor1.BtnUnOrderedList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnUnOrderedList.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnUnOrderedList.Image")));
            this.winFormHtmlEditor1.BtnUnOrderedList.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnUnOrderedList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnUnOrderedList.Name = "_factoryBtnUnOrderedList";
            this.winFormHtmlEditor1.BtnUnOrderedList.Size = new System.Drawing.Size(24, 26);
            this.winFormHtmlEditor1.BtnUnOrderedList.Text = "Bullet List";
            this.winFormHtmlEditor1.Charset = "utf-8";
            // 
            // winFormHtmlEditor1.CmbFontName
            // 
            this.winFormHtmlEditor1.CmbFontName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.winFormHtmlEditor1.CmbFontName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.winFormHtmlEditor1.CmbFontName.MaxDropDownItems = 17;
            this.winFormHtmlEditor1.CmbFontName.Name = "_factoryCmbFontName";
            this.winFormHtmlEditor1.CmbFontName.Size = new System.Drawing.Size(125, 29);
            this.winFormHtmlEditor1.CmbFontName.Text = "Times New Roman";
            // 
            // winFormHtmlEditor1.CmbFontSize
            // 
            this.winFormHtmlEditor1.CmbFontSize.Name = "_factoryCmbFontSize";
            this.winFormHtmlEditor1.CmbFontSize.Size = new System.Drawing.Size(75, 29);
            this.winFormHtmlEditor1.CmbFontSize.Text = "12pt";
            // 
            // winFormHtmlEditor1.CmbTitleInsert
            // 
            this.winFormHtmlEditor1.CmbTitleInsert.Name = "_factoryCmbTitleInsert";
            this.winFormHtmlEditor1.CmbTitleInsert.Size = new System.Drawing.Size(100, 29);
            this.tableLayoutPanelMain.SetColumnSpan(this.winFormHtmlEditor1, 3);
            this.winFormHtmlEditor1.DocumentHtml = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">\r\n<html><head>\r\n<me" +
    "ta content=\"text/html; charset=utf-8\" http-equiv=\"Content-Type\" />\r\n</head>\r\n<bo" +
    "dy></body></html>";
            this.winFormHtmlEditor1.EditorContextMenuStrip = null;
            this.winFormHtmlEditor1.EditorMode = SpiceLogic.HtmlEditorControl.Domain.BOs.EditorModes.ReadOnly_Preview;
            this.winFormHtmlEditor1.HeaderStyleContentElementID = "page_style";
            this.winFormHtmlEditor1.HorizontalScroll = null;
            this.winFormHtmlEditor1.Location = new System.Drawing.Point(3, 244);
            this.winFormHtmlEditor1.Name = "winFormHtmlEditor1";
            this.winFormHtmlEditor1.Options.ConvertFileUrlsToLocalPaths = true;
            this.winFormHtmlEditor1.Options.CustomDOCTYPE = null;
            this.winFormHtmlEditor1.Options.FooterTagNavigatorFont = null;
            this.winFormHtmlEditor1.Options.FooterTagNavigatorTextColor = System.Drawing.Color.Teal;
            this.winFormHtmlEditor1.Options.FTPSettingsForRemoteResources.ConnectionMode = SpiceLogic.HtmlEditorControl.Domain.BOs.UserOptions.FTPSettings.ConnectionModes.Active;
            this.winFormHtmlEditor1.Options.FTPSettingsForRemoteResources.Host = null;
            this.winFormHtmlEditor1.Options.FTPSettingsForRemoteResources.Password = null;
            this.winFormHtmlEditor1.Options.FTPSettingsForRemoteResources.Port = null;
            this.winFormHtmlEditor1.Options.FTPSettingsForRemoteResources.RemoteFolderPath = null;
            this.winFormHtmlEditor1.Options.FTPSettingsForRemoteResources.Timeout = 4000;
            this.winFormHtmlEditor1.Options.FTPSettingsForRemoteResources.UrlOfTheRemoteFolderPath = null;
            this.winFormHtmlEditor1.Options.FTPSettingsForRemoteResources.UserName = null;
            this.winFormHtmlEditor1.Size = new System.Drawing.Size(726, 166);
            this.winFormHtmlEditor1.SpellCheckOptions.CurlyUnderlineImageFilePath = null;
            dictionaryFileInfo2.AffixFilePath = null;
            dictionaryFileInfo2.DictionaryFilePath = "en-US.dic";
            dictionaryFileInfo2.EnableUserDictionary = true;
            dictionaryFileInfo2.UserDictionaryFilePath = "user.dic";
            this.winFormHtmlEditor1.SpellCheckOptions.DictionaryFile = dictionaryFileInfo2;
            this.winFormHtmlEditor1.SpellCheckOptions.WaitAlertMessage = "Searching next misspelled word..... (please wait)";
            this.winFormHtmlEditor1.TabIndex = 1;
            // 
            // winFormHtmlEditor1.WinFormHtmlEditor_Toolbar1
            // 
            // 
            // winFormHtmlEditor1.ToolStripSeparator1
            // 
            this.winFormHtmlEditor1.ToolStripSeparator1.Name = "_toolStripSeparator1";
            this.winFormHtmlEditor1.ToolStripSeparator1.Size = new System.Drawing.Size(6, 29);
            // 
            // winFormHtmlEditor1.ToolStripSeparator2
            // 
            this.winFormHtmlEditor1.ToolStripSeparator2.Name = "_toolStripSeparator2";
            this.winFormHtmlEditor1.ToolStripSeparator2.Size = new System.Drawing.Size(6, 29);
            // 
            // winFormHtmlEditor1.ToolStripSeparator3
            // 
            this.winFormHtmlEditor1.ToolStripSeparator3.Name = "_toolStripSeparator3";
            this.winFormHtmlEditor1.ToolStripSeparator3.Size = new System.Drawing.Size(6, 29);
            // 
            // winFormHtmlEditor1.ToolStripSeparator4
            // 
            this.winFormHtmlEditor1.ToolStripSeparator4.Name = "_toolStripSeparator4";
            this.winFormHtmlEditor1.ToolStripSeparator4.Size = new System.Drawing.Size(6, 29);
            this.winFormHtmlEditor1.Toolbar1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.winFormHtmlEditor1.BtnNew,
            this.winFormHtmlEditor1.BtnOpen,
            this.winFormHtmlEditor1.BtnSave,
            this.winFormHtmlEditor1.ToolStripSeparator1,
            this.winFormHtmlEditor1.CmbFontName,
            this.winFormHtmlEditor1.CmbFontSize,
            this.winFormHtmlEditor1.ToolStripSeparator2,
            this.winFormHtmlEditor1.BtnCut,
            this.winFormHtmlEditor1.BtnCopy,
            this.winFormHtmlEditor1.BtnPaste,
            this.winFormHtmlEditor1.BtnPasteFromMSWord,
            this.winFormHtmlEditor1.ToolStripSeparator3,
            this.winFormHtmlEditor1.BtnBold,
            this.winFormHtmlEditor1.BtnItalic,
            this.winFormHtmlEditor1.BtnUnderline,
            this.winFormHtmlEditor1.ToolStripSeparator4,
            this.winFormHtmlEditor1.BtnFormatReset,
            this.winFormHtmlEditor1.BtnFormatUndo,
            this.winFormHtmlEditor1.BtnFormatRedo,
            this.winFormHtmlEditor1.BtnPrint,
            this.winFormHtmlEditor1.BtnSpellCheck,
            this.winFormHtmlEditor1.BtnSearch});
            this.winFormHtmlEditor1.Toolbar1.Location = new System.Drawing.Point(0, 0);
            this.winFormHtmlEditor1.Toolbar1.Name = "WinFormHtmlEditor_Toolbar1";
            this.winFormHtmlEditor1.Toolbar1.Size = new System.Drawing.Size(726, 29);
            this.winFormHtmlEditor1.Toolbar1.TabIndex = 0;
            // 
            // winFormHtmlEditor1.WinFormHtmlEditor_Toolbar2
            // 
            // 
            // winFormHtmlEditor1.ToolStripSeparator5
            // 
            this.winFormHtmlEditor1.ToolStripSeparator5.Name = "_toolStripSeparator5";
            this.winFormHtmlEditor1.ToolStripSeparator5.Size = new System.Drawing.Size(6, 29);
            // 
            // winFormHtmlEditor1.ToolStripSeparator6
            // 
            this.winFormHtmlEditor1.ToolStripSeparator6.Name = "_toolStripSeparator6";
            this.winFormHtmlEditor1.ToolStripSeparator6.Size = new System.Drawing.Size(6, 29);
            // 
            // winFormHtmlEditor1.ToolStripSeparator7
            // 
            this.winFormHtmlEditor1.ToolStripSeparator7.Name = "_toolStripSeparator7";
            this.winFormHtmlEditor1.ToolStripSeparator7.Size = new System.Drawing.Size(6, 29);
            // 
            // winFormHtmlEditor1.ToolStripSeparator8
            // 
            this.winFormHtmlEditor1.ToolStripSeparator8.Name = "_toolStripSeparator8";
            this.winFormHtmlEditor1.ToolStripSeparator8.Size = new System.Drawing.Size(6, 29);
            // 
            // winFormHtmlEditor1.ToolStripSeparator9
            // 
            this.winFormHtmlEditor1.ToolStripSeparator9.Name = "_toolStripSeparator9";
            this.winFormHtmlEditor1.ToolStripSeparator9.Size = new System.Drawing.Size(6, 29);
            this.winFormHtmlEditor1.Toolbar2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.winFormHtmlEditor1.CmbTitleInsert,
            this.winFormHtmlEditor1.BtnHighlightColor,
            this.winFormHtmlEditor1.BtnFontColor,
            this.winFormHtmlEditor1.ToolStripSeparator5,
            this.winFormHtmlEditor1.BtnHyperlink,
            this.winFormHtmlEditor1.BtnImage,
            this.winFormHtmlEditor1.BtnInsertYouTubeVideo,
            this.winFormHtmlEditor1.BtnTable,
            this.winFormHtmlEditor1.BtnSymbol,
            this.winFormHtmlEditor1.BtnHorizontalRule,
            this.winFormHtmlEditor1.ToolStripSeparator6,
            this.winFormHtmlEditor1.BtnOrderedList,
            this.winFormHtmlEditor1.BtnUnOrderedList,
            this.winFormHtmlEditor1.ToolStripSeparator7,
            this.winFormHtmlEditor1.BtnAlignLeft,
            this.winFormHtmlEditor1.BtnAlignCenter,
            this.winFormHtmlEditor1.BtnAlignRight,
            this.winFormHtmlEditor1.ToolStripSeparator8,
            this.winFormHtmlEditor1.BtnOutdent,
            this.winFormHtmlEditor1.BtnIndent,
            this.winFormHtmlEditor1.ToolStripSeparator9,
            this.winFormHtmlEditor1.BtnStrikeThrough,
            this.winFormHtmlEditor1.BtnSuperScript,
            this.winFormHtmlEditor1.BtnSubscript,
            this.winFormHtmlEditor1.BtnBodyStyle});
            this.winFormHtmlEditor1.Toolbar2.Location = new System.Drawing.Point(0, 29);
            this.winFormHtmlEditor1.Toolbar2.Name = "WinFormHtmlEditor_Toolbar2";
            this.winFormHtmlEditor1.Toolbar2.Size = new System.Drawing.Size(726, 29);
            this.winFormHtmlEditor1.Toolbar2.TabIndex = 0;
            this.winFormHtmlEditor1.ToolbarContextMenuStrip = null;
            // 
            // winFormHtmlEditor1.WinFormHtmlEditor_ToolbarFooter
            // 
            this.winFormHtmlEditor1.ToolbarFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.winFormHtmlEditor1.ToolbarFooter.Location = new System.Drawing.Point(0, 141);
            this.winFormHtmlEditor1.ToolbarFooter.Name = "WinFormHtmlEditor_ToolbarFooter";
            this.winFormHtmlEditor1.ToolbarFooter.Size = new System.Drawing.Size(726, 25);
            this.winFormHtmlEditor1.ToolbarFooter.TabIndex = 7;
            this.winFormHtmlEditor1.VerticalScroll = null;
            this.winFormHtmlEditor1.z__ignore = false;
            // 
            // DynamicReadingPane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "DynamicReadingPane";
            this.Size = new System.Drawing.Size(737, 428);
            this.FormRegionShowing += new System.EventHandler(this.DynamicReadingPaneFormRegionShowing);
            this.FormRegionClosed += new System.EventHandler(this.DynamicReadingPaneFormRegionClosed);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.PerformLayout();
            this.tableLayoutAttach.ResumeLayout(false);
            this.tableLayoutAttach.PerformLayout();
            this.panelAttach.ResumeLayout(false);
            this.mnuAttach.ResumeLayout(false);
            this.winFormHtmlEditor1.Toolbar1.ResumeLayout(false);
            this.winFormHtmlEditor1.Toolbar1.PerformLayout();
            this.winFormHtmlEditor1.Toolbar2.ResumeLayout(false);
            this.winFormHtmlEditor1.Toolbar2.PerformLayout();
            this.winFormHtmlEditor1.ResumeLayout(false);
            this.winFormHtmlEditor1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        #region Form Region Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private static void InitializeManifest(Microsoft.Office.Tools.Outlook.FormRegionManifest manifest,
                                               Microsoft.Office.Tools.Outlook.Factory factory)
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DynamicReadingPane));
            manifest.ExactMessageClass = true;
            manifest.FormRegionName = "ECS Message";
            manifest.FormRegionType = Microsoft.Office.Tools.Outlook.FormRegionType.Replacement;
            manifest.Icons.Default = ((System.Drawing.Icon)(resources.GetObject("DynamicReadingPane.Manifest.Icons.Default")));
            manifest.Icons.Forwarded = ((System.Drawing.Icon)(resources.GetObject("DynamicReadingPane.Manifest.Icons.Forwarded")));
            manifest.Icons.Read = ((System.Drawing.Icon)(resources.GetObject("DynamicReadingPane.Manifest.Icons.Read")));
            manifest.Icons.Replied = ((System.Drawing.Icon)(resources.GetObject("DynamicReadingPane.Manifest.Icons.Replied")));
            manifest.Icons.Unread = ((System.Drawing.Icon)(resources.GetObject("DynamicReadingPane.Manifest.Icons.Unread")));
            manifest.ShowInspectorCompose = false;
            manifest.ShowInspectorRead = false;
            manifest.Title = "ECS Message";

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ContextMenuStrip mnuAttach;
        private System.Windows.Forms.ToolStripMenuItem previewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator mnuSep1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private PreviewHandlerControl previewHandlerControl;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripMenuItem useDefaultApplicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem browseForEditorToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private EmbeddedMsg embeddedMsg1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutAttach;
        private AttachPanel btnMessage;
        private System.Windows.Forms.Panel panelAttach;
        private System.Windows.Forms.Panel panelVertLine;
        private MessageHeader messageHdr14;
        private MessageHeader2013 messageHdr15;
        private SpiceLogic.WinHTMLEditor.WinForm.WinFormHtmlEditor winFormHtmlEditor1;

        public partial class DynamicReadingPaneFactory : Microsoft.Office.Tools.Outlook.IFormRegionFactory
        {
            public event Microsoft.Office.Tools.Outlook.FormRegionInitializingEventHandler FormRegionInitializing;

            private Microsoft.Office.Tools.Outlook.FormRegionManifest _Manifest;

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public DynamicReadingPaneFactory()
            {
                Logger.Verbose("DynamicReadingPane", "ReadingPaneFactory");
                this._Manifest = Globals.Factory.CreateFormRegionManifest();
                DynamicReadingPane.InitializeManifest(this._Manifest, Globals.Factory);
                this.FormRegionInitializing +=
                    new Microsoft.Office.Tools.Outlook.FormRegionInitializingEventHandler(
                        this.DynamicReadingPaneFactoryFormRegionInitializing);

            }

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public Microsoft.Office.Tools.Outlook.FormRegionManifest Manifest
            {
                get { return this._Manifest; }
            }

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            Microsoft.Office.Tools.Outlook.IFormRegion Microsoft.Office.Tools.Outlook.IFormRegionFactory.
                CreateFormRegion(Microsoft.Office.Interop.Outlook.FormRegion formRegion)
            {
                DynamicReadingPane form = new DynamicReadingPane(formRegion);
                form.Factory = this;
                return form;
            }

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            byte[] Microsoft.Office.Tools.Outlook.IFormRegionFactory.GetFormRegionStorage(object outlookItem,
                                                                                          Microsoft.Office.Interop.
                                                                                              Outlook.OlFormRegionMode
                                                                                              formRegionMode,
                                                                                          Microsoft.Office.Interop.
                                                                                              Outlook.OlFormRegionSize
                                                                                              formRegionSize)
            {
                throw new System.NotSupportedException();
            }

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            bool Microsoft.Office.Tools.Outlook.IFormRegionFactory.IsDisplayedForItem(object outlookItem,
                                                                                      Microsoft.Office.Interop.Outlook.
                                                                                          OlFormRegionMode
                                                                                          formRegionMode,
                                                                                      Microsoft.Office.Interop.Outlook.
                                                                                          OlFormRegionSize
                                                                                          formRegionSize)
            {
                if (this.FormRegionInitializing != null)
                {
                    Microsoft.Office.Tools.Outlook.FormRegionInitializingEventArgs cancelArgs =
                        Globals.Factory.CreateFormRegionInitializingEventArgs(outlookItem, formRegionMode,
                                                                              formRegionSize, false);
                    this.FormRegionInitializing(this, cancelArgs);
                    return !cancelArgs.Cancel;
                }
                else
                {
                    return true;
                }
            }

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            Microsoft.Office.Tools.Outlook.FormRegionKindConstants Microsoft.Office.Tools.Outlook.IFormRegionFactory.
                Kind
            {
                get { return Microsoft.Office.Tools.Outlook.FormRegionKindConstants.WindowsForms; }
            }
        }
    }
}

namespace ChiaraMail
{
    partial class WindowFormRegionCollection
    {
        internal DynamicReadingPane DynamicReadingPane
        {
            get
            {
                foreach (var item in this)
                {
                    if (item.GetType() == typeof(DynamicReadingPane))
                        return (DynamicReadingPane)item;
                }
                return null;
            }
        }
    }
}
