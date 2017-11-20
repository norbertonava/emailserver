using System;
using System.Data;
using System.Net.Mail;

namespace EmailServer.Core
{
    public class SMTP
    {
        public static void SendBadResponse(string toEmailAddress)
        {
            DataTable dt = Database.GetConfiguration();

            string email = dt.Rows[0]["email"].ToString();
            string smtp_url = dt.Rows[0]["smtp_url"].ToString();
            int smtp_port = Convert.ToInt32(dt.Rows[0]["smtp_port"]);
            bool smtp_usessl = dt.Rows[0]["smtp_usessl"].ToString().Equals("1");
            string email_password = dt.Rows[0]["email_password"].ToString();
            string display_name = dt.Rows[0]["display_name"].ToString();
            string subject = dt.Rows[0]["bad_response_mail_subject"].ToString();
            string body = dt.Rows[0]["bad_response_mail_body"].ToString();

            MailMessage message = new MailMessage();

            message.From = new MailAddress(email, display_name);
            message.To.Add(new MailAddress(toEmailAddress));

            message.Subject = subject;
            message.Body = body;

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(email, email_password);

            client.Port = smtp_port;
            client.Host = smtp_url;
            client.EnableSsl = smtp_usessl;
            client.Send(message);
        }
    }
}