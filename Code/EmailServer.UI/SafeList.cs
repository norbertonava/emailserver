using EmailServer.UI.Process;
using System;
using System.Data;
using System.Windows.Forms;

namespace EmailServer.UI
{
    public partial class SafeListForm : Form, IForm
    {
        private bool hasChanges = false;

        public SafeListForm()
        {
            InitializeComponent();
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            long num = 0;
            try
            {
                using (RemoteAdmin.RemoteAdminSoapClient client = new RemoteAdmin.RemoteAdminSoapClient())
                {
                    if (string.IsNullOrEmpty(txtPhoneNumber.Text) || txtPhoneNumber.Text.Length != 10 || !Int64.TryParse(txtPhoneNumber.Text, out num))
                    {
                        MessageBox.Show("Please enter a valid 10-digit number.");
                    }
                    else if (string.IsNullOrEmpty(this.txtEmail.Text))
                    {
                        MessageBox.Show("Please enter a valid e-mail address.");
                    }
                    else if (this.txtToken.Text.Length < 10)
                    {
                        MessageBox.Show("Please enter a valid 10-character token.");
                    }
                    else if (!client.UserExists(num.ToString()))
                    {
                        MessageBox.Show("User does not exist.");
                    }
                    else
                    {
                        client.SaveSafeList(num.ToString(), this.txtEmail.Text, this.txtToken.Text);
                        LoadGrid();
                        this.txtEmail.Text = string.Empty;
                        this.txtPhoneNumber.Text = string.Empty;
                        this.txtToken.Text = string.Empty;
                        this.txtPhoneNumber.Focus();
                        this.hasChanges = false;
                    }
                }
            }
            catch (Exception e)
            {
                FileLog.SaveEntryToTextFile(e.Message, e);
            }
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

        private void Control_ValueChanged(object sender, EventArgs e)
        {
            this.hasChanges = true;
            EnableToolbar(this.hasChanges, false, false);
        }

        private void SafeListForm_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void LoadGrid()
        {
            try
            {
                using (RemoteAdmin.RemoteAdminSoapClient client = new RemoteAdmin.RemoteAdminSoapClient())
                {
                    DataTable dt = client.GetSafeList(string.Empty);
                    this.safeListGrid.AllowUserToAddRows = false;
                    this.safeListGrid.DataSource = dt;
                }
            }
            catch (Exception e)
            {
                FileLog.SaveEntryToTextFile(e.Message, e);
            }
        }

        private void SafeListForm_Activated(object sender, EventArgs e)
        {
            EnableToolbar(this.hasChanges, false, false);
        }

        private void safeListGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            this.safeListGrid.Columns["phone_number"].HeaderText = "Phone Number";
            this.safeListGrid.Columns["phone_number"].Width = 120;
            this.safeListGrid.Columns["phone_number"].ReadOnly = true;

            this.safeListGrid.Columns["sender_mail"].HeaderText = "Sender e-mail";
            this.safeListGrid.Columns["sender_mail"].Width = 150;
            this.safeListGrid.Columns["sender_mail"].ReadOnly = true;

            this.safeListGrid.Columns["token"].HeaderText = "Token";
            this.safeListGrid.Columns["token"].Width = 120;
            this.safeListGrid.Columns["token"].ReadOnly = true;

            this.safeListGrid.Columns["active"].HeaderText = "Active";
            this.safeListGrid.Columns["active"].Width = 50;
            this.safeListGrid.Columns["active"].ReadOnly = true;
            
            this.safeListGrid.Columns["active_date"].HeaderText = "Active date";
            this.safeListGrid.Columns["active_date"].Width = 150;
            this.safeListGrid.Columns["active_date"].ReadOnly = true;
        }

        private void SafeListForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (hasChanges)
            {
                DialogResult dr = MessageBox.Show("Do you want to close and discard changes?", "Exit", MessageBoxButtons.OKCancel);
                e.Cancel = dr == DialogResult.Cancel;
            }
        }
    }
}