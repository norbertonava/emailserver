using EmailServer.UI.Process;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Timers;

namespace EmailServer.Process
{
    public class Worker
    {
        BackgroundWorker BackgroundWorker;
        int ElapseTime;
        string EmailAddress;
        string EmailPassword;
        string SmtpAddress;
        int SmtpPort;
        bool SmtpUseSSL;
        string Pop3Address;
        int Pop3Port;
        bool Pop3UseSSL;
        Timer timer;

        public long Log_id { get; set; }
        public DataTable Log {get;set;}
        public string Status { get; set; }

        public Worker(int elapseTime, string emailAddress, string emailPassword, string smtpAddress, int smtpPort, bool smtpUseSSL,
            string pop3Address, int pop3Port, bool pop3UseSSL)
        {
            try
            {
                this.ElapseTime = elapseTime;
                this.EmailAddress = emailAddress;
                this.EmailPassword = emailPassword;
                this.SmtpAddress = smtpAddress;
                this.SmtpPort = smtpPort;
                this.SmtpUseSSL = smtpUseSSL;
                this.Pop3Address = pop3Address;
                this.Pop3Port = pop3Port;
                this.Pop3UseSSL = pop3UseSSL;


                //Log
                using (UI.RemoteAdmin.RemoteAdminSoapClient client = new UI.RemoteAdmin.RemoteAdminSoapClient())
                {
                    this.Log_id = client.GetMaxLogId();
                }

                this.Log = new DataTable();
                this.Log.Columns.Add(new DataColumn("date", typeof(DateTime)));
                this.Log.Columns.Add(new DataColumn("action", typeof(string)));
                this.Log.Columns.Add(new DataColumn("data", typeof(string)));

                //Timer
                this.timer = new System.Timers.Timer();
                this.timer.Interval = elapseTime * 1000;
                this.timer.Elapsed += Timer_Elapsed;

                //Background Worker
                this.BackgroundWorker = new BackgroundWorker();
                this.BackgroundWorker.DoWork += BackgroundWorker_DoWork;
                this.BackgroundWorker.WorkerSupportsCancellation = true;
                this.BackgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
                this.Status = "Stopped";
            }
            catch (Exception e)
            {
                FileLog.SaveEntryToTextFile(e.Message, e);
            }
        }

        public void Pause()
        {
            this.timer.Enabled = false;
            if (this.BackgroundWorker.IsBusy)
                this.Status = "Pausing...";
            else
                this.Status = "Paused";
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!this.BackgroundWorker.IsBusy)
            {
                this.BackgroundWorker.RunWorkerAsync();
            }
        }

        public void Start()
        {
            this.timer.Enabled = true;
            this.BackgroundWorker.RunWorkerAsync();
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (this.timer.Enabled)
            {
                this.Status = "Waiting to fetch...";
            }
            else
            {
                this.Status = "Paused";
            }
            LoadLog();
        }

        private void LoadLog()
        {
            try
            {
                DataTable dt = null;
                using (UI.RemoteAdmin.RemoteAdminSoapClient client = new UI.RemoteAdmin.RemoteAdminSoapClient())
                {
                    dt = client.GetLogInfo(Log_id);
                }

                foreach (DataRow row in dt.Rows)
                {
                    DataRow newRow = this.Log.NewRow();
                    newRow["date"] = row["date"];
                    newRow["action"] = row["action"];
                    newRow["data"] = row["data"];
                    Log_id = Math.Max(Log_id, Convert.ToInt64(row["id_log"]));
                    this.Log.Rows.Add(newRow);
                }
            }
            catch (Exception e)
            {   
                FileLog.SaveEntryToTextFile(e.Message, e);
            }
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            this.Status = "Fetching";
            try
            {
                using (UI.RemoteAdmin.RemoteAdminSoapClient client = new UI.RemoteAdmin.RemoteAdminSoapClient())
                {
                    client.FetchPOP3Fetch(this.Pop3Address, Pop3Port, Pop3UseSSL, EmailAddress, EmailPassword);
                }
            }
            catch (Exception ex)
            {
                FileLog.SaveEntryToTextFile(ex.Message, ex);
            }
        }
    }
}
