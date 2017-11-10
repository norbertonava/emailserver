using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

[DesignerCategory("code"), DebuggerStepThrough, WebServiceBinding(Name = "MailAPISoap", Namespace = "http://tempuri.org/")]
internal class MailAPI : SoapHttpClientProtocol
{
	public MailAPI()
	{
		base.set_Url("http://localhost/MailAPI/MailAPI.asmx");
	}

	[SoapDocumentMethod]
	public DataSet GetDomains()
	{
		object[] array = base.Invoke("GetDomains", new object[0]);
		return (DataSet)array[0];
	}

	public IAsyncResult BeginGetDomains(AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("GetDomains", new object[0], callback, asyncState);
	}

	public DataSet EndGetDomains(IAsyncResult asyncResult)
	{
		object[] array = base.EndInvoke(asyncResult);
		return (DataSet)array[0];
	}

	[SoapDocumentMethod]
	public void AddDomain(string domainID, string domainName, string description)
	{
		base.Invoke("AddDomain", new object[]
		{
			domainID,
			domainName,
			description
		});
	}

	public IAsyncResult BeginAddDomain(string domainID, string domainName, string description, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("AddDomain", new object[]
		{
			domainID,
			domainName,
			description
		}, callback, asyncState);
	}

	public void EndAddDomain(IAsyncResult asyncResult)
	{
		base.EndInvoke(asyncResult);
	}

	[SoapDocumentMethod]
	public void DeleteDomain(string domainID)
	{
		base.Invoke("DeleteDomain", new object[]
		{
			domainID
		});
	}

	public IAsyncResult BeginDeleteDomain(string domainID, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("DeleteDomain", new object[]
		{
			domainID
		}, callback, asyncState);
	}

	public void EndDeleteDomain(IAsyncResult asyncResult)
	{
		base.EndInvoke(asyncResult);
	}

	[SoapDocumentMethod]
	public bool DomainExists(string source)
	{
		object[] array = base.Invoke("DomainExists", new object[]
		{
			source
		});
		return (bool)array[0];
	}

	public IAsyncResult BeginDomainExists(string source, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("DomainExists", new object[]
		{
			source
		}, callback, asyncState);
	}

	public bool EndDomainExists(IAsyncResult asyncResult)
	{
		object[] array = base.EndInvoke(asyncResult);
		return (bool)array[0];
	}

	[SoapDocumentMethod]
	public DataSet GetUsers(string domainName)
	{
		object[] array = base.Invoke("GetUsers", new object[]
		{
			domainName
		});
		return (DataSet)array[0];
	}

	public IAsyncResult BeginGetUsers(string domainName, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("GetUsers", new object[]
		{
			domainName
		}, callback, asyncState);
	}

	public DataSet EndGetUsers(IAsyncResult asyncResult)
	{
		object[] array = base.EndInvoke(asyncResult);
		return (DataSet)array[0];
	}

	[SoapDocumentMethod]
	public void AddUser(string userID, string userName, string fullName, string password, string description, string domainName, int mailboxSize, bool enabled, bool allowRelay)
	{
		base.Invoke("AddUser", new object[]
		{
			userID,
			userName,
			fullName,
			password,
			description,
			domainName,
			mailboxSize,
			enabled,
			allowRelay
		});
	}

	public IAsyncResult BeginAddUser(string userID, string userName, string fullName, string password, string description, string domainName, int mailboxSize, bool enabled, bool allowRelay, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("AddUser", new object[]
		{
			userID,
			userName,
			fullName,
			password,
			description,
			domainName,
			mailboxSize,
			enabled,
			allowRelay
		}, callback, asyncState);
	}

	public void EndAddUser(IAsyncResult asyncResult)
	{
		base.EndInvoke(asyncResult);
	}

	[SoapDocumentMethod]
	public void DeleteUser(string userID)
	{
		base.Invoke("DeleteUser", new object[]
		{
			userID
		});
	}

	public IAsyncResult BeginDeleteUser(string userID, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("DeleteUser", new object[]
		{
			userID
		}, callback, asyncState);
	}

	public void EndDeleteUser(IAsyncResult asyncResult)
	{
		base.EndInvoke(asyncResult);
	}

	[SoapDocumentMethod]
	public void UpdateUser(string userID, string userName, string fullName, string password, string description, string domainName, int mailboxSize, bool enabled, bool allowRelay)
	{
		base.Invoke("UpdateUser", new object[]
		{
			userID,
			userName,
			fullName,
			password,
			description,
			domainName,
			mailboxSize,
			enabled,
			allowRelay
		});
	}

	public IAsyncResult BeginUpdateUser(string userID, string userName, string fullName, string password, string description, string domainName, int mailboxSize, bool enabled, bool allowRelay, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("UpdateUser", new object[]
		{
			userID,
			userName,
			fullName,
			password,
			description,
			domainName,
			mailboxSize,
			enabled,
			allowRelay
		}, callback, asyncState);
	}

	public void EndUpdateUser(IAsyncResult asyncResult)
	{
		base.EndInvoke(asyncResult);
	}

	[SoapDocumentMethod]
	public void AddUserAddress(string addressID, string userName, string address)
	{
		base.Invoke("AddUserAddress", new object[]
		{
			addressID,
			userName,
			address
		});
	}

	public IAsyncResult BeginAddUserAddress(string addressID, string userName, string address, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("AddUserAddress", new object[]
		{
			addressID,
			userName,
			address
		}, callback, asyncState);
	}

	public void EndAddUserAddress(IAsyncResult asyncResult)
	{
		base.EndInvoke(asyncResult);
	}

	[SoapDocumentMethod]
	public void DeleteUserAddress(string addressID)
	{
		base.Invoke("DeleteUserAddress", new object[]
		{
			addressID
		});
	}

	public IAsyncResult BeginDeleteUserAddress(string addressID, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("DeleteUserAddress", new object[]
		{
			addressID
		}, callback, asyncState);
	}

	public void EndDeleteUserAddress(IAsyncResult asyncResult)
	{
		base.EndInvoke(asyncResult);
	}

	[SoapDocumentMethod]
	public DataSet GetUserAddresses(string userName)
	{
		object[] array = base.Invoke("GetUserAddresses", new object[]
		{
			userName
		});
		return (DataSet)array[0];
	}

	public IAsyncResult BeginGetUserAddresses(string userName, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("GetUserAddresses", new object[]
		{
			userName
		}, callback, asyncState);
	}

	public DataSet EndGetUserAddresses(IAsyncResult asyncResult)
	{
		object[] array = base.EndInvoke(asyncResult);
		return (DataSet)array[0];
	}

	[SoapDocumentMethod]
	public bool UserExists(string userName)
	{
		object[] array = base.Invoke("UserExists", new object[]
		{
			userName
		});
		return (bool)array[0];
	}

	public IAsyncResult BeginUserExists(string userName, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("UserExists", new object[]
		{
			userName
		}, callback, asyncState);
	}

	public bool EndUserExists(IAsyncResult asyncResult)
	{
		object[] array = base.EndInvoke(asyncResult);
		return (bool)array[0];
	}

	[SoapDocumentMethod]
	public string MapUser(string emailAddress)
	{
		object[] array = base.Invoke("MapUser", new object[]
		{
			emailAddress
		});
		return (string)array[0];
	}

	public IAsyncResult BeginMapUser(string emailAddress, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("MapUser", new object[]
		{
			emailAddress
		}, callback, asyncState);
	}

	public string EndMapUser(IAsyncResult asyncResult)
	{
		object[] array = base.EndInvoke(asyncResult);
		return (string)array[0];
	}

	[SoapDocumentMethod]
	public bool ValidateMailboxSize(string userName)
	{
		object[] array = base.Invoke("ValidateMailboxSize", new object[]
		{
			userName
		});
		return (bool)array[0];
	}

	public IAsyncResult BeginValidateMailboxSize(string userName, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("ValidateMailboxSize", new object[]
		{
			userName
		}, callback, asyncState);
	}

	public bool EndValidateMailboxSize(IAsyncResult asyncResult)
	{
		object[] array = base.EndInvoke(asyncResult);
		return (bool)array[0];
	}

	[SoapDocumentMethod]
	public void AddUserRemoteServer(string serverID, string userName, string remoteServer, int remotePort, string remoteUser, string remotePassword)
	{
		base.Invoke("AddUserRemoteServer", new object[]
		{
			serverID,
			userName,
			remoteServer,
			remotePort,
			remoteUser,
			remotePassword
		});
	}

	public IAsyncResult BeginAddUserRemoteServer(string serverID, string userName, string remoteServer, int remotePort, string remoteUser, string remotePassword, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("AddUserRemoteServer", new object[]
		{
			serverID,
			userName,
			remoteServer,
			remotePort,
			remoteUser,
			remotePassword
		}, callback, asyncState);
	}

	public void EndAddUserRemoteServer(IAsyncResult asyncResult)
	{
		base.EndInvoke(asyncResult);
	}

	[SoapDocumentMethod]
	public void DeleteUserRemoteServer(string serverID)
	{
		base.Invoke("DeleteUserRemoteServer", new object[]
		{
			serverID
		});
	}

	public IAsyncResult BeginDeleteUserRemoteServer(string serverID, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("DeleteUserRemoteServer", new object[]
		{
			serverID
		}, callback, asyncState);
	}

	public void EndDeleteUserRemoteServer(IAsyncResult asyncResult)
	{
		base.EndInvoke(asyncResult);
	}

	[SoapDocumentMethod]
	public DataSet GetUserRemoteServers(string userName)
	{
		object[] array = base.Invoke("GetUserRemoteServers", new object[]
		{
			userName
		});
		return (DataSet)array[0];
	}

	public IAsyncResult BeginGetUserRemoteServers(string userName, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("GetUserRemoteServers", new object[]
		{
			userName
		}, callback, asyncState);
	}

	public DataSet EndGetUserRemoteServers(IAsyncResult asyncResult)
	{
		object[] array = base.EndInvoke(asyncResult);
		return (DataSet)array[0];
	}

	[SoapDocumentMethod]
	public DataSet GetUserMessageRules(string userName)
	{
		object[] array = base.Invoke("GetUserMessageRules", new object[]
		{
			userName
		});
		return (DataSet)array[0];
	}

	public IAsyncResult BeginGetUserMessageRules(string userName, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("GetUserMessageRules", new object[]
		{
			userName
		}, callback, asyncState);
	}

	public DataSet EndGetUserMessageRules(IAsyncResult asyncResult)
	{
		object[] array = base.EndInvoke(asyncResult);
		return (DataSet)array[0];
	}

	[SoapDocumentMethod]
	public void AddUserMessageRule(string ruleID, string userName, string description, int type, string fieldName, int compareType, string compareValue, int action, string actionData, bool storeMessage, bool checkNextRule, int cost, bool enabled)
	{
		base.Invoke("AddUserMessageRule", new object[]
		{
			ruleID,
			userName,
			description,
			type,
			fieldName,
			compareType,
			compareValue,
			action,
			actionData,
			storeMessage,
			checkNextRule,
			cost,
			enabled
		});
	}

	public IAsyncResult BeginAddUserMessageRule(string ruleID, string userName, string description, int type, string fieldName, int compareType, string compareValue, int action, string actionData, bool storeMessage, bool checkNextRule, int cost, bool enabled, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("AddUserMessageRule", new object[]
		{
			ruleID,
			userName,
			description,
			type,
			fieldName,
			compareType,
			compareValue,
			action,
			actionData,
			storeMessage,
			checkNextRule,
			cost,
			enabled
		}, callback, asyncState);
	}

	public void EndAddUserMessageRule(IAsyncResult asyncResult)
	{
		base.EndInvoke(asyncResult);
	}

	[SoapDocumentMethod]
	public void DeleteUserMessageRule(string ruleID)
	{
		base.Invoke("DeleteUserMessageRule", new object[]
		{
			ruleID
		});
	}

	public IAsyncResult BeginDeleteUserMessageRule(string ruleID, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("DeleteUserMessageRule", new object[]
		{
			ruleID
		}, callback, asyncState);
	}

	public void EndDeleteUserMessageRule(IAsyncResult asyncResult)
	{
		base.EndInvoke(asyncResult);
	}

	[SoapDocumentMethod]
	public void UpdateUserMessageRule(string ruleID, string userName, string description, int type, string fieldName, int compareType, string compareValue, int action, string actionData, bool storeMessage, bool checkNextRule, int cost, bool enabled)
	{
		base.Invoke("UpdateUserMessageRule", new object[]
		{
			ruleID,
			userName,
			description,
			type,
			fieldName,
			compareType,
			compareValue,
			action,
			actionData,
			storeMessage,
			checkNextRule,
			cost,
			enabled
		});
	}

	public IAsyncResult BeginUpdateUserMessageRule(string ruleID, string userName, string description, int type, string fieldName, int compareType, string compareValue, int action, string actionData, bool storeMessage, bool checkNextRule, int cost, bool enabled, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("UpdateUserMessageRule", new object[]
		{
			ruleID,
			userName,
			description,
			type,
			fieldName,
			compareType,
			compareValue,
			action,
			actionData,
			storeMessage,
			checkNextRule,
			cost,
			enabled
		}, callback, asyncState);
	}

	public void EndUpdateUserMessageRule(IAsyncResult asyncResult)
	{
		base.EndInvoke(asyncResult);
	}

	[SoapDocumentMethod]
	public DataSet AuthUser(string userName, string passwData, string authData, int authType)
	{
		object[] array = base.Invoke("AuthUser", new object[]
		{
			userName,
			passwData,
			authData,
			authType
		});
		return (DataSet)array[0];
	}

	public IAsyncResult BeginAuthUser(string userName, string passwData, string authData, int authType, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("AuthUser", new object[]
		{
			userName,
			passwData,
			authData,
			authType
		}, callback, asyncState);
	}

	public DataSet EndAuthUser(IAsyncResult asyncResult)
	{
		object[] array = base.EndInvoke(asyncResult);
		return (DataSet)array[0];
	}

	[SoapDocumentMethod]
	public DataSet GetMailingLists(string domainName)
	{
		object[] array = base.Invoke("GetMailingLists", new object[]
		{
			domainName
		});
		return (DataSet)array[0];
	}

	public IAsyncResult BeginGetMailingLists(string domainName, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("GetMailingLists", new object[]
		{
			domainName
		}, callback, asyncState);
	}

	public DataSet EndGetMailingLists(IAsyncResult asyncResult)
	{
		object[] array = base.EndInvoke(asyncResult);
		return (DataSet)array[0];
	}

	[SoapDocumentMethod]
	public void AddMailingList(string mailingListID, string mailingListName, string description, string domainName, bool isPublic)
	{
		base.Invoke("AddMailingList", new object[]
		{
			mailingListID,
			mailingListName,
			description,
			domainName,
			isPublic
		});
	}

	public IAsyncResult BeginAddMailingList(string mailingListID, string mailingListName, string description, string domainName, bool isPublic, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("AddMailingList", new object[]
		{
			mailingListID,
			mailingListName,
			description,
			domainName,
			isPublic
		}, callback, asyncState);
	}

	public void EndAddMailingList(IAsyncResult asyncResult)
	{
		base.EndInvoke(asyncResult);
	}

	[SoapDocumentMethod]
	public void DeleteMailingList(string mailingListID)
	{
		base.Invoke("DeleteMailingList", new object[]
		{
			mailingListID
		});
	}

	public IAsyncResult BeginDeleteMailingList(string mailingListID, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("DeleteMailingList", new object[]
		{
			mailingListID
		}, callback, asyncState);
	}

	public void EndDeleteMailingList(IAsyncResult asyncResult)
	{
		base.EndInvoke(asyncResult);
	}

	[SoapDocumentMethod]
	public void UpdateMailingList(string mailingListID, string mailingListName, string description, string domainName, bool isPublic)
	{
		base.Invoke("UpdateMailingList", new object[]
		{
			mailingListID,
			mailingListName,
			description,
			domainName,
			isPublic
		});
	}

	public IAsyncResult BeginUpdateMailingList(string mailingListID, string mailingListName, string description, string domainName, bool isPublic, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("UpdateMailingList", new object[]
		{
			mailingListID,
			mailingListName,
			description,
			domainName,
			isPublic
		}, callback, asyncState);
	}

	public void EndUpdateMailingList(IAsyncResult asyncResult)
	{
		base.EndInvoke(asyncResult);
	}

	[SoapDocumentMethod]
	public void AddMailingListAddress(string addressID, string mailingListName, string address)
	{
		base.Invoke("AddMailingListAddress", new object[]
		{
			addressID,
			mailingListName,
			address
		});
	}

	public IAsyncResult BeginAddMailingListAddress(string addressID, string mailingListName, string address, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("AddMailingListAddress", new object[]
		{
			addressID,
			mailingListName,
			address
		}, callback, asyncState);
	}

	public void EndAddMailingListAddress(IAsyncResult asyncResult)
	{
		base.EndInvoke(asyncResult);
	}

	[SoapDocumentMethod]
	public void DeleteMailingListAddress(string addressID)
	{
		base.Invoke("DeleteMailingListAddress", new object[]
		{
			addressID
		});
	}

	public IAsyncResult BeginDeleteMailingListAddress(string addressID, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("DeleteMailingListAddress", new object[]
		{
			addressID
		}, callback, asyncState);
	}

	public void EndDeleteMailingListAddress(IAsyncResult asyncResult)
	{
		base.EndInvoke(asyncResult);
	}

	[SoapDocumentMethod]
	public DataSet GetMailingListAddresses(string mailingListName)
	{
		object[] array = base.Invoke("GetMailingListAddresses", new object[]
		{
			mailingListName
		});
		return (DataSet)array[0];
	}

	public IAsyncResult BeginGetMailingListAddresses(string mailingListName, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("GetMailingListAddresses", new object[]
		{
			mailingListName
		}, callback, asyncState);
	}

	public DataSet EndGetMailingListAddresses(IAsyncResult asyncResult)
	{
		object[] array = base.EndInvoke(asyncResult);
		return (DataSet)array[0];
	}

	[SoapDocumentMethod]
	public bool MailingListExists(string mailingListName)
	{
		object[] array = base.Invoke("MailingListExists", new object[]
		{
			mailingListName
		});
		return (bool)array[0];
	}

	public IAsyncResult BeginMailingListExists(string mailingListName, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("MailingListExists", new object[]
		{
			mailingListName
		}, callback, asyncState);
	}

	public bool EndMailingListExists(IAsyncResult asyncResult)
	{
		object[] array = base.EndInvoke(asyncResult);
		return (bool)array[0];
	}

	[SoapDocumentMethod]
	public bool IsMailingListPublic(string emailAddress)
	{
		object[] array = base.Invoke("IsMailingListPublic", new object[]
		{
			emailAddress
		});
		return (bool)array[0];
	}

	public IAsyncResult BeginIsMailingListPublic(string emailAddress, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("IsMailingListPublic", new object[]
		{
			emailAddress
		}, callback, asyncState);
	}

	public bool EndIsMailingListPublic(IAsyncResult asyncResult)
	{
		object[] array = base.EndInvoke(asyncResult);
		return (bool)array[0];
	}

	[SoapDocumentMethod]
	public DataSet GetRoutes(string domainName)
	{
		object[] array = base.Invoke("GetRoutes", new object[]
		{
			domainName
		});
		return (DataSet)array[0];
	}

	public IAsyncResult BeginGetRoutes(string domainName, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("GetRoutes", new object[]
		{
			domainName
		}, callback, asyncState);
	}

	public DataSet EndGetRoutes(IAsyncResult asyncResult)
	{
		object[] array = base.EndInvoke(asyncResult);
		return (DataSet)array[0];
	}

	[SoapDocumentMethod]
	public void AddRoute(string routeID, string pattern, string mailbox, string description, string domainName)
	{
		base.Invoke("AddRoute", new object[]
		{
			routeID,
			pattern,
			mailbox,
			description,
			domainName
		});
	}

	public IAsyncResult BeginAddRoute(string routeID, string pattern, string mailbox, string description, string domainName, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("AddRoute", new object[]
		{
			routeID,
			pattern,
			mailbox,
			description,
			domainName
		}, callback, asyncState);
	}

	public void EndAddRoute(IAsyncResult asyncResult)
	{
		base.EndInvoke(asyncResult);
	}

	[SoapDocumentMethod]
	public void DeleteRoute(string routeID)
	{
		base.Invoke("DeleteRoute", new object[]
		{
			routeID
		});
	}

	public IAsyncResult BeginDeleteRoute(string routeID, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("DeleteRoute", new object[]
		{
			routeID
		}, callback, asyncState);
	}

	public void EndDeleteRoute(IAsyncResult asyncResult)
	{
		base.EndInvoke(asyncResult);
	}

	[SoapDocumentMethod]
	public void UpdateRoute(string routeID, string pattern, string mailbox, string description, string domainName)
	{
		base.Invoke("UpdateRoute", new object[]
		{
			routeID,
			pattern,
			mailbox,
			description,
			domainName
		});
	}

	public IAsyncResult BeginUpdateRoute(string routeID, string pattern, string mailbox, string description, string domainName, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("UpdateRoute", new object[]
		{
			routeID,
			pattern,
			mailbox,
			description,
			domainName
		}, callback, asyncState);
	}

	public void EndUpdateRoute(IAsyncResult asyncResult)
	{
		base.EndInvoke(asyncResult);
	}

	[SoapDocumentMethod]
	public string GetMailboxFromPattern(string emailAddress)
	{
		object[] array = base.Invoke("GetMailboxFromPattern", new object[]
		{
			emailAddress
		});
		return (string)array[0];
	}

	public IAsyncResult BeginGetMailboxFromPattern(string emailAddress, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("GetMailboxFromPattern", new object[]
		{
			emailAddress
		}, callback, asyncState);
	}

	public string EndGetMailboxFromPattern(IAsyncResult asyncResult)
	{
		object[] array = base.EndInvoke(asyncResult);
		return (string)array[0];
	}

	[SoapDocumentMethod]
	public DataSet GetMessagesInfo(string userName, string folder)
	{
		object[] array = base.Invoke("GetMessagesInfo", new object[]
		{
			userName,
			folder
		});
		return (DataSet)array[0];
	}

	public IAsyncResult BeginGetMessagesInfo(string userName, string folder, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("GetMessagesInfo", new object[]
		{
			userName,
			folder
		}, callback, asyncState);
	}

	public DataSet EndGetMessagesInfo(IAsyncResult asyncResult)
	{
		object[] array = base.EndInvoke(asyncResult);
		return (DataSet)array[0];
	}

	[SoapDocumentMethod]
	public void StoreMessage(string mailbox, string folder, [XmlElement(DataType = "base64Binary")] byte[] msgData, string to, string from, DateTime date, int flags)
	{
		base.Invoke("StoreMessage", new object[]
		{
			mailbox,
			folder,
			msgData,
			to,
			from,
			date,
			flags
		});
	}

	public IAsyncResult BeginStoreMessage(string mailbox, string folder, byte[] msgData, string to, string from, DateTime date, int flags, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("StoreMessage", new object[]
		{
			mailbox,
			folder,
			msgData,
			to,
			from,
			date,
			flags
		}, callback, asyncState);
	}

	public void EndStoreMessage(IAsyncResult asyncResult)
	{
		base.EndInvoke(asyncResult);
	}

	[SoapDocumentMethod]
	public string StoreMessageFlags(string mailbox, string folder, string msgID, long UID, string[] msgFlags, long size, DateTime date)
	{
		object[] array = base.Invoke("StoreMessageFlags", new object[]
		{
			mailbox,
			folder,
			msgID,
			UID,
			msgFlags,
			size,
			date
		});
		return (string)array[0];
	}

	public IAsyncResult BeginStoreMessageFlags(string mailbox, string folder, string msgID, long UID, int msgFlags, long size, DateTime date, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("StoreMessageFlags", new object[]
		{
			mailbox,
			folder,
			msgID,
			UID,
			msgFlags,
			size,
			date
		}, callback, asyncState);
	}

	public string EndStoreMessageFlags(IAsyncResult asyncResult)
	{
		object[] array = base.EndInvoke(asyncResult);
		return (string)array[0];
	}

	[SoapDocumentMethod]
	public void DeleteMessage(string mailbox, string folder, string msgID)
	{
		base.Invoke("DeleteMessage", new object[]
		{
			mailbox,
			folder,
			msgID
		});
	}

	public IAsyncResult BeginDeleteMessage(string mailbox, string folder, string msgID, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("DeleteMessage", new object[]
		{
			mailbox,
			folder,
			msgID
		}, callback, asyncState);
	}

	public void EndDeleteMessage(IAsyncResult asyncResult)
	{
		base.EndInvoke(asyncResult);
	}

	[SoapDocumentMethod]
	[return: XmlElement(DataType = "base64Binary")]
	public byte[] GetMessage(string mailbox, string folder, string msgID)
	{
		object[] array = base.Invoke("GetMessage", new object[]
		{
			mailbox,
			folder,
			msgID
		});
		return (byte[])array[0];
	}

	public IAsyncResult BeginGetMessage(string mailbox, string folder, string msgID, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("GetMessage", new object[]
		{
			mailbox,
			folder,
			msgID
		}, callback, asyncState);
	}

	public byte[] EndGetMessage(IAsyncResult asyncResult)
	{
		object[] array = base.EndInvoke(asyncResult);
		return (byte[])array[0];
	}

	[SoapDocumentMethod]
	[return: XmlElement(DataType = "base64Binary")]
	public byte[] GetMessageTopLines(string mailbox, string folder, string msgID, int nrLines)
	{
		object[] array = base.Invoke("GetMessageTopLines", new object[]
		{
			mailbox,
			folder,
			msgID,
			nrLines
		});
		return (byte[])array[0];
	}

	public IAsyncResult BeginGetMessageTopLines(string mailbox, string folder, string msgID, int nrLines, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("GetMessageTopLines", new object[]
		{
			mailbox,
			folder,
			msgID,
			nrLines
		}, callback, asyncState);
	}

	public byte[] EndGetMessageTopLines(IAsyncResult asyncResult)
	{
		object[] array = base.EndInvoke(asyncResult);
		return (byte[])array[0];
	}

	[SoapDocumentMethod]
	public void CopyMessage(string userName, string folder, string destFolderUser, string destFolder, string msgID, long UID, string[] msgFlags, long size, DateTime date)
	{
		base.Invoke("CopyMessage", new object[]
		{
			userName,
			folder,
			destFolderUser,
			destFolder,
			msgID,
			UID,
			msgFlags,
			size,
			date
		});
	}

	public IAsyncResult BeginCopyMessage(string userName, string folder, string destFolder, string msgID, long UID, int msgFlags, long size, DateTime date, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("CopyMessage", new object[]
		{
			userName,
			folder,
			destFolder,
			msgID,
			UID,
			msgFlags,
			size,
			date
		}, callback, asyncState);
	}

	public void EndCopyMessage(IAsyncResult asyncResult)
	{
		base.EndInvoke(asyncResult);
	}

	[SoapDocumentMethod]
	public string[] GetFolders(string userName)
	{
		object[] array = base.Invoke("GetFolders", new object[]
		{
			userName
		});
		return (string[])array[0];
	}

	public IAsyncResult BeginGetFolders(string userName, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("GetFolders", new object[]
		{
			userName
		}, callback, asyncState);
	}

	public string[] EndGetFolders(IAsyncResult asyncResult)
	{
		object[] array = base.EndInvoke(asyncResult);
		return (string[])array[0];
	}

	[SoapDocumentMethod]
	public string[] GetSubscribedFolders(string userName)
	{
		object[] array = base.Invoke("GetSubscribedFolders", new object[]
		{
			userName
		});
		return (string[])array[0];
	}

	public IAsyncResult BeginGetSubscribedFolders(string userName, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("GetSubscribedFolders", new object[]
		{
			userName
		}, callback, asyncState);
	}

	public string[] EndGetSubscribedFolders(IAsyncResult asyncResult)
	{
		object[] array = base.EndInvoke(asyncResult);
		return (string[])array[0];
	}

	[SoapDocumentMethod]
	public void SubscribeFolder(string userName, string folder)
	{
		base.Invoke("SubscribeFolder", new object[]
		{
			userName,
			folder
		});
	}

	public IAsyncResult BeginSubscribeFolder(string userName, string folder, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("SubscribeFolder", new object[]
		{
			userName,
			folder
		}, callback, asyncState);
	}

	public void EndSubscribeFolder(IAsyncResult asyncResult)
	{
		base.EndInvoke(asyncResult);
	}

	[SoapDocumentMethod]
	public void UnSubscribeFolder(string userName, string folder)
	{
		base.Invoke("UnSubscribeFolder", new object[]
		{
			userName,
			folder
		});
	}

	public IAsyncResult BeginUnSubscribeFolder(string userName, string folder, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("UnSubscribeFolder", new object[]
		{
			userName,
			folder
		}, callback, asyncState);
	}

	public void EndUnSubscribeFolder(IAsyncResult asyncResult)
	{
		base.EndInvoke(asyncResult);
	}

	[SoapDocumentMethod]
	public void CreateFolder(string userName, string folder)
	{
		base.Invoke("CreateFolder", new object[]
		{
			userName,
			folder
		});
	}

	public IAsyncResult BeginCreateFolder(string userName, string folder, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("CreateFolder", new object[]
		{
			userName,
			folder
		}, callback, asyncState);
	}

	public void EndCreateFolder(IAsyncResult asyncResult)
	{
		base.EndInvoke(asyncResult);
	}

	[SoapDocumentMethod]
	public void DeleteFolder(string userName, string folder)
	{
		base.Invoke("DeleteFolder", new object[]
		{
			userName,
			folder
		});
	}

	public IAsyncResult BeginDeleteFolder(string userName, string folder, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("DeleteFolder", new object[]
		{
			userName,
			folder
		}, callback, asyncState);
	}

	public void EndDeleteFolder(IAsyncResult asyncResult)
	{
		base.EndInvoke(asyncResult);
	}

	[SoapDocumentMethod]
	public void RenameFolder(string userName, string folder, string newFolder)
	{
		base.Invoke("RenameFolder", new object[]
		{
			userName,
			folder,
			newFolder
		});
	}

	public IAsyncResult BeginRenameFolder(string userName, string folder, string newFolder, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("RenameFolder", new object[]
		{
			userName,
			folder,
			newFolder
		}, callback, asyncState);
	}

	public void EndRenameFolder(IAsyncResult asyncResult)
	{
		base.EndInvoke(asyncResult);
	}

	[SoapDocumentMethod]
	public long GetMailboxSize(string userName)
	{
		object[] array = base.Invoke("GetMailboxSize", new object[]
		{
			userName
		});
		return (long)array[0];
	}

	public IAsyncResult BeginGetMailboxSize(string userName, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("GetMailboxSize", new object[]
		{
			userName
		}, callback, asyncState);
	}

	public long EndGetMailboxSize(IAsyncResult asyncResult)
	{
		object[] array = base.EndInvoke(asyncResult);
		return (long)array[0];
	}

	[SoapDocumentMethod]
	public DataSet GetSecurityList()
	{
		object[] array = base.Invoke("GetSecurityList", new object[0]);
		return (DataSet)array[0];
	}

	public IAsyncResult BeginGetSecurityList(AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("GetSecurityList", new object[0], callback, asyncState);
	}

	public DataSet EndGetSecurityList(IAsyncResult asyncResult)
	{
		object[] array = base.EndInvoke(asyncResult);
		return (DataSet)array[0];
	}

	[SoapDocumentMethod]
	public void AddSecurityEntry(string securityID, string description, string protocol, string type, string action, string content, long startIP, long endIP)
	{
		base.Invoke("AddSecurityEntry", new object[]
		{
			securityID,
			description,
			protocol,
			type,
			action,
			content,
			startIP,
			endIP
		});
	}

	public IAsyncResult BeginAddSecurityEntry(string securityID, string description, string protocol, string type, string action, string content, long startIP, long endIP, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("AddSecurityEntry", new object[]
		{
			securityID,
			description,
			protocol,
			type,
			action,
			content,
			startIP,
			endIP
		}, callback, asyncState);
	}

	public void EndAddSecurityEntry(IAsyncResult asyncResult)
	{
		base.EndInvoke(asyncResult);
	}

	[SoapDocumentMethod]
	public void DeleteSecurityEntry(string securityID)
	{
		base.Invoke("DeleteSecurityEntry", new object[]
		{
			securityID
		});
	}

	public IAsyncResult BeginDeleteSecurityEntry(string securityID, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("DeleteSecurityEntry", new object[]
		{
			securityID
		}, callback, asyncState);
	}

	public void EndDeleteSecurityEntry(IAsyncResult asyncResult)
	{
		base.EndInvoke(asyncResult);
	}

	[SoapDocumentMethod]
	public void UpdateSecurityEntry(string securityID, string description, string protocol, string type, string action, string content, long startIP, long endIP)
	{
		base.Invoke("UpdateSecurityEntry", new object[]
		{
			securityID,
			description,
			protocol,
			type,
			action,
			content,
			startIP,
			endIP
		});
	}

	public IAsyncResult BeginUpdateSecurityEntry(string securityID, string description, string protocol, string type, string action, string content, long startIP, long endIP, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("UpdateSecurityEntry", new object[]
		{
			securityID,
			description,
			protocol,
			type,
			action,
			content,
			startIP,
			endIP
		}, callback, asyncState);
	}

	public void EndUpdateSecurityEntry(IAsyncResult asyncResult)
	{
		base.EndInvoke(asyncResult);
	}

	[SoapDocumentMethod]
	public bool IsRelayAllowed(string userName, string ip)
	{
		object[] array = base.Invoke("IsRelayAllowed", new object[]
		{
			userName,
			ip
		});
		return (bool)array[0];
	}

	public IAsyncResult BeginIsRelayAllowed(string userName, string ip, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("IsRelayAllowed", new object[]
		{
			userName,
			ip
		}, callback, asyncState);
	}

	public bool EndIsRelayAllowed(IAsyncResult asyncResult)
	{
		object[] array = base.EndInvoke(asyncResult);
		return (bool)array[0];
	}

	[SoapDocumentMethod]
	public bool IsAccessAllowed(string protocol, string ip)
	{
		object[] array = base.Invoke("IsAccessAllowed", new object[]
		{
			protocol,
			ip
		});
		return (bool)array[0];
	}

	public IAsyncResult BeginIsAccessAllowed(string protocol, string ip, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("IsAccessAllowed", new object[]
		{
			protocol,
			ip
		}, callback, asyncState);
	}

	public bool EndIsAccessAllowed(IAsyncResult asyncResult)
	{
		object[] array = base.EndInvoke(asyncResult);
		return (bool)array[0];
	}

	[SoapDocumentMethod]
	public DataSet GetFilters()
	{
		object[] array = base.Invoke("GetFilters", new object[0]);
		return (DataSet)array[0];
	}

	public IAsyncResult BeginGetFilters(AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("GetFilters", new object[0], callback, asyncState);
	}

	public DataSet EndGetFilters(IAsyncResult asyncResult)
	{
		object[] array = base.EndInvoke(asyncResult);
		return (DataSet)array[0];
	}

	[SoapDocumentMethod]
	public void AddFilter(string filterID, string description, string type, string assembly, string className, int cost, bool enabled)
	{
		base.Invoke("AddFilter", new object[]
		{
			filterID,
			description,
			type,
			assembly,
			className,
			cost,
			enabled
		});
	}

	public IAsyncResult BeginAddFilter(string filterID, string description, string type, string assembly, string className, int cost, bool enabled, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("AddFilter", new object[]
		{
			filterID,
			description,
			type,
			assembly,
			className,
			cost,
			enabled
		}, callback, asyncState);
	}

	public void EndAddFilter(IAsyncResult asyncResult)
	{
		base.EndInvoke(asyncResult);
	}

	[SoapDocumentMethod]
	public void DeleteFilter(string filterID)
	{
		base.Invoke("DeleteFilter", new object[]
		{
			filterID
		});
	}

	public IAsyncResult BeginDeleteFilter(string filterID, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("DeleteFilter", new object[]
		{
			filterID
		}, callback, asyncState);
	}

	public void EndDeleteFilter(IAsyncResult asyncResult)
	{
		base.EndInvoke(asyncResult);
	}

	[SoapDocumentMethod]
	public void UpdateFilter(string filterID, string description, string type, string assembly, string className, int cost, bool enabled)
	{
		base.Invoke("UpdateFilter", new object[]
		{
			filterID,
			description,
			type,
			assembly,
			className,
			cost,
			enabled
		});
	}

	public IAsyncResult BeginUpdateFilter(string filterID, string description, string type, string assembly, string className, int cost, bool enabled, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("UpdateFilter", new object[]
		{
			filterID,
			description,
			type,
			assembly,
			className,
			cost,
			enabled
		}, callback, asyncState);
	}

	public void EndUpdateFilter(IAsyncResult asyncResult)
	{
		base.EndInvoke(asyncResult);
	}

	[SoapDocumentMethod]
	public DataSet GetSettings()
	{
		object[] array = base.Invoke("GetSettings", new object[0]);
		return (DataSet)array[0];
	}

	public IAsyncResult BeginGetSettings(AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("GetSettings", new object[0], callback, asyncState);
	}

	public DataSet EndGetSettings(IAsyncResult asyncResult)
	{
		object[] array = base.EndInvoke(asyncResult);
		return (DataSet)array[0];
	}

	[SoapDocumentMethod]
	public void UpdateSettings(DataSet settings)
	{
		base.Invoke("UpdateSettings", new object[]
		{
			settings
		});
	}

	public IAsyncResult BeginUpdateSettings(DataSet settings, AsyncCallback callback, object asyncState)
	{
		return base.BeginInvoke("UpdateSettings", new object[]
		{
			settings
		}, callback, asyncState);
	}

	public void EndUpdateSettings(IAsyncResult asyncResult)
	{
		base.EndInvoke(asyncResult);
	}
}
