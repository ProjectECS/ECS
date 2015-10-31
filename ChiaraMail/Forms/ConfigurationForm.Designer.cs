namespace ChiaraMail.Forms
{
    partial class ConfigurationForm
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblEmail = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboEmail = new System.Windows.Forms.ComboBox();
            this.lblHead1 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnDefault = new System.Windows.Forms.Button();
            this.tlpConfigDetail = new System.Windows.Forms.TableLayoutPanel();
            this.txtInclude = new System.Windows.Forms.TextBox();
            this.txtEncrypt = new System.Windows.Forms.TextBox();
            this.txtSend = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtHostName = new System.Windows.Forms.TextBox();
            this.lblConfigName = new System.Windows.Forms.Label();
            this.lblHostName = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblPort = new System.Windows.Forms.Label();
            this.lblSend = new System.Windows.Forms.Label();
            this.lblEncrypt = new System.Windows.Forms.Label();
            this.lblInclude = new System.Windows.Forms.Label();
            this.txtConfigName = new System.Windows.Forms.TextBox();
            this.lblHead2 = new System.Windows.Forms.Label();
            this.lblHead3 = new System.Windows.Forms.Label();
            this.line1 = new System.Windows.Forms.GroupBox();
            this.lvwConfig = new System.Windows.Forms.ListView();
            this.btnRegister = new System.Windows.Forms.Button();
            this.lblAllow = new System.Windows.Forms.Label();
            this.txtAllow = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tlpConfigDetail.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(33, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnOKClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(114, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblEmail
            // 
            this.lblEmail.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(3, 93);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(77, 13);
            this.lblEmail.TabIndex = 1;
            this.lblEmail.Text = "&Email address";
            this.lblEmail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblEmail, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.cboEmail, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblHead1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnAdd, 2, 6);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 15);
            this.tableLayoutPanel1.Controls.Add(this.btnEdit, 2, 7);
            this.tableLayoutPanel1.Controls.Add(this.btnRemove, 2, 8);
            this.tableLayoutPanel1.Controls.Add(this.btnDefault, 2, 9);
            this.tableLayoutPanel1.Controls.Add(this.tlpConfigDetail, 0, 11);
            this.tableLayoutPanel1.Controls.Add(this.lblHead2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblHead3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.line1, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.lvwConfig, 0, 6);
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 16;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(437, 464);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox1, 3);
            this.groupBox1.Location = new System.Drawing.Point(3, 81);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(431, 2);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            // 
            // cboEmail
            // 
            this.cboEmail.FormattingEnabled = true;
            this.cboEmail.Location = new System.Drawing.Point(86, 89);
            this.cboEmail.Name = "cboEmail";
            this.cboEmail.Size = new System.Drawing.Size(215, 21);
            this.cboEmail.TabIndex = 2;
            // 
            // lblHead1
            // 
            this.lblHead1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHead1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblHead1, 3);
            this.lblHead1.Location = new System.Drawing.Point(3, 0);
            this.lblHead1.MaximumSize = new System.Drawing.Size(431, 400);
            this.lblHead1.Name = "lblHead1";
            this.lblHead1.Size = new System.Drawing.Size(431, 52);
            this.lblHead1.TabIndex = 0;
            this.lblHead1.Text = "Line 1\r\n\r\nLine 2\r\n\r\n";
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(348, 126);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(86, 23);
            this.btnAdd.TabIndex = 15;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Location = new System.Drawing.Point(102, 431);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(223, 30);
            this.panel1.TabIndex = 11;
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Location = new System.Drawing.Point(348, 155);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(86, 23);
            this.btnEdit.TabIndex = 16;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.Location = new System.Drawing.Point(348, 184);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(86, 23);
            this.btnRemove.TabIndex = 17;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            // 
            // btnDefault
            // 
            this.btnDefault.AutoSize = true;
            this.btnDefault.Location = new System.Drawing.Point(348, 213);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(86, 23);
            this.btnDefault.TabIndex = 18;
            this.btnDefault.Text = "Make Default";
            this.btnDefault.UseVisualStyleBackColor = true;
            // 
            // tlpConfigDetail
            // 
            this.tlpConfigDetail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpConfigDetail.AutoSize = true;
            this.tlpConfigDetail.ColumnCount = 2;
            this.tableLayoutPanel1.SetColumnSpan(this.tlpConfigDetail, 3);
            this.tlpConfigDetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpConfigDetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpConfigDetail.Controls.Add(this.txtAllow, 1, 7);
            this.tlpConfigDetail.Controls.Add(this.lblAllow, 0, 7);
            this.tlpConfigDetail.Controls.Add(this.txtInclude, 1, 6);
            this.tlpConfigDetail.Controls.Add(this.txtEncrypt, 1, 5);
            this.tlpConfigDetail.Controls.Add(this.txtSend, 1, 4);
            this.tlpConfigDetail.Controls.Add(this.txtPort, 1, 3);
            this.tlpConfigDetail.Controls.Add(this.txtPassword, 1, 2);
            this.tlpConfigDetail.Controls.Add(this.txtHostName, 1, 1);
            this.tlpConfigDetail.Controls.Add(this.lblConfigName, 0, 0);
            this.tlpConfigDetail.Controls.Add(this.lblHostName, 0, 1);
            this.tlpConfigDetail.Controls.Add(this.lblPassword, 0, 2);
            this.tlpConfigDetail.Controls.Add(this.lblPort, 0, 3);
            this.tlpConfigDetail.Controls.Add(this.lblSend, 0, 4);
            this.tlpConfigDetail.Controls.Add(this.lblEncrypt, 0, 5);
            this.tlpConfigDetail.Controls.Add(this.lblInclude, 0, 6);
            this.tlpConfigDetail.Controls.Add(this.txtConfigName, 1, 0);
            this.tlpConfigDetail.Location = new System.Drawing.Point(3, 250);
            this.tlpConfigDetail.Name = "tlpConfigDetail";
            this.tlpConfigDetail.RowCount = 8;
            this.tlpConfigDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpConfigDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpConfigDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpConfigDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpConfigDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpConfigDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpConfigDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpConfigDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpConfigDetail.Size = new System.Drawing.Size(431, 160);
            this.tlpConfigDetail.TabIndex = 19;
            // 
            // txtInclude
            // 
            this.txtInclude.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInclude.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInclude.Location = new System.Drawing.Point(147, 123);
            this.txtInclude.Name = "txtInclude";
            this.txtInclude.ReadOnly = true;
            this.txtInclude.Size = new System.Drawing.Size(281, 15);
            this.txtInclude.TabIndex = 13;
            // 
            // txtEncrypt
            // 
            this.txtEncrypt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEncrypt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEncrypt.Location = new System.Drawing.Point(147, 103);
            this.txtEncrypt.Name = "txtEncrypt";
            this.txtEncrypt.ReadOnly = true;
            this.txtEncrypt.Size = new System.Drawing.Size(281, 15);
            this.txtEncrypt.TabIndex = 12;
            // 
            // txtSend
            // 
            this.txtSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSend.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSend.Location = new System.Drawing.Point(147, 83);
            this.txtSend.Name = "txtSend";
            this.txtSend.ReadOnly = true;
            this.txtSend.Size = new System.Drawing.Size(281, 15);
            this.txtSend.TabIndex = 11;
            // 
            // txtPort
            // 
            this.txtPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPort.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPort.Location = new System.Drawing.Point(147, 63);
            this.txtPort.Name = "txtPort";
            this.txtPort.ReadOnly = true;
            this.txtPort.Size = new System.Drawing.Size(281, 15);
            this.txtPort.TabIndex = 10;
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPassword.Location = new System.Drawing.Point(147, 43);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.ReadOnly = true;
            this.txtPassword.Size = new System.Drawing.Size(281, 15);
            this.txtPassword.TabIndex = 9;
            // 
            // txtHostName
            // 
            this.txtHostName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHostName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtHostName.Location = new System.Drawing.Point(147, 23);
            this.txtHostName.Name = "txtHostName";
            this.txtHostName.ReadOnly = true;
            this.txtHostName.Size = new System.Drawing.Size(281, 15);
            this.txtHostName.TabIndex = 8;
            // 
            // lblConfigName
            // 
            this.lblConfigName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblConfigName.AutoSize = true;
            this.lblConfigName.Location = new System.Drawing.Point(3, 3);
            this.lblConfigName.Name = "lblConfigName";
            this.lblConfigName.Size = new System.Drawing.Size(115, 13);
            this.lblConfigName.TabIndex = 0;
            this.lblConfigName.Text = "Configuration Name:";
            // 
            // lblHostName
            // 
            this.lblHostName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblHostName.AutoSize = true;
            this.lblHostName.Location = new System.Drawing.Point(3, 23);
            this.lblHostName.Name = "lblHostName";
            this.lblHostName.Size = new System.Drawing.Size(118, 13);
            this.lblHostName.TabIndex = 1;
            this.lblHostName.Text = "Content Server Name:";
            // 
            // lblPassword
            // 
            this.lblPassword.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(3, 43);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(138, 13);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.Text = "Content Server Password:";
            // 
            // lblPort
            // 
            this.lblPort.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(3, 63);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(110, 13);
            this.lblPort.TabIndex = 3;
            this.lblPort.Text = "Content Server Port:";
            // 
            // lblSend
            // 
            this.lblSend.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSend.AutoSize = true;
            this.lblSend.Location = new System.Drawing.Point(3, 83);
            this.lblSend.Name = "lblSend";
            this.lblSend.Size = new System.Drawing.Size(72, 13);
            this.lblSend.TabIndex = 4;
            this.lblSend.Text = "Send as ECS:";
            // 
            // lblEncrypt
            // 
            this.lblEncrypt.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblEncrypt.AutoSize = true;
            this.lblEncrypt.Location = new System.Drawing.Point(3, 103);
            this.lblEncrypt.Name = "lblEncrypt";
            this.lblEncrypt.Size = new System.Drawing.Size(93, 13);
            this.lblEncrypt.TabIndex = 5;
            this.lblEncrypt.Text = "Encrypt Content:";
            // 
            // lblInclude
            // 
            this.lblInclude.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblInclude.AutoSize = true;
            this.lblInclude.Location = new System.Drawing.Point(3, 123);
            this.lblInclude.Name = "lblInclude";
            this.lblInclude.Size = new System.Drawing.Size(93, 13);
            this.lblInclude.TabIndex = 6;
            this.lblInclude.Text = "Include Content:";
            // 
            // txtConfigName
            // 
            this.txtConfigName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConfigName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtConfigName.Location = new System.Drawing.Point(147, 3);
            this.txtConfigName.Name = "txtConfigName";
            this.txtConfigName.ReadOnly = true;
            this.txtConfigName.Size = new System.Drawing.Size(281, 15);
            this.txtConfigName.TabIndex = 7;
            // 
            // lblHead2
            // 
            this.lblHead2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHead2.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblHead2, 3);
            this.lblHead2.Location = new System.Drawing.Point(3, 52);
            this.lblHead2.MaximumSize = new System.Drawing.Size(431, 300);
            this.lblHead2.Name = "lblHead2";
            this.lblHead2.Size = new System.Drawing.Size(431, 13);
            this.lblHead2.TabIndex = 20;
            this.lblHead2.Text = "label1";
            // 
            // lblHead3
            // 
            this.lblHead3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHead3.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lblHead3, 3);
            this.lblHead3.Location = new System.Drawing.Point(3, 65);
            this.lblHead3.MaximumSize = new System.Drawing.Size(431, 300);
            this.lblHead3.Name = "lblHead3";
            this.lblHead3.Size = new System.Drawing.Size(431, 13);
            this.lblHead3.TabIndex = 21;
            this.lblHead3.Text = "label1";
            // 
            // line1
            // 
            this.line1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.line1, 3);
            this.line1.Location = new System.Drawing.Point(3, 242);
            this.line1.Name = "line1";
            this.line1.Size = new System.Drawing.Size(431, 2);
            this.line1.TabIndex = 22;
            this.line1.TabStop = false;
            // 
            // lvwConfig
            // 
            this.lvwConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.lvwConfig, 2);
            this.lvwConfig.Location = new System.Drawing.Point(3, 126);
            this.lvwConfig.MultiSelect = false;
            this.lvwConfig.Name = "lvwConfig";
            this.tableLayoutPanel1.SetRowSpan(this.lvwConfig, 4);
            this.lvwConfig.Size = new System.Drawing.Size(339, 110);
            this.lvwConfig.TabIndex = 24;
            this.lvwConfig.UseCompatibleStateImageBehavior = false;
            this.lvwConfig.View = System.Windows.Forms.View.List;
            this.lvwConfig.SelectedIndexChanged += new System.EventHandler(this.LvwConfigSelectedIndexChanged);
            // 
            // btnRegister
            // 
            this.btnRegister.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnRegister.AutoSize = true;
            this.btnRegister.Location = new System.Drawing.Point(249, 788);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(172, 23);
            this.btnRegister.TabIndex = 14;
            this.btnRegister.Text = "Create my ChiaraMail account!";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Visible = false;
            // 
            // lblAllow
            // 
            this.lblAllow.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAllow.AutoSize = true;
            this.lblAllow.Location = new System.Drawing.Point(3, 143);
            this.lblAllow.Name = "lblAllow";
            this.lblAllow.Size = new System.Drawing.Size(102, 13);
            this.lblAllow.TabIndex = 14;
            this.lblAllow.Text = "Allow Forwarding:";
            // 
            // txtAllow
            // 
            this.txtAllow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAllow.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAllow.Location = new System.Drawing.Point(147, 143);
            this.txtAllow.Name = "txtAllow";
            this.txtAllow.ReadOnly = true;
            this.txtAllow.Size = new System.Drawing.Size(281, 15);
            this.txtAllow.TabIndex = 15;
            // 
            // ConfigurationForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(461, 491);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btnRegister);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigurationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ChiaraMail for Outlook Account Settings";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tlpConfigDetail.ResumeLayout(false);
            this.tlpConfigDetail.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox cboEmail;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblHead1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnDefault;
        private System.Windows.Forms.TableLayoutPanel tlpConfigDetail;
        private System.Windows.Forms.TextBox txtInclude;
        private System.Windows.Forms.TextBox txtEncrypt;
        private System.Windows.Forms.TextBox txtSend;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtHostName;
        private System.Windows.Forms.Label lblConfigName;
        private System.Windows.Forms.Label lblHostName;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Label lblSend;
        private System.Windows.Forms.Label lblEncrypt;
        private System.Windows.Forms.Label lblInclude;
        private System.Windows.Forms.TextBox txtConfigName;
        private System.Windows.Forms.Label lblHead2;
        private System.Windows.Forms.Label lblHead3;
        private System.Windows.Forms.GroupBox line1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView lvwConfig;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.TextBox txtAllow;
        private System.Windows.Forms.Label lblAllow;
    }
}