using EmailServer.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Descripción breve de EmailServer
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
// [System.Web.Script.Services.ScriptService]
public class RemoteAdmin : System.Web.Services.WebService
{

    public RemoteAdmin()
    {

        //Elimine la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hola a todos";
    }

    [WebMethod]
    public DataTable GetConfiguration()
    {
        return Database.GetConfiguration();
    }

    [WebMethod]
    public void SaveConfiguration(int fetch_seconds, string email, string smtp_url,
            int smtp_port, bool smtp_usessl, string pop3_url, int pop3_port, bool pop3_usessl, string email_password, string display_name,
            string bad_response_mail_subject, string bad_response_mail_body)
    {
        Database.SaveConfiguration(fetch_seconds, email, smtp_url,
            smtp_port, smtp_usessl, pop3_url, pop3_port, pop3_usessl, email_password, display_name,
            bad_response_mail_subject, bad_response_mail_body);
    }

    [WebMethod]
    public long GetMaxLogId()
    {
        return Database.GetMaxLogId();
    }

    [WebMethod]
    public DataTable GetLogInfo(long log_id)
    {
        return Database.GetLogInfo(log_id);
    }

    [WebMethod]
    public void FetchPOP3Fetch(string hostname, int port, bool useSsl, string username, string password)
    {
        POP3.Fetch(hostname, port, useSsl, username, password);
    }

    [WebMethod]
    public bool UserExists(string phone_number)
    {
        return Database.UserExists(phone_number);
    }

    [WebMethod]
    public void SaveSafeList(string phone_number, string sender_mail, string token)
    {
        Database.SaveSafeList(phone_number, sender_mail, token);
    }

    [WebMethod]
    public DataTable GetSafeList(string sender_mail)
    {
        return Database.GetSafeList(sender_mail);
    }

    [WebMethod]
    public void SaveUser(string phone_number)
    {
        Database.SaveUser(phone_number);
    }

    [WebMethod]
    public DataTable GetUsers()
    {
        return Database.GetUsers();
    }
}
