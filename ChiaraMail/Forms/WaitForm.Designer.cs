namespace ChiaraMail.Forms
{
    partial class WaitForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            picWait = new System.Windows.Forms.PictureBox();
            lblWait = new System.Windows.Forms.Label();
            btnCancel = new System.Windows.Forms.Button();
            bw = new System.ComponentModel.BackgroundWorker();
            lblSize = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(picWait)).BeginInit();
            SuspendLayout();
            // 
            // picWait
            // 
            picWait.Image = global::ChiaraMail.Properties.Resources.snake_loader_cornflower;
            picWait.Location = new System.Drawing.Point(12, 12);
            picWait.Name = "picWait";
            picWait.Size = new System.Drawing.Size(32, 32);
            picWait.TabIndex = 0;
            picWait.TabStop = false;
            // 
            // lblWait
            // 
            lblWait.AutoEllipsis = true;
            lblWait.Location = new System.Drawing.Point(49, 12);
            lblWait.Name = "lblWait";
            lblWait.Size = new System.Drawing.Size(306, 32);
            lblWait.TabIndex = 1;
            lblWait.Text = "Contacting server...\r\n";
            // 
            // btnCancel
            // 
            btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            btnCancel.Location = new System.Drawing.Point(294, 49);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(62, 23);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "&Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += new System.EventHandler(BtnCancelClick);
            // 
            // bw
            // 
            bw.WorkerSupportsCancellation = true;
            // 
            // lblSize
            // 
            lblSize.AutoSize = true;
            lblSize.Location = new System.Drawing.Point(49, 49);
            lblSize.Name = "lblSize";
            lblSize.Size = new System.Drawing.Size(0, 13);
            lblSize.TabIndex = 3;
            // 
            // WaitForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(368, 84);
            Controls.Add(lblSize);
            Controls.Add(btnCancel);
            Controls.Add(lblWait);
            Controls.Add(picWait);
            Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            Name = "WaitForm";
            SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = Properties.Resources.product_name;
            ((System.ComponentModel.ISupportInitialize)(picWait)).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picWait;
        private System.Windows.Forms.Label lblWait;
        private System.Windows.Forms.Button btnCancel;
        private System.ComponentModel.BackgroundWorker bw;
        private System.Windows.Forms.Label lblSize;
    }
}