
using ChiaraMail.Controls;
using ChiaraMail.FormRegions;

namespace ChiaraMail.FormRegions
{
    [System.ComponentModel.ToolboxItemAttribute(false)]
    partial class DynamicInspector : Microsoft.Office.Tools.Outlook.FormRegionBase
    {
        public DynamicInspector(Microsoft.Office.Interop.Outlook.FormRegion formRegion)
            : base(Globals.Factory, formRegion)
        {
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
            System.ComponentModel.ComponentResourceManager resources =
                new System.ComponentModel.ComponentResourceManager(typeof(DynamicInspector));
            this.txtFrom = new System.Windows.Forms.TextBox();
            this.txtSent = new System.Windows.Forms.TextBox();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutAttach = new System.Windows.Forms.TableLayoutPanel();
            this.btnMessage = new ChiaraMail.Controls.AttachPanel();
            this.panelAttach = new System.Windows.Forms.Panel();
            this.panelVertLine = new System.Windows.Forms.Panel();
            this.htmlEditor1 = new SpiceLogic.WinHTMLEditor.HTMLEditor();
            this.previewHandlerControl = new ChiaraMail.PreviewHandlerControl();
            this.embeddedMsg1 = new ChiaraMail.Controls.EmbeddedMsg();
            this.inspectorHdr = new ChiaraMail.Controls.InspectorHeader();
            this.msgHdr15 = new ChiaraMail.Controls.MessageHeader2013();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.mnuAttach = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.previewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.useDefaultApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.browseForEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tableLayoutPanelMain.SuspendLayout();
            this.tableLayoutAttach.SuspendLayout();
            this.panelAttach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.htmlEditor1)).BeginInit();
            this.htmlEditor1.SuspendLayout();
            this.mnuAttach.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFrom
            // 
            this.txtFrom.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                   | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFrom.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFrom.Location = new System.Drawing.Point(76, 3);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.ReadOnly = true;
            this.txtFrom.Size = new System.Drawing.Size(517, 14);
            this.txtFrom.TabIndex = 5;
            // 
            // txtSent
            // 
            this.txtSent.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSent.Location = new System.Drawing.Point(668, 3);
            this.txtSent.Name = "txtSent";
            this.txtSent.ReadOnly = true;
            this.txtSent.Size = new System.Drawing.Size(143, 14);
            this.txtSent.TabIndex = 6;
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                   | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanelMain.ColumnCount = 1;
            this.tableLayoutPanelMain.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutAttach, 0, 3);
            this.tableLayoutPanelMain.Controls.Add(this.htmlEditor1, 0, 4);
            this.tableLayoutPanelMain.Controls.Add(this.previewHandlerControl, 0, 5);
            this.tableLayoutPanelMain.Controls.Add(this.embeddedMsg1, 0, 6);
            this.tableLayoutPanelMain.Controls.Add(this.inspectorHdr, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.msgHdr15, 0, 2);
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.Padding = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanelMain.RowCount = 7;
            this.tableLayoutPanelMain.RowStyles.Add(
                new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 2F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.RowStyles.Add(
                new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(810, 381);
            this.tableLayoutPanelMain.TabIndex = 11;
            this.tableLayoutPanelMain.Paint += new System.Windows.Forms.PaintEventHandler(this.TableLayoutPanelMainPaint);
            // 
            // tableLayoutAttach
            // 
            this.tableLayoutAttach.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                   | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutAttach.ColumnCount = 2;
            this.tableLayoutAttach.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutAttach.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutAttach.Controls.Add(this.btnMessage, 0, 0);
            this.tableLayoutAttach.Controls.Add(this.panelAttach, 1, 0);
            this.tableLayoutAttach.Location = new System.Drawing.Point(5, 199);
            this.tableLayoutAttach.Name = "tableLayoutAttach";
            this.tableLayoutAttach.RowCount = 1;
            this.tableLayoutAttach.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutAttach.Size = new System.Drawing.Size(800, 22);
            this.tableLayoutAttach.TabIndex = 16;
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
            this.panelAttach.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                   | System.Windows.Forms.AnchorStyles.Right)));
            this.panelAttach.AutoScroll = true;
            this.panelAttach.Controls.Add(this.panelVertLine);
            this.panelAttach.Location = new System.Drawing.Point(75, 0);
            this.panelAttach.Margin = new System.Windows.Forms.Padding(0);
            this.panelAttach.Name = "panelAttach";
            this.panelAttach.Size = new System.Drawing.Size(725, 18);
            this.panelAttach.TabIndex = 2;
            // 
            // panelVertLine
            // 
            this.panelVertLine.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                   | System.Windows.Forms.AnchorStyles.Left)));
            this.panelVertLine.BackColor = System.Drawing.Color.DarkGray;
            this.panelVertLine.Location = new System.Drawing.Point(0, 0);
            this.panelVertLine.Margin = new System.Windows.Forms.Padding(0);
            this.panelVertLine.Name = "panelVertLine";
            this.panelVertLine.Size = new System.Drawing.Size(1, 18);
            this.panelVertLine.TabIndex = 0;
            // 
            // htmlEditor1
            // 
            this.htmlEditor1.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                   | System.Windows.Forms.AnchorStyles.Right)));
            this.htmlEditor1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.htmlEditor1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.htmlEditor1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.htmlEditor1.BackgroundImagePath = "";
            this.htmlEditor1.BaseUrl = "";
            this.htmlEditor1.BodyColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))),
                                                                       ((int)(((byte)(255)))),
                                                                       ((int)(((byte)(255)))));
            this.htmlEditor1.BodyHtml = null;
            this.htmlEditor1.BodyStyle = null;
            this.htmlEditor1.Charset = "unicode";
            this.htmlEditor1.Cursor = System.Windows.Forms.Cursors.Default;
            this.htmlEditor1.DefaultForeColor = System.Drawing.Color.Black;
            this.htmlEditor1.DocumentHtml = resources.GetString("htmlEditor1.DocumentHtml");
            this.htmlEditor1.DocumentTitle = "";
            this.htmlEditor1.EditorContextMenuStrip = null;
            this.htmlEditor1.FactoryToolbarItems.Add(new SpiceLogic.WinHTMLEditor.FactoryItem("FACTORY_New",
                                                                                              SpiceLogic.WinHTMLEditor
                                                                                                        .ToolStripHosts
                                                                                                        .TopStrip, true,
                                                                                              true, true, true, "New",
                                                                                              ((System.Drawing.Image)
                                                                                               (resources.GetObject(
                                                                                                   "htmlEditor1.FactoryToolbarItems"))),
                                                                                              System.Drawing.Color.Empty,
                                                                                              null,
                                                                                              System.Windows.Forms
                                                                                                    .ImageLayout.None,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemDisplayStyle
                                                                                                    .Image,
                                                                                              new System.Drawing.Font(
                                                                                                  "Microsoft Sans Serif",
                                                                                                  8.25F,
                                                                                                  System.Drawing
                                                                                                        .FontStyle
                                                                                                        .Regular,
                                                                                                  System.Drawing
                                                                                                        .GraphicsUnit
                                                                                                        .Point,
                                                                                                  ((byte)(0))),
                                                                                              System.Drawing.Color.Black,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemImageScaling
                                                                                                    .None,
                                                                                              System.Drawing.Color
                                                                                                    .Magenta,
                                                                                              System.Windows.Forms
                                                                                                    .RightToLeft.No,
                                                                                              "New",
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripTextDirection
                                                                                                    .Horizontal,
                                                                                              System.Windows.Forms
                                                                                                    .TextImageRelation
                                                                                                    .ImageBeforeText,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemAlignment
                                                                                                    .Left,
                                                                                              new System.Drawing.Size(
                                                                                                  23, 26)));
            this.htmlEditor1.FactoryToolbarItems.Add(new SpiceLogic.WinHTMLEditor.FactoryItem("FACTORY_Open",
                                                                                              SpiceLogic.WinHTMLEditor
                                                                                                        .ToolStripHosts
                                                                                                        .TopStrip, true,
                                                                                              true, true, true, "Open",
                                                                                              ((System.Drawing.Image)
                                                                                               (resources.GetObject(
                                                                                                   "htmlEditor1.FactoryToolbarItems1"))),
                                                                                              System.Drawing.Color.Empty,
                                                                                              null,
                                                                                              System.Windows.Forms
                                                                                                    .ImageLayout.None,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemDisplayStyle
                                                                                                    .Image,
                                                                                              new System.Drawing.Font(
                                                                                                  "Microsoft Sans Serif",
                                                                                                  8.25F,
                                                                                                  System.Drawing
                                                                                                        .FontStyle
                                                                                                        .Regular,
                                                                                                  System.Drawing
                                                                                                        .GraphicsUnit
                                                                                                        .Point,
                                                                                                  ((byte)(0))),
                                                                                              System.Drawing.Color.Black,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemImageScaling
                                                                                                    .None,
                                                                                              System.Drawing.Color
                                                                                                    .Magenta,
                                                                                              System.Windows.Forms
                                                                                                    .RightToLeft.No,
                                                                                              "Open",
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripTextDirection
                                                                                                    .Horizontal,
                                                                                              System.Windows.Forms
                                                                                                    .TextImageRelation
                                                                                                    .ImageBeforeText,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemAlignment
                                                                                                    .Left,
                                                                                              new System.Drawing.Size(
                                                                                                  23, 26)));
            this.htmlEditor1.FactoryToolbarItems.Add(new SpiceLogic.WinHTMLEditor.FactoryItem("FACTORY_Save",
                                                                                              SpiceLogic.WinHTMLEditor
                                                                                                        .ToolStripHosts
                                                                                                        .TopStrip, true,
                                                                                              true, true, true, "Save",
                                                                                              ((System.Drawing.Image)
                                                                                               (resources.GetObject(
                                                                                                   "htmlEditor1.FactoryToolbarItems2"))),
                                                                                              System.Drawing.Color.Empty,
                                                                                              null,
                                                                                              System.Windows.Forms
                                                                                                    .ImageLayout.None,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemDisplayStyle
                                                                                                    .Image,
                                                                                              new System.Drawing.Font(
                                                                                                  "Microsoft Sans Serif",
                                                                                                  8.25F,
                                                                                                  System.Drawing
                                                                                                        .FontStyle
                                                                                                        .Regular,
                                                                                                  System.Drawing
                                                                                                        .GraphicsUnit
                                                                                                        .Point,
                                                                                                  ((byte)(0))),
                                                                                              System.Drawing.Color.Black,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemImageScaling
                                                                                                    .None,
                                                                                              System.Drawing.Color
                                                                                                    .Magenta,
                                                                                              System.Windows.Forms
                                                                                                    .RightToLeft.No,
                                                                                              "Save",
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripTextDirection
                                                                                                    .Horizontal,
                                                                                              System.Windows.Forms
                                                                                                    .TextImageRelation
                                                                                                    .ImageBeforeText,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemAlignment
                                                                                                    .Left,
                                                                                              new System.Drawing.Size(
                                                                                                  26, 26)));
            this.htmlEditor1.FactoryToolbarItems.Add(new SpiceLogic.WinHTMLEditor.FactoryItem("FACTORY_Separator1",
                                                                                              SpiceLogic.WinHTMLEditor
                                                                                                        .ToolStripHosts
                                                                                                        .TopStrip, true,
                                                                                              true, true, true, null,
                                                                                              null,
                                                                                              System.Drawing.Color.Empty,
                                                                                              null,
                                                                                              System.Windows.Forms
                                                                                                    .ImageLayout.None,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemDisplayStyle
                                                                                                    .None,
                                                                                              new System.Drawing.Font(
                                                                                                  "Microsoft Sans Serif",
                                                                                                  8.25F,
                                                                                                  System.Drawing
                                                                                                        .FontStyle
                                                                                                        .Regular,
                                                                                                  System.Drawing
                                                                                                        .GraphicsUnit
                                                                                                        .Point,
                                                                                                  ((byte)(0))),
                                                                                              System.Drawing.Color.Black,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemImageScaling
                                                                                                    .SizeToFit,
                                                                                              System.Drawing.Color.Empty,
                                                                                              System.Windows.Forms
                                                                                                    .RightToLeft.No,
                                                                                              null,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripTextDirection
                                                                                                    .Horizontal,
                                                                                              System.Windows.Forms
                                                                                                    .TextImageRelation
                                                                                                    .ImageBeforeText,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemAlignment
                                                                                                    .Left,
                                                                                              new System.Drawing.Size(
                                                                                                  0, 0)));
            this.htmlEditor1.FactoryToolbarItems.Add(new SpiceLogic.WinHTMLEditor.FactoryItem("FACTORY_Font Name",
                                                                                              SpiceLogic.WinHTMLEditor
                                                                                                        .ToolStripHosts
                                                                                                        .TopStrip, true,
                                                                                              true, true, true,
                                                                                              "Font Name", null,
                                                                                              System.Drawing.Color.Empty,
                                                                                              null,
                                                                                              System.Windows.Forms
                                                                                                    .ImageLayout.None,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemDisplayStyle
                                                                                                    .None,
                                                                                              new System.Drawing.Font(
                                                                                                  "Verdana", 8F),
                                                                                              System.Drawing.Color.Black,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemImageScaling
                                                                                                    .SizeToFit,
                                                                                              System.Drawing.Color.Empty,
                                                                                              System.Windows.Forms
                                                                                                    .RightToLeft.No,
                                                                                              null,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripTextDirection
                                                                                                    .Horizontal,
                                                                                              System.Windows.Forms
                                                                                                    .TextImageRelation
                                                                                                    .ImageBeforeText,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemAlignment
                                                                                                    .Left,
                                                                                              new System.Drawing.Size(
                                                                                                  160, 24)));
            this.htmlEditor1.FactoryToolbarItems.Add(new SpiceLogic.WinHTMLEditor.FactoryItem("FACTORY_Font Size",
                                                                                              SpiceLogic.WinHTMLEditor
                                                                                                        .ToolStripHosts
                                                                                                        .TopStrip, true,
                                                                                              true, true, true,
                                                                                              "Font Size", null,
                                                                                              System.Drawing.Color.Empty,
                                                                                              null,
                                                                                              System.Windows.Forms
                                                                                                    .ImageLayout.None,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemDisplayStyle
                                                                                                    .None,
                                                                                              new System.Drawing.Font(
                                                                                                  "Microsoft Sans Serif",
                                                                                                  8.25F,
                                                                                                  System.Drawing
                                                                                                        .FontStyle
                                                                                                        .Regular,
                                                                                                  System.Drawing
                                                                                                        .GraphicsUnit
                                                                                                        .Point,
                                                                                                  ((byte)(0))),
                                                                                              System.Drawing.Color.Black,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemImageScaling
                                                                                                    .SizeToFit,
                                                                                              System.Drawing.Color.Empty,
                                                                                              System.Windows.Forms
                                                                                                    .RightToLeft.No,
                                                                                              null,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripTextDirection
                                                                                                    .Horizontal,
                                                                                              System.Windows.Forms
                                                                                                    .TextImageRelation
                                                                                                    .ImageBeforeText,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemAlignment
                                                                                                    .Left,
                                                                                              new System.Drawing.Size(
                                                                                                  0, 0)));
            this.htmlEditor1.FactoryToolbarItems.Add(new SpiceLogic.WinHTMLEditor.FactoryItem("FACTORY_Separator2",
                                                                                              SpiceLogic.WinHTMLEditor
                                                                                                        .ToolStripHosts
                                                                                                        .TopStrip, true,
                                                                                              true, true, true, null,
                                                                                              null,
                                                                                              System.Drawing.Color.Empty,
                                                                                              null,
                                                                                              System.Windows.Forms
                                                                                                    .ImageLayout.None,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemDisplayStyle
                                                                                                    .None,
                                                                                              new System.Drawing.Font(
                                                                                                  "Microsoft Sans Serif",
                                                                                                  8.25F,
                                                                                                  System.Drawing
                                                                                                        .FontStyle
                                                                                                        .Regular,
                                                                                                  System.Drawing
                                                                                                        .GraphicsUnit
                                                                                                        .Point,
                                                                                                  ((byte)(0))),
                                                                                              System.Drawing.Color.Black,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemImageScaling
                                                                                                    .SizeToFit,
                                                                                              System.Drawing.Color.Empty,
                                                                                              System.Windows.Forms
                                                                                                    .RightToLeft.No,
                                                                                              null,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripTextDirection
                                                                                                    .Horizontal,
                                                                                              System.Windows.Forms
                                                                                                    .TextImageRelation
                                                                                                    .ImageBeforeText,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemAlignment
                                                                                                    .Left,
                                                                                              new System.Drawing.Size(
                                                                                                  0, 0)));
            this.htmlEditor1.FactoryToolbarItems.Add(new SpiceLogic.WinHTMLEditor.FactoryItem("FACTORY_Cut",
                                                                                              SpiceLogic.WinHTMLEditor
                                                                                                        .ToolStripHosts
                                                                                                        .TopStrip, true,
                                                                                              true, true, true, "Cut",
                                                                                              ((System.Drawing.Image)
                                                                                               (resources.GetObject(
                                                                                                   "htmlEditor1.FactoryToolbarItems3"))),
                                                                                              System.Drawing.Color.Empty,
                                                                                              null,
                                                                                              System.Windows.Forms
                                                                                                    .ImageLayout.None,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemDisplayStyle
                                                                                                    .Image,
                                                                                              new System.Drawing.Font(
                                                                                                  "Microsoft Sans Serif",
                                                                                                  8.25F,
                                                                                                  System.Drawing
                                                                                                        .FontStyle
                                                                                                        .Regular,
                                                                                                  System.Drawing
                                                                                                        .GraphicsUnit
                                                                                                        .Point,
                                                                                                  ((byte)(0))),
                                                                                              System.Drawing.Color.Black,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemImageScaling
                                                                                                    .None,
                                                                                              System.Drawing.Color
                                                                                                    .Magenta,
                                                                                              System.Windows.Forms
                                                                                                    .RightToLeft.No,
                                                                                              "Cut",
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripTextDirection
                                                                                                    .Horizontal,
                                                                                              System.Windows.Forms
                                                                                                    .TextImageRelation
                                                                                                    .ImageBeforeText,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemAlignment
                                                                                                    .Left,
                                                                                              new System.Drawing.Size(
                                                                                                  26, 26)));
            this.htmlEditor1.FactoryToolbarItems.Add(new SpiceLogic.WinHTMLEditor.FactoryItem("FACTORY_Copy",
                                                                                              SpiceLogic.WinHTMLEditor
                                                                                                        .ToolStripHosts
                                                                                                        .TopStrip, true,
                                                                                              true, true, true, "Copy",
                                                                                              ((System.Drawing.Image)
                                                                                               (resources.GetObject(
                                                                                                   "htmlEditor1.FactoryToolbarItems4"))),
                                                                                              System.Drawing.Color.Empty,
                                                                                              null,
                                                                                              System.Windows.Forms
                                                                                                    .ImageLayout.None,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemDisplayStyle
                                                                                                    .Image,
                                                                                              new System.Drawing.Font(
                                                                                                  "Microsoft Sans Serif",
                                                                                                  8.25F,
                                                                                                  System.Drawing
                                                                                                        .FontStyle
                                                                                                        .Regular,
                                                                                                  System.Drawing
                                                                                                        .GraphicsUnit
                                                                                                        .Point,
                                                                                                  ((byte)(0))),
                                                                                              System.Drawing.Color.Black,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemImageScaling
                                                                                                    .None,
                                                                                              System.Drawing.Color
                                                                                                    .Magenta,
                                                                                              System.Windows.Forms
                                                                                                    .RightToLeft.No,
                                                                                              "Copy",
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripTextDirection
                                                                                                    .Horizontal,
                                                                                              System.Windows.Forms
                                                                                                    .TextImageRelation
                                                                                                    .ImageBeforeText,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemAlignment
                                                                                                    .Left,
                                                                                              new System.Drawing.Size(
                                                                                                  27, 26)));
            this.htmlEditor1.FactoryToolbarItems.Add(new SpiceLogic.WinHTMLEditor.FactoryItem("FACTORY_Paste",
                                                                                              SpiceLogic.WinHTMLEditor
                                                                                                        .ToolStripHosts
                                                                                                        .TopStrip, true,
                                                                                              true, true, true, "Paste",
                                                                                              ((System.Drawing.Image)
                                                                                               (resources.GetObject(
                                                                                                   "htmlEditor1.FactoryToolbarItems5"))),
                                                                                              System.Drawing.Color.Empty,
                                                                                              null,
                                                                                              System.Windows.Forms
                                                                                                    .ImageLayout.None,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemDisplayStyle
                                                                                                    .Image,
                                                                                              new System.Drawing.Font(
                                                                                                  "Microsoft Sans Serif",
                                                                                                  8.25F,
                                                                                                  System.Drawing
                                                                                                        .FontStyle
                                                                                                        .Regular,
                                                                                                  System.Drawing
                                                                                                        .GraphicsUnit
                                                                                                        .Point,
                                                                                                  ((byte)(0))),
                                                                                              System.Drawing.Color.Black,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemImageScaling
                                                                                                    .None,
                                                                                              System.Drawing.Color
                                                                                                    .Magenta,
                                                                                              System.Windows.Forms
                                                                                                    .RightToLeft.No,
                                                                                              "Paste",
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripTextDirection
                                                                                                    .Horizontal,
                                                                                              System.Windows.Forms
                                                                                                    .TextImageRelation
                                                                                                    .ImageBeforeText,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemAlignment
                                                                                                    .Left,
                                                                                              new System.Drawing.Size(
                                                                                                  27, 26)));
            this.htmlEditor1.FactoryToolbarItems.Add(new SpiceLogic.WinHTMLEditor.FactoryItem("FACTORY_Separator3",
                                                                                              SpiceLogic.WinHTMLEditor
                                                                                                        .ToolStripHosts
                                                                                                        .TopStrip, true,
                                                                                              true, true, true, null,
                                                                                              null,
                                                                                              System.Drawing.Color.Empty,
                                                                                              null,
                                                                                              System.Windows.Forms
                                                                                                    .ImageLayout.None,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemDisplayStyle
                                                                                                    .None,
                                                                                              new System.Drawing.Font(
                                                                                                  "Microsoft Sans Serif",
                                                                                                  8.25F,
                                                                                                  System.Drawing
                                                                                                        .FontStyle
                                                                                                        .Regular,
                                                                                                  System.Drawing
                                                                                                        .GraphicsUnit
                                                                                                        .Point,
                                                                                                  ((byte)(0))),
                                                                                              System.Drawing.Color.Black,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemImageScaling
                                                                                                    .SizeToFit,
                                                                                              System.Drawing.Color.Empty,
                                                                                              System.Windows.Forms
                                                                                                    .RightToLeft.No,
                                                                                              null,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripTextDirection
                                                                                                    .Horizontal,
                                                                                              System.Windows.Forms
                                                                                                    .TextImageRelation
                                                                                                    .ImageBeforeText,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemAlignment
                                                                                                    .Left,
                                                                                              new System.Drawing.Size(
                                                                                                  0, 0)));
            this.htmlEditor1.FactoryToolbarItems.Add(new SpiceLogic.WinHTMLEditor.FactoryItem("FACTORY_Bold",
                                                                                              SpiceLogic.WinHTMLEditor
                                                                                                        .ToolStripHosts
                                                                                                        .TopStrip, true,
                                                                                              true, true, true, "Bold",
                                                                                              ((System.Drawing.Image)
                                                                                               (resources.GetObject(
                                                                                                   "htmlEditor1.FactoryToolbarItems6"))),
                                                                                              System.Drawing.Color.Empty,
                                                                                              null,
                                                                                              System.Windows.Forms
                                                                                                    .ImageLayout.None,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemDisplayStyle
                                                                                                    .Image,
                                                                                              new System.Drawing.Font(
                                                                                                  "Microsoft Sans Serif",
                                                                                                  8.25F,
                                                                                                  System.Drawing
                                                                                                        .FontStyle
                                                                                                        .Regular,
                                                                                                  System.Drawing
                                                                                                        .GraphicsUnit
                                                                                                        .Point,
                                                                                                  ((byte)(0))),
                                                                                              System.Drawing.Color.Black,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemImageScaling
                                                                                                    .None,
                                                                                              System.Drawing.Color
                                                                                                    .Magenta,
                                                                                              System.Windows.Forms
                                                                                                    .RightToLeft.No,
                                                                                              "Bold",
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripTextDirection
                                                                                                    .Horizontal,
                                                                                              System.Windows.Forms
                                                                                                    .TextImageRelation
                                                                                                    .ImageBeforeText,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemAlignment
                                                                                                    .Left,
                                                                                              new System.Drawing.Size(
                                                                                                  23, 26)));
            this.htmlEditor1.FactoryToolbarItems.Add(new SpiceLogic.WinHTMLEditor.FactoryItem("FACTORY_Italic",
                                                                                              SpiceLogic.WinHTMLEditor
                                                                                                        .ToolStripHosts
                                                                                                        .TopStrip, true,
                                                                                              true, true, true, "Italic",
                                                                                              ((System.Drawing.Image)
                                                                                               (resources.GetObject(
                                                                                                   "htmlEditor1.FactoryToolbarItems7"))),
                                                                                              System.Drawing.Color.Empty,
                                                                                              null,
                                                                                              System.Windows.Forms
                                                                                                    .ImageLayout.None,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemDisplayStyle
                                                                                                    .Image,
                                                                                              new System.Drawing.Font(
                                                                                                  "Microsoft Sans Serif",
                                                                                                  8.25F,
                                                                                                  System.Drawing
                                                                                                        .FontStyle
                                                                                                        .Regular,
                                                                                                  System.Drawing
                                                                                                        .GraphicsUnit
                                                                                                        .Point,
                                                                                                  ((byte)(0))),
                                                                                              System.Drawing.Color.Black,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemImageScaling
                                                                                                    .None,
                                                                                              System.Drawing.Color
                                                                                                    .Magenta,
                                                                                              System.Windows.Forms
                                                                                                    .RightToLeft.No,
                                                                                              "Italic",
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripTextDirection
                                                                                                    .Horizontal,
                                                                                              System.Windows.Forms
                                                                                                    .TextImageRelation
                                                                                                    .ImageBeforeText,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemAlignment
                                                                                                    .Left,
                                                                                              new System.Drawing.Size(
                                                                                                  23, 26)));
            this.htmlEditor1.FactoryToolbarItems.Add(new SpiceLogic.WinHTMLEditor.FactoryItem("FACTORY_Underline",
                                                                                              SpiceLogic.WinHTMLEditor
                                                                                                        .ToolStripHosts
                                                                                                        .TopStrip, true,
                                                                                              true, true, true,
                                                                                              "Underline",
                                                                                              ((System.Drawing.Image)
                                                                                               (resources.GetObject(
                                                                                                   "htmlEditor1.FactoryToolbarItems8"))),
                                                                                              System.Drawing.Color.Empty,
                                                                                              null,
                                                                                              System.Windows.Forms
                                                                                                    .ImageLayout.None,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemDisplayStyle
                                                                                                    .Image,
                                                                                              new System.Drawing.Font(
                                                                                                  "Microsoft Sans Serif",
                                                                                                  8.25F,
                                                                                                  System.Drawing
                                                                                                        .FontStyle
                                                                                                        .Regular,
                                                                                                  System.Drawing
                                                                                                        .GraphicsUnit
                                                                                                        .Point,
                                                                                                  ((byte)(0))),
                                                                                              System.Drawing.Color.Black,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemImageScaling
                                                                                                    .None,
                                                                                              System.Drawing.Color
                                                                                                    .Magenta,
                                                                                              System.Windows.Forms
                                                                                                    .RightToLeft.No,
                                                                                              "Underline",
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripTextDirection
                                                                                                    .Horizontal,
                                                                                              System.Windows.Forms
                                                                                                    .TextImageRelation
                                                                                                    .ImageBeforeText,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemAlignment
                                                                                                    .Left,
                                                                                              new System.Drawing.Size(
                                                                                                  23, 26)));
            this.htmlEditor1.FactoryToolbarItems.Add(new SpiceLogic.WinHTMLEditor.FactoryItem(
                                                         "FACTORY_Horizontal Rule",
                                                         SpiceLogic.WinHTMLEditor.ToolStripHosts.TopStrip, true, true,
                                                         true, true, "Horizontal Rule",
                                                         ((System.Drawing.Image)
                                                          (resources.GetObject("htmlEditor1.FactoryToolbarItems9"))),
                                                         System.Drawing.Color.Empty, null,
                                                         System.Windows.Forms.ImageLayout.None,
                                                         System.Windows.Forms.ToolStripItemDisplayStyle.Image,
                                                         new System.Drawing.Font("Microsoft Sans Serif", 8.25F,
                                                                                 System.Drawing.FontStyle.Regular,
                                                                                 System.Drawing.GraphicsUnit.Point,
                                                                                 ((byte)(0))),
                                                         System.Drawing.Color.Black,
                                                         System.Drawing.ContentAlignment.MiddleCenter,
                                                         System.Windows.Forms.ToolStripItemImageScaling.None,
                                                         System.Drawing.Color.Magenta,
                                                         System.Windows.Forms.RightToLeft.No, "Horizontal Rule",
                                                         System.Drawing.ContentAlignment.MiddleCenter,
                                                         System.Windows.Forms.ToolStripTextDirection.Horizontal,
                                                         System.Windows.Forms.TextImageRelation.ImageBeforeText,
                                                         System.Windows.Forms.ToolStripItemAlignment.Left,
                                                         new System.Drawing.Size(24, 26)));
            this.htmlEditor1.FactoryToolbarItems.Add(new SpiceLogic.WinHTMLEditor.FactoryItem("FACTORY_Format Reset",
                                                                                              SpiceLogic.WinHTMLEditor
                                                                                                        .ToolStripHosts
                                                                                                        .TopStrip, true,
                                                                                              true, true, true,
                                                                                              "Format Reset",
                                                                                              ((System.Drawing.Image)
                                                                                               (resources.GetObject(
                                                                                                   "htmlEditor1.FactoryToolbarItems10"))),
                                                                                              System.Drawing.Color.Empty,
                                                                                              null,
                                                                                              System.Windows.Forms
                                                                                                    .ImageLayout.None,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemDisplayStyle
                                                                                                    .Image,
                                                                                              new System.Drawing.Font(
                                                                                                  "Microsoft Sans Serif",
                                                                                                  8.25F,
                                                                                                  System.Drawing
                                                                                                        .FontStyle
                                                                                                        .Regular,
                                                                                                  System.Drawing
                                                                                                        .GraphicsUnit
                                                                                                        .Point,
                                                                                                  ((byte)(0))),
                                                                                              System.Drawing.Color.Black,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemImageScaling
                                                                                                    .None,
                                                                                              System.Drawing.Color
                                                                                                    .Magenta,
                                                                                              System.Windows.Forms
                                                                                                    .RightToLeft.No,
                                                                                              "Format Reset",
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripTextDirection
                                                                                                    .Horizontal,
                                                                                              System.Windows.Forms
                                                                                                    .TextImageRelation
                                                                                                    .ImageBeforeText,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemAlignment
                                                                                                    .Left,
                                                                                              new System.Drawing.Size(
                                                                                                  34, 26)));
            this.htmlEditor1.FactoryToolbarItems.Add(new SpiceLogic.WinHTMLEditor.FactoryItem("FACTORY_Undo",
                                                                                              SpiceLogic.WinHTMLEditor
                                                                                                        .ToolStripHosts
                                                                                                        .TopStrip, true,
                                                                                              true, true, true, "Undo",
                                                                                              ((System.Drawing.Image)
                                                                                               (resources.GetObject(
                                                                                                   "htmlEditor1.FactoryToolbarItems11"))),
                                                                                              System.Drawing.Color.Empty,
                                                                                              null,
                                                                                              System.Windows.Forms
                                                                                                    .ImageLayout.None,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemDisplayStyle
                                                                                                    .Image,
                                                                                              new System.Drawing.Font(
                                                                                                  "Microsoft Sans Serif",
                                                                                                  8.25F,
                                                                                                  System.Drawing
                                                                                                        .FontStyle
                                                                                                        .Regular,
                                                                                                  System.Drawing
                                                                                                        .GraphicsUnit
                                                                                                        .Point,
                                                                                                  ((byte)(0))),
                                                                                              System.Drawing.Color.Black,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemImageScaling
                                                                                                    .SizeToFit,
                                                                                              System.Drawing.Color
                                                                                                    .Magenta,
                                                                                              System.Windows.Forms
                                                                                                    .RightToLeft.No,
                                                                                              "Undo",
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripTextDirection
                                                                                                    .Horizontal,
                                                                                              System.Windows.Forms
                                                                                                    .TextImageRelation
                                                                                                    .ImageBeforeText,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemAlignment
                                                                                                    .Left,
                                                                                              new System.Drawing.Size(
                                                                                                  24, 26)));
            this.htmlEditor1.FactoryToolbarItems.Add(new SpiceLogic.WinHTMLEditor.FactoryItem("FACTORY_Redo",
                                                                                              SpiceLogic.WinHTMLEditor
                                                                                                        .ToolStripHosts
                                                                                                        .TopStrip, true,
                                                                                              true, true, true, "Redo",
                                                                                              ((System.Drawing.Image)
                                                                                               (resources.GetObject(
                                                                                                   "htmlEditor1.FactoryToolbarItems12"))),
                                                                                              System.Drawing.Color.Empty,
                                                                                              null,
                                                                                              System.Windows.Forms
                                                                                                    .ImageLayout.None,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemDisplayStyle
                                                                                                    .Image,
                                                                                              new System.Drawing.Font(
                                                                                                  "Microsoft Sans Serif",
                                                                                                  8.25F,
                                                                                                  System.Drawing
                                                                                                        .FontStyle
                                                                                                        .Regular,
                                                                                                  System.Drawing
                                                                                                        .GraphicsUnit
                                                                                                        .Point,
                                                                                                  ((byte)(0))),
                                                                                              System.Drawing.Color.Black,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemImageScaling
                                                                                                    .SizeToFit,
                                                                                              System.Drawing.Color
                                                                                                    .Magenta,
                                                                                              System.Windows.Forms
                                                                                                    .RightToLeft.No,
                                                                                              "Redo",
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripTextDirection
                                                                                                    .Horizontal,
                                                                                              System.Windows.Forms
                                                                                                    .TextImageRelation
                                                                                                    .ImageBeforeText,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemAlignment
                                                                                                    .Left,
                                                                                              new System.Drawing.Size(
                                                                                                  24, 26)));
            this.htmlEditor1.FactoryToolbarItems.Add(new SpiceLogic.WinHTMLEditor.FactoryItem("FACTORY_Edit Source",
                                                                                              SpiceLogic.WinHTMLEditor
                                                                                                        .ToolStripHosts
                                                                                                        .TopStrip, true,
                                                                                              true, true, true,
                                                                                              "Edit Source",
                                                                                              ((System.Drawing.Image)
                                                                                               (resources.GetObject(
                                                                                                   "htmlEditor1.FactoryToolbarItems13"))),
                                                                                              System.Drawing.Color.Empty,
                                                                                              null,
                                                                                              System.Windows.Forms
                                                                                                    .ImageLayout.None,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemDisplayStyle
                                                                                                    .Image,
                                                                                              new System.Drawing.Font(
                                                                                                  "Microsoft Sans Serif",
                                                                                                  8.25F,
                                                                                                  System.Drawing
                                                                                                        .FontStyle
                                                                                                        .Regular,
                                                                                                  System.Drawing
                                                                                                        .GraphicsUnit
                                                                                                        .Point,
                                                                                                  ((byte)(0))),
                                                                                              System.Drawing.Color.Black,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemImageScaling
                                                                                                    .None,
                                                                                              System.Drawing.Color
                                                                                                    .Magenta,
                                                                                              System.Windows.Forms
                                                                                                    .RightToLeft.No,
                                                                                              "Edit Source",
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripTextDirection
                                                                                                    .Horizontal,
                                                                                              System.Windows.Forms
                                                                                                    .TextImageRelation
                                                                                                    .ImageBeforeText,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemAlignment
                                                                                                    .Left,
                                                                                              new System.Drawing.Size(
                                                                                                  34, 26)));
            this.htmlEditor1.FactoryToolbarItems.Add(new SpiceLogic.WinHTMLEditor.FactoryItem("FACTORY_Spell Check",
                                                                                              SpiceLogic.WinHTMLEditor
                                                                                                        .ToolStripHosts
                                                                                                        .TopStrip, true,
                                                                                              true, true, true,
                                                                                              "Spell Check",
                                                                                              ((System.Drawing.Image)
                                                                                               (resources.GetObject(
                                                                                                   "htmlEditor1.FactoryToolbarItems14"))),
                                                                                              System.Drawing.Color.Empty,
                                                                                              null,
                                                                                              System.Windows.Forms
                                                                                                    .ImageLayout.None,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemDisplayStyle
                                                                                                    .Image,
                                                                                              new System.Drawing.Font(
                                                                                                  "Microsoft Sans Serif",
                                                                                                  8.25F,
                                                                                                  System.Drawing
                                                                                                        .FontStyle
                                                                                                        .Regular,
                                                                                                  System.Drawing
                                                                                                        .GraphicsUnit
                                                                                                        .Point,
                                                                                                  ((byte)(0))),
                                                                                              System.Drawing.Color.Black,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemImageScaling
                                                                                                    .SizeToFit,
                                                                                              System.Drawing.Color
                                                                                                    .Magenta,
                                                                                              System.Windows.Forms
                                                                                                    .RightToLeft.No,
                                                                                              "Spell Check",
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripTextDirection
                                                                                                    .Horizontal,
                                                                                              System.Windows.Forms
                                                                                                    .TextImageRelation
                                                                                                    .ImageBeforeText,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemAlignment
                                                                                                    .Left,
                                                                                              new System.Drawing.Size(
                                                                                                  23, 26)));
            this.htmlEditor1.FactoryToolbarItems.Add(new SpiceLogic.WinHTMLEditor.FactoryItem("FACTORY_Title Insert",
                                                                                              SpiceLogic.WinHTMLEditor
                                                                                                        .ToolStripHosts
                                                                                                        .BottomStrip,
                                                                                              true, true, true, true,
                                                                                              "Title Insert", null,
                                                                                              System.Drawing.Color.Empty,
                                                                                              null,
                                                                                              System.Windows.Forms
                                                                                                    .ImageLayout.None,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemDisplayStyle
                                                                                                    .None,
                                                                                              new System.Drawing.Font(
                                                                                                  "Microsoft Sans Serif",
                                                                                                  8.25F,
                                                                                                  System.Drawing
                                                                                                        .FontStyle
                                                                                                        .Regular,
                                                                                                  System.Drawing
                                                                                                        .GraphicsUnit
                                                                                                        .Point,
                                                                                                  ((byte)(0))),
                                                                                              System.Drawing.Color.Black,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemImageScaling
                                                                                                    .SizeToFit,
                                                                                              System.Drawing.Color.Empty,
                                                                                              System.Windows.Forms
                                                                                                    .RightToLeft.No,
                                                                                              null,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripTextDirection
                                                                                                    .Horizontal,
                                                                                              System.Windows.Forms
                                                                                                    .TextImageRelation
                                                                                                    .ImageBeforeText,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemAlignment
                                                                                                    .Left,
                                                                                              new System.Drawing.Size(
                                                                                                  105, 29)));
            this.htmlEditor1.FactoryToolbarItems.Add(
                new SpiceLogic.WinHTMLEditor.FactoryItem("FACTORY_Text Highlight Color",
                                                         SpiceLogic.WinHTMLEditor.ToolStripHosts.BottomStrip, true, true,
                                                         true, true, "Text Highlight Color",
                                                         ((System.Drawing.Image)
                                                          (resources.GetObject("htmlEditor1.FactoryToolbarItems15"))),
                                                         System.Drawing.Color.Empty, null,
                                                         System.Windows.Forms.ImageLayout.None,
                                                         System.Windows.Forms.ToolStripItemDisplayStyle.Image,
                                                         new System.Drawing.Font("Microsoft Sans Serif", 8.25F,
                                                                                 System.Drawing.FontStyle.Regular,
                                                                                 System.Drawing.GraphicsUnit.Point,
                                                                                 ((byte)(0))),
                                                         System.Drawing.Color.Black,
                                                         System.Drawing.ContentAlignment.MiddleCenter,
                                                         System.Windows.Forms.ToolStripItemImageScaling.None,
                                                         System.Drawing.Color.Magenta,
                                                         System.Windows.Forms.RightToLeft.No, "Text Highlight Color",
                                                         System.Drawing.ContentAlignment.MiddleCenter,
                                                         System.Windows.Forms.ToolStripTextDirection.Horizontal,
                                                         System.Windows.Forms.TextImageRelation.ImageBeforeText,
                                                         System.Windows.Forms.ToolStripItemAlignment.Left,
                                                         new System.Drawing.Size(23, 26)));
            this.htmlEditor1.FactoryToolbarItems.Add(new SpiceLogic.WinHTMLEditor.FactoryItem("FACTORY_Font Color",
                                                                                              SpiceLogic.WinHTMLEditor
                                                                                                        .ToolStripHosts
                                                                                                        .BottomStrip,
                                                                                              true, true, true, true,
                                                                                              "Font Color",
                                                                                              ((System.Drawing.Image)
                                                                                               (resources.GetObject(
                                                                                                   "htmlEditor1.FactoryToolbarItems16"))),
                                                                                              System.Drawing.Color.Empty,
                                                                                              null,
                                                                                              System.Windows.Forms
                                                                                                    .ImageLayout.None,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemDisplayStyle
                                                                                                    .Image,
                                                                                              new System.Drawing.Font(
                                                                                                  "Microsoft Sans Serif",
                                                                                                  8.25F,
                                                                                                  System.Drawing
                                                                                                        .FontStyle
                                                                                                        .Regular,
                                                                                                  System.Drawing
                                                                                                        .GraphicsUnit
                                                                                                        .Point,
                                                                                                  ((byte)(0))),
                                                                                              System.Drawing.Color.Black,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemImageScaling
                                                                                                    .None,
                                                                                              System.Drawing.Color
                                                                                                    .Magenta,
                                                                                              System.Windows.Forms
                                                                                                    .RightToLeft.No,
                                                                                              "Font Color",
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripTextDirection
                                                                                                    .Horizontal,
                                                                                              System.Windows.Forms
                                                                                                    .TextImageRelation
                                                                                                    .ImageBeforeText,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemAlignment
                                                                                                    .Left,
                                                                                              new System.Drawing.Size(
                                                                                                  23, 26)));
            this.htmlEditor1.FactoryToolbarItems.Add(new SpiceLogic.WinHTMLEditor.FactoryItem("FACTORY_Separator4",
                                                                                              SpiceLogic.WinHTMLEditor
                                                                                                        .ToolStripHosts
                                                                                                        .BottomStrip,
                                                                                              true, true, true, true,
                                                                                              null, null,
                                                                                              System.Drawing.Color.Empty,
                                                                                              null,
                                                                                              System.Windows.Forms
                                                                                                    .ImageLayout.None,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemDisplayStyle
                                                                                                    .None,
                                                                                              new System.Drawing.Font(
                                                                                                  "Microsoft Sans Serif",
                                                                                                  8.25F,
                                                                                                  System.Drawing
                                                                                                        .FontStyle
                                                                                                        .Regular,
                                                                                                  System.Drawing
                                                                                                        .GraphicsUnit
                                                                                                        .Point,
                                                                                                  ((byte)(0))),
                                                                                              System.Drawing.Color.Black,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemImageScaling
                                                                                                    .SizeToFit,
                                                                                              System.Drawing.Color.Empty,
                                                                                              System.Windows.Forms
                                                                                                    .RightToLeft.No,
                                                                                              null,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripTextDirection
                                                                                                    .Horizontal,
                                                                                              System.Windows.Forms
                                                                                                    .TextImageRelation
                                                                                                    .ImageBeforeText,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemAlignment
                                                                                                    .Left,
                                                                                              new System.Drawing.Size(
                                                                                                  0, 0)));
            this.htmlEditor1.FactoryToolbarItems.Add(new SpiceLogic.WinHTMLEditor.FactoryItem("FACTORY_HyperLink",
                                                                                              SpiceLogic.WinHTMLEditor
                                                                                                        .ToolStripHosts
                                                                                                        .BottomStrip,
                                                                                              true, true, true, true,
                                                                                              "HyperLink",
                                                                                              ((System.Drawing.Image)
                                                                                               (resources.GetObject(
                                                                                                   "htmlEditor1.FactoryToolbarItems17"))),
                                                                                              System.Drawing.Color.Empty,
                                                                                              null,
                                                                                              System.Windows.Forms
                                                                                                    .ImageLayout.None,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemDisplayStyle
                                                                                                    .Image,
                                                                                              new System.Drawing.Font(
                                                                                                  "Microsoft Sans Serif",
                                                                                                  8.25F,
                                                                                                  System.Drawing
                                                                                                        .FontStyle
                                                                                                        .Regular,
                                                                                                  System.Drawing
                                                                                                        .GraphicsUnit
                                                                                                        .Point,
                                                                                                  ((byte)(0))),
                                                                                              System.Drawing.Color.Black,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemImageScaling
                                                                                                    .None,
                                                                                              System.Drawing.Color
                                                                                                    .Magenta,
                                                                                              System.Windows.Forms
                                                                                                    .RightToLeft.No,
                                                                                              "HyperLink",
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripTextDirection
                                                                                                    .Horizontal,
                                                                                              System.Windows.Forms
                                                                                                    .TextImageRelation
                                                                                                    .ImageBeforeText,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemAlignment
                                                                                                    .Left,
                                                                                              new System.Drawing.Size(
                                                                                                  23, 26)));
            this.htmlEditor1.FactoryToolbarItems.Add(new SpiceLogic.WinHTMLEditor.FactoryItem("FACTORY_Image",
                                                                                              SpiceLogic.WinHTMLEditor
                                                                                                        .ToolStripHosts
                                                                                                        .BottomStrip,
                                                                                              true, true, true, true,
                                                                                              "Image",
                                                                                              ((System.Drawing.Image)
                                                                                               (resources.GetObject(
                                                                                                   "htmlEditor1.FactoryToolbarItems18"))),
                                                                                              System.Drawing.Color.Empty,
                                                                                              null,
                                                                                              System.Windows.Forms
                                                                                                    .ImageLayout.None,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemDisplayStyle
                                                                                                    .Image,
                                                                                              new System.Drawing.Font(
                                                                                                  "Microsoft Sans Serif",
                                                                                                  8.25F,
                                                                                                  System.Drawing
                                                                                                        .FontStyle
                                                                                                        .Regular,
                                                                                                  System.Drawing
                                                                                                        .GraphicsUnit
                                                                                                        .Point,
                                                                                                  ((byte)(0))),
                                                                                              System.Drawing.Color.Black,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemImageScaling
                                                                                                    .None,
                                                                                              System.Drawing.Color
                                                                                                    .Magenta,
                                                                                              System.Windows.Forms
                                                                                                    .RightToLeft.No,
                                                                                              "Image",
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripTextDirection
                                                                                                    .Horizontal,
                                                                                              System.Windows.Forms
                                                                                                    .TextImageRelation
                                                                                                    .ImageBeforeText,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemAlignment
                                                                                                    .Left,
                                                                                              new System.Drawing.Size(
                                                                                                  24, 26)));
            this.htmlEditor1.FactoryToolbarItems.Add(new SpiceLogic.WinHTMLEditor.FactoryItem("FACTORY_Separator5",
                                                                                              SpiceLogic.WinHTMLEditor
                                                                                                        .ToolStripHosts
                                                                                                        .BottomStrip,
                                                                                              true, true, true, true,
                                                                                              null, null,
                                                                                              System.Drawing.Color.Empty,
                                                                                              null,
                                                                                              System.Windows.Forms
                                                                                                    .ImageLayout.None,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemDisplayStyle
                                                                                                    .None,
                                                                                              new System.Drawing.Font(
                                                                                                  "Microsoft Sans Serif",
                                                                                                  8.25F,
                                                                                                  System.Drawing
                                                                                                        .FontStyle
                                                                                                        .Regular,
                                                                                                  System.Drawing
                                                                                                        .GraphicsUnit
                                                                                                        .Point,
                                                                                                  ((byte)(0))),
                                                                                              System.Drawing.Color.Black,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemImageScaling
                                                                                                    .SizeToFit,
                                                                                              System.Drawing.Color.Empty,
                                                                                              System.Windows.Forms
                                                                                                    .RightToLeft.No,
                                                                                              null,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripTextDirection
                                                                                                    .Horizontal,
                                                                                              System.Windows.Forms
                                                                                                    .TextImageRelation
                                                                                                    .ImageBeforeText,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemAlignment
                                                                                                    .Left,
                                                                                              new System.Drawing.Size(
                                                                                                  0, 0)));
            this.htmlEditor1.FactoryToolbarItems.Add(
                new SpiceLogic.WinHTMLEditor.FactoryItem("FACTORY_Insert Ordered List",
                                                         SpiceLogic.WinHTMLEditor.ToolStripHosts.BottomStrip, true, true,
                                                         true, true, "Insert Ordered List",
                                                         ((System.Drawing.Image)
                                                          (resources.GetObject("htmlEditor1.FactoryToolbarItems19"))),
                                                         System.Drawing.Color.Empty, null,
                                                         System.Windows.Forms.ImageLayout.None,
                                                         System.Windows.Forms.ToolStripItemDisplayStyle.Image,
                                                         new System.Drawing.Font("Microsoft Sans Serif", 8.25F,
                                                                                 System.Drawing.FontStyle.Regular,
                                                                                 System.Drawing.GraphicsUnit.Point,
                                                                                 ((byte)(0))),
                                                         System.Drawing.Color.Black,
                                                         System.Drawing.ContentAlignment.MiddleCenter,
                                                         System.Windows.Forms.ToolStripItemImageScaling.None,
                                                         System.Drawing.Color.Magenta,
                                                         System.Windows.Forms.RightToLeft.No, "Insert Ordered List",
                                                         System.Drawing.ContentAlignment.MiddleCenter,
                                                         System.Windows.Forms.ToolStripTextDirection.Horizontal,
                                                         System.Windows.Forms.TextImageRelation.ImageBeforeText,
                                                         System.Windows.Forms.ToolStripItemAlignment.Left,
                                                         new System.Drawing.Size(24, 26)));
            this.htmlEditor1.FactoryToolbarItems.Add(
                new SpiceLogic.WinHTMLEditor.FactoryItem("FACTORY_Insert Unordered List",
                                                         SpiceLogic.WinHTMLEditor.ToolStripHosts.BottomStrip, true, true,
                                                         true, true, "Insert Unordered List",
                                                         ((System.Drawing.Image)
                                                          (resources.GetObject("htmlEditor1.FactoryToolbarItems20"))),
                                                         System.Drawing.Color.Empty, null,
                                                         System.Windows.Forms.ImageLayout.None,
                                                         System.Windows.Forms.ToolStripItemDisplayStyle.Image,
                                                         new System.Drawing.Font("Microsoft Sans Serif", 8.25F,
                                                                                 System.Drawing.FontStyle.Regular,
                                                                                 System.Drawing.GraphicsUnit.Point,
                                                                                 ((byte)(0))),
                                                         System.Drawing.Color.Black,
                                                         System.Drawing.ContentAlignment.MiddleCenter,
                                                         System.Windows.Forms.ToolStripItemImageScaling.None,
                                                         System.Drawing.Color.Magenta,
                                                         System.Windows.Forms.RightToLeft.No, "Insert Unordered List",
                                                         System.Drawing.ContentAlignment.MiddleCenter,
                                                         System.Windows.Forms.ToolStripTextDirection.Horizontal,
                                                         System.Windows.Forms.TextImageRelation.ImageBeforeText,
                                                         System.Windows.Forms.ToolStripItemAlignment.Left,
                                                         new System.Drawing.Size(24, 26)));
            this.htmlEditor1.FactoryToolbarItems.Add(new SpiceLogic.WinHTMLEditor.FactoryItem("FACTORY_Separator6",
                                                                                              SpiceLogic.WinHTMLEditor
                                                                                                        .ToolStripHosts
                                                                                                        .BottomStrip,
                                                                                              true, true, true, true,
                                                                                              null, null,
                                                                                              System.Drawing.Color.Empty,
                                                                                              null,
                                                                                              System.Windows.Forms
                                                                                                    .ImageLayout.None,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemDisplayStyle
                                                                                                    .None,
                                                                                              new System.Drawing.Font(
                                                                                                  "Microsoft Sans Serif",
                                                                                                  8.25F,
                                                                                                  System.Drawing
                                                                                                        .FontStyle
                                                                                                        .Regular,
                                                                                                  System.Drawing
                                                                                                        .GraphicsUnit
                                                                                                        .Point,
                                                                                                  ((byte)(0))),
                                                                                              System.Drawing.Color.Black,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemImageScaling
                                                                                                    .SizeToFit,
                                                                                              System.Drawing.Color.Empty,
                                                                                              System.Windows.Forms
                                                                                                    .RightToLeft.No,
                                                                                              null,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripTextDirection
                                                                                                    .Horizontal,
                                                                                              System.Windows.Forms
                                                                                                    .TextImageRelation
                                                                                                    .ImageBeforeText,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemAlignment
                                                                                                    .Left,
                                                                                              new System.Drawing.Size(
                                                                                                  0, 0)));
            this.htmlEditor1.FactoryToolbarItems.Add(new SpiceLogic.WinHTMLEditor.FactoryItem("FACTORY_Left",
                                                                                              SpiceLogic.WinHTMLEditor
                                                                                                        .ToolStripHosts
                                                                                                        .BottomStrip,
                                                                                              true, true, true, true,
                                                                                              "Left",
                                                                                              ((System.Drawing.Image)
                                                                                               (resources.GetObject(
                                                                                                   "htmlEditor1.FactoryToolbarItems21"))),
                                                                                              System.Drawing.Color.Empty,
                                                                                              null,
                                                                                              System.Windows.Forms
                                                                                                    .ImageLayout.None,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemDisplayStyle
                                                                                                    .Image,
                                                                                              new System.Drawing.Font(
                                                                                                  "Microsoft Sans Serif",
                                                                                                  8.25F,
                                                                                                  System.Drawing
                                                                                                        .FontStyle
                                                                                                        .Regular,
                                                                                                  System.Drawing
                                                                                                        .GraphicsUnit
                                                                                                        .Point,
                                                                                                  ((byte)(0))),
                                                                                              System.Drawing.Color.Black,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemImageScaling
                                                                                                    .None,
                                                                                              System.Drawing.Color
                                                                                                    .Magenta,
                                                                                              System.Windows.Forms
                                                                                                    .RightToLeft.No,
                                                                                              "Left",
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripTextDirection
                                                                                                    .Horizontal,
                                                                                              System.Windows.Forms
                                                                                                    .TextImageRelation
                                                                                                    .ImageBeforeText,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemAlignment
                                                                                                    .Left,
                                                                                              new System.Drawing.Size(
                                                                                                  26, 26)));
            this.htmlEditor1.FactoryToolbarItems.Add(new SpiceLogic.WinHTMLEditor.FactoryItem("FACTORY_Center",
                                                                                              SpiceLogic.WinHTMLEditor
                                                                                                        .ToolStripHosts
                                                                                                        .BottomStrip,
                                                                                              true, true, true, true,
                                                                                              "Center",
                                                                                              ((System.Drawing.Image)
                                                                                               (resources.GetObject(
                                                                                                   "htmlEditor1.FactoryToolbarItems22"))),
                                                                                              System.Drawing.Color.Empty,
                                                                                              null,
                                                                                              System.Windows.Forms
                                                                                                    .ImageLayout.None,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemDisplayStyle
                                                                                                    .Image,
                                                                                              new System.Drawing.Font(
                                                                                                  "Microsoft Sans Serif",
                                                                                                  8.25F,
                                                                                                  System.Drawing
                                                                                                        .FontStyle
                                                                                                        .Regular,
                                                                                                  System.Drawing
                                                                                                        .GraphicsUnit
                                                                                                        .Point,
                                                                                                  ((byte)(0))),
                                                                                              System.Drawing.Color.Black,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemImageScaling
                                                                                                    .None,
                                                                                              System.Drawing.Color
                                                                                                    .Magenta,
                                                                                              System.Windows.Forms
                                                                                                    .RightToLeft.No,
                                                                                              "Center",
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripTextDirection
                                                                                                    .Horizontal,
                                                                                              System.Windows.Forms
                                                                                                    .TextImageRelation
                                                                                                    .ImageBeforeText,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemAlignment
                                                                                                    .Left,
                                                                                              new System.Drawing.Size(
                                                                                                  26, 26)));
            this.htmlEditor1.FactoryToolbarItems.Add(new SpiceLogic.WinHTMLEditor.FactoryItem("FACTORY_Right",
                                                                                              SpiceLogic.WinHTMLEditor
                                                                                                        .ToolStripHosts
                                                                                                        .BottomStrip,
                                                                                              true, true, true, true,
                                                                                              "Right",
                                                                                              ((System.Drawing.Image)
                                                                                               (resources.GetObject(
                                                                                                   "htmlEditor1.FactoryToolbarItems23"))),
                                                                                              System.Drawing.Color.Empty,
                                                                                              null,
                                                                                              System.Windows.Forms
                                                                                                    .ImageLayout.None,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemDisplayStyle
                                                                                                    .Image,
                                                                                              new System.Drawing.Font(
                                                                                                  "Microsoft Sans Serif",
                                                                                                  8.25F,
                                                                                                  System.Drawing
                                                                                                        .FontStyle
                                                                                                        .Regular,
                                                                                                  System.Drawing
                                                                                                        .GraphicsUnit
                                                                                                        .Point,
                                                                                                  ((byte)(0))),
                                                                                              System.Drawing.Color.Black,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemImageScaling
                                                                                                    .None,
                                                                                              System.Drawing.Color
                                                                                                    .Magenta,
                                                                                              System.Windows.Forms
                                                                                                    .RightToLeft.No,
                                                                                              "Right",
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripTextDirection
                                                                                                    .Horizontal,
                                                                                              System.Windows.Forms
                                                                                                    .TextImageRelation
                                                                                                    .ImageBeforeText,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemAlignment
                                                                                                    .Left,
                                                                                              new System.Drawing.Size(
                                                                                                  26, 26)));
            this.htmlEditor1.FactoryToolbarItems.Add(new SpiceLogic.WinHTMLEditor.FactoryItem("FACTORY_Separator7",
                                                                                              SpiceLogic.WinHTMLEditor
                                                                                                        .ToolStripHosts
                                                                                                        .BottomStrip,
                                                                                              true, true, true, true,
                                                                                              null, null,
                                                                                              System.Drawing.Color.Empty,
                                                                                              null,
                                                                                              System.Windows.Forms
                                                                                                    .ImageLayout.None,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemDisplayStyle
                                                                                                    .None,
                                                                                              new System.Drawing.Font(
                                                                                                  "Microsoft Sans Serif",
                                                                                                  8.25F,
                                                                                                  System.Drawing
                                                                                                        .FontStyle
                                                                                                        .Regular,
                                                                                                  System.Drawing
                                                                                                        .GraphicsUnit
                                                                                                        .Point,
                                                                                                  ((byte)(0))),
                                                                                              System.Drawing.Color.Black,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemImageScaling
                                                                                                    .SizeToFit,
                                                                                              System.Drawing.Color.Empty,
                                                                                              System.Windows.Forms
                                                                                                    .RightToLeft.No,
                                                                                              null,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripTextDirection
                                                                                                    .Horizontal,
                                                                                              System.Windows.Forms
                                                                                                    .TextImageRelation
                                                                                                    .ImageBeforeText,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemAlignment
                                                                                                    .Left,
                                                                                              new System.Drawing.Size(
                                                                                                  0, 0)));
            this.htmlEditor1.FactoryToolbarItems.Add(new SpiceLogic.WinHTMLEditor.FactoryItem("FACTORY_Table",
                                                                                              SpiceLogic.WinHTMLEditor
                                                                                                        .ToolStripHosts
                                                                                                        .BottomStrip,
                                                                                              true, true, true, true,
                                                                                              "Table",
                                                                                              ((System.Drawing.Image)
                                                                                               (resources.GetObject(
                                                                                                   "htmlEditor1.FactoryToolbarItems24"))),
                                                                                              System.Drawing.Color.Empty,
                                                                                              null,
                                                                                              System.Windows.Forms
                                                                                                    .ImageLayout.None,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemDisplayStyle
                                                                                                    .Image,
                                                                                              new System.Drawing.Font(
                                                                                                  "Microsoft Sans Serif",
                                                                                                  8.25F,
                                                                                                  System.Drawing
                                                                                                        .FontStyle
                                                                                                        .Regular,
                                                                                                  System.Drawing
                                                                                                        .GraphicsUnit
                                                                                                        .Point,
                                                                                                  ((byte)(0))),
                                                                                              System.Drawing.Color.Black,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemImageScaling
                                                                                                    .None,
                                                                                              System.Drawing.Color
                                                                                                    .Magenta,
                                                                                              System.Windows.Forms
                                                                                                    .RightToLeft.No,
                                                                                              "Table",
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripTextDirection
                                                                                                    .Horizontal,
                                                                                              System.Windows.Forms
                                                                                                    .TextImageRelation
                                                                                                    .ImageBeforeText,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemAlignment
                                                                                                    .Left,
                                                                                              new System.Drawing.Size(
                                                                                                  24, 26)));
            this.htmlEditor1.FactoryToolbarItems.Add(new SpiceLogic.WinHTMLEditor.FactoryItem("FACTORY_Separator8",
                                                                                              SpiceLogic.WinHTMLEditor
                                                                                                        .ToolStripHosts
                                                                                                        .BottomStrip,
                                                                                              true, true, true, true,
                                                                                              null, null,
                                                                                              System.Drawing.Color.Empty,
                                                                                              null,
                                                                                              System.Windows.Forms
                                                                                                    .ImageLayout.None,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemDisplayStyle
                                                                                                    .None,
                                                                                              new System.Drawing.Font(
                                                                                                  "Microsoft Sans Serif",
                                                                                                  8.25F,
                                                                                                  System.Drawing
                                                                                                        .FontStyle
                                                                                                        .Regular,
                                                                                                  System.Drawing
                                                                                                        .GraphicsUnit
                                                                                                        .Point,
                                                                                                  ((byte)(0))),
                                                                                              System.Drawing.Color.Black,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemImageScaling
                                                                                                    .SizeToFit,
                                                                                              System.Drawing.Color.Empty,
                                                                                              System.Windows.Forms
                                                                                                    .RightToLeft.No,
                                                                                              null,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripTextDirection
                                                                                                    .Horizontal,
                                                                                              System.Windows.Forms
                                                                                                    .TextImageRelation
                                                                                                    .ImageBeforeText,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemAlignment
                                                                                                    .Left,
                                                                                              new System.Drawing.Size(
                                                                                                  0, 0)));
            this.htmlEditor1.FactoryToolbarItems.Add(new SpiceLogic.WinHTMLEditor.FactoryItem("FACTORY_Outdent",
                                                                                              SpiceLogic.WinHTMLEditor
                                                                                                        .ToolStripHosts
                                                                                                        .BottomStrip,
                                                                                              true, true, true, true,
                                                                                              "Outdent",
                                                                                              ((System.Drawing.Image)
                                                                                               (resources.GetObject(
                                                                                                   "htmlEditor1.FactoryToolbarItems25"))),
                                                                                              System.Drawing.Color.Empty,
                                                                                              null,
                                                                                              System.Windows.Forms
                                                                                                    .ImageLayout.None,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemDisplayStyle
                                                                                                    .Image,
                                                                                              new System.Drawing.Font(
                                                                                                  "Microsoft Sans Serif",
                                                                                                  8.25F,
                                                                                                  System.Drawing
                                                                                                        .FontStyle
                                                                                                        .Regular,
                                                                                                  System.Drawing
                                                                                                        .GraphicsUnit
                                                                                                        .Point,
                                                                                                  ((byte)(0))),
                                                                                              System.Drawing.Color.Black,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemImageScaling
                                                                                                    .None,
                                                                                              System.Drawing.Color
                                                                                                    .Magenta,
                                                                                              System.Windows.Forms
                                                                                                    .RightToLeft.No,
                                                                                              "Outdent",
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripTextDirection
                                                                                                    .Horizontal,
                                                                                              System.Windows.Forms
                                                                                                    .TextImageRelation
                                                                                                    .ImageBeforeText,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemAlignment
                                                                                                    .Left,
                                                                                              new System.Drawing.Size(
                                                                                                  24, 26)));
            this.htmlEditor1.FactoryToolbarItems.Add(new SpiceLogic.WinHTMLEditor.FactoryItem("FACTORY_Indent",
                                                                                              SpiceLogic.WinHTMLEditor
                                                                                                        .ToolStripHosts
                                                                                                        .BottomStrip,
                                                                                              true, true, true, true,
                                                                                              "Indent",
                                                                                              ((System.Drawing.Image)
                                                                                               (resources.GetObject(
                                                                                                   "htmlEditor1.FactoryToolbarItems26"))),
                                                                                              System.Drawing.Color.Empty,
                                                                                              null,
                                                                                              System.Windows.Forms
                                                                                                    .ImageLayout.None,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemDisplayStyle
                                                                                                    .Image,
                                                                                              new System.Drawing.Font(
                                                                                                  "Microsoft Sans Serif",
                                                                                                  8.25F,
                                                                                                  System.Drawing
                                                                                                        .FontStyle
                                                                                                        .Regular,
                                                                                                  System.Drawing
                                                                                                        .GraphicsUnit
                                                                                                        .Point,
                                                                                                  ((byte)(0))),
                                                                                              System.Drawing.Color.Black,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemImageScaling
                                                                                                    .None,
                                                                                              System.Drawing.Color
                                                                                                    .Magenta,
                                                                                              System.Windows.Forms
                                                                                                    .RightToLeft.No,
                                                                                              "Indent",
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripTextDirection
                                                                                                    .Horizontal,
                                                                                              System.Windows.Forms
                                                                                                    .TextImageRelation
                                                                                                    .ImageBeforeText,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemAlignment
                                                                                                    .Left,
                                                                                              new System.Drawing.Size(
                                                                                                  24, 26)));
            this.htmlEditor1.FactoryToolbarItems.Add(new SpiceLogic.WinHTMLEditor.FactoryItem("FACTORY_Separator9",
                                                                                              SpiceLogic.WinHTMLEditor
                                                                                                        .ToolStripHosts
                                                                                                        .BottomStrip,
                                                                                              true, true, true, true,
                                                                                              null, null,
                                                                                              System.Drawing.Color.Empty,
                                                                                              null,
                                                                                              System.Windows.Forms
                                                                                                    .ImageLayout.None,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemDisplayStyle
                                                                                                    .None,
                                                                                              new System.Drawing.Font(
                                                                                                  "Microsoft Sans Serif",
                                                                                                  8.25F,
                                                                                                  System.Drawing
                                                                                                        .FontStyle
                                                                                                        .Regular,
                                                                                                  System.Drawing
                                                                                                        .GraphicsUnit
                                                                                                        .Point,
                                                                                                  ((byte)(0))),
                                                                                              System.Drawing.Color.Black,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemImageScaling
                                                                                                    .SizeToFit,
                                                                                              System.Drawing.Color.Empty,
                                                                                              System.Windows.Forms
                                                                                                    .RightToLeft.No,
                                                                                              null,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripTextDirection
                                                                                                    .Horizontal,
                                                                                              System.Windows.Forms
                                                                                                    .TextImageRelation
                                                                                                    .ImageBeforeText,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemAlignment
                                                                                                    .Left,
                                                                                              new System.Drawing.Size(
                                                                                                  0, 0)));
            this.htmlEditor1.FactoryToolbarItems.Add(new SpiceLogic.WinHTMLEditor.FactoryItem("FACTORY_Strike through",
                                                                                              SpiceLogic.WinHTMLEditor
                                                                                                        .ToolStripHosts
                                                                                                        .BottomStrip,
                                                                                              true, true, true, true,
                                                                                              "Strike through",
                                                                                              ((System.Drawing.Image)
                                                                                               (resources.GetObject(
                                                                                                   "htmlEditor1.FactoryToolbarItems27"))),
                                                                                              System.Drawing.Color.Empty,
                                                                                              null,
                                                                                              System.Windows.Forms
                                                                                                    .ImageLayout.None,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemDisplayStyle
                                                                                                    .Image,
                                                                                              new System.Drawing.Font(
                                                                                                  "Microsoft Sans Serif",
                                                                                                  8.25F,
                                                                                                  System.Drawing
                                                                                                        .FontStyle
                                                                                                        .Regular,
                                                                                                  System.Drawing
                                                                                                        .GraphicsUnit
                                                                                                        .Point,
                                                                                                  ((byte)(0))),
                                                                                              System.Drawing.Color.Black,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemImageScaling
                                                                                                    .None,
                                                                                              System.Drawing.Color
                                                                                                    .Magenta,
                                                                                              System.Windows.Forms
                                                                                                    .RightToLeft.No,
                                                                                              "Strike through",
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripTextDirection
                                                                                                    .Horizontal,
                                                                                              System.Windows.Forms
                                                                                                    .TextImageRelation
                                                                                                    .ImageBeforeText,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemAlignment
                                                                                                    .Left,
                                                                                              new System.Drawing.Size(
                                                                                                  24, 26)));
            this.htmlEditor1.FactoryToolbarItems.Add(new SpiceLogic.WinHTMLEditor.FactoryItem("FACTORY_SuperScript",
                                                                                              SpiceLogic.WinHTMLEditor
                                                                                                        .ToolStripHosts
                                                                                                        .BottomStrip,
                                                                                              true, true, true, true,
                                                                                              "SuperScript",
                                                                                              ((System.Drawing.Image)
                                                                                               (resources.GetObject(
                                                                                                   "htmlEditor1.FactoryToolbarItems28"))),
                                                                                              System.Drawing.Color.Empty,
                                                                                              null,
                                                                                              System.Windows.Forms
                                                                                                    .ImageLayout.None,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemDisplayStyle
                                                                                                    .Image,
                                                                                              new System.Drawing.Font(
                                                                                                  "Microsoft Sans Serif",
                                                                                                  8.25F,
                                                                                                  System.Drawing
                                                                                                        .FontStyle
                                                                                                        .Regular,
                                                                                                  System.Drawing
                                                                                                        .GraphicsUnit
                                                                                                        .Point,
                                                                                                  ((byte)(0))),
                                                                                              System.Drawing.Color.Black,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemImageScaling
                                                                                                    .None,
                                                                                              System.Drawing.Color
                                                                                                    .Magenta,
                                                                                              System.Windows.Forms
                                                                                                    .RightToLeft.No,
                                                                                              "SuperScript",
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripTextDirection
                                                                                                    .Horizontal,
                                                                                              System.Windows.Forms
                                                                                                    .TextImageRelation
                                                                                                    .ImageBeforeText,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemAlignment
                                                                                                    .Left,
                                                                                              new System.Drawing.Size(
                                                                                                  27, 26)));
            this.htmlEditor1.FactoryToolbarItems.Add(new SpiceLogic.WinHTMLEditor.FactoryItem("FACTORY_Subscript",
                                                                                              SpiceLogic.WinHTMLEditor
                                                                                                        .ToolStripHosts
                                                                                                        .BottomStrip,
                                                                                              true, true, true, true,
                                                                                              "Subscript",
                                                                                              ((System.Drawing.Image)
                                                                                               (resources.GetObject(
                                                                                                   "htmlEditor1.FactoryToolbarItems29"))),
                                                                                              System.Drawing.Color.Empty,
                                                                                              null,
                                                                                              System.Windows.Forms
                                                                                                    .ImageLayout.None,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemDisplayStyle
                                                                                                    .Image,
                                                                                              new System.Drawing.Font(
                                                                                                  "Microsoft Sans Serif",
                                                                                                  8.25F,
                                                                                                  System.Drawing
                                                                                                        .FontStyle
                                                                                                        .Regular,
                                                                                                  System.Drawing
                                                                                                        .GraphicsUnit
                                                                                                        .Point,
                                                                                                  ((byte)(0))),
                                                                                              System.Drawing.Color.Black,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemImageScaling
                                                                                                    .None,
                                                                                              System.Drawing.Color
                                                                                                    .Magenta,
                                                                                              System.Windows.Forms
                                                                                                    .RightToLeft.No,
                                                                                              "Subscript",
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripTextDirection
                                                                                                    .Horizontal,
                                                                                              System.Windows.Forms
                                                                                                    .TextImageRelation
                                                                                                    .ImageBeforeText,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemAlignment
                                                                                                    .Left,
                                                                                              new System.Drawing.Size(
                                                                                                  27, 26)));
            this.htmlEditor1.FactoryToolbarItems.Add(new SpiceLogic.WinHTMLEditor.FactoryItem("FACTORY_Body Style",
                                                                                              SpiceLogic.WinHTMLEditor
                                                                                                        .ToolStripHosts
                                                                                                        .BottomStrip,
                                                                                              true, true, true, true,
                                                                                              "Body Style",
                                                                                              ((System.Drawing.Image)
                                                                                               (resources.GetObject(
                                                                                                   "htmlEditor1.FactoryToolbarItems30"))),
                                                                                              System.Drawing.Color.Empty,
                                                                                              null,
                                                                                              System.Windows.Forms
                                                                                                    .ImageLayout.None,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemDisplayStyle
                                                                                                    .Image,
                                                                                              new System.Drawing.Font(
                                                                                                  "Microsoft Sans Serif",
                                                                                                  8.25F,
                                                                                                  System.Drawing
                                                                                                        .FontStyle
                                                                                                        .Regular,
                                                                                                  System.Drawing
                                                                                                        .GraphicsUnit
                                                                                                        .Point,
                                                                                                  ((byte)(0))),
                                                                                              System.Drawing.Color.Black,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemImageScaling
                                                                                                    .None,
                                                                                              System.Drawing.Color
                                                                                                    .Magenta,
                                                                                              System.Windows.Forms
                                                                                                    .RightToLeft.No,
                                                                                              "Body Style",
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripTextDirection
                                                                                                    .Horizontal,
                                                                                              System.Windows.Forms
                                                                                                    .TextImageRelation
                                                                                                    .ImageBeforeText,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemAlignment
                                                                                                    .Left,
                                                                                              new System.Drawing.Size(
                                                                                                  34, 26)));
            this.htmlEditor1.FactoryToolbarItems.Add(new SpiceLogic.WinHTMLEditor.FactoryItem("FACTORY_Print",
                                                                                              SpiceLogic.WinHTMLEditor
                                                                                                        .ToolStripHosts
                                                                                                        .BottomStrip,
                                                                                              true, true, true, true,
                                                                                              "Print",
                                                                                              ((System.Drawing.Image)
                                                                                               (resources.GetObject(
                                                                                                   "htmlEditor1.FactoryToolbarItems31"))),
                                                                                              System.Drawing.Color.Empty,
                                                                                              null,
                                                                                              System.Windows.Forms
                                                                                                    .ImageLayout.None,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemDisplayStyle
                                                                                                    .Image,
                                                                                              new System.Drawing.Font(
                                                                                                  "Microsoft Sans Serif",
                                                                                                  8.25F,
                                                                                                  System.Drawing
                                                                                                        .FontStyle
                                                                                                        .Regular,
                                                                                                  System.Drawing
                                                                                                        .GraphicsUnit
                                                                                                        .Point,
                                                                                                  ((byte)(0))),
                                                                                              System.Drawing.Color.Black,
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemImageScaling
                                                                                                    .None,
                                                                                              System.Drawing.Color
                                                                                                    .Magenta,
                                                                                              System.Windows.Forms
                                                                                                    .RightToLeft.No,
                                                                                              "Print",
                                                                                              System.Drawing
                                                                                                    .ContentAlignment
                                                                                                    .MiddleCenter,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripTextDirection
                                                                                                    .Horizontal,
                                                                                              System.Windows.Forms
                                                                                                    .TextImageRelation
                                                                                                    .ImageBeforeText,
                                                                                              System.Windows.Forms
                                                                                                    .ToolStripItemAlignment
                                                                                                    .Left,
                                                                                              new System.Drawing.Size(
                                                                                                  23, 26)));
            this.htmlEditor1.LicenseKey = "CD11-B720-A0FE-DE37-7F09-61E9-5314-C0D9";
            this.htmlEditor1.Location = new System.Drawing.Point(5, 227);
            this.htmlEditor1.Name = "htmlEditor1";
            this.htmlEditor1.ScrollBars = SpiceLogic.WinHTMLEditor.ScrollBarVisibility.Default;
            this.htmlEditor1.Size = new System.Drawing.Size(800, 127);
            // 
            // 
            // 
            this.htmlEditor1.SpellCheckDictionary.DictionaryFile = "en-US.dic";
            // 
            // 
            // 
            this.htmlEditor1.SpellChecker.Dictionary = this.htmlEditor1.SpellCheckDictionary;
            this.htmlEditor1.TabIndex = 15;
            // 
            // htmlEditor1.HtmlEditorToolbar1
            // 
            this.htmlEditor1.Toolbar1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.htmlEditor1.Toolbar1.Location = new System.Drawing.Point(0, 0);
            this.htmlEditor1.Toolbar1.Name = "HtmlEditorToolbar1";
            this.htmlEditor1.Toolbar1.Size = new System.Drawing.Size(800, 27);
            this.htmlEditor1.Toolbar1.TabIndex = 0;
            this.htmlEditor1.Toolbar1.Text = "toolStrip1";
            this.htmlEditor1.Toolbar1.Visible = false;
            // 
            // htmlEditor1.HtmlEditorToolbar2
            // 
            this.htmlEditor1.Toolbar2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.htmlEditor1.Toolbar2.Location = new System.Drawing.Point(0, 0);
            this.htmlEditor1.Toolbar2.Name = "HtmlEditorToolbar2";
            this.htmlEditor1.Toolbar2.Size = new System.Drawing.Size(800, 29);
            this.htmlEditor1.Toolbar2.TabIndex = 1;
            this.htmlEditor1.Toolbar2.Text = "toolStrip2";
            this.htmlEditor1.Toolbar2.Visible = false;
            this.htmlEditor1.ToolbarContextMenuStrip = null;
            this.htmlEditor1.ToolbarCursor = System.Windows.Forms.Cursors.Default;
            // 
            // previewHandlerControl
            // 
            this.previewHandlerControl.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                   | System.Windows.Forms.AnchorStyles.Right)));
            this.previewHandlerControl.BackColor = System.Drawing.Color.White;
            this.previewHandlerControl.Location = new System.Drawing.Point(5, 360);
            this.previewHandlerControl.Name = "previewHandlerControl";
            this.previewHandlerControl.Size = new System.Drawing.Size(800, 1);
            this.previewHandlerControl.TabIndex = 19;
            this.previewHandlerControl.Visible = false;
            // 
            // embeddedMsg1
            // 
            this.embeddedMsg1.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                   | System.Windows.Forms.AnchorStyles.Right)));
            this.embeddedMsg1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.embeddedMsg1.Location = new System.Drawing.Point(5, 367);
            this.embeddedMsg1.Name = "embeddedMsg1";
            this.embeddedMsg1.Size = new System.Drawing.Size(800, 9);
            this.embeddedMsg1.TabIndex = 20;
            this.embeddedMsg1.Visible = false;
            // 
            // inspectorHdr
            // 
            this.inspectorHdr.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                   | System.Windows.Forms.AnchorStyles.Right)));
            this.inspectorHdr.AutoSize = true;
            this.inspectorHdr.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.inspectorHdr.Location = new System.Drawing.Point(5, 7);
            this.inspectorHdr.MinimumSize = new System.Drawing.Size(400, 60);
            this.inspectorHdr.Name = "inspectorHdr";
            this.inspectorHdr.Size = new System.Drawing.Size(800, 60);
            this.inspectorHdr.TabIndex = 21;
            // 
            // msgHdr15
            // 
            this.msgHdr15.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                   | System.Windows.Forms.AnchorStyles.Right)));
            this.msgHdr15.AutoSize = true;
            this.msgHdr15.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.msgHdr15.Location = new System.Drawing.Point(5, 73);
            this.msgHdr15.MinimumSize = new System.Drawing.Size(400, 0);
            this.msgHdr15.Name = "msgHdr15";
            this.msgHdr15.Size = new System.Drawing.Size(800, 120);
            this.msgHdr15.TabIndex = 22;
            // 
            // mnuAttach
            // 
            this.mnuAttach.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
                {
                    this.previewToolStripMenuItem,
                    this.mnuSep1,
                    this.openToolStripMenuItem,
                    this.saveToolStripMenuItem
                });
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
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
                {
                    this.useDefaultApplicationToolStripMenuItem,
                    this.browseForEditorToolStripMenuItem
                });
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
            this.useDefaultApplicationToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.useDefaultApplicationToolStripMenuItem.Text = "Use default application ";
            this.useDefaultApplicationToolStripMenuItem.Click +=
                new System.EventHandler(this.UseDefaultApplicationToolStripMenuItemClick);
            // 
            // browseForEditorToolStripMenuItem
            // 
            this.browseForEditorToolStripMenuItem.Name = "browseForEditorToolStripMenuItem";
            this.browseForEditorToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.browseForEditorToolStripMenuItem.Text = "Browse for editor...";
            this.browseForEditorToolStripMenuItem.Click +=
                new System.EventHandler(this.BrowseForEditorToolStripMenuItemClick);
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
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // DynamicInspector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Controls.Add(this.txtSent);
            this.Controls.Add(this.txtFrom);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular,
                                                System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "DynamicInspector";
            this.Size = new System.Drawing.Size(816, 387);
            this.FormRegionShowing += new System.EventHandler(this.DynamicInspectorFormRegionShowing);
            this.FormRegionClosed += new System.EventHandler(this.DynamicInspectorFormRegionClosed);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.PerformLayout();
            this.tableLayoutAttach.ResumeLayout(false);
            this.tableLayoutAttach.PerformLayout();
            this.panelAttach.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.htmlEditor1)).EndInit();
            this.htmlEditor1.ResumeLayout(false);
            this.htmlEditor1.PerformLayout();
            this.mnuAttach.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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
            System.ComponentModel.ComponentResourceManager resources =
                new System.ComponentModel.ComponentResourceManager(typeof(DynamicInspector));
            manifest.ExactMessageClass = true;
            manifest.FormRegionName = "ECS Message Inspector";
            manifest.FormRegionType = Microsoft.Office.Tools.Outlook.FormRegionType.Replacement;
            manifest.Icons.Default =
                ((System.Drawing.Icon)(resources.GetObject("DynamicInspector.Manifest.Icons.Default")));
            manifest.Icons.Forwarded =
                ((System.Drawing.Icon)(resources.GetObject("DynamicInspector.Manifest.Icons.Forwarded")));
            manifest.Icons.Read = ((System.Drawing.Icon)(resources.GetObject("DynamicInspector.Manifest.Icons.Read")));
            manifest.Icons.Replied =
                ((System.Drawing.Icon)(resources.GetObject("DynamicInspector.Manifest.Icons.Replied")));
            manifest.Icons.Unread =
                ((System.Drawing.Icon)(resources.GetObject("DynamicInspector.Manifest.Icons.Unread")));
            manifest.ShowInspectorCompose = false;
            manifest.ShowReadingPane = false;
            manifest.Title = "ECS Message";

        }

        #endregion

        private System.Windows.Forms.TextBox txtFrom;
        private System.Windows.Forms.TextBox txtSent;
        //private HtmlEditorControl.HtmlEditorControl editor;
        internal System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        internal SpiceLogic.WinHTMLEditor.HTMLEditor htmlEditor1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutAttach;
        private AttachPanel btnMessage;
        private System.Windows.Forms.Panel panelAttach;
        private System.Windows.Forms.Panel panelVertLine;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ContextMenuStrip mnuAttach;
        private System.Windows.Forms.ToolStripMenuItem previewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator mnuSep1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private PreviewHandlerControl previewHandlerControl;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripMenuItem useDefaultApplicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem browseForEditorToolStripMenuItem;
        private EmbeddedMsg embeddedMsg1;
        private InspectorHeader inspectorHdr;
        private MessageHeader2013 msgHdr15;

        public partial class DynamicInspectorFactory : Microsoft.Office.Tools.Outlook.IFormRegionFactory
        {
            public event Microsoft.Office.Tools.Outlook.FormRegionInitializingEventHandler FormRegionInitializing;

            private Microsoft.Office.Tools.Outlook.FormRegionManifest _Manifest;

            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public DynamicInspectorFactory()
            {
                this._Manifest = Globals.Factory.CreateFormRegionManifest();
                DynamicInspector.InitializeManifest(this._Manifest, Globals.Factory);
                this.FormRegionInitializing +=
                    new Microsoft.Office.Tools.Outlook.FormRegionInitializingEventHandler(
                        this.DynamicInspectorFactory_FormRegionInitializing);
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
                DynamicInspector form = new DynamicInspector(formRegion);
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
        internal DynamicInspector DynamicInspector
        {
            get
            {
                foreach (var item in this)
                {
                    if (item.GetType() == typeof(DynamicInspector))
                        return (DynamicInspector)item;
                }
                return null;
            }
        }
    }
}
