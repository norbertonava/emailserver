using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace EmailServer.UI.Process
{
    public class FileLog
    {
        public static void SaveEntryToTextFile(string Message, Exception e)
        {
            MessageBox.Show("Unexpected error ocurred. Refer to log for details.");
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
