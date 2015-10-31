using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ChiaraMail.Properties;

namespace ChiaraMail.Forms
{
    public partial class ContentServerForm : Form
    {
        public int Key;
        public Account Account;
        private EcsConfiguration _configuration;
        public ContentServerForm()
        {
            InitializeComponent();
            Shown += ContentServerFormShown;
            
        }

        private void ContentServerFormShown(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                txtDescription.Enabled = false;
                txtServer.Enabled = false;
                udPort.Enabled = false;
            }
            if (!Account.Configurations.TryGetValue(Key, out _configuration)) return;
            txtDescription.Text = _configuration.Description;
            txtServer.Text = _configuration.Server;
            txtPassword.Text = _configuration.Password;
            udPort.Value = Convert.ToInt32(_configuration.Port);
            chkDefaultOn.Checked = _configuration.DefaultOn;
            chkEncrypt.Checked = _configuration.Encrypt;
            chkNoPlaceholder.Checked = _configuration.NoPlaceholder;
            chkAllowForwarding.Checked = _configuration.AllowForwarding;
        }

        private void BtnOKClick(object sender, EventArgs e)
        {
            //must have Description
            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                MessageBox.Show(Resources.prompt_config_missing_name,
                                Resources.product_name, 
                                MessageBoxButtons.OK, 
                                MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
                return;
            }
            //must have server
            if (string.IsNullOrEmpty(txtServer.Text))
            {
                MessageBox.Show(Resources.prompt_config_missing_server,
                                Resources.product_name, 
                                MessageBoxButtons.OK, 
                                MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
                return;               
            }
            //check for invalid server
            var regex = new Regex(@"\A(https?://)?[\w-]+(\.[\w-]+)+\Z", RegexOptions.IgnoreCase);
            if (!regex.IsMatch(txtServer.Text))
            {
                MessageBox.Show(string.Format(
                    Resources.prompt_config_invalid_server_name, 
                    txtServer.Text),
                                Resources.product_name,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;
                return;
            }
            //disallow dupes for description
            if (Key != 0)
            {
                var host = txtServer.Text + Convert.ToString(udPort.Value).ToLower();
                foreach (var config in Account.Configurations.Values)
                {
                    if (config.Key == Key) continue;
                    if (config.Description.Equals(txtDescription.Text, StringComparison.CurrentCultureIgnoreCase))
                    {
                        MessageBox.Show(string.Format(
                            Resources.prompt_config_duplicate_name,
                            txtDescription.Text),
                                        Resources.product_name,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                        DialogResult = DialogResult.None;
                        return;
                    }
                    //disallow dupes for server+port
                    if (!host.Equals(config.Server + config.Port, StringComparison.CurrentCultureIgnoreCase)) continue;
                    MessageBox.Show(string.Format(
                        Resources.prompt_config_duplicate_host,
                        txtServer.Text + ":" + udPort.Value,
                        Environment.NewLine),
                                    Resources.product_name,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    DialogResult = DialogResult.None;
                    return;
                }
            }
            var configuration = new EcsConfiguration
                {
                    Key = Key,
                    Description = txtDescription.Text,
                    Server = txtServer.Text,
                    Port = Convert.ToInt32(udPort.Value).ToString("d"),
                    Password = txtPassword.Text,
                    DefaultOn = chkDefaultOn.Checked,
                    Encrypt = chkEncrypt.Checked,
                    NoPlaceholder = chkNoPlaceholder.Checked,
                    AllowForwarding = chkAllowForwarding.Checked
                };
            if (!Account.Configurations.ContainsKey(Key))
            {
                Account.Configurations.Add(Key, configuration);
            }
            else
            {
                Account.Configurations[Key] = configuration;
            }
            DialogResult = DialogResult.OK;
        }

        private void ChkDefaultOnCheckedChanged(object sender, EventArgs e)
        {
            chkEncrypt.Enabled = chkDefaultOn.Checked;
            chkNoPlaceholder.Enabled = chkDefaultOn.Checked;
            chkAllowForwarding.Enabled = chkDefaultOn.Checked;
        }

        private void ChkDisplayCheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = (chkDisplay.Checked)
                                           ? new char()
                                           : '*';
        }
    }
}
