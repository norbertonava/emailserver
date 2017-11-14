using Merculia.MailServer;
using Merculia.Net;
using Merculia.Net.IMAP.Server;
using System.ComponentModel;
using System.IO;
using System.Net;
using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Data;
using System.Configuration;

namespace WebService.WebServices
{
    /// <summary>
    /// Descripción breve de RemoteAdmin
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class RemoteAdmin : System.Web.Services.WebService
    {

        private IMailServerApi m_pApi = null;

        private IContainer components = null;

        public RemoteAdmin()
        {
            string conn = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            this.InitializeComponent();
            this.m_pApi = new mssql_API(conn);
            //this.m_pApi = (IMailServerApi)base.Application["MailAPI"];
        }

        private void InitializeComponent()
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        [WebMethod]
        public DataSet GetDomains()
        {
            return this.m_pApi.GetDomains().Table.DataSet;
        }

        [WebMethod]
        public void AddDomain(string domainID, string domainName, string description)
        {
            this.m_pApi.AddDomain(domainID, domainName, description);
        }

        [WebMethod]
        public void DeleteDomain(string domainID)
        {
            this.m_pApi.DeleteDomain(domainID);
        }

        [WebMethod]
        public bool DomainExists(string source)
        {
            return this.m_pApi.DomainExists(source);
        }

        [WebMethod]
        public DataSet GetUsers(string domainName)
        {
            return this.m_pApi.GetUsers(domainName).Table.DataSet;
        }

        [WebMethod]
        public void AddUser(string userID, string userName, string fullName, string password, string description, string domainName, int mailboxSize, bool enabled, UserPermissions_enum permissions)
        {
            this.m_pApi.AddUser(userID, userName, fullName, password, description, domainName, mailboxSize, enabled, permissions);

        }

        [WebMethod]
        public void DeleteUser(string userID)
        {
            this.m_pApi.DeleteUser(userID);
        }

        [WebMethod]
        public void UpdateUser(string userID, string userName, string fullName, string password, string description, string domainName, int mailboxSize, bool enabled, UserPermissions_enum permissions)
        {
            this.m_pApi.UpdateUser(userID, userName, fullName, password, description, domainName, mailboxSize, enabled, permissions);
        }

        [WebMethod]
        public void AddUserAddress(string addressID, string userName, string address)
        {
            this.m_pApi.AddUserAddress(userName, address);
        }

        [WebMethod]
        public void DeleteUserAddress(string addressID)
        {
            this.m_pApi.DeleteUserAddress(addressID);
        }

        [WebMethod]
        public DataSet GetUserAddresses(string userName)
        {
            return this.m_pApi.GetUserAddresses(userName).Table.DataSet;
        }

        [WebMethod]
        public bool UserExists(string userName)
        {
            return this.m_pApi.UserExists(userName);
        }

        [WebMethod]
        public string MapUser(string emailAddress)
        {
            return this.m_pApi.MapUser(emailAddress);
        }

        [WebMethod]
        public bool ValidateMailboxSize(string userName)
        {
            return this.m_pApi.ValidateMailboxSize(userName);
        }

        [WebMethod]
        public void AddUserRemoteServer(string serverID, string userName, string remoteServer, int remotePort, string remoteUser, string remotePassword)
        {
            //this.m_pApi.AddUserRemoteServer(serverID, userName, remoteServer, remotePort, remoteUser, remotePassword);
            //NNAVA
            this.m_pApi.AddUserRemoteServer(serverID, userName, "", remoteServer, remotePort, remoteUser, remotePassword, false, true);

        }

        [WebMethod]
        public void DeleteUserRemoteServer(string serverID)
        {
            this.m_pApi.DeleteUserRemoteServer(serverID);
        }

        [WebMethod]
        public DataSet GetUserRemoteServers(string userName)
        {
            return this.m_pApi.GetUserRemoteServers(userName).Table.DataSet;
        }

        [WebMethod]
        public DataSet GetUserMessageRules(string userName)
        {
            return this.m_pApi.GetUserMessageRules(userName).Table.DataSet;
        }

        [WebMethod]
        public void AddUserMessageRule(string userID, string ruleID, long cost, bool enabled, GlobalMessageRule_CheckNextRule_enum checkNextRule, string description, string matchExpression)
        {
            //this.m_pApi.AddUserMessageRule(ruleID, userName, description, (CompareSource)type, fieldName, (CompareType)compareType, compareValue, (MatchAction)action, actionData, storeMessage, checkNextRule, cost, enabled);
            this.m_pApi.AddUserMessageRule(userID, ruleID, cost, enabled, checkNextRule, description, matchExpression);

        }

        [WebMethod]
        public void DeleteUserMessageRule(string userId, string ruleID)
        {
            this.m_pApi.DeleteUserMessageRule(userId, ruleID);
        }

        [WebMethod]
        void UpdateUserMessageRule(string userID, string ruleID, long cost, bool enabled, GlobalMessageRule_CheckNextRule_enum checkNextRule, string description, string matchExpression)
        //public void UpdateUserMessageRule(string ruleID, string userName, string description, int type, string fieldName, int compareType, string compareValue, int action, string actionData, bool storeMessage, bool checkNextRule, int cost, bool enabled)
        {
            //this.m_pApi.UpdateUserMessageRule(ruleID, userName, description, (CompareSource)type, fieldName, (CompareType)compareType, compareValue, (MatchAction)action, actionData, storeMessage, checkNextRule, cost, enabled);
            this.m_pApi.UpdateUserMessageRule(userID, ruleID, cost, enabled, checkNextRule, description, matchExpression);

        }

        [WebMethod]
        public DataSet AuthUser(string userName, string passwData, string authData, int authType)
        {
            return this.m_pApi.AuthUser(userName, passwData, authData, (Merculia.Net.AuthType)authType);
        }

        [WebMethod]
        public DataSet GetMailingLists(string domainName)
        {
            return this.m_pApi.GetMailingLists(domainName).Table.DataSet;
        }

        [WebMethod]
        public void AddMailingList(string mailingListID, string mailingListName, string description, string domainName, bool isPublic)
        {
            this.m_pApi.AddMailingList(mailingListID, mailingListName, description, domainName, isPublic);
        }

        [WebMethod]
        public void DeleteMailingList(string mailingListID)
        {
            this.m_pApi.DeleteMailingList(mailingListID);
        }

        [WebMethod]
        public void UpdateMailingList(string mailingListID, string mailingListName, string description, string domainName, bool isPublic)
        {
            this.m_pApi.UpdateMailingList(mailingListID, mailingListName, description, domainName, isPublic);
        }

        [WebMethod]
        public void AddMailingListAddress(string addressID, string mailingListName, string address)
        {
            this.m_pApi.AddMailingListAddress(addressID, mailingListName, address);
        }

        [WebMethod]
        public void DeleteMailingListAddress(string addressID)
        {
            this.m_pApi.DeleteMailingListAddress(addressID);
        }

        [WebMethod]
        public DataSet GetMailingListAddresses(string mailingListName)
        {
            return this.m_pApi.GetMailingListAddresses(mailingListName).Table.DataSet;
        }

        [WebMethod]
        public bool MailingListExists(string mailingListName)
        {
            return this.m_pApi.MailingListExists(mailingListName);
        }

        [WebMethod]
        public bool IsMailingListPublic(string emailAddress)
        {
            throw new NotImplementedException();
            //return this.m_pApi.IsMailingListPublic(emailAddress);
        }

        [WebMethod]
        //public DataSet GetRoutes(string domainName)
        public DataSet GetRoutes()
        {
            return this.m_pApi.GetRoutes().Table.DataSet;
        }

        [WebMethod]
        //public void AddRoute(string routeID, string pattern, string mailbox, string description, string domainName)
        public void AddRoute(string routeID, long cost, bool enabled, string description, string pattern, RouteAction_enum action, byte[] actionData)
        {
            this.m_pApi.AddRoute(routeID, cost, enabled, description, pattern, action, actionData);
        }

        [WebMethod]
        public void DeleteRoute(string routeID)
        {
            this.m_pApi.DeleteRoute(routeID);
        }

        [WebMethod]
        //public void UpdateRoute(string routeID, string pattern, string mailbox, string description, string domainName)
        public void UpdateRoute(string routeID, long cost, bool enabled, string description, string pattern, RouteAction_enum action, byte[] actionData)
        {
            this.m_pApi.UpdateRoute(routeID, cost, enabled, description, pattern, action, actionData);
        }

        [WebMethod]
        public string GetMailboxFromPattern(string emailAddress)
        {
            //return this.m_pApi.GetMailboxFromPattern(emailAddress);
            throw new NotImplementedException();
        }

        [WebMethod]
        public DataSet GetMessagesInfoData(string userName, string folderOwnerUser, string folder)
        {
            //Merculia.Net.IMAP.Server.IMAP_Messages iMAP_Messages = new Merculia.Net.IMAP.Server.IMAP_Messages("");

            List<IMAP_MessageInfo> iMAP_Messages = new List<IMAP_MessageInfo>();
            this.m_pApi.GetMessagesInfo(userName, folderOwnerUser, folder, iMAP_Messages);
            DataSet dataSet = new DataSet();
            DataTable dataTable = dataSet.Tables.Add("MessagesInfo");
            dataTable.Columns.Add("messageID");
            dataTable.Columns.Add("UID");
            dataTable.Columns.Add("flags");
            dataTable.Columns.Add("size");
            dataTable.Columns.Add("date");

            for (int i = 0; i < iMAP_Messages.Count; i++)
            {
                IMAP_MessageInfo iMAP_Message = iMAP_Messages[i];
                DataRow dataRow = dataTable.NewRow();
                dataRow["messageID"] = iMAP_Message.ID;
                dataRow["UID"] = iMAP_Message.UID;
                dataRow["flags"] = Net_Utils.ArrayToString(iMAP_Message.Flags, " ");
                dataRow["size"] = iMAP_Message.Size;
                dataRow["date"] = iMAP_Message.InternalDate;
                dataTable.Rows.Add(dataRow);
            }
            return dataSet;
        }

        [WebMethod]
        public void StoreMessage(string accessingUser, string folderOwnerUser, string folder, Stream msgStream, DateTime date, string[] flags)
        {
            this.m_pApi.StoreMessage(accessingUser, folderOwnerUser, folder, msgStream, date, flags);
        }

        [WebMethod]
        public void StoreMessageFlags(string accessingUser, string folderOwnerUser, string folder, IMAP_MessageInfo messageInfo, string[] flags)
        {
            //Merculia.Net.IMAP.Server.IMAP_Messages iMAP_Messages = new Merculia.Net.IMAP.Server.IMAP_Messages("");
            //iMAP_Messages.AddMessage(msgID, (int)UID, (Merculia.Net.IMAP.IMAP_MessageFlags)msgFlags, size, date);
            //this.m_pApi.StoreMessageFlags(mailbox, folder, iMAP_Messages[0], iMAP_Messages[0].Flags);
            //return iMAP_Messages[0].MessageID;
            this.m_pApi.StoreMessageFlags(accessingUser, folderOwnerUser, folder, messageInfo, flags);
        }

        [WebMethod]
        public void DeleteMessage(string accessingUser, string folderOwnerUser, string folder, string messageID, int uid)
        {
            this.m_pApi.DeleteMessage(accessingUser, folderOwnerUser, folder, messageID, uid);

        }

        [WebMethod]
        public void GetMessagesInfo(string accessingUser, string folderOwnerUser, string folder, List<IMAP_MessageInfo> messageInfos)
        {
            this.m_pApi.GetMessagesInfo(accessingUser, folderOwnerUser, folder, messageInfos);
        }

        [WebMethod]
        public byte[] GetMessageTopLines(string accessingUser, string folderOwnerUser, string folder, string msgID, int nrLines)
        {
            return this.m_pApi.GetMessageTopLines(accessingUser, folderOwnerUser, folder, msgID, nrLines);

        }

        [WebMethod]
        public void CopyMessage(string accessingUser, string folderOwnerUser, string folder, string destFolderUser, string destFolder, IMAP_MessageInfo messageInfo)
        {
            //Merculia.Net.IMAP.Server.IMAP_Messages iMAP_Messages = new Merculia.Net.IMAP.Server.IMAP_Messages("");
            //iMAP_Messages.AddMessage(msgID, (int)UID, (Merculia.Net.IMAP.IMAP_MessageFlags)msgFlags, size, date);
            //this.m_pApi.CopyMessage(userName, destFolderUser, folder, destFolder, iMAP_Messages[0]);
            this.m_pApi.CopyMessage(accessingUser, folderOwnerUser, folder, destFolderUser, destFolder, messageInfo);
        }

        [WebMethod]
        public string[] GetFolders(string userName, bool includeSharedFolders)
        {
            return this.m_pApi.GetFolders(userName, includeSharedFolders);
        }

        [WebMethod]
        public string[] GetSubscribedFolders(string userName)
        {
            return this.m_pApi.GetSubscribedFolders(userName);
        }

        [WebMethod]
        public void SubscribeFolder(string userName, string folder)
        {
            this.m_pApi.SubscribeFolder(userName, folder);
        }

        [WebMethod]
        public void UnSubscribeFolder(string userName, string folder)
        {
            this.m_pApi.UnSubscribeFolder(userName, folder);
        }

        [WebMethod]
        public void CreateFolder(string accessingUser, string folderOwnerUser, string folder)
        {
            this.m_pApi.CreateFolder(accessingUser, folderOwnerUser, folder);

        }

        [WebMethod]
        public void DeleteFolder(string accessingUser, string folderOwnerUser, string folder)
        {
            this.m_pApi.DeleteFolder(accessingUser, folderOwnerUser, folder);
        }

        [WebMethod]
        public void RenameFolder(string accessingUser, string folderOwnerUser, string folder, string newFolder)
        {
            this.m_pApi.RenameFolder(accessingUser, folderOwnerUser, folder, newFolder);

        }

        [WebMethod]
        public long GetMailboxSize(string userName)
        {
            return this.m_pApi.GetMailboxSize(userName);
        }

        [WebMethod]
        public DataSet GetSecurityList()
        {
            return this.m_pApi.GetSecurityList().Table.DataSet;
        }

        [WebMethod]
        public void AddSecurityEntryAddSecurityEntry(string id, bool enabled, string description, Service_enum service, IPSecurityAction_enum action, long startIP, long endIP)
        {
            IPAddress iPAddress = new IPAddress(startIP);
            IPAddress endIPAddress = new IPAddress(endIP);
            this.m_pApi.AddSecurityEntry(id, enabled, description, service, action, iPAddress, endIPAddress);
        }

        [WebMethod]
        public void DeleteSecurityEntry(string securityID)
        {
            this.m_pApi.DeleteSecurityEntry(securityID);
        }

        [WebMethod]
        public void UpdateSecurityEntry(string id, bool enabled, string description, Service_enum service, IPSecurityAction_enum action, long startIP, long endIP)
        {
            IPAddress iPAddress = new IPAddress(startIP);
            IPAddress endIPAddress = new IPAddress(endIP);
            this.m_pApi.UpdateSecurityEntry(id, enabled, description, service, action, iPAddress, endIPAddress);
        }

        //[WebMethod]
        //public bool IsRelayAllowed(string userName, string ip)
        //{
        //	return this.m_pApi.IsRelayAllowed(userName, ip);
        //}

        //[WebMethod]
        //public bool IsAccessAllowed(string protocol, string ip)
        //{
        //	return this.m_pApi.IsAccessAllowed(protocol, ip);
        //}

        [WebMethod]
        public DataSet GetFilters()
        {
            return this.m_pApi.GetFilters().Table.DataSet;
        }

        [WebMethod]
        public void AddFilter(string filterID, string description, string type, string assembly, string className, int cost, bool enabled)
        {
            this.m_pApi.AddFilter(filterID, description, type, assembly, className, cost, enabled);
        }

        [WebMethod]
        public void DeleteFilter(string filterID)
        {
            this.m_pApi.DeleteFilter(filterID);
        }

        [WebMethod]
        public void UpdateFilter(string filterID, string description, string type, string assembly, string className, int cost, bool enabled)
        {
            this.m_pApi.UpdateFilter(filterID, description, type, assembly, className, cost, enabled);
        }

        [WebMethod]
        public DataSet GetSettings()
        {
            return this.m_pApi.GetSettings().Table.DataSet;
        }

        [WebMethod]
        public void UpdateSettings(DataSet settings)
        {
            this.m_pApi.UpdateSettings(settings.Tables[0].Rows[0]);
        }
    }
}
