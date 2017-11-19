using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailServer.Core
{
    public class Database
    {
        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        }

        public static bool IsValidPhoneNumber()
        {
            return false;
        }

        public static void SaveMessage(string phone_number, string title, string body, string sender_mail, DateTime date_sent)
        {
            using (WMySqlCommand sqlCmd = new WMySqlCommand(GetConnectionString(), "SaveMessage"))
            { 
                sqlCmd.AddParameter("p_phone_number", MySqlDbType.VarChar, phone_number);
                sqlCmd.AddParameter("p_title", MySqlDbType.VarChar, title);
                sqlCmd.AddParameter("p_body", MySqlDbType.VarChar, body);
                sqlCmd.AddParameter("p_sender_mail", MySqlDbType.VarChar, sender_mail);
                sqlCmd.AddParameter("p_date_sent", MySqlDbType.DateTime, date_sent);
                sqlCmd.Execute();
            }
        }

        public static void SaveLogEntry()
        {

        }

        public static DataTable GetConfiguration()
        {
            DataTable dt;
            using (WMySqlCommand sqlCmd = new WMySqlCommand(GetConnectionString(), "GetConfiguration"))
            {
                DataSet ds = sqlCmd.Execute();
                ds.Tables[0].TableName = "Configuration";
                dt = ds.Tables["Configuration"];
            }
            return dt;
        }

        public static void SaveConfiguration(int fetch_seconds, string email, string smtp_url,
            int smtp_port, bool smtp_usessl, string pop3_url, int pop3_port, bool pop3_usessl, string email_password)
        {
            using (WMySqlCommand sqlCmd = new WMySqlCommand(GetConnectionString(), "SaveConfiguration"))
            {
                sqlCmd.AddParameter("p_fetch_seconds", MySqlDbType.Int32, fetch_seconds);
                sqlCmd.AddParameter("p_email", MySqlDbType.VarChar, email);
                sqlCmd.AddParameter("p_smtp_url", MySqlDbType.VarChar, smtp_url);
                sqlCmd.AddParameter("p_smtp_port", MySqlDbType.Int32, smtp_port);
                sqlCmd.AddParameter("p_smtp_usessl", MySqlDbType.Int32, smtp_usessl ? 1 : 0);
                sqlCmd.AddParameter("p_pop3_url", MySqlDbType.VarChar, pop3_url);
                sqlCmd.AddParameter("p_pop3_port", MySqlDbType.Int32, pop3_port);
                sqlCmd.AddParameter("p_pop3_usessl", MySqlDbType.Int32, pop3_usessl ? 1 : 0);
                sqlCmd.AddParameter("p_email_password", MySqlDbType.VarChar, email_password);
                sqlCmd.Execute();
            }
        }
    }
}