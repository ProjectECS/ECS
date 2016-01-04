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
            this.components = new System.ComponentModel.Container();
            this.picWait = new System.Windows.Forms.PictureBox();
            this.lblWait = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.bw = new System.ComponentModel.BackgroundWorker();
            this.lblSize = new System.Windows.Forms.Label();
            this.tmrProgressBar = new System.Windows.Forms.Timer(this.components);
            this.progressBar = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.picWait)).BeginInit();
            this.SuspendLayout();
            // 
            // picWait
            // 
            this.picWait.Image = global::ChiaraMail.Properties.Resources.snake_loader_cornflower;
            this.picWait.Location = new System.Drawing.Point(-39, 12);
            this.picWait.Name = "picWait";
            this.picWait.Size = new System.Drawing.Size(32, 32);
            this.picWait.TabIndex = 0;
            this.picWait.TabStop = false;
            this.picWait.Visible = false;
            // 
            // lblWait
            // 
            this.lblWait.AutoEllipsis = true;
            this.lblWait.Location = new System.Drawing.Point(12, 12);
            this.lblWait.Name = "lblWait";
            this.lblWait.Size = new System.Drawing.Size(344, 32);
            this.lblWait.TabIndex = 1;
            this.lblWait.Text = "Contacting server...\r\n";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(294, 49);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(62, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
            // 
            // bw
            // 
            this.bw.WorkerSupportsCancellation = true;
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Location = new System.Drawing.Point(49, 49);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(0, 13);
            this.lblSize.TabIndex = 3;
            // 
            // tmrProgressBar
            // 
            this.tmrProgressBar.Interval = 1000;
            this.tmrProgressBar.Tick += new System.EventHandler(this.tmrProgressBar_Tick);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 50);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(276, 21);
            this.progressBar.TabIndex = 4;
            // 
            // WaitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 84);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lblSize);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblWait);
            this.Controls.Add(this.picWait);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "WaitForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ChiaraMail for Outlook";
            ((System.ComponentModel.ISupportInitialize)(this.picWait)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picWait;
        private System.Windows.Forms.Label lblWait;
        private System.Windows.Forms.Button btnCancel;
        private System.ComponentModel.BackgroundWorker bw;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Timer tmrProgressBar;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}