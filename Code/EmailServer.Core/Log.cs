﻿using System;
using System.IO;
using System.Text;

namespace EmailServer.Core
{
    public class Log
    {

        public static void SaveLogEntryToDB(string action, string message)
        {
            Database.SaveLogEntry(action, message);
        }

        public static void SaveEntryToTextFile(string Message, Exception e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("{0} at {1}", Message, DateTime.Now.ToString()));
            sb.AppendLine("Stack trace:");
            sb.AppendLine(e.StackTrace);
            sb.AppendLine("================================================");

            File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "log.txt", sb.ToString());
            sb.Clear();
        }
    }
}
