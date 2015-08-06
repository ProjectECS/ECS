using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChiaraMail.Properties;

namespace ChiaraMail.Forms
{
    public partial class ConfigurationForm : Form
    {
        private string _activeEmail = "";
        private readonly Dictionary<string, Account> _accounts;
        public ConfigurationForm()
        {
            InitializeComponent();
            _accounts = ThisAddIn.Accounts;
            lblHead1.Text = Resources.config_hdr1 + 
                Environment.NewLine + Environment.NewLine;
            lblHead2.Text = Resources.config_hdr2 +
                Environment.NewLine + Environment.NewLine;
            lblHead3.Text = Resources.config_hdr3;
            //instantiate event handlers
            cboEmail.SelectedIndexChanged += CboEmailSelectedIndexChanged;
            cboEmail.KeyDown += cboEmail_KeyDown;
            lvwConfig.SelectedIndexChanged += LvwConfigSelectedIndexChanged;
            lvwConfig.DoubleClick += LvwConfigDoubleClick;
            btnAdd.Click += BtnAddClick;
            btnEdit.Click += BtnEditClick;
            btnRemove.Click += BtnRemoveClick;
            btnDefault.Click += BtnDefaultClick;
            //btnRegister.Click += BtnRegisterClick;
            //initialize
            cboEmail.DropDownStyle = _accounts.Count.Equals(1)
                ? ComboBoxStyle.DropDown
                : ComboBoxStyle.DropDownList;
            cboEmail.Items.Clear();
            lvwConfig.Items.Clear();
            //load the combo with the account address(es)
            foreach (var email in _accounts.Keys)
            {
                cboEmail.Items.Add(email);
            }
            cboEmail.SelectedIndex = 0;            
        }

        private void BtnDefaultClick(object sender, EventArgs e)
        {
            if (lvwConfig.SelectedItems.Count == 0) return;
            //remove (Default) from previous default
            var current = _accounts[_activeEmail].DefaultConfiguration;
            SetItemText(current.ToString("d"), false);
            //add it to the new selection
            var key = lvwConfig.SelectedItems[0].Name;
            SetItemText(key, true);
            //and save change
            _accounts[_activeEmail].DefaultConfiguration = int.Parse(key);            
        }

        private void BtnRemoveClick(object sender, EventArgs e)
        {
            if (lvwConfig.SelectedItems.Count == 0) return;
            var name = lvwConfig.SelectedItems[0].Name;
            lvwConfig.Items.RemoveByKey(name);
            var key = int.Parse(name);
            _accounts[_activeEmail].Configurations.Remove(key);
            //select public config
            SelectConfig(0);
            if (_accounts[_activeEmail].DefaultConfiguration != key) return;
            //we deleted the default configuration, make 0 (public) the default
            _accounts[_activeEmail].DefaultConfiguration = 0;
            SetItemText("0", true);
            
        }

        private void BtnEditClick(object sender, EventArgs e)
        {
            if (lvwConfig.SelectedItems.Count == 0) return;
            var key = int.Parse(lvwConfig.SelectedItems[0].Name);
            var account = _accounts[_activeEmail];
            var config = account.Configurations[key];
            if (string.IsNullOrEmpty(config.Password) && 
                !string.IsNullOrEmpty(config.Server) && 
                !string.IsNullOrEmpty(config.Port))
            {
                account.Configurations[key].Password = FetchPassword(account, 
                    config.Server, 
                    config.Port);
            }
            var frm = new ContentServerForm


            {
                Account = _accounts[_activeEmail],
                Key = key
            };
            if (frm.ShowDialog(this) != DialogResult.OK) return;
            //if we still don't have a password try to get it
            if (string.IsNullOrEmpty(frm.Account.Configurations[key].Password) && 
                !string.IsNullOrEmpty(frm.txtServer.Text) &&
                !string.IsNullOrEmpty(frm.udPort.Text))
            {
                frm.Account.Configurations[key].Password = FetchPassword(frm.Account, frm.txtServer.Text, frm.udPort.Text);
            }
            //store the new configuration
            _accounts[_activeEmail] = frm.Account;
            //load it
            LoadConfigs(frm.Account);
            //select this one
            SelectConfig(key);
        }

        private void BtnAddClick(object sender, EventArgs e)
        {
            var key = GetNextKey();
            var frm = new ContentServerForm
                {
                    Account = _accounts[_activeEmail], 
                    Key = key
                };
            if (frm.ShowDialog(this) != DialogResult.OK) return;
            //if we don't have a password try to get it
            if (string.IsNullOrEmpty(frm.Account.Configurations[key].Password) && 
                !string.IsNullOrEmpty(frm.txtServer.Text) && 
                !string.IsNullOrEmpty(frm.udPort.Text))
            {
                frm.Account.Configurations[key].Password = FetchPassword(frm.Account, frm.txtServer.Text, frm.udPort.Text);
            }
            //store the new configuration
            _accounts[_activeEmail] = frm.Account;
            //load it
            LoadConfigs(frm.Account);
            //select this one
            SelectConfig(key);
        }

        private string FetchPassword(Account account,string host, string port)
        {
            try
            {
                Logger.Info("FetchPassword", string.Format(
                    "submitting RegisterUser request for {0} account {1} ({2}:{3})",
                    account.Protocol, 
                    account.SMTPAddress,
                    account.Host, 
                    account.Port));
                var response = ContentHandler.RegisterUser(
                    account.SMTPAddress,"password", account.UserName, account.Protocol,
                    account.Host, account.Port, account.LoginName,host,port);
                if (string.IsNullOrEmpty(response)) return string.Empty;
                if (response.StartsWith("Error:"))
                {
                    MessageBox.Show(Resources.register_user_failed,
                                    Resources.product_name,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
                else
                {
                    return response;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("FetchPassword",ex.Message);
            }
            return string.Empty;
        }

        //private void BtnRegisterClick(object sender, EventArgs e)
        //{
        //    //var email = cboEmail.Text;
        //    var account = ThisAddIn.Accounts[_activeEmail];
        //    //fire auto-register
        //    var addr = Encoding.Unicode.GetString(
        //        new byte[]
        //            {
        //                33, 0, 70, 0, 71, 0, 119, 0, 51, 0, 56, 0, 59, 
        //                0, 95, 0, 38, 0, 104, 0, 73, 0, 111, 0, 80, 0, 
        //                68, 0, 67, 0, 54, 0, 56, 0, 56, 0, 55, 0
        //            });
        //    Logger.Info("", string.Format(
        //        "responding to user-initiated registration request for {0} <{1}>",
        //        account.UserName, account.SMTPAddress));
        //    try
        //    {
        //        Cursor = Cursors.WaitCursor;
        //        ContentHandler.RegisterUser(account.SMTPAddress, account.UserName, addr);
        //    }
        //    finally
        //    {
        //        Cursor = Cursors.Default;
        //    }
        //    //display info
        //    MessageBox.Show(this, string.Format(
        //        Resources.auto_register_info,Environment.NewLine),
        //        Resources.product_name,
        //        MessageBoxButtons.OK,
        //        MessageBoxIcon.Exclamation);
        //}

        private void BtnOKClick(object sender, EventArgs e)
        {
            //save the changes
            ThisAddIn.Accounts = _accounts;
            DialogResult = DialogResult.OK;
        }

        private void cboEmail_KeyDown(object sender, KeyEventArgs e)
        {
            //disallow everything except Tab
            if(e.KeyCode!=Keys.Tab) e.SuppressKeyPress = true;
        }

        private void CboEmailSelectedIndexChanged(object sender, EventArgs e)
        {
            var email = cboEmail.SelectedItem.ToString();
            if (email.Equals(_activeEmail)) return;
            _activeEmail = email;
            lvwConfig.Items.Clear();
            //load any configuration(s) for this account
            LoadConfigs(_accounts[email]);
        }

        private void LvwConfigSelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwConfig.SelectedItems.Count == 0)
            {
                txtConfigName.Text = "";
                txtHostName.Text = "";
                txtPassword.Text = "";
                txtPort.Text = "";
                txtSend.Text = "";
                txtEncrypt.Text = "";
                txtInclude.Text = "";
                btnRemove.Enabled = false;
                btnEdit.Enabled = false;
                btnDefault.Enabled = false;
                //btnRegister.Visible = false;
                return;
            }
            //get the selection
            var key = Convert.ToInt32(lvwConfig.SelectedItems[0].Name);
            var config = _accounts[_activeEmail].Configurations[key];
            //enable/disable buttons based on selection
            btnEdit.Enabled = true;
            btnRemove.Enabled = key != 0;
            btnDefault.Enabled = key != _accounts[_activeEmail].DefaultConfiguration;
            //btnRegister.Visible = key == 0;
            //btnRegister.Enabled = string.IsNullOrEmpty(config.Password);
            //load panel with data from selected item
            txtConfigName.Text = config.Description;
            txtHostName.Text = config.Server;
            txtPassword.Text = config.Password;
            txtPort.Text = config.Port;
            txtSend.Text = config.DefaultOn ? "Yes" : "No";
            txtEncrypt.Text = config.Encrypt ? "Yes" : "No";
            txtInclude.Text = config.NoPlaceholder ? "Yes" : "No";
        }

        private void LvwConfigDoubleClick(object sender, EventArgs e)
        {
            //treat this as Edit
            btnEdit.PerformClick();
        }



        private void SetItemText(string key, bool isDefault)
        {
            var item = lvwConfig.Items.Find(key, false)[0];
            item.Text = string.Format("{0}{1}",
                _accounts[_activeEmail].Configurations[int.Parse(key)].Description,
                isDefault ? Resources.config_default_tag : string.Empty);
    
        }

        private int GetNextKey()
        {
            var account = _accounts[_activeEmail];
            return account.Configurations.Keys.Max() + 1;
        }
       
        private void SelectConfig(int key)
        {
            lvwConfig.Items.Find(key.ToString("d"), false)[0].Selected = true;
        }

        private void LoadConfigs(Account account)
        {
            lvwConfig.Items.Clear();
            //load any configuration(s) for this account
            var defaultKey = "0";
            if (account.Configurations == null || account.Configurations.Count == 0)
            {
                //always have default/public config
                lvwConfig.Items.Add(new ListViewItem
                    {
                        // ReSharper disable LocalizableElement
                        Name = "0",
                        // ReSharper restore LocalizableElement
                        Text = Resources.config_public_server_description
                            + Resources.config_default_tag
                    });
            }
            else
            {
                defaultKey = Convert.ToString(account.DefaultConfiguration);
                foreach (var configuration in account.Configurations.Values)
                {
                    var key = Convert.ToString(configuration.Key);
                    lvwConfig.Items.Add(new ListViewItem
                    {
                        Name = key,
                        Text = string.Format("{0}{1}",
                            configuration.Description,
                            key == defaultKey
                                ? Resources.config_default_tag 
                                : "")
                    });
                }
            }
            lvwConfig.Items.Find(defaultKey, false)[0].Selected = true;    
        }
    }
}
