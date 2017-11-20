using EmailServer.Core;
using System;
using System.Data;
using System.Windows.Forms;

namespace EmailServer.UI
{
    public partial class UserForm : Form, IForm
    {
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
            if (string.IsNullOrEmpty(txtPhonNumber.Text) || txtPhonNumber.Text.Length != 10 || !Int64.TryParse(txtPhonNumber.Text, out num))
            {
                MessageBox.Show("Please enter a valid 10-digit number.");
            }
            else
            {
                Database.SaveUser(num.ToString());
                this.txtPhonNumber.Text = string.Empty;
                LoadGrid();
            }
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void LoadGrid()
        {
            DataTable dt = Database.GetUsers();
            userGrid.DataSource = dt;
            userGrid.AllowUserToAddRows = false;
            userGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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
    }
}
