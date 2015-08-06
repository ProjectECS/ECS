namespace ChiaraMail.Controls
{
    partial class MessageHeader2013
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblAttachName = new System.Windows.Forms.Label();
            this.lblSubjectField = new System.Windows.Forms.Label();
            this.lblSender = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblCc = new System.Windows.Forms.Label();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnForward = new ChiaraMail.Controls.ReplyControl();
            this.btnReplyAll = new ChiaraMail.Controls.ReplyControl();
            this.btnReply = new ChiaraMail.Controls.ReplyControl();
            this.lblTo = new System.Windows.Forms.Label();
            this.lblToRecip = new System.Windows.Forms.Label();
            this.lblCcRecip = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 4F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lblAttachName, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.lblSubjectField, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblSender, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblDate, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblCc, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.panelButtons, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblTo, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.lblToRecip, 2, 6);
            this.tableLayoutPanel1.Controls.Add(this.lblCcRecip, 2, 7);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 9;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(400, 120);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblAttachName
            // 
            this.lblAttachName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAttachName.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblAttachName, 3);
            this.lblAttachName.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAttachName.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblAttachName.Location = new System.Drawing.Point(7, 74);
            this.lblAttachName.Name = "lblAttachName";
            this.lblAttachName.Size = new System.Drawing.Size(92, 20);
            this.lblAttachName.TabIndex = 15;
            this.lblAttachName.Text = "AttachName";
            // 
            // lblSubjectField
            // 
            this.lblSubjectField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubjectField.AutoSize = true;
            this.lblSubjectField.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubjectField.Location = new System.Drawing.Point(73, 60);
            this.lblSubjectField.Name = "lblSubjectField";
            this.lblSubjectField.Size = new System.Drawing.Size(324, 13);
            this.lblSubjectField.TabIndex = 13;
            this.lblSubjectField.Text = "subject";
            // 
            // lblSender
            // 
            this.lblSender.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSender.AutoSize = true;
            this.lblSender.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSender.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblSender.Location = new System.Drawing.Point(73, 39);
            this.lblSender.Name = "lblSender";
            this.lblSender.Size = new System.Drawing.Size(55, 20);
            this.lblSender.TabIndex = 12;
            this.lblSender.Text = "Sender";
            // 
            // lblDate
            // 
            this.lblDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblDate.Location = new System.Drawing.Point(73, 26);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(31, 13);
            this.lblDate.TabIndex = 11;
            this.lblDate.Text = "Date";
            // 
            // lblCc
            // 
            this.lblCc.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblCc, 2);
            this.lblCc.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCc.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblCc.Location = new System.Drawing.Point(3, 107);
            this.lblCc.Name = "lblCc";
            this.lblCc.Size = new System.Drawing.Size(22, 13);
            this.lblCc.TabIndex = 9;
            this.lblCc.Text = "Cc:";
            // 
            // panelButtons
            // 
            this.panelButtons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panelButtons.AutoSize = true;
            this.panelButtons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.SetColumnSpan(this.panelButtons, 3);
            this.panelButtons.Controls.Add(this.btnForward);
            this.panelButtons.Controls.Add(this.btnReplyAll);
            this.panelButtons.Controls.Add(this.btnReply);
            this.panelButtons.Location = new System.Drawing.Point(4, 4);
            this.panelButtons.Margin = new System.Windows.Forms.Padding(0);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.panelButtons.Size = new System.Drawing.Size(396, 22);
            this.panelButtons.TabIndex = 0;
            // 
            // btnForward
            // 
            this.btnForward.AutoSize = true;
            this.btnForward.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnForward.BackColor = System.Drawing.Color.Transparent;
            this.btnForward.Caption = "Forward";
            this.btnForward.Location = new System.Drawing.Point(133, 1);
            this.btnForward.Margin = new System.Windows.Forms.Padding(1);
            this.btnForward.Name = "btnForward";
            this.btnForward.Picture = global::ChiaraMail.Properties.Resources.Forward;
            this.btnForward.Size = new System.Drawing.Size(72, 17);
            this.btnForward.TabIndex = 5;
            // 
            // btnReplyAll
            // 
            this.btnReplyAll.AutoSize = true;
            this.btnReplyAll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnReplyAll.BackColor = System.Drawing.Color.Transparent;
            this.btnReplyAll.Caption = "Reply All";
            this.btnReplyAll.Location = new System.Drawing.Point(58, 1);
            this.btnReplyAll.Margin = new System.Windows.Forms.Padding(1);
            this.btnReplyAll.Name = "btnReplyAll";
            this.btnReplyAll.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.btnReplyAll.Picture = global::ChiaraMail.Properties.Resources.ReplyAll;
            this.btnReplyAll.Size = new System.Drawing.Size(78, 17);
            this.btnReplyAll.TabIndex = 4;
            // 
            // btnReply
            // 
            this.btnReply.AutoSize = true;
            this.btnReply.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnReply.BackColor = System.Drawing.Color.Transparent;
            this.btnReply.Caption = "Reply";
            this.btnReply.Location = new System.Drawing.Point(1, 1);
            this.btnReply.Margin = new System.Windows.Forms.Padding(1);
            this.btnReply.Name = "btnReply";
            this.btnReply.Picture = null;
            this.btnReply.Size = new System.Drawing.Size(58, 17);
            this.btnReply.TabIndex = 3;
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblTo, 2);
            this.lblTo.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTo.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblTo.Location = new System.Drawing.Point(3, 94);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(19, 13);
            this.lblTo.TabIndex = 4;
            this.lblTo.Text = "To";
            // 
            // lblToRecip
            // 
            this.lblToRecip.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblToRecip, 2);
            this.lblToRecip.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToRecip.Location = new System.Drawing.Point(53, 94);
            this.lblToRecip.Name = "lblToRecip";
            this.lblToRecip.Size = new System.Drawing.Size(19, 13);
            this.lblToRecip.TabIndex = 8;
            this.lblToRecip.Text = "To";
            // 
            // lblCcRecip
            // 
            this.lblCcRecip.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblCcRecip, 2);
            this.lblCcRecip.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCcRecip.Location = new System.Drawing.Point(53, 107);
            this.lblCcRecip.Name = "lblCcRecip";
            this.lblCcRecip.Size = new System.Drawing.Size(19, 13);
            this.lblCcRecip.TabIndex = 10;
            this.lblCcRecip.Text = "Cc";
            // 
            // pictureBox1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.pictureBox1, 2);
            this.pictureBox1.Image = global::ChiaraMail.Properties.Resources.DummyUser;
            this.pictureBox1.Location = new System.Drawing.Point(4, 26);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.tableLayoutPanel1.SetRowSpan(this.pictureBox1, 3);
            this.pictureBox1.Size = new System.Drawing.Size(66, 48);
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // MessageHeader2013
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(400, 0);
            this.Name = "MessageHeader2013";
            this.Size = new System.Drawing.Size(400, 120);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.panelButtons.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label lblCc;
        private System.Windows.Forms.Label lblToRecip;
        private System.Windows.Forms.Label lblCcRecip;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblSender;
        private System.Windows.Forms.Label lblSubjectField;
        private System.Windows.Forms.PictureBox pictureBox1;
        private ReplyControl btnForward;
        private ReplyControl btnReplyAll;
        private ReplyControl btnReply;
        private System.Windows.Forms.Label lblAttachName;
    }
}
