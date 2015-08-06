namespace ChiaraMail.Controls
{
    partial class EmbeddedMsg
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.panelAttach = new System.Windows.Forms.Panel();
            this.lblAttach = new System.Windows.Forms.Label();
            this.wb1 = new System.Windows.Forms.WebBrowser();
            this.msgHdr15 = new ChiaraMail.Controls.MessageHeader2013();
            this.msgHdr14 = new ChiaraMail.Controls.MessageHeader();
            this.tableLayoutPanelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 3;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanelMain.Controls.Add(this.panelAttach, 1, 3);
            this.tableLayoutPanelMain.Controls.Add(this.lblAttach, 0, 3);
            this.tableLayoutPanelMain.Controls.Add(this.wb1, 0, 4);
            this.tableLayoutPanelMain.Controls.Add(this.msgHdr15, 0, 2);
            this.tableLayoutPanelMain.Controls.Add(this.msgHdr14, 0, 1);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 6;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 2F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(750, 526);
            this.tableLayoutPanelMain.TabIndex = 2;
            // 
            // panelAttach
            // 
            this.panelAttach.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelAttach.AutoScroll = true;
            this.panelAttach.Location = new System.Drawing.Point(80, 215);
            this.panelAttach.Margin = new System.Windows.Forms.Padding(0);
            this.panelAttach.Name = "panelAttach";
            this.panelAttach.Size = new System.Drawing.Size(665, 20);
            this.panelAttach.TabIndex = 20;
            // 
            // lblAttach
            // 
            this.lblAttach.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAttach.AutoSize = true;
            this.lblAttach.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAttach.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblAttach.Location = new System.Drawing.Point(3, 218);
            this.lblAttach.Name = "lblAttach";
            this.lblAttach.Size = new System.Drawing.Size(74, 13);
            this.lblAttach.TabIndex = 19;
            this.lblAttach.Text = "Attachments:";
            // 
            // wb1
            // 
            this.wb1.AllowNavigation = false;
            this.wb1.AllowWebBrowserDrop = false;
            this.wb1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanelMain.SetColumnSpan(this.wb1, 3);
            this.wb1.IsWebBrowserContextMenuEnabled = false;
            this.wb1.Location = new System.Drawing.Point(3, 238);
            this.wb1.MinimumSize = new System.Drawing.Size(20, 20);
            this.wb1.Name = "wb1";
            this.wb1.Size = new System.Drawing.Size(744, 285);
            this.wb1.TabIndex = 18;
            this.wb1.WebBrowserShortcutsEnabled = false;
            // 
            // msgHdr15
            // 
            this.msgHdr15.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.msgHdr15.AutoSize = true;
            this.msgHdr15.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanelMain.SetColumnSpan(this.msgHdr15, 2);
            this.msgHdr15.Location = new System.Drawing.Point(3, 92);
            this.msgHdr15.MinimumSize = new System.Drawing.Size(400, 0);
            this.msgHdr15.Name = "msgHdr15";
            this.msgHdr15.Size = new System.Drawing.Size(739, 120);
            this.msgHdr15.TabIndex = 21;
            // 
            // msgHdr14
            // 
            this.msgHdr14.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.msgHdr14.AutoSize = true;
            this.msgHdr14.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanelMain.SetColumnSpan(this.msgHdr14, 2);
            this.msgHdr14.Location = new System.Drawing.Point(3, 5);
            this.msgHdr14.MinimumSize = new System.Drawing.Size(400, 60);
            this.msgHdr14.Name = "msgHdr14";
            this.msgHdr14.Size = new System.Drawing.Size(739, 81);
            this.msgHdr14.TabIndex = 22;
            // 
            // EmbeddedMsg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Name = "EmbeddedMsg";
            this.Size = new System.Drawing.Size(750, 526);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.Label lblAttach;
        private System.Windows.Forms.Panel panelAttach;
        internal System.Windows.Forms.WebBrowser wb1;
        private MessageHeader2013 msgHdr15;
        private MessageHeader msgHdr14;
    }
}
