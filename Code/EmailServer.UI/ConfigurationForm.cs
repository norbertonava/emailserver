using EmailServer.UI.Process;
using System;
using System.Data;
using System.Windows.Forms;

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
            try
            {
                DataTable config = null;

                using (RemoteAdmin.RemoteAdminSoapClient client = new RemoteAdmin.RemoteAdminSoapClient())
                {
                    config = client.GetConfiguration();
                }

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
                this.txtDisplayName.Text = row["display_name"].ToString();
                this.txtBadResponseMailSubject.Text = row["bad_response_mail_subject"].ToString();
                this.txtBadResponseMailBody.Text = row["bad_response_mail_body"].ToString();
                hasChanges = false;
            }
            catch (Exception ex)
            {
                FileLog.SaveEntryToTextFile(ex.Message, ex);
            }
        }

        private void Control_ValueChanged(object sender, EventArgs e)
        {
            hasChanges = true;
            EnableToolbar(this.hasChanges, false, false);
        }

        private void ConfigurationForm_Activated(object sender, EventArgs e)
        {
            EnableToolbar(this.hasChanges, false, false);
        }

        public void Save()
        {
            if (Convert.ToInt32(this.numSeconds.Value) < 0
                || string.IsNullOrEmpty(this.txtEmail.Text)
                || string.IsNullOrEmpty(this.txtSMTPAddress.Text)
                || Convert.ToInt32(this.numSMTPPort.Value) < 0
                || string.IsNullOrEmpty(this.txtPOP3Address.Text)
                || Convert.ToInt32(this.numPOP3Port.Value) < 0
                || string.IsNullOrEmpty(this.txtPassword.Text)
                || string.IsNullOrEmpty(this.txtDisplayName.Text)
                || string.IsNullOrEmpty(this.txtBadResponseMailSubject.Text)
                || string.IsNullOrEmpty(this.txtBadResponseMailBody.Text)
                )
            {
                MessageBox.Show("Please enter all the required fields");
                return;
            }
            try
            {
                using (RemoteAdmin.RemoteAdminSoapClient client = new RemoteAdmin.RemoteAdminSoapClient())
                {
                    client.SaveConfiguration(Convert.ToInt32(this.numSeconds.Value), this.txtEmail.Text, this.txtSMTPAddress.Text, Convert.ToInt32(this.numSMTPPort.Value),
                    this.chkSMTPUseSSL.Checked, this.txtPOP3Address.Text, Convert.ToInt32(this.numPOP3Port.Value),
                    this.chkPOP3UseSSL.Checked, this.txtPassword.Text, this.txtDisplayName.Text, this.txtBadResponseMailSubject.Text, this.txtBadResponseMailBody.Text);
                }
                this.hasChanges = false;
                MessageBox.Show("Changes were saved. Must restart application.");
            }
            catch (Exception e)
            {
                FileLog.SaveEntryToTextFile(e.Message, e);
            }
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void EnableToolbar(bool save, bool start, bool pause)
        {
            MDI mdi = ((MDI)this.MdiParent);
            mdi.EnableToolbar(save, start, pause);
        }

        private void ConfigurationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (hasChanges)
            {
                DialogResult dr = MessageBox.Show("Do you want to close and discard changes?", "Exit", MessageBoxButtons.OKCancel);
                e.Cancel = dr == DialogResult.Cancel;
            }
        }
    }
}