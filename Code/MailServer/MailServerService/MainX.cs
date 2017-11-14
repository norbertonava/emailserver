using System;
using System.IO;
using System.Collections;
using System.Configuration.Install;
using System.Reflection;
using System.ServiceProcess;

using Merculia.MailServer;
using Merculia.MailServer.UI;

namespace Merculia.MailServer
{
	/// <summary>
	/// Application main class.
	/// </summary>
	public class MainX
	{
		#region static method Main

		/// <summary>
		/// Application main entry point.
		/// </summary>
		/// <param name="args">Command line argumnets.</param>
		public static void Main(string[] args)
		{
            try{
                if (args.Length > 0 && args[0].ToLower() == "-install")
                {
                    ManagedInstallerClass.InstallHelper(new string[] { "MailServerService.exe" });

                    ServiceController c = new ServiceController("Merculia Mail Server");
                    c.Start();
                }
                else if (args.Length > 0 && args[0].ToLower() == "-uninstall")
                {
                    ManagedInstallerClass.InstallHelper(new string[] { "/u", "MailServerService.exe" });
                }
                else if (args.Length > 0 && args[0].ToLower() == "-debug")
                {
                    System.ServiceProcess.ServiceBase[] servicesToRun = new System.ServiceProcess.ServiceBase[] { new MailServer_Service() };
                    RunInteractive(servicesToRun);
                }
                else
                {
                    System.ServiceProcess.ServiceBase[] servicesToRun = new System.ServiceProcess.ServiceBase[] { new MailServer_Service() };
                    System.ServiceProcess.ServiceBase.Run(servicesToRun);
                }
            }
            catch(Exception x){
                if(x.InnerException is System.Security.SecurityException){
                    System.Windows.Forms.MessageBox.Show("You need administrator rights to run this application, run this application 'Run as Administrator'.","Error:",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
                }
                else{
                    System.Windows.Forms.MessageBox.Show("Error: " + x.ToString(),"Error:",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
                }

                Environment.Exit(1);
            }
		}

        #endregion

        static void RunInteractive(System.ServiceProcess.ServiceBase[] servicesToRun)
        {
            Console.WriteLine("Services running in interactive mode.");
            Console.WriteLine();

            MethodInfo onStartMethod = typeof(ServiceBase).GetMethod("OnStart",
                BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (ServiceBase service in servicesToRun)
            {
                Console.Write("Starting {0}...", service.ServiceName);
                onStartMethod.Invoke(service, new object[] { new string[] { } });
                Console.Write("Started");
            }

            //Console.WriteLine();
            //Console.WriteLine();
            //Console.WriteLine(
            //    "Press any key to stop the services and end the process...");
            //Console.ReadKey();
            //Console.WriteLine();

            //MethodInfo onStopMethod = typeof(ServiceBase).GetMethod("OnStop",
            //    BindingFlags.Instance | BindingFlags.NonPublic);
            //foreach (ServiceBase service in servicesToRun)
            //{
            //    Console.Write("Stopping {0}...", service.ServiceName);
            //    onStopMethod.Invoke(service, null);
            //    Console.WriteLine("Stopped");
            //}

            //Console.WriteLine("All services stopped.");
            //// Keep the console alive for a second to allow the user to see the message.
            //System.Threading.Thread.Sleep(1000);
        }
    }
}
