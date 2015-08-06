namespace ChiaraMail.Controls
{
    partial class InspectorHeader
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
            this.lblFrom = new System.Windows.Forms.Label();
            this.lblTo = new System.Windows.Forms.Label();
            this.lblCc = new System.Windows.Forms.Label();
            this.lblSubject = new System.Windows.Forms.Label();
            this.lblSent = new System.Windows.Forms.Label();
            this.txtFrom = new System.Windows.Forms.TextBox();
            this.txtSent = new System.Windows.Forms.TextBox();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.lblSubjectField = new System.Windows.Forms.Label();
            this.lblCcRecip = new System.Windows.Forms.Label();
            this.lblToRecip = new System.Windows.Forms.Label();
            this.lblSender = new System.Windows.Forms.Label();
            this.tableLayoutPanelSent = new System.Windows.Forms.TableLayoutPanel();
            this.lblDate = new System.Windows.Forms.Label();
            this.tableLayoutPanelMain.SuspendLayout();
            this.tableLayoutPanelSent.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Location = new System.Drawing.Point(5, 4);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(33, 13);
            this.lblFrom.TabIndex = 0;
            this.lblFrom.Text = "From:";
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(5, 17);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(23, 13);
            this.lblTo.TabIndex = 1;
            this.lblTo.Text = "To:";
            // 
            // lblCc
            // 
            this.lblCc.AutoSize = true;
            this.lblCc.Location = new System.Drawing.Point(5, 30);
            this.lblCc.Name = "lblCc";
            this.lblCc.Size = new System.Drawing.Size(23, 13);
            this.lblCc.TabIndex = 2;
            this.lblCc.Text = "Cc:";
            // 
            // lblSubject
            // 
            this.lblSubject.AutoSize = true;
            this.lblSubject.Location = new System.Drawing.Point(5, 43);
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size(46, 13);
            this.lblSubject.TabIndex = 3;
            this.lblSubject.Text = "Subject:";
            // 
            // lblSent
            // 
            this.lblSent.AutoSize = true;
            this.lblSent.Location = new System.Drawing.Point(3, 0);
            this.lblSent.Name = "lblSent";
            this.lblSent.Size = new System.Drawing.Size(32, 13);
            this.lblSent.TabIndex = 4;
            this.lblSent.Text = "Sent:";
            // 
            // txtFrom
            // 
            this.txtFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFrom.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFrom.Location = new System.Drawing.Point(76, 3);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.ReadOnly = true;
            this.txtFrom.Size = new System.Drawing.Size(517, 13);
            this.txtFrom.TabIndex = 5;
            // 
            // txtSent
            // 
            this.txtSent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSent.Location = new System.Drawing.Point(668, 3);
            this.txtSent.Name = "txtSent";
            this.txtSent.ReadOnly = true;
            this.txtSent.Size = new System.Drawing.Size(143, 13);
            this.txtSent.TabIndex = 6;
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 3;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelMain.Controls.Add(this.lblSubjectField, 1, 4);
            this.tableLayoutPanelMain.Controls.Add(this.lblCcRecip, 1, 3);
            this.tableLayoutPanelMain.Controls.Add(this.lblToRecip, 1, 2);
            this.tableLayoutPanelMain.Controls.Add(this.lblFrom, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.lblTo, 0, 2);
            this.tableLayoutPanelMain.Controls.Add(this.lblCc, 0, 3);
            this.tableLayoutPanelMain.Controls.Add(this.lblSubject, 0, 4);
            this.tableLayoutPanelMain.Controls.Add(this.lblSender, 1, 1);
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanelSent, 2, 1);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.Padding = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanelMain.RowCount = 6;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 2F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(580, 60);
            this.tableLayoutPanelMain.TabIndex = 1;
            // 
            // lblSubjectField
            // 
            this.lblSubjectField.AutoSize = true;
            this.tableLayoutPanelMain.SetColumnSpan(this.lblSubjectField, 2);
            this.lblSubjectField.Location = new System.Drawing.Point(85, 43);
            this.lblSubjectField.Name = "lblSubjectField";
            this.lblSubjectField.Size = new System.Drawing.Size(41, 13);
            this.lblSubjectField.TabIndex = 14;
            this.lblSubjectField.Text = "subject";
            // 
            // lblCcRecip
            // 
            this.lblCcRecip.AutoSize = true;
            this.tableLayoutPanelMain.SetColumnSpan(this.lblCcRecip, 2);
            this.lblCcRecip.Location = new System.Drawing.Point(85, 30);
            this.lblCcRecip.Name = "lblCcRecip";
            this.lblCcRecip.Size = new System.Drawing.Size(45, 13);
            this.lblCcRecip.TabIndex = 14;
            this.lblCcRecip.Text = "cc recip";
            // 
            // lblToRecip
            // 
            this.lblToRecip.AutoSize = true;
            this.tableLayoutPanelMain.SetColumnSpan(this.lblToRecip, 2);
            this.lblToRecip.Location = new System.Drawing.Point(85, 17);
            this.lblToRecip.Name = "lblToRecip";
            this.lblToRecip.Size = new System.Drawing.Size(30, 13);
            this.lblToRecip.TabIndex = 14;
            this.lblToRecip.Text = "recip";
            // 
            // lblSender
            // 
            this.lblSender.AutoSize = true;
            this.lblSender.Location = new System.Drawing.Point(85, 4);
            this.lblSender.Name = "lblSender";
            this.lblSender.Size = new System.Drawing.Size(39, 13);
            this.lblSender.TabIndex = 13;
            this.lblSender.Text = "sender";
            // 
            // tableLayoutPanelSent
            // 
            this.tableLayoutPanelSent.AutoSize = true;
            this.tableLayoutPanelSent.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanelSent.ColumnCount = 2;
            this.tableLayoutPanelSent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelSent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelSent.Controls.Add(this.lblSent, 0, 0);
            this.tableLayoutPanelSent.Controls.Add(this.lblDate, 1, 0);
            this.tableLayoutPanelSent.Location = new System.Drawing.Point(501, 4);
            this.tableLayoutPanelSent.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelSent.Name = "tableLayoutPanelSent";
            this.tableLayoutPanelSent.RowCount = 1;
            this.tableLayoutPanelSent.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelSent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 13F));
            this.tableLayoutPanelSent.Size = new System.Drawing.Size(77, 13);
            this.tableLayoutPanelSent.TabIndex = 12;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(41, 0);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(33, 13);
            this.lblDate.TabIndex = 5;
            this.lblDate.Text = "None";
            // 
            // InspectorHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Name = "InspectorHeader";
            this.Size = new System.Drawing.Size(580, 60);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.PerformLayout();
            this.tableLayoutPanelSent.ResumeLayout(false);
            this.tableLayoutPanelSent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label lblCc;
        private System.Windows.Forms.Label lblSubject;
        private System.Windows.Forms.Label lblSent;
        private System.Windows.Forms.TextBox txtFrom;
        private System.Windows.Forms.TextBox txtSent;
        private System.Windows.Forms.Label lblSubjectField;
        private System.Windows.Forms.Label lblCcRecip;
        private System.Windows.Forms.Label lblToRecip;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblSender;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelSent;
        internal System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;

    }
}
