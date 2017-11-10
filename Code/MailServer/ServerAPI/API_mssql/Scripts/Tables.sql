
Create table lsDomains(        
    DomainID     nvarchar(100) Default '',
    DomainName   nvarchar(100) Default '',
    Description  nvarchar(100) Default ''
);

Create table lsFilters(        
    FilterID    nvarchar(100) Default '',
    Cost        bigint        Default 0,
    Assembly    nvarchar(100) Default '',
    ClassName   nvarchar(100) Default '',
	Enabled     tinyint           Default 1,
	Description nvarchar(100) Default '',
    Type        nvarchar(100) Default ''
);


Create table lsMailingLists(        
    MailingListID   nvarchar(100) Default '',
    DomainName      nvarchar(100) Default '',
    MailingListName nvarchar(100) Default '',
	Description     nvarchar(100) Default '',
	Enabled         tinyint           Default 1
);

Create table lsMailingListAddresses(        
    AddressID     nvarchar(100) Default '',
    MailingListID nvarchar(100) Default '',
    Address       nvarchar(100) Default ''
);

Create table lsMailingListACL(        
    MailingListID nvarchar(100) Default '',
    UserOrGroup   nvarchar(100) Default ''
);

Create table lsRouting(        
    RouteID     nvarchar(100) Default '',
    Cost        bigint        Default 0,
	Enabled     tinyint           Default 1,
	Description nvarchar(100) Default '',
    Pattern     nvarchar(100) Default '',
	Action      int           Default 0,
	ActionData  longblob         
);

Create table lsIPSecurity(
    ID          nvarchar(100) Default '',
	Enabled     tinyint           Default 1,
    Description nvarchar(100) Default '',
    Service     int           Default 0,
	Action      int           Default 0,
	StartIP     nvarchar(100) Default '',
	EndIP       nvarchar(100) Default ''        
);

Create table lsSettings(        
    Settings longblob        
);



Create table lsUsersDefaultFolders(
    FolderName varchar(200) UNIQUE Default '',
    Permanent  tinyint                 Default 1
);



Create table lsUserAddresses(        
    AddressID nvarchar(100) Default '',
    UserID    nvarchar(100) Default '',
    Address   nvarchar(100) Default ''
);

Create table lsUserRemoteServers(        
    ServerID       nvarchar(100) Default '',
    UserID         nvarchar(100) Default '',
    Description    nvarchar(100) Default '',
    RemoteServer   nvarchar(100) Default '',
	RemotePort     int           Default 110,
	RemoteUserName nvarchar(100) Default '',
	RemotePassword nvarchar(100) Default '',
    UseSSL         tinyint           Default 0,
    Enabled        tinyint           Default 1
);

Create table lsUserMessageRules(
	UserID          nvarchar(100) Default '',
    RuleID          nvarchar(100) Default '',
    Cost            bigint        Default 0,
    Enabled         tinyint           Default 1,
    CheckNextRuleIf int           Default 0,
    Description     nvarchar(400) Default '',
    MatchExpression longblob
);

Create table lsUserMessageRuleActions(
	UserID          nvarchar(100) Default '',
    RuleID          nvarchar(100) Default '',
    ActionID        nvarchar(100) Default '',
    Description     nvarchar(400) Default '',
    ActionType      int           Default 0,
    ActionData      longblob
);

Create table lsUsers(        
    UserID        nvarchar(100) Default '',
    FullName      nvarchar(100) Default '',
    UserName      nvarchar(100) Default '',
	Password      nvarchar(100) Default '',
	Description   nvarchar(100) Default '',
	DomainName    nvarchar(100) Default '',
	Mailbox_Size  bigint        Default 0,
	Enabled       tinyint           Default 1,
	Permissions   int           Default 255,
    CreationTime  datetime      Default CURRENT_TIMESTAMP,
    LastLoginTime datetime     Default CURRENT_TIMESTAMP
);


Create table lsIMAPFolders(        
    UserID       nvarchar(100) Default '',
    FolderName   nvarchar(100) Default '',
	CreationTime datetime      Default CURRENT_TIMESTAMP
);

Create table lsIMAPSubscribedFolders(        
    UserID     nvarchar(100) Default '',
    FolderName nvarchar(100) Default ''
);

Create table lsMailStore(        
    MessageID    nvarchar(100) Default '',
    Mailbox      nvarchar(100) Default '',
	Folder       nvarchar(100) Default '',
	Data         longblob      ,
	Size         bigint        Default 0,
	TopLines     longblob,
	MessageFlags nvarchar(1000),
	Date         datetime      Default CURRENT_TIMESTAMP,
	UID          int           /*Auto_increment*/
);



/* This table holds recycle bin messages
    MessageID  - Message ID.
    DeleteTime - Date time when message was deleted.
    User       - User whos message it is.
    Folder     - Original folder where message was.
    Size       - Message size in bytes.
    Envelope   - Message IMAP Envelope string.
    Data       - Message data.
*/
create table lsRecycleBin(
    MessageID  nvarchar(100)  Default '',
    DeleteTime datetime      DEFAULT CURRENT_TIMESTAMP,
    `User`     nvarchar(200)  Default '',
    Folder     nvarchar(500)  Default '',
    Size       bigint         Default 0, 
    Envelope   nvarchar(2000) Default '',
    Data       longblob
);



/* Holds Recycle Bin settings.
    DeleteToRecycleBin  - Specifies if messages must be deleted to recycle bin.
    DeleteMessagesAfter - Specifies after what days messages will be deleted.
*/
create table lsRecycleBinSettings(        
    DeleteToRecycleBin  tinyint Default 0,
    DeleteMessagesAfter int Default 1
);



Create table lsIMAP_ACL(        
    Folder        nvarchar(500) Default '',
    `User`        nvarchar(100) Default '',
    `Permissions` nvarchar(20)  Default ''
);

Create table lsGlobalMessageRules(  
    RuleID          nvarchar(100) Default '',
    Cost            bigint        Default 0,
    Enabled         tinyint           Default 1,
    CheckNextRuleIf int           Default 0,
    Description     nvarchar(400) Default '',
    MatchExpression longblob
);

Create table lsGlobalMessageRuleActions(
    RuleID          nvarchar(100) Default '',
    ActionID        nvarchar(100) Default '',
    Description     nvarchar(400) Default '',
    ActionType      int           Default 0,
    ActionData      longblob
);

Create table lsGroups(
    GroupID         nvarchar(100) Default '',
    GroupName       nvarchar(100) Default '',
    Description     nvarchar(400) Default '',
    Enabled         tinyint           Default 1
);

Create table lsGroupMembers(
    GroupID         nvarchar(100) Default '',
    UserOrGroup     nvarchar(100) Default ''
);

CREATE table lsSharedFoldersRoots(
    RootID         nvarchar(100) Default '', 
    Enabled        tinyint           Default 1,
    Folder         nvarchar(400) Default '', 
    Description    nvarchar(400) Default '', 
    RootType       int           Default 0,
    BoundedUser    nvarchar(100) Default '',
    BoundedFolder  nvarchar(400) Default ''
);
