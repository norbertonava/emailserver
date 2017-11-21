using EmailServer.UI.Process;
using System;
using System.Data;
using System.Windows.Forms;

namespace EmailServer.UI
{
    public partial class UserForm : Form, IForm
    {
        private bool hasChanges = false;

        public UserForm()
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
                    if (string.IsNullOrEmpty(txtPhonNumber.Text) || txtPhonNumber.Text.Length != 10 || !Int64.TryParse(txtPhonNumber.Text, out num))
                    {
                        MessageBox.Show("Please enter a valid 10-digit number.");
                    }
                    else if (client.UserExists(num.ToString()))
                    {
                        MessageBox.Show("User already exists.");
                    }
                    else
                    {
                        client.SaveUser(num.ToString());
                        this.txtPhonNumber.Text = string.Empty;
                        LoadGrid();
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

        private void UserForm_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void LoadGrid()
        {
            try
            {
                using (RemoteAdmin.RemoteAdminSoapClient client = new RemoteAdmin.RemoteAdminSoapClient())
                {
                    DataTable dt = client.GetUsers();
                    userGrid.DataSource = dt;
                    userGrid.AllowUserToAddRows = false;
                    userGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                }
            }
            catch (Exception e)
            {
                FileLog.SaveEntryToTextFile(e.Message, e);
            }
        }

        private void userGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            this.userGrid.Columns["phone_number"].HeaderText = "Phone Number";
            this.userGrid.Columns["phone_number"].Width = 200;
            this.userGrid.Columns["phone_number"].ReadOnly = true;

            this.userGrid.Columns["date_modified"].HeaderText = "Date Modified";
            this.userGrid.Columns["date_modified"].Width = 200;
            this.userGrid.Columns["date_modified"].ReadOnly = true;
        }

        private void txtPhonNumber_TextChanged(object sender, EventArgs e)
        {
            this.hasChanges = true;
            EnableToolbar(this.hasChanges, false, false);
        }

        private void UserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (hasChanges)
            {
                DialogResult dr = MessageBox.Show("Do you want to close and discard changes?", "Exit", MessageBoxButtons.OKCancel);
                e.Cancel = dr == DialogResult.Cancel;
            }
        }

        private void UserForm_Activated(object sender, EventArgs e)
        {
            EnableToolbar(this.hasChanges, false, false);
        }
    }
}
