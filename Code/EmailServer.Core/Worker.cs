using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace EmailServer.Core
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

        public Worker(int elapseTime, string emailAddress, string emailPassword, string smtpAddress, int smtpPort, bool smtpUseSSL,
            string pop3Address, int pop3Port, bool pop3UseSSL)
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

            //Timer
            this.timer = new System.Timers.Timer();
            this.timer.Interval = elapseTime * 1000;
            this.timer.Elapsed += Timer_Elapsed;

            //Background Worker
            this.BackgroundWorker = new BackgroundWorker();
            this.BackgroundWorker.DoWork += BackgroundWorker_DoWork;
            this.BackgroundWorker.WorkerSupportsCancellation = true;
            this.BackgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
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
            //TimeSpan tpInicio = new TimeSpan(DateTime.Now.Ticks);
            //TimeSpan tpFinal;

            ////Si es la primera vez, cargamos la configuración
            //if (!this.estaCargado)
            //{
            //    this.CargarInfoAsync();
            //    tpFinal = new TimeSpan(DateTime.Now.Ticks);
            //    this.tiempoEnCargar = tpFinal.Subtract(tpInicio).TotalSeconds.ToString();
            //}
            //else
            //{
            //    //De otro modo, vamos a leer
            //    DataTable dt = this.BuscarYLeerNuevosAsync();
            //    tpFinal = new TimeSpan(DateTime.Now.Ticks);
            //    this.tiempoUltimaLectura = tpFinal.Subtract(tpInicio).TotalSeconds.ToString();
            //}

            //this.actualizarPantalla = true;
            //this.esperando = this.estaDetenido ? 0 : this.esperando;
            //this.tmrEspera.Enabled = !this.estaDetenido;
            //this.statusProceso = this.estaDetenido ? "Detenido" : this.statusProceso;
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            POP3.Fetch(this.Pop3Address, Pop3Port, Pop3UseSSL, EmailAddress, EmailPassword, new List<string>());
        }

    }
}
