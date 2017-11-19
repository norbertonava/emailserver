using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmailServer.Core;

namespace EmailServer.UI
{
    public partial class ConfigurationForm : Form, IForm
    {
        private bool hasChanges = false;

        public ConfigurationForm()
        {
            InitializeComponent();
        }

        private void ConfigurationForm_Load(object sender, EventArgs e)
        {
            DataTable config = Database.GetConfiguration();
            if (config.Rows.Count == 0)
                return;

            DataRow row = config.Rows[0];

            this.numSeconds.Value = Convert.ToInt32(row["fetch_seconds"]);
            this.txtEmail.Text = row["email"].ToString();
            this.txtSMTPAddress.Text = row["smtp_url"].ToString();
            this.numSMTPPort.Value = Convert.ToInt32(row["smtp_port"]);
            this.chkSMTPUseSSL.Checked = row["smtp_usessl"].ToString().Equals("1");
            this.txtPOP3Address.Text = row["pop3_url"].ToString();
            this.numPOP3Port.Value = Convert.ToInt32(row["pop3_port"]);
            this.chkPOP3UseSSL.Checked = row["pop3_usessl"].ToString().Equals("1");
            this.txtPassword.Text = row["email_password"].ToString();
        }

        private void Control_ValueChanged(object sender, EventArgs e)
        {
            hasChanges = true;
            EnableToolbar();
        }

        private void ConfigurationForm_Activated(object sender, EventArgs e)
        {
            EnableToolbar();
        }

        private void EnableToolbar()
        {
            MDI mdi = ((MDI)this.MdiParent);
            mdi.EnableToolbar(this.hasChanges, false, false);
        }

        public void Save()
        {
            if (Convert.ToInt32(this.numSeconds.Value) < 0
                || string.IsNullOrEmpty(this.txtEmail.Text)
                || string.IsNullOrEmpty(this.txtSMTPAddress.Text)
                || Convert.ToInt32(this.numSMTPPort.Value) < 0
                || string.IsNullOrEmpty(this.txtPOP3Address.Text)
                || Convert.ToInt32(this.numPOP3Port.Value) < 0
                || string.IsNullOrEmpty(this.txtPassword.Text))
            {
                MessageBox.Show("Please enter all the required fields");
                return;
            }

            Database.SaveConfiguration(Convert.ToInt32(this.numSeconds.Value), this.txtEmail.Text, this.txtSMTPAddress.Text, Convert.ToInt32(this.numSMTPPort.Value), this.chkSMTPUseSSL.Checked,
                this.txtPOP3Address.Text, Convert.ToInt32(this.numPOP3Port.Value), this.chkPOP3UseSSL.Checked, this.txtPassword.Text);
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void IFormSave()
        {
            throw new NotImplementedException();
        }
    }
}