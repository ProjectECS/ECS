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
            System.ComponentModel.ComponentResourceManager resources =
                new System.ComponentModel.ComponentResourceManager(typeof(DynamicReadingPane));
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.htmlEditor1 = new SpiceLogic.WinHTMLEditor.HTMLEditor();
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
            this.tableLayoutPanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.htmlEditor1)).BeginInit();
            this.htmlEditor1.SuspendLayout();
            this.tableLayoutAttach.SuspendLayout();
            this.panelAttach.SuspendLayout();
            this.mnuAttach.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 4;
            this.tableLayoutPanelMain.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 68F));
            this.tableLayoutPanelMain.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 68F));
            this.tableLayoutPanelMain.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanelMain.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelMain.Controls.Add(this.btnEdit, 1, 0);
            this.tableLayoutPanelMain.Controls.Add(this.btnDelete, 2, 0);
            this.tableLayoutPanelMain.Controls.Add(this.htmlEditor1, 0, 4);
            this.tableLayoutPanelMain.Controls.Add(this.previewHandlerControl, 0, 5);
            this.tableLayoutPanelMain.Controls.Add(this.embeddedMsg1, 0, 6);
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutAttach, 0, 3);
            this.tableLayoutPanelMain.Controls.Add(this.messageHdr14, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.messageHdr15, 0, 2);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular,
                                                                     System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
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
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(737, 428);
            this.tableLayoutPanelMain.TabIndex = 0;
            this.tableLayoutPanelMain.Paint += new System.Windows.Forms.PaintEventHandler(this.TableLayoutPanel1Paint);
            // 
            // btnEdit
            // 
            this.btnEdit.AutoSize = true;
            this.btnEdit.Enabled = false;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular,
                                                        System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular,
                                                          System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.tableLayoutPanelMain.SetColumnSpan(this.htmlEditor1, 3);
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
            this.htmlEditor1.InsertSpaceOnTabKey = true;
            this.htmlEditor1.LicenseKey = "CD11-B720-A0FE-DE37-7F09-61E9-5314-C0D9";
            this.htmlEditor1.Location = new System.Drawing.Point(3, 244);
            this.htmlEditor1.Name = "htmlEditor1";
            this.htmlEditor1.ScrollBars = SpiceLogic.WinHTMLEditor.ScrollBarVisibility.Default;
            this.htmlEditor1.Size = new System.Drawing.Size(726, 166);
            // 
            // 
            // 
            this.htmlEditor1.SpellCheckDictionary.DictionaryFile = "en-US.dic";
            // 
            // 
            // 
            this.htmlEditor1.SpellChecker.Dictionary = this.htmlEditor1.SpellCheckDictionary;
            this.htmlEditor1.TabIndex = 1;
            // 
            // htmlEditor1.HtmlEditorToolbar1
            // 
            this.htmlEditor1.Toolbar1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.htmlEditor1.Toolbar1.Location = new System.Drawing.Point(0, 0);
            this.htmlEditor1.Toolbar1.Name = "HtmlEditorToolbar1";
            this.htmlEditor1.Toolbar1.Size = new System.Drawing.Size(726, 27);
            this.htmlEditor1.Toolbar1.TabIndex = 0;
            this.htmlEditor1.Toolbar1.Text = "toolStrip1";
            this.htmlEditor1.Toolbar1.Visible = false;
            // 
            // htmlEditor1.HtmlEditorToolbar2
            // 
            this.htmlEditor1.Toolbar2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.htmlEditor1.Toolbar2.Location = new System.Drawing.Point(0, 0);
            this.htmlEditor1.Toolbar2.Name = "HtmlEditorToolbar2";
            this.htmlEditor1.Toolbar2.Size = new System.Drawing.Size(726, 29);
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
            this.tableLayoutPanelMain.SetColumnSpan(this.previewHandlerControl, 3);
            this.previewHandlerControl.Location = new System.Drawing.Point(3, 416);
            this.previewHandlerControl.Name = "previewHandlerControl";
            this.previewHandlerControl.Size = new System.Drawing.Size(726, 1);
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
            this.tableLayoutPanelMain.SetColumnSpan(this.embeddedMsg1, 3);
            this.embeddedMsg1.Location = new System.Drawing.Point(3, 423);
            this.embeddedMsg1.Name = "embeddedMsg1";
            this.embeddedMsg1.Size = new System.Drawing.Size(726, 2);
            this.embeddedMsg1.TabIndex = 20;
            this.embeddedMsg1.Visible = false;
            // 
            // tableLayoutAttach
            // 
            this.tableLayoutAttach.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                   | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutAttach.ColumnCount = 2;
            this.tableLayoutPanelMain.SetColumnSpan(this.tableLayoutAttach, 3);
            this.tableLayoutAttach.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutAttach.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutAttach.ColumnStyles.Add(
                new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutAttach.Controls.Add(this.btnMessage, 0, 0);
            this.tableLayoutAttach.Controls.Add(this.panelAttach, 1, 0);
            this.tableLayoutAttach.Location = new System.Drawing.Point(2, 217);
            this.tableLayoutAttach.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutAttach.Name = "tableLayoutAttach";
            this.tableLayoutAttach.RowCount = 2;
            this.tableLayoutAttach.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutAttach.RowStyles.Add(
                new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
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
            this.panelAttach.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
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
            this.panelVertLine.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
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
            this.messageHdr14.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
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
            this.messageHdr15.Anchor =
                ((System.Windows.Forms.AnchorStyles)
                 (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
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
            this.useDefaultApplicationToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.useDefaultApplicationToolStripMenuItem.Text = "Use default application";
            this.useDefaultApplicationToolStripMenuItem.Click +=
                new System.EventHandler(this.UseDefaultApplicationToolStripMenuItemClick);
            // 
            // browseForEditorToolStripMenuItem
            // 
            this.browseForEditorToolStripMenuItem.Name = "browseForEditorToolStripMenuItem";
            this.browseForEditorToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
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
            // DynamicReadingPane
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular,
                                                System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "DynamicReadingPane";
            this.Size = new System.Drawing.Size(737, 428);
            this.FormRegionShowing += new System.EventHandler(this.DynamicReadingPaneFormRegionShowing);
            this.FormRegionClosed += new System.EventHandler(this.DynamicReadingPaneFormRegionClosed);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.htmlEditor1)).EndInit();
            this.htmlEditor1.ResumeLayout(false);
            this.htmlEditor1.PerformLayout();
            this.tableLayoutAttach.ResumeLayout(false);
            this.tableLayoutAttach.PerformLayout();
            this.panelAttach.ResumeLayout(false);
            this.mnuAttach.ResumeLayout(false);
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
            System.ComponentModel.ComponentResourceManager resources =
                new System.ComponentModel.ComponentResourceManager(typeof(DynamicReadingPane));
            manifest.ExactMessageClass = true;
            manifest.FormRegionName = "ECS Message";
            manifest.FormRegionType = Microsoft.Office.Tools.Outlook.FormRegionType.Replacement;
            manifest.Icons.Default =
                ((System.Drawing.Icon)(resources.GetObject("DynamicReadingPane.Manifest.Icons.Default")));
            manifest.Icons.Forwarded =
                ((System.Drawing.Icon)(resources.GetObject("DynamicReadingPane.Manifest.Icons.Forwarded")));
            manifest.Icons.Read =
                ((System.Drawing.Icon)(resources.GetObject("DynamicReadingPane.Manifest.Icons.Read")));
            manifest.Icons.Replied =
                ((System.Drawing.Icon)(resources.GetObject("DynamicReadingPane.Manifest.Icons.Replied")));
            manifest.Icons.Unread =
                ((System.Drawing.Icon)(resources.GetObject("DynamicReadingPane.Manifest.Icons.Unread")));
            manifest.ShowInspectorCompose = false;
            manifest.ShowInspectorRead = false;
            manifest.Title = "ECS Message";

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private SpiceLogic.WinHTMLEditor.HTMLEditor htmlEditor1;
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
