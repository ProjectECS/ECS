namespace ChiaraMail.Controls
{
    partial class MessageHeader
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
            this.lblCc = new System.Windows.Forms.Label();
            this.lblCcRecip = new System.Windows.Forms.Label();
            this.lblToRecip = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblTo = new System.Windows.Forms.Label();
            this.lblSent = new System.Windows.Forms.Label();
            this.lblSender = new System.Windows.Forms.Label();
            this.lblSubjectField = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lblCc, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblCcRecip, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblToRecip, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblDate, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblTo, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblSent, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblSender, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblSubjectField, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(400, 81);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblCc
            // 
            this.lblCc.AutoSize = true;
            this.lblCc.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCc.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblCc.Location = new System.Drawing.Point(3, 68);
            this.lblCc.Name = "lblCc";
            this.lblCc.Size = new System.Drawing.Size(22, 13);
            this.lblCc.TabIndex = 10;
            this.lblCc.Text = "Cc:";
            // 
            // lblCcRecip
            // 
            this.lblCcRecip.AutoSize = true;
            this.lblCcRecip.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCcRecip.Location = new System.Drawing.Point(42, 68);
            this.lblCcRecip.Name = "lblCcRecip";
            this.lblCcRecip.Size = new System.Drawing.Size(19, 13);
            this.lblCcRecip.TabIndex = 9;
            this.lblCcRecip.Text = "Cc";
            // 
            // lblToRecip
            // 
            this.lblToRecip.AutoSize = true;
            this.lblToRecip.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToRecip.Location = new System.Drawing.Point(42, 55);
            this.lblToRecip.Name = "lblToRecip";
            this.lblToRecip.Size = new System.Drawing.Size(19, 13);
            this.lblToRecip.TabIndex = 8;
            this.lblToRecip.Text = "To";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(42, 42);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(31, 13);
            this.lblDate.TabIndex = 7;
            this.lblDate.Text = "Date";
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTo.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblTo.Location = new System.Drawing.Point(3, 55);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(22, 13);
            this.lblTo.TabIndex = 4;
            this.lblTo.Text = "To:";
            // 
            // lblSent
            // 
            this.lblSent.AutoSize = true;
            this.lblSent.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSent.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblSent.Location = new System.Drawing.Point(3, 42);
            this.lblSent.Name = "lblSent";
            this.lblSent.Size = new System.Drawing.Size(33, 13);
            this.lblSent.TabIndex = 3;
            this.lblSent.Text = "Sent:";
            // 
            // lblSender
            // 
            this.lblSender.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblSender, 2);
            this.lblSender.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSender.Location = new System.Drawing.Point(3, 21);
            this.lblSender.Name = "lblSender";
            this.lblSender.Size = new System.Drawing.Size(59, 21);
            this.lblSender.TabIndex = 2;
            this.lblSender.Text = "Sender";
            // 
            // lblSubjectField
            // 
            this.lblSubjectField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubjectField.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblSubjectField, 2);
            this.lblSubjectField.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubjectField.Location = new System.Drawing.Point(3, 0);
            this.lblSubjectField.Name = "lblSubjectField";
            this.lblSubjectField.Size = new System.Drawing.Size(394, 21);
            this.lblSubjectField.TabIndex = 1;
            this.lblSubjectField.Text = "subject";
            // 
            // MessageHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(400, 60);
            this.Name = "MessageHeader";
            this.Size = new System.Drawing.Size(400, 81);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblSubjectField;
        private System.Windows.Forms.Label lblSender;
        private System.Windows.Forms.Label lblSent;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblToRecip;
        private System.Windows.Forms.Label lblCcRecip;
        private System.Windows.Forms.Label lblCc;
    }
}
