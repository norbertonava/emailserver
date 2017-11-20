using System;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace EmailServer.Core
{
    public class Database
    {
        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        }

        public static bool IsValidPhoneNumber(string phone)
        {
            try
            {
                using (WMySqlCommand sqlCmd = new WMySqlCommand(GetConnectionString(), "IsPhoneNumberValid"))
                {
                    sqlCmd.AddParameter("p_phone_number", MySqlDbType.VarChar, phone);
                    DataTable dt = sqlCmd.Execute().Tables[0];

                    if (dt.Rows.Count < 0)
                        return false;

                    DataRow row = dt.Rows[0];
                    if (Convert.ToInt32(row["Exist"]) == 0)
                        return false;
                }
            }
            catch(Exception e)
            {
                SaveLogEntry("Error", string.Format("{0}:{1}", e.Message, e.StackTrace));
            }

            return true;
        }

        public static long SaveMessage(string phone_number, string title, string body, string sender_mail, DateTime date_sent)
        {
            long messageId = 0;
            try
            {
                using (WMySqlCommand sqlCmd = new WMySqlCommand(GetConnectionString(), "SaveMessage"))
                {
                    sqlCmd.AddParameter("p_phone_number", MySqlDbType.VarChar, phone_number);
                    sqlCmd.AddParameter("p_title", MySqlDbType.VarChar, title);
                    sqlCmd.AddParameter("p_body", MySqlDbType.VarChar, body);
                    sqlCmd.AddParameter("p_sender_mail", MySqlDbType.VarChar, sender_mail);
                    sqlCmd.AddParameter("p_date_sent", MySqlDbType.DateTime, date_sent);
                    DataTable dt = sqlCmd.Execute().Tables[0];

                    messageId = Convert.ToInt64(dt.Rows[0]["p_id_message"]);
                }
            }
            catch (Exception e)
            {
                SaveLogEntry("Error", string.Format("{0}:{1}", e.Message, e.StackTrace));
            }
            return messageId;
        }

        public static void SaveAttachment(long message_id, byte[] data, string file_name)
        {
            try
            {
                using (WMySqlCommand sqlCmd = new WMySqlCommand(GetConnectionString(), "SaveAttachment"))
                {
                    sqlCmd.AddParameter("p_id_message", MySqlDbType.Int64, message_id);
                    sqlCmd.AddParameter("p_data", MySqlDbType.LongBlob, data);
                    sqlCmd.AddParameter("p_file_name", MySqlDbType.VarChar, file_name);

                    sqlCmd.Execute();
                }
            }
            catch (Exception e)
            {
                SaveLogEntry("Error", string.Format("{0}:{1}", e.Message, e.StackTrace));
            }
        }

        public static void SaveUser(string phone_number)
        {
            try
            {
                using (WMySqlCommand sqlCmd = new WMySqlCommand(GetConnectionString(), "SaveUser"))
                {
                    sqlCmd.AddParameter("p_phone_number", MySqlDbType.VarChar, phone_number);
                    sqlCmd.Execute();
                }
            }
            catch (Exception e)
            {
                SaveLogEntry("Error", string.Format("{0}:{1}", e.Message, e.StackTrace));
            }
        }

        public static DataTable GetConfiguration()
        {
            DataTable dt = null;
            try
            {
                using (WMySqlCommand sqlCmd = new WMySqlCommand(GetConnectionString(), "GetConfiguration"))
                {
                    DataSet ds = sqlCmd.Execute();
                    ds.Tables[0].TableName = "Configuration";
                    dt = ds.Tables["Configuration"];
                }
            }
            catch (Exception e)
            {
                SaveLogEntry("Error", string.Format("{0}:{1}", e.Message, e.StackTrace));
            }
            return dt;
        }

        public static DataTable GetUsers()
        {
            DataTable dt = null;
            try
            {
                using (WMySqlCommand sqlCmd = new WMySqlCommand(GetConnectionString(), "GetUsers"))
                {
                    DataSet ds = sqlCmd.Execute();
                    ds.Tables[0].TableName = "Users";
                    dt = ds.Tables["Users"];
                }
            }
            catch (Exception e)
            {
                SaveLogEntry("Error", string.Format("{0}:{1}", e.Message, e.StackTrace));
            }
            return dt;
        }

        public static void SaveConfiguration(int fetch_seconds, string email, string smtp_url,
            int smtp_port, bool smtp_usessl, string pop3_url, int pop3_port, bool pop3_usessl, string email_password, string display_name,
            string bad_response_mail_subject, string bad_response_mail_body)
        {
            try
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
                    sqlCmd.AddParameter("p_display_name", MySqlDbType.VarChar, display_name);
                    sqlCmd.AddParameter("p_bad_response_mail_subject", MySqlDbType.VarChar, bad_response_mail_subject);
                    sqlCmd.AddParameter("p_bad_response_mail_body", MySqlDbType.LongText, bad_response_mail_body);

                    sqlCmd.Execute();
                }
            }
            catch (Exception e)
            {
                SaveLogEntry("Error", string.Format("{0}:{1}", e.Message, e.StackTrace));
            }
        }

        public static void SaveLogEntry(string action, string data)
        {
            using (WMySqlCommand sqlCmd = new WMySqlCommand(GetConnectionString(), "SaveLogEntry"))
            {
                sqlCmd.AddParameter("p_action", MySqlDbType.VarChar, action);
                sqlCmd.AddParameter("p_data", MySqlDbType.LongText, data);
                sqlCmd.Execute();
            }
        }
    }
}