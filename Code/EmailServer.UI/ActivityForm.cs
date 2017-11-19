using EmailServer.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmailServer.UI
{
    public partial class ActivityForm : Form, IForm
    {
        private Worker worker;
        bool isRunning;

        public ActivityForm()
        {
            InitializeComponent();
        }

        private void ActivityForm_Activated(object sender, EventArgs e)
        {
            EnableToolbar();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Pause()
        {

            isRunning = false;
            EnableToolbar();
        }

        public void Start()
        {
            DataTable config = Database.GetConfiguration();
            if (config.Rows.Count == 0)
            {
                MessageBox.Show("Must configure the server first.");
                return;
            }

            DataRow row = config.Rows[0];
            int Seconds = Convert.ToInt32(row["fetch_seconds"]);
            string EmailAddress = row["email"].ToString();
            string SMTPAddress = row["smtp_url"].ToString();
            int SMTPPort = Convert.ToInt32(row["smtp_port"]);
            bool SMTPUseSSL = row["smtp_usessl"].ToString().Equals("1");
            string POP3Address = row["pop3_url"].ToString();
            int POP3Port = Convert.ToInt32(row["pop3_port"]);
            bool POP3UseSSL = row["pop3_usessl"].ToString().Equals("1");
            string Password = row["email_password"].ToString();


            if (worker == null)
                worker = new Worker(Seconds, EmailAddress, Password, SMTPAddress, SMTPPort, SMTPUseSSL, POP3Address, POP3Port, POP3UseSSL);
            worker.Start();

            isRunning = true;
            EnableToolbar();
        }

        private void EnableToolbar()
        {
            MDI mdi = ((MDI)this.MdiParent);
            mdi.EnableToolbar(true, !isRunning, isRunning);
        }
    }
}
