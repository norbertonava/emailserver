using EmailServer.Process;
using EmailServer.UI.Process;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace EmailServer.UI
{
    public partial class ActivityForm : Form, IForm
    {
        private Worker worker;
        bool isRunning;
        Timer timer;
        private long last_log_id;

        public ActivityForm()
        {
            InitializeComponent();
        }

        private void ActivityForm_Activated(object sender, EventArgs e)
        {
            EnableToolbar(true, !isRunning, isRunning);
        }

        public void Save()
        {
            if (this.gridLog.DataSource == null || this.gridLog.Rows.Count == 0)
            {
                MessageBox.Show("There's no activity to save.");
                return;
            }

            string FileName = string.Empty;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text file (*.txt)|*.txt|All files (*.*)|*.*";

            if (saveFileDialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            FileName = saveFileDialog.FileName;

            StringBuilder strb = new StringBuilder();
            strb.Append("Date\t\t\t\tAction\t\t\tData");

            foreach (DataRow row in ((DataTable)this.gridLog.DataSource).Rows)
            {
                strb.AppendFormat("{0}\t{1}\t\t{2}\n", row["date"].ToString(), row["action"].ToString(), row["data"]);
            }

            if (File.Exists(FileName))
                File.Delete(FileName);

            File.WriteAllText(FileName, strb.ToString());
        }

        public void Pause()
        {
            this.worker.Pause();
            isRunning = false;
            EnableToolbar(true, !isRunning, isRunning);
        }

        public void Start()
        {
            try
            {
                DataTable config = null;

                using (RemoteAdmin.RemoteAdminSoapClient client = new RemoteAdmin.RemoteAdminSoapClient())
                {
                    config = client.GetConfiguration();//Database.GetConfiguration();
                }

                if (config.Rows.Count == 0)
                {
                    MessageBox.Show("Must configure the server first.");
                    return;
                }

                if (worker == null)
                {
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
                    worker = new Worker(Seconds, EmailAddress, Password, SMTPAddress, SMTPPort, SMTPUseSSL, POP3Address, POP3Port, POP3UseSSL);
                }

                this.last_log_id = worker.Log_id;
                worker.Start();

                isRunning = true;
                EnableToolbar(true, !isRunning, isRunning);
            }
            catch (Exception e)
            {
                FileLog.SaveEntryToTextFile(e.Message, e);
            }
        }

        public void EnableToolbar(bool save, bool start, bool pause)
        {
            MDI mdi = ((MDI)this.MdiParent);
            mdi.EnableToolbar(save, start, pause);
        }

        private void ActivityForm_Load(object sender, EventArgs e)
        {
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Enabled = true;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (worker == null)
                return;
            MDI mdi = (MDI)this.MdiParent;
            mdi.SetStatus(worker.Status);

            if (last_log_id < worker.Log_id)
            {
                DataTable dt = worker.Log.Copy();
                this.gridLog.DataSource = dt;
                last_log_id = worker.Log_id;
            }
        }

        private void ActivityForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(e.CloseReason != CloseReason.MdiFormClosing)
                e.Cancel = true;
        }

        private void gridLog_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            this.gridLog.Columns["date"].HeaderText = "Date";
            this.gridLog.Columns["date"].Width = 200;
            this.gridLog.Columns["date"].ReadOnly = true;

            this.gridLog.Columns["action"].HeaderText = "Action";
            this.gridLog.Columns["action"].Width = 200;
            this.gridLog.Columns["action"].ReadOnly = true;

            this.gridLog.Columns["data"].HeaderText = "Data";
            this.gridLog.Columns["data"].Width = 400;
            this.gridLog.Columns["data"].ReadOnly = true;
        }
    }
}