namespace ChiaraMail.Forms
{
    partial class ContentServerForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkDisplay = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.udPort = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkNoPlaceholder = new System.Windows.Forms.CheckBox();
            this.chkEncrypt = new System.Windows.Forms.CheckBox();
            this.chkDefaultOn = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udPort)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkDisplay);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.udPort);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtServer);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtDescription);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(264, 162);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configuration";
            // 
            // chkDisplay
            // 
            this.chkDisplay.AutoSize = true;
            this.chkDisplay.Location = new System.Drawing.Point(10, 106);
            this.chkDisplay.Name = "chkDisplay";
            this.chkDisplay.Size = new System.Drawing.Size(116, 17);
            this.chkDisplay.TabIndex = 9;
            this.chkDisplay.Text = "Display password";
            this.chkDisplay.UseVisualStyleBackColor = true;
            this.chkDisplay.CheckedChanged += new System.EventHandler(this.ChkDisplayCheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(117, 133);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Default:   443";
            // 
            // udPort
            // 
            this.udPort.Location = new System.Drawing.Point(67, 128);
            this.udPort.Maximum = new decimal(new int[] {
            1023,
            0,
            0,
            0});
            this.udPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udPort.Name = "udPort";
            this.udPort.Size = new System.Drawing.Size(43, 22);
            this.udPort.TabIndex = 7;
            this.udPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.udPort.Value = new decimal(new int[] {
            443,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "P&ort";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(66, 74);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(187, 22);
            this.txtPassword.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "&Password";
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(66, 47);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(187, 22);
            this.txtServer.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Ser&ver";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(66, 20);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(187, 22);
            this.txtDescription.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "&Name";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkNoPlaceholder);
            this.groupBox2.Controls.Add(this.chkEncrypt);
            this.groupBox2.Controls.Add(this.chkDefaultOn);
            this.groupBox2.Location = new System.Drawing.Point(13, 181);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(265, 101);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "When this configuration is the account default";
            // 
            // chkNoPlaceholder
            // 
            this.chkNoPlaceholder.AutoSize = true;
            this.chkNoPlaceholder.Enabled = false;
            this.chkNoPlaceholder.Location = new System.Drawing.Point(7, 69);
            this.chkNoPlaceholder.Name = "chkNoPlaceholder";
            this.chkNoPlaceholder.Size = new System.Drawing.Size(107, 17);
            this.chkNoPlaceholder.TabIndex = 2;
            this.chkNoPlaceholder.Text = "&Include content";
            this.chkNoPlaceholder.UseVisualStyleBackColor = true;
            // 
            // chkEncrypt
            // 
            this.chkEncrypt.AutoSize = true;
            this.chkEncrypt.Enabled = false;
            this.chkEncrypt.Location = new System.Drawing.Point(7, 45);
            this.chkEncrypt.Name = "chkEncrypt";
            this.chkEncrypt.Size = new System.Drawing.Size(107, 17);
            this.chkEncrypt.TabIndex = 1;
            this.chkEncrypt.Text = "En&crypt content";
            this.chkEncrypt.UseVisualStyleBackColor = true;
            // 
            // chkDefaultOn
            // 
            this.chkDefaultOn.AutoSize = true;
            this.chkDefaultOn.Location = new System.Drawing.Point(7, 21);
            this.chkDefaultOn.Name = "chkDefaultOn";
            this.chkDefaultOn.Size = new System.Drawing.Size(88, 17);
            this.chkDefaultOn.TabIndex = 0;
            this.chkDefaultOn.Text = "&Send as ECS";
            this.chkDefaultOn.UseVisualStyleBackColor = true;
            this.chkDefaultOn.CheckedChanged += new System.EventHandler(this.ChkDefaultOnCheckedChanged);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.Location = new System.Drawing.Point(67, 288);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnOKClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(148, 288);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // ContentServerForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(290, 325);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ContentServerForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ECS Content Server";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udPort)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.NumericUpDown udPort;
        private System.Windows.Forms.Label label4;
        internal System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        internal System.Windows.Forms.CheckBox chkNoPlaceholder;
        internal System.Windows.Forms.CheckBox chkEncrypt;
        internal System.Windows.Forms.CheckBox chkDefaultOn;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkDisplay;
    }
}