using Merculia.Net;
using Merculia.Net.IMAP;
using Merculia.Net.IMAP.Server;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;

namespace Merculia.MailServer
{
    public class webservices_API : IMailServerApi
    {
        private MailAPI m_pApi = null;

        public webservices_API(string intitString)
        {
            this.m_pApi = new MailAPI();
            string text = "";
            string text2 = "";
            string[] array = intitString.Split(new char[]
            {
                ';'
            });
            string[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                string text3 = array2[i];
                if (text3.ToLower().IndexOf("url=") > -1)
                {
                    if (text3.EndsWith("/"))
                    {
                        this.m_pApi.Url = text3.Substring(4) + "MailAPI.asmx";
                    }
                    else
                    {
                        this.m_pApi.Url = text3.Substring(4) + "/MailAPI.asmx";
                    }
                }
                if (text3.ToLower().IndexOf("username=") > -1)
                {
                    text = text3.Substring(9);
                }
                if (text3.ToLower().IndexOf("password=") > -1)
                {
                    text2 = text3.Substring(9);
                }
            }
            if (text.Length > 0)
            {
                this.m_pApi.Credentials = new NetworkCredential(text, text2);
            }
        }

        public DataView GetDomains()
        {
            return this.m_pApi.GetDomains().Tables[0].DefaultView;
        }

        public void AddDomain(string domainID, string domainName, string description)
        {
            this.m_pApi.AddDomain(domainID, domainName, description);
        }

        public void DeleteDomain(string domainID)
        {
            this.m_pApi.DeleteDomain(domainID);
        }

        public bool DomainExists(string source)
        {
            return this.m_pApi.DomainExists(source);
        }

        public DataView GetUsers(string domainName)
        {
            return this.m_pApi.GetUsers(domainName).Tables[0].DefaultView;
        }

        public void AddUser(string userID, string userName, string fullName, string password, string description, string domainName, int mailboxSize, bool enabled, bool allowRelay)
        {
            this.m_pApi.AddUser(userID, userName, fullName, password, description, domainName, mailboxSize, enabled, allowRelay);
        }

        public void DeleteUser(string userID)
        {
            this.m_pApi.DeleteUser(userID);
        }

        public void UpdateUser(string userID, string userName, string fullName, string password, string description, string domainName, int mailboxSize, bool enabled, bool allowRelay)
        {
            this.m_pApi.UpdateUser(userID, userName, fullName, password, description, domainName, mailboxSize, enabled, allowRelay);
        }

        public void AddUserAddress(string addressID, string userName, string address)
        {
            this.m_pApi.AddUserAddress(addressID, userName, address);
        }

        public void DeleteUserAddress(string addressID)
        {
            this.m_pApi.DeleteUserAddress(addressID);
        }

        public DataView GetUserAddresses(string userName)
        {
            return this.m_pApi.GetUserAddresses(userName).Tables[0].DefaultView;
        }

        public bool UserExists(string userName)
        {
            return this.m_pApi.UserExists(userName);
        }

        public string MapUser(string emailAddress)
        {
            return this.m_pApi.MapUser(emailAddress);
        }

        public bool ValidateMailboxSize(string userName)
        {
            return this.m_pApi.ValidateMailboxSize(userName);
        }

        public void AddUserRemoteServer(string serverID, string userName, string remoteServer, int remotePort, string remoteUser, string remotePassword)
        {
            this.m_pApi.AddUserRemoteServer(serverID, userName, remoteServer, remotePort, remoteUser, remotePassword);
        }

        public void DeleteUserRemoteServer(string serverID)
        {
            this.m_pApi.DeleteUserRemoteServer(serverID);
        }

        public DataView GetUserRemoteServers(string userName)
        {
            return this.m_pApi.GetUserRemoteServers(userName).Tables[0].DefaultView;
        }

        public DataView GetUserMessageRules(string userName)
        {
            return this.m_pApi.GetUserMessageRules(userName).Tables[0].DefaultView;
        }

        public void AddUserMessageRule(string ruleID, string userName, string description, CompareSource type, string fieldName, CompareType compareType, string compareValue, MatchAction action, string actionData, bool storeMessage, bool checkNextRule, int cost, bool enabled)
        {
            this.m_pApi.AddUserMessageRule(ruleID, userName, description, (int)type, fieldName, (int)compareType, compareValue, (int)action, actionData, storeMessage, checkNextRule, cost, enabled);
        }

        public void DeleteUserMessageRule(string ruleID)
        {
            this.m_pApi.DeleteUserMessageRule(ruleID);
        }

        public void UpdateUserMessageRule(string ruleID, string userName, string description, CompareSource type, string fieldName, CompareType compareType, string compareValue, MatchAction action, string actionData, bool storeMessage, bool checkNextRule, int cost, bool enabled)
        {
            this.m_pApi.UpdateUserMessageRule(ruleID, userName, description, (int)type, fieldName, (int)compareType, compareValue, (int)action, actionData, storeMessage, checkNextRule, cost, enabled);
        }

        public DataSet AuthUser(string userName, string passwData, string authData, Merculia.Net.AuthType authType)
        {
            return this.m_pApi.AuthUser(userName, passwData, authData, (int)authType);
        }

        public DataView GetMailingLists(string domainName)
        {
            return this.m_pApi.GetMailingLists(domainName).Tables[0].DefaultView;
        }

        public void AddMailingList(string mailingListID, string mailingListName, string description, string domainName, bool isPublic)
        {
            this.m_pApi.AddMailingList(mailingListID, mailingListName, description, domainName, isPublic);
        }

        public void DeleteMailingList(string mailingListID)
        {
            this.m_pApi.DeleteMailingList(mailingListID);
        }

        public void UpdateMailingList(string mailingListID, string mailingListName, string description, string domainName, bool isPublic)
        {
            this.m_pApi.UpdateMailingList(mailingListID, mailingListName, description, domainName, isPublic);
        }

        public void AddMailingListAddress(string addressID, string mailingListName, string address)
        {
            this.m_pApi.AddMailingListAddress(addressID, mailingListName, address);
        }

        public void DeleteMailingListAddress(string addressID)
        {
            this.m_pApi.DeleteMailingListAddress(addressID);
        }

        public DataView GetMailingListAddresses(string mailingListName)
        {
            return this.m_pApi.GetMailingListAddresses(mailingListName).Tables[0].DefaultView;
        }

        public bool MailingListExists(string mailingListName)
        {
            return this.m_pApi.MailingListExists(mailingListName);
        }

        public bool IsMailingListPublic(string emailAddress)
        {
            return this.m_pApi.IsMailingListPublic(emailAddress);
        }

        public DataView GetRoutes(string domainName)
        {
            return this.m_pApi.GetRoutes(domainName).Tables[0].DefaultView;
        }

        public void AddRoute(string routeID, string pattern, string mailbox, string description, string domainName)
        {
            this.m_pApi.AddRoute(routeID, pattern, mailbox, description, domainName);
        }

        public void DeleteRoute(string routeID)
        {
            this.m_pApi.DeleteRoute(routeID);
        }

        public void UpdateRoute(string routeID, string pattern, string mailbox, string description, string domainName)
        {
            this.m_pApi.UpdateRoute(routeID, pattern, mailbox, description, domainName);
        }

        public string GetMailboxFromPattern(string emailAddress)
        {
            return this.m_pApi.GetMailboxFromPattern(emailAddress);
        }

        public void GetMessagesInfo(string userName, string folder, List<IMAP_MessageInfo> messages)
        {
            DataSet messagesInfo = this.m_pApi.GetMessagesInfo(userName, folder);
            foreach (DataRow dataRow in messagesInfo.Tables["MessagesInfo"].Rows)
            {
                string text = dataRow["messageID"].ToString();
                long uid= Convert.ToInt64(dataRow["UID"].ToString());
                string[] iMAP_MessageFlags = dataRow["flags"].ToString().Split(' ');

                //Merculia.Net.IMAP.IMAP_MessageFlags iMAP_MessageFlags = (Merculia.Net.IMAP.IMAP_MessageFlags)Convert.ToInt32(dataRow["flags"]);
                int num2 = Convert.ToInt32(dataRow["size"]);
                DateTime dateTime = Convert.ToDateTime(dataRow["date"]);
                messages.Add(new IMAP_MessageInfo(text, uid, iMAP_MessageFlags, num2, dateTime));
            }
        }
    

		public void StoreMessage(string mailbox, string folder, MemoryStream msgStream, string to, string from, DateTime date, IMAP_MessageFlags flags)
		{
			this.m_pApi.StoreMessage(mailbox, folder, msgStream.ToArray(), to, from, date, (int)flags);
		}

        public void StoreMessageFlags(string accessingUser, string folderOwnerUser, string folder, IMAP_MessageInfo messageInfo, string[] flags)
            //NNAVA
        //public void StoreMessageFlags(string mailbox, string folder, IMAP_Message message, string[] msgFlags)
		{
			string messageID = this.m_pApi.StoreMessageFlags(accessingUser, folder, messageInfo.ID, (long)messageInfo.UID, flags, messageInfo.Size, messageInfo.InternalDate);
            //NNAVA
            //messageInfo.ID= messageID;
		}

		public void DeleteMessage(string mailbox, string folder, string msgID)
		{
			this.m_pApi.DeleteMessage(mailbox, folder, msgID);
		}

		public byte[] GetMessage(string mailbox, string folder, string msgID)
		{
			return this.m_pApi.GetMessage(mailbox, folder, msgID);
		}

		public byte[] GetMessageTopLines(string mailbox, string folder, string msgID, int nrLines)
		{
			return this.m_pApi.GetMessageTopLines(mailbox, folder, msgID, nrLines);
		}

        public void CopyMessage(string accessingUser, string folderOwnerUser, string folder, string destFolderUser, string destFolder, IMAP_MessageInfo messageInfo)
            //NNAVA
        //public void CopyMessage(string userName, string folder, string destFolderUser, string destFolder, Merculia.Net.IMAP.Server.IMAP_Message message)
		{
            this.m_pApi.CopyMessage(accessingUser, folder, destFolderUser, destFolder, messageInfo.ID, messageInfo.UID, messageInfo.Flags, messageInfo.Size, messageInfo.InternalDate);
		}

		public string[] GetFolders(string userName)
		{
			return this.m_pApi.GetFolders(userName);
		}

		public string[] GetSubscribedFolders(string userName)
		{
			return this.m_pApi.GetSubscribedFolders(userName);
		}

		public void SubscribeFolder(string userName, string folder)
		{
			this.m_pApi.SubscribeFolder(userName, folder);
		}

		public void UnSubscribeFolder(string userName, string folder)
		{
			this.m_pApi.UnSubscribeFolder(userName, folder);
		}

		public void CreateFolder(string userName, string folder)
		{
			this.m_pApi.CreateFolder(userName, folder);
		}

		public void DeleteFolder(string userName, string folder)
		{
			this.m_pApi.DeleteFolder(userName, folder);
		}

		public void RenameFolder(string userName, string folder, string newFolder)
		{
			this.m_pApi.RenameFolder(userName, folder, newFolder);
		}

		public bool FolderExists(string folderName)
		{
			throw new Exception("TODO:");
		}

		public string GetPublicFoldersAccount()
		{
			return "publicfolders";
		}

		public DataView GetFolderACL(string folderName)
		{
			throw new Exception("TODO:");
		}

		public void DeleteFolderACL(string folderName, string userName)
		{
			throw new Exception("TODO:");
		}

        //public void SetFolderACL(string folderName, string userName, Merculia.Net.IMAP.Server.IMAP_Flags_SetType setType, Merculia.Net.IMAP.IMAP_ACL_Flags aclFlags)
        public void SetFolderACL(string accessingUser, string folderOwnerUser, string folder, string userOrGroup, IMAP_Flags_SetType setType, IMAP_ACL_Flags aclFlags)
        {
            throw new Exception("TODO:");
        }

		public Merculia.Net.IMAP.IMAP_ACL_Flags GetUserACL(string folderName, string userName)
		{
			throw new Exception("TODO:");
		}

		public long GetMailboxSize(string userName)
		{
			return this.m_pApi.GetMailboxSize(userName);
		}

		public DataView GetSecurityList()
		{
			return this.m_pApi.GetSecurityList().Tables[0].DefaultView;
		}

		public void AddSecurityEntry(string securityID, string description, string protocol, string type, string action, string content, long startIP, long endIP)
		{
			this.m_pApi.AddSecurityEntry(securityID, description, protocol, type, action, content, startIP, endIP);
		}

		public void DeleteSecurityEntry(string securityID)
		{
			this.m_pApi.DeleteSecurityEntry(securityID);
		}

		public void UpdateSecurityEntry(string securityID, string description, string protocol, string type, string action, string content, long startIP, long endIP)
		{
			this.m_pApi.UpdateSecurityEntry(securityID, description, protocol, type, action, content, startIP, endIP);
		}

		public bool IsRelayAllowed(string userName, string ip)
		{
			return this.m_pApi.IsRelayAllowed(userName, ip);
		}

		public bool IsAccessAllowed(string protocol, string ip)
		{
			return this.m_pApi.IsAccessAllowed(protocol, ip);
		}

		public DataView GetFilters()
		{
			return this.m_pApi.GetFilters().Tables[0].DefaultView;
		}

		public void AddFilter(string filterID, string description, string type, string assembly, string className, int cost, bool enabled)
		{
			this.m_pApi.AddFilter(filterID, description, type, assembly, className, cost, enabled);
		}

		public void DeleteFilter(string filterID)
		{
			this.m_pApi.DeleteFilter(filterID);
		}

		public void UpdateFilter(string filterID, string description, string type, string assembly, string className, int cost, bool enabled)
		{
			this.m_pApi.UpdateFilter(filterID, description, type, assembly, className, cost, enabled);
		}

		public DataRow GetSettings()
		{
            return this.m_pApi.GetSettings().Tables[0].Rows[0];
		}

		public void UpdateSettings(DataRow settings)
		{
			this.m_pApi.UpdateSettings(settings.Table.DataSet);
		}

        public void UpdateDomain(string domainID, string domainName, string description)
        {
            throw new NotImplementedException();
        }

        public string GetUserID(string userName)
        {
            throw new NotImplementedException();
        }

        public void AddUser(string userID, string userName, string fullName, string password, string description, string domainName, int mailboxSize, bool enabled, UserPermissions_enum permissions)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(string userID, string userName, string fullName, string password, string description, string domainName, int mailboxSize, bool enabled, UserPermissions_enum permissions)
        {
            throw new NotImplementedException();
        }

        public void AddUserAddress(string userName, string emailAddress)
        {
            throw new NotImplementedException();
        }

        public UserPermissions_enum GetUserPermissions(string userName)
        {
            throw new NotImplementedException();
        }

        public DateTime GetUserLastLoginTime(string userName)
        {
            throw new NotImplementedException();
        }

        public void UpdateUserLastLoginTime(string userName)
        {
            throw new NotImplementedException();
        }

        public void AddUserRemoteServer(string serverID, string userName, string description, string remoteServer, int remotePort, string remoteUser, string remotePassword, bool useSSL, bool enabled)
        {
            throw new NotImplementedException();
        }

        public void UpdateUserRemoteServer(string serverID, string userName, string description, string remoteServer, int remotePort, string remoteUser, string remotePassword, bool useSSL, bool enabled)
        {
            throw new NotImplementedException();
        }

        public void AddUserMessageRule(string userID, string ruleID, long cost, bool enabled, GlobalMessageRule_CheckNextRule_enum checkNextRule, string description, string matchExpression)
        {
            throw new NotImplementedException();
        }

        public void DeleteUserMessageRule(string userID, string ruleID)
        {
            throw new NotImplementedException();
        }

        public void UpdateUserMessageRule(string userID, string ruleID, long cost, bool enabled, GlobalMessageRule_CheckNextRule_enum checkNextRule, string description, string matchExpression)
        {
            throw new NotImplementedException();
        }

        public DataView GetUserMessageRuleActions(string userID, string ruleID)
        {
            throw new NotImplementedException();
        }

        public void AddUserMessageRuleAction(string userID, string ruleID, string actionID, string description, GlobalMessageRuleAction_enum actionType, byte[] actionData)
        {
            throw new NotImplementedException();
        }

        public void DeleteUserMessageRuleAction(string userID, string ruleID, string actionID)
        {
            throw new NotImplementedException();
        }

        public void UpdateUserMessageRuleAction(string userID, string ruleID, string actionID, string description, GlobalMessageRuleAction_enum actionType, byte[] actionData)
        {
            throw new NotImplementedException();
        }

        public bool GroupExists(string groupName)
        {
            throw new NotImplementedException();
        }

        public DataView GetGroups()
        {
            throw new NotImplementedException();
        }

        public void AddGroup(string groupID, string groupName, string description, bool enabled)
        {
            throw new NotImplementedException();
        }

        public void DeleteGroup(string groupID)
        {
            throw new NotImplementedException();
        }

        public void UpdateGroup(string groupID, string groupName, string description, bool enabled)
        {
            throw new NotImplementedException();
        }

        public bool GroupMemberExists(string groupName, string userOrGroup)
        {
            throw new NotImplementedException();
        }

        public string[] GetGroupMembers(string groupName)
        {
            throw new NotImplementedException();
        }

        public void AddGroupMember(string groupName, string userOrGroup)
        {
            throw new NotImplementedException();
        }

        public void DeleteGroupMember(string groupName, string userOrGroup)
        {
            throw new NotImplementedException();
        }

        public string[] GetGroupUsers(string groupName)
        {
            throw new NotImplementedException();
        }

        public DataView GetMailingListACL(string mailingListName)
        {
            throw new NotImplementedException();
        }

        public void AddMailingListACL(string mailingListName, string userOrGroup)
        {
            throw new NotImplementedException();
        }

        public void DeleteMailingListACL(string mailingListName, string userOrGroup)
        {
            throw new NotImplementedException();
        }

        public bool CanAccessMailingList(string mailingListName, string user)
        {
            throw new NotImplementedException();
        }

        public DataView GetGlobalMessageRules()
        {
            throw new NotImplementedException();
        }

        public void AddGlobalMessageRule(string ruleID, long cost, bool enabled, GlobalMessageRule_CheckNextRule_enum checkNextRule, string description, string matchExpression)
        {
            throw new NotImplementedException();
        }

        public void DeleteGlobalMessageRule(string ruleID)
        {
            throw new NotImplementedException();
        }

        public void UpdateGlobalMessageRule(string ruleID, long cost, bool enabled, GlobalMessageRule_CheckNextRule_enum checkNextRule, string description, string matchExpression)
        {
            throw new NotImplementedException();
        }

        public DataView GetGlobalMessageRuleActions(string ruleID)
        {
            throw new NotImplementedException();
        }

        public void AddGlobalMessageRuleAction(string ruleID, string actionID, string description, GlobalMessageRuleAction_enum actionType, byte[] actionData)
        {
            throw new NotImplementedException();
        }

        public void DeleteGlobalMessageRuleAction(string ruleID, string actionID)
        {
            throw new NotImplementedException();
        }

        public void UpdateGlobalMessageRuleAction(string ruleID, string actionID, string description, GlobalMessageRuleAction_enum actionType, byte[] actionData)
        {
            throw new NotImplementedException();
        }

        public DataView GetRoutes()
        {
            throw new NotImplementedException();
        }

        public void AddRoute(string routeID, long cost, bool enabled, string description, string pattern, RouteAction_enum action, byte[] actionData)
        {
            throw new NotImplementedException();
        }

        public void UpdateRoute(string routeID, long cost, bool enabled, string description, string pattern, RouteAction_enum action, byte[] actionData)
        {
            throw new NotImplementedException();
        }

        public void GetMessagesInfo(string accessingUser, string folderOwnerUser, string folder, List<IMAP_MessageInfo> messageInfos)
        {
            throw new NotImplementedException();
        }

        public void StoreMessage(string accessingUser, string folderOwnerUser, string folder, Stream msgStream, DateTime date, string[] flags)
        {
            throw new NotImplementedException();
        }

        public void DeleteMessage(string accessingUser, string folderOwnerUser, string folder, string messageID, int uid)
        {
            throw new NotImplementedException();
        }

        public void GetMessageItems(string accessingUser, string folderOwnerUser, string folder, EmailMessageItems e)
        {
            throw new NotImplementedException();
        }

        public byte[] GetMessageTopLines(string accessingUser, string folderOwnerUser, string folder, string msgID, int nrLines)
        {
            throw new NotImplementedException();
        }

        public void Search(string accessingUser, string folderOwnerUser, string folder, IMAP_e_Search e)
        {
            throw new NotImplementedException();
        }

        public string[] GetFolders(string userName, bool includeSharedFolders)
        {
            throw new NotImplementedException();
        }

        public void CreateFolder(string accessingUser, string folderOwnerUser, string folder)
        {
            throw new NotImplementedException();
        }

        public void DeleteFolder(string accessingUser, string folderOwnerUser, string folder)
        {
            throw new NotImplementedException();
        }

        public void RenameFolder(string accessingUser, string folderOwnerUser, string folder, string newFolder)
        {
            throw new NotImplementedException();
        }

        public DateTime FolderCreationTime(string folderOwnerUser, string folder)
        {
            throw new NotImplementedException();
        }

        public SharedFolderRoot[] GetSharedFolderRoots()
        {
            throw new NotImplementedException();
        }

        public void AddSharedFolderRoot(string rootID, bool enabled, string folder, string description, SharedFolderRootType_enum rootType, string boundedUser, string boundedFolder)
        {
            throw new NotImplementedException();
        }

        public void DeleteSharedFolderRoot(string rootID)
        {
            throw new NotImplementedException();
        }

        public void UpdateSharedFolderRoot(string rootID, bool enabled, string folder, string description, SharedFolderRootType_enum rootType, string boundedUser, string boundedFolder)
        {
            throw new NotImplementedException();
        }

        public DataView GetFolderACL(string accessingUser, string folderOwnerUser, string folder)
        {
            throw new NotImplementedException();
        }

        public void DeleteFolderACL(string accessingUser, string folderOwnerUser, string folder, string userOrGroup)
        {
            throw new NotImplementedException();
        }

        public IMAP_ACL_Flags GetUserACL(string folderOwnerUser, string folder, string user)
        {
            throw new NotImplementedException();
        }

        public void CreateUserDefaultFolders(string userName)
        {
            throw new NotImplementedException();
        }

        public DataView GetUsersDefaultFolders()
        {
            throw new NotImplementedException();
        }

        public void AddUsersDefaultFolder(string folderName, bool permanent)
        {
            throw new NotImplementedException();
        }

        public void DeleteUsersDefaultFolder(string folderName)
        {
            throw new NotImplementedException();
        }

        public DataTable GetRecycleBinSettings()
        {
            throw new NotImplementedException();
        }

        public void UpdateRecycleBinSettings(bool deleteToRecycleBin, int deleteMessagesAfter)
        {
            throw new NotImplementedException();
        }

        public DataView GetRecycleBinMessagesInfo(string user, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Stream GetRecycleBinMessage(string messageID)
        {
            throw new NotImplementedException();
        }

        public void DeleteRecycleBinMessage(string messageID)
        {
            throw new NotImplementedException();
        }

        public void RestoreRecycleBinMessage(string messageID)
        {
            throw new NotImplementedException();
        }

        public void AddSecurityEntry(string id, bool enabled, string description, Service_enum service, IPSecurityAction_enum action, IPAddress startIP, IPAddress endIP)
        {
            throw new NotImplementedException();
        }

        public void UpdateSecurityEntry(string id, bool enabled, string description, Service_enum service, IPSecurityAction_enum action, IPAddress startIP, IPAddress endIP)
        {
            throw new NotImplementedException();
        }

        public void AddFilter(string filterID, string description, string type, string assembly, string className, long cost, bool enabled)
        {
            throw new NotImplementedException();
        }

        public void UpdateFilter(string filterID, string description, string type, string assembly, string className, long cost, bool enabled)
        {
            throw new NotImplementedException();
        }
    }
}
