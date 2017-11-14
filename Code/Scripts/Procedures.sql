

DELIMITER //

CREATE PROCEDURE lspr_AddDomain ( 
	p_DomainID    nvarchar(100) /* = NULL */,
	p_DomainName  nvarchar(100) /* = NULL */,
	p_Description nvarchar(100) /* = NULL */)
BEGIN


if(not exists(select * from lsDomains where (DomainID=p_DomainID)))
then
	if(not exists(select * from lsDomains where (DomainName=p_DomainName)))
	then
		insert lsDomains (DomainID,DomainName,Description) values (p_DomainID,p_DomainName,p_Description);

		select null as ErrorText;
	else
		select CONCAT('Domain with specified name "' , p_DomainName , '" already exists !') as ErrorText;
	end if;
else
	select CONCAT('Domain with specified ID "' , p_DomainID , '" already exists !') as ErrorText;
end if;



END;
//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_AddFilter (
	p_FilterID    nvarchar(100) /* = NULL */,
	p_Description nvarchar(100) /* = NULL */,
	p_Type        nvarchar(100) /* = NULL */,
	p_Assembly    nvarchar(100) /* = NULL */,
	p_ClassName   nvarchar(100) /* = NULL */,
	p_Cost        bigint        /* = 0 */,
	p_Enabled     tinyint           /* = true */)
BEGIN


if(not exists(select * from lsFilters where (FilterID=p_FilterID)))
then
	insert lsFilters (FilterID,Description,Type,Assembly,ClassName,Cost,Enabled) 
	values (p_FilterID,p_Description,p_Type,p_Assembly,p_ClassName,p_Cost,p_Enabled);

	select null as ErrorText;
else
	select CONCAT('Filter with specified ID "' , p_FilterID , '" already exists !') as ErrorText;
end if;



END;
//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_AddGlobalMessageRule (
	p_ruleID          nvarchar(100) /* = NULL */,
	p_cost            bigint        /* = NULL */,
	p_enabled         tinyint           /* = NULL */,
	p_checkNextRule   int           /* = NULL */,
	p_description     nvarchar(400) /* = NULL */,
	p_matchExpression longblob         /* = NULL */)
BEGIN
	if(not exists(select * from lsGlobalMessageRules where (RuleID = p_ruleID)))
	then
		insert lsGlobalMessageRules (RuleID,Cost,Enabled,CheckNextRuleIf,Description,MatchExpression) 
			values (p_ruleID,p_cost,p_enabled,p_checkNextRule,p_description,p_matchExpression);

		select null as ErrorText;
	else
		select CONCAT('Rule with specified ID "' , p_ruleID , '" already exists !') as ErrorText;
	end if;
END;


//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_AddGlobalMessageRuleAction (
	p_ruleID          nvarchar(100) /* = NULL */,
	p_actionID        nvarchar(100) /* = NULL */,
	p_description     nvarchar(400) /* = NULL */,
	p_actionType      int           /* = NULL */,
	p_actionData      longblob         /* = NULL */)
BEGIN
	if(not exists(select * from lsGlobalMessageRuleActions where (RuleID = p_ruleID AND ActionID = p_actionID)))
	then
		insert lsGlobalMessageRuleActions (RuleID,ActionID,Description,ActionType,ActionData) 
			values (p_ruleID,p_actionID,p_description,p_actionType,p_actionData);

		select null as ErrorText;
	else
		select CONCAT('Action with specified ID "' , p_actionID , '" already exists !') as ErrorText;
	end if;
END;


//

DELIMITER ;




/*   Implementation notes:
      Decsription:
	    Adds new user group
	  Returns:
		If successful returns nothing, otherwise returns 1 row with error text in column 'ErrorText'.

	  Implementation:
		*) Ensure that group ID won't exist already. Return error text.
        *) Ensure that group or user with specified name doesn't exist. Return error text.
        *) Add group.
		 
*/

DELIMITER //

CREATE PROCEDURE lspr_AddGroup (
	p_groupID     nvarchar(100) /* = NULL */,
	p_groupName   nvarchar(100) /* = NULL */,
	p_description nvarchar(400) /* = NULL */,
	p_enabled     tinyint           /* = NULL */)
sp_lbl:

BEGIN
	-- Ensure that group ID won't exist already. 
	if(exists(select * from lsGroups where (GroupID = p_groupID)))
	then
		select CONCAT('Invalid group ID, specified group ID ''' , p_groupID , ''' already exists !') as ErrorText;
		leave sp_lbl;
	end if;

	-- Ensure that group name won't exist already.
	if(exists(select * from lsGroups where (GroupName = p_groupName)))
	then
		select CONCAT('Invalid group name, specified group ''' , p_groupName , ''' already exists !') as ErrorText;
		leave sp_lbl;
	-- Ensure that user name with groupName doen't exist.
	elseif exists(select * from lsUsers where (UserName = p_groupName))
	then
		select CONCAT('Invalid group name, user with specified name ''' , p_groupName , ''' already exists !') as ErrorText;
		leave sp_lbl;
	end if;
	
	-- Insert group
	insert lsGroups (GroupID,GroupName,Description,Enabled) 
		select (p_groupID,p_groupName,p_description,p_enabled);

	select null as ErrorText;
END;


//

DELIMITER ;




/*  Implementation notes:
      Decsription:
	    Adds new user group member.
	  Returns:
		If successful returns nothing, otherwise returns 1 row with error text in column 'ErrorText'.

	  Implementation:
		*) Ensure that group exists. Return error text.
        *) Don't allow to add same group as group member. Return error text.
        *) Ensure that group member doesn't exist. Return error text.
        *) Add group member.
		 
*/

DELIMITER //

CREATE PROCEDURE lspr_AddGroupMember (
	p_groupName   nvarchar(100) /* = NULL */,
	p_userOrGroup nvarchar(100) /* = NULL */)
sp_lbl:

BEGIN	
	declare v_groupID nvarchar(100);

	-- Ensure that group exists.
	if(not exists(select * from lsGroups where (GroupName = p_groupName)))
	then
		select CONCAT('Invalid group name, specified group ''' , p_groupName , ''' doesn''t exist !') as ErrorText;
		leave sp_lbl;
	end if;

	-- Don't allow to add same group as group member.
	if(p_groupName = p_userOrGroup)
	then
		 select 'Invalid group member, can''t add goup itself as same group member !' as ErrorText;
		 leave sp_lbl;
	end if;

	-- Get groupID
	set v_groupID = (select GroupID from lsGroups where (GroupName = p_groupName));

	-- Ensure that group member doesn't exist.
	if(exists(select * from lsGroupMembers where (GroupID = v_groupID AND UserOrGroup = p_userOrGroup)))
	then
		select CONCAT('Invalid group member, specified group member ''' , p_userOrGroup , ''' already exists !') as ErrorText;
		leave sp_lbl;
	end if;
	
	-- Insert group member
	insert lsGroupMembers (GroupID,UserOrGroup) 
		select (v_groupID,p_userOrGroup);
	
	select null as ErrorText;
END;


//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_AddMailingList (
	p_MailingListID	 varchar(100) /* = NULL */,
	p_MailingListName varchar(100) /* = NULL */,
	p_Description     varchar(100) /* = NULL */,
	p_DomainName      varchar(100) /* = NULL */,
	p_enabled         tinyint          /* = false */)
BEGIN


if(not exists(select * from lsMailingLists where (MailingListID=p_MailingListID)))
then
	if(not exists(select * from lsMailingLists where (MailingListName=p_MailingListName)))
	then
		insert lsMailingLists (MailingListID,MailingListName,Description,DomainName,Enabled) 
		values (p_MailingListID,p_MailingListName,p_Description,p_DomainName,p_enabled);

		select null as ErrorText;
	else
		select CONCAT('Mailing list with specified name "' , p_MailingListName , '" already exists !') as ErrorText;
	end if;
else
	select CONCAT('Mailing list with specified ID "' , p_MailingListID , '" already exists !') as ErrorText;
end if;



END;
//

DELIMITER ;




/*  Implementation notes:
      Decsription:
	    Mailing list ACL entry
	  Returns:
		If successful returns nothing, otherwise returns 1 row with error text in column 'ErrorText'.

	  Implementation:
		*) Ensure that mailing list exists.
        *) Ensure that user or group already doesn't exist in list.
        *) Add ACL entry.
		 
*/

DELIMITER //

CREATE PROCEDURE lspr_AddMailingListACL (
	p_mailingListName nvarchar(100) /* = NULL */,
	p_userOrGroup     nvarchar(100) /* = NULL */)
sp_lbl:

BEGIN

	declare v_mailingListID nvarchar(100);

	-- Ensure that mailing list exists.
	if(not exists(select * from lsMailingLists where (MailingListName = p_mailingListName)))
	then
		select CONCAT('Invalid mailing list name, specified mailing list ''' , p_mailingListName , ''' doesn''t exist !') as ErrorText;
		leave sp_lbl;
	end if;

	-- Get mailing list ID
	set v_mailingListID = (select MailingListID from lsMailingLists where (MailingListName = p_mailingListName));
	
	-- Ensure that user or group already doesn't exist in list.
	if(exists(select * from lsMailingListACL where (MailingListID = v_mailingListID AND UserOrGroup = p_userOrGroup)))
	then
		select CONCAT('Invalid userOrGroup, specified userOrGroup ''' , p_userOrGroup , '''already exist !') as ErrorText;
		leave sp_lbl;
	end if;

	
	-- Insert group
	insert lsMailingListACL (MailingListID,UserOrGroup) 
		select (v_mailingListID,p_userOrGroup);

	select null as ErrorText;
END;


//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_AddMailingListAddress ( 
	p_AddressID       nvarchar(100) /* = NULL */,
	p_MailingListName nvarchar(100) /* = NULL */,
	p_Address         nvarchar(100) /* = NULL */)
BEGIN

	declare v_MailingListID nvarchar(100);
if(not exists(select * from lsMailingListAddresses where (AddressID=p_AddressID)))
then
	set v_MailingListID = (select MailingListID from lsMailingLists where MailingListName=p_MailingListName);

	if(not exists(select * from lsMailingListAddresses where (MailingListID=v_MailingListID AND Address=p_Address)))
	then		
		insert lsMailingListAddresses (AddressID,MailingListID,Address) 
        values (p_AddressID,v_MailingListID,p_Address);

		select null as ErrorText;
	else
		select CONCAT('Mailing list address with specified name "' , p_Address , '" already exists !') as ErrorText;
	end if;
else
	select CONCAT('Mailing list address with specified ID "' , p_AddressID , '" already exists !') as ErrorText;
end if;



END;
//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_AddRoute (
	p_routeID     varchar(100) /* = NULL */,
	p_cost        bigint       /* = NULL */,
	p_enabled     tinyint          /* = NULL */,
	p_description varchar(100) /* = NULL */,
	p_pattern     varchar(100) /* = NULL */,
	p_action      int          /* = NULL */,
	p_actionData  longblob        /* = NULL */)
BEGIN


if(not exists(select * from lsRouting where (RouteID=p_routeID)))
then
	if(not exists(select * from lsRouting where (Pattern=p_pattern)))
	then
		insert lsRouting (RouteID,Cost,Enabled,Description,Pattern,Action,ActionData) 
		values (p_routeID,p_cost,p_enabled,p_description,p_pattern,p_action,p_actionData);

		select null as ErrorText;
	else
		select CONCAT('Route with specified pattern "' , p_pattern , '" already exists !') as ErrorText;
	end if;
else
	select CONCAT('Route with specified ID "' , p_routeID , '" already exists !') as ErrorText;
end if;



END;
//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_AddSecurityEntry (
	p_id          varchar(100) /* = NULL */,
	p_enabled     tinyint          /* = 1 */,
	p_description varchar(100) /* = NULL */,
	p_service     varchar(100) /* = NULL */,
	p_action      varchar(100) /* = NULL */,
	p_startIP     varchar(100) /* = NULL */,
	p_endIP       varchar(100) /* = NULL */)
BEGIN


if(not exists(select * from lsIPSecurity where (ID=p_id)))
then
	insert lsIPSecurity (ID,Enabled,Description,Service,Action,StartIP,EndIP) 
	values (p_id,p_enabled,p_description,p_service,p_action,p_startIP,p_endIP);

	select null as ErrorText;
else
	select CONCAT('Security entry with specified ID "' , p_id , '" already exists !') as ErrorText;
end if;



END;
//

DELIMITER ;




/*  Implementation notes:
      Decsription:
	    Adds new shared folder root.
	  Returns:
		If successful returns nothing, otherwise returns 1 row with error text in column 'ErrorText'.

	  Implementation:
		*) Ensure that root doesn't exists.
        *) Add root folder.
		 
*/

DELIMITER //

CREATE PROCEDURE lspr_AddSharedFolderRoot (
	p_rootID        nvarchar(100) /* = NULL */,
	p_enabled       tinyint           /* = NULL */,
	p_folder        nvarchar(400) /* = NULL */,
	p_description   nvarchar(400) /* = NULL */,
	p_rootType      int           /* = NULL */,
	p_boundedUser   nvarchar(100) /* = NULL */,
	p_boundedFolder nvarchar(400) /* = NULL */)
sp_lbl:

BEGIN
	-- Ensure that root ID won't exist already. 
	if(exists(select * from lsSharedFoldersRoots where (RootID = p_rootID)))
	then
		select CONCAT('Invalid root ID, specified root ID ''' , p_rootID , ''' already exists !') as ErrorText;
		leave sp_lbl;
	end if;

	-- Ensure that root folder name won't exist already.
	if(exists(select * from lsSharedFoldersRoots where (Folder = p_folder)))
	then
		select CONCAT('Invalid root folder name, specified folder ''' , p_folder , ''' already exists !') as ErrorText;
		leave sp_lbl;
	end if;
	
	-- Insert root folder
	insert lsSharedFoldersRoots (RootID,Enabled,Folder,Description,RootType,BoundedUser,BoundedFolder) 
		select (p_rootID,p_enabled,p_folder,p_description,p_rootType,p_boundedUser,p_boundedFolder);

	select null as ErrorText;
END;


//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_AddUser (
	p_UserID	     varchar(100) /* = NULL */,
	p_FullName    varchar(100) /* = NULL */,
	p_UserName    varchar(100) /* = NULL */,
	p_Password    varchar(100) /* = NULL */,
	p_Description varchar(100) /* = NULL */,
	p_DomainName  varchar(100) /* = NULL */,
	p_MailboxSize bigint	  /* = 0 */,
	p_Enabled     tinyint          /* = true */,
	p_permissions int          /* = 255 */)
BEGIN


if(not exists(select * from lsUsers where (UserID=p_UserID)))
then
	if(not exists(select * from lsUsers where (UserName=p_UserName)))
	then
		insert lsUsers (UserID,FullName,UserName,Password,Description,Mailbox_Size,DomainName,Enabled,`Permissions`,CreationTime) 
		values (p_UserID,p_FullName,p_UserName,p_Password,p_Description,p_MailboxSize,p_DomainName,p_Enabled,p_permissions,now());

		select null as ErrorText;
	else
		select CONCAT('User with specified name "' , p_UserName , '" already exists !') as ErrorText;
	end if;
else
	select CONCAT('User with specified ID "' , p_UserID , '" already exists !') as ErrorText;
end if;



END;
//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_AddUserAddress (
	p_UserName  nvarchar(100) /* = NULL */,
	p_Address   nvarchar(100) /* = NULL */)

BEGIN
	declare v_UserID nvarchar(100);
	set v_UserID = (select UserID from lsUsers where UserName=p_UserName);

	if(not exists(select * from lsUserAddresses where (UserID=v_UserID AND Address=p_Address)))
	then
		insert lsUserAddresses (UserID,Address) values (v_UserID,p_Address);

		select null as ErrorText;
	else
		select CONCAT('User address with specified name "' , p_Address , '" already exists !') as ErrorText;
	end if;
END;


//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_AddUserMessageRule (
	p_userID          nvarchar(100) /* = NULL */,
	p_ruleID          nvarchar(100) /* = NULL */,
	p_cost            bigint        /* = NULL */,
	p_enabled         tinyint           /* = NULL */,
	p_checkNextRule   int           /* = NULL */,
	p_description     nvarchar(400) /* = NULL */,
	p_matchExpression longblob         /* = NULL */)
BEGIN
	if(not exists(select * from lsUserMessageRules where (RuleID = p_ruleID)))
	then
		insert lsUserMessageRules (UserID,RuleID,Cost,Enabled,CheckNextRuleIf,Description,MatchExpression) 
			values (p_userID,p_ruleID,p_cost,p_enabled,p_checkNextRule,p_description,p_matchExpression);

		select null as ErrorText;
	else
		select CONCAT('Rule with specified ID "' , p_ruleID , '" already exists !') as ErrorText;
	end if;
END;


//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_AddUserMessageRuleAction (
	p_userID          nvarchar(100) /* = NULL */,
	p_ruleID          nvarchar(100) /* = NULL */,
	p_actionID        nvarchar(100) /* = NULL */,
	p_description     nvarchar(400) /* = NULL */,
	p_actionType      int           /* = NULL */,
	p_actionData      longblob         /* = NULL */)
BEGIN
	if(not exists(select * from lsUserMessageRuleActions where (RuleID = p_ruleID AND ActionID = p_actionID)))
	then
		insert lsUserMessageRuleActions (UserID,RuleID,ActionID,Description,ActionType,ActionData) 
			values (p_userID,p_ruleID,p_actionID,p_description,p_actionType,p_actionData);

		select null as ErrorText;
	else
		select CONCAT('Action with specified ID "' , p_actionID , '" already exists !') as ErrorText;
	end if;
END;


//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_AddUserRemoteServer ( 
	p_ServerID       nvarchar(100) /* = NULL */,
	p_UserName       nvarchar(100) /* = NULL */,
	p_Description    nvarchar(100) /* = NULL */,
	p_RemoteServer   nvarchar(100) /* = NULL */,
	p_RemotePort     int           /* = NULL */,
	p_RemoteUserName nvarchar(100) /* = NULL */,
	p_RemotePassword nvarchar(100) /* = NULL */,
	p_UseSSL         tinyint           /* = NULL */,
	p_Enabled        tinyint           /* = NULL */)
BEGIN

	declare v_UserID nvarchar(100);
if(not exists(select * from lsUserRemoteServers where (ServerID=p_ServerID)))
then
	-- Get userID
	set v_UserID = (select UserID from lsUsers where UserName=p_UserName);

	insert lsUserRemoteServers (
		ServerID,
		UserID,
		Description,
		RemoteServer,
		RemotePort,
		RemoteUserName,
		RemotePassword,
		UseSSL,
		Enabled
	) 
	values (
		p_ServerID,
		v_UserID,
		p_Description,
		p_RemoteServer,
		p_RemotePort,
		p_RemoteUserName,
		p_RemotePassword,
		p_UseSSL,
		p_Enabled
	);

	select null as ErrorText;
else
	select CONCAT('User remote server with specified ID "' , p_ServerID , '" already exists !') as ErrorText;
end if;



END;
//

DELIMITER ;




/* Adds users default folder.
    @folderName - Users default folder name.
    @permanent  - Specifies if folder is permanent, users can't delete it.
*/
DELIMITER //

CREATE PROCEDURE lspr_AddUsersDefaultFolder ( 
    p_folderName nvarchar(200),
    p_permanent  tinyint)
sp_lbl:
BEGIN

IF(exists(select * from lsUsersDefaultFolders where (FolderName = p_folderName)))
THEN
    select CONCAT('Users default folder with specified name ''' , p_folderName , ''' already exists !') as ErrorText;
    leave sp_lbl;
END IF;

insert into lsUsersDefaultFolders (FolderName,Permanent)
    values (p_folderName,p_permanent);

select null as ErrorText;



END;
//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_CreateFolder ( 
	p_UserName nvarchar(100),
	p_Folder   nvarchar(100))
BEGIN


declare v_UserID char(36);
set v_UserID = (select UserID from lsUsers where UserName = p_UserName);

if exists(select * from  lsIMAPFolders where UserID = v_UserID AND FolderName = p_Folder)
then
	select CONCAT('Folder(' , p_Folder  , ') already exists') as ErrorText;
else
	insert into lsIMAPFolders (UserID,FolderName,CreationTime) values (v_UserID,p_Folder,now());
end if;



END;
//

DELIMITER ;




DELIMITER //
-- NNAVA
CREATE PROCEDURE lspr_DeleteDomain (
	p_DomainID nvarchar(100) /* = NULL */)
BEGIN
DECLARE NOT_FOUND INT DEFAULT 0;
/*
declare v_DomainName nvarchar(100);

declare rsUsers cursor for select UserID from lsUsers where DomainName=v_DomainName;

declare rsMailingLists cursor for select MailingListID from lsMailingLists where DomainName=v_DomainName;
DECLARE CONTINUE HANDLER FOR NOT FOUND SET NOT_FOUND = 1;
set v_DomainName = (select DomainName from lsDomains where DomainID = p_DomainID);

-- - Delete domain users ---------------------------------------------------------
open rsUsers;

declare v_UserID nvarchar(100);
fetch next from;  rsUsers into v_UserID
while(NOT_FOUND = 0)
do
	call lspr_DeleteUser v_UserID;=v_UserID
	-- Get next data row
	fetch next from;  rsUsers into v_UserID
end while;
close rsUsers;
-- -------------------------------------------------------------------------------

-- - Delete domain mailing lists --------------------------------------------------
set not_found = 0;
open rsMailingLists;

declare v_MailingListID nvarchar(100);
fetch next from;  rsMailingLists into v_MailingListID
while(NOT_FOUND = 0)
do
	call lspr_DeleteMailingList v_MailingListID;=v_MailingListID
	-- Get next data row
	fetch next from;  rsMailingLists into v_MailingListID
end while;
close rsMailingLists;
-- -------------------------------------------------------------------------------

delete from lsDomains where DomainID=p_DomainID;

*/

END;
//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_DeleteFilter (
	p_FilterID nvarchar(100) /* = NULL */)
BEGIN

delete from lsFilters where (FilterID=p_FilterID);



END;
//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_DeleteFolder ( 
	p_UserName nvarchar(100),
	p_Folder   nvarchar(100))
BEGIN

declare v_UserID char(36);
set v_UserID = (select UserID from lsUsers where UserName = p_UserName);

if exists(select * from  lsIMAPFolders where UserID = v_UserID AND FolderName = p_Folder)
then
	-- Delete specified folder and it's subfolders messages
	delete from lsMailStore where (Mailbox = p_UserName AND Folder LIKE (CONCAT(p_Folder , '%')));

	-- Delete folder and it's sub folders
	delete from lsIMAPFolders where (UserID = v_UserID AND FolderName LIKE (CONCAT(p_Folder , '%')));

	-- Delete specified folder and it's subfolders ACL, if any
	delete from lsIMAP_ACL where (Folder LIKE (CONCAT(p_UserName , '/' , p_Folder , '%')));
else
	select CONCAT('Folder(' , p_Folder  , ') doesn''t exist') as ErrorText;	
end if;



END;
//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_DeleteFolderACL (
	p_FolderName nvarchar(500) /* = NULL */,
	p_UserName   nvarchar(500) /* = NULL */)
BEGIN

delete from lsIMAP_ACL where (Folder = p_FolderName AND `User` = p_UserName);



END;
//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_DeleteGlobalMessageRule (
	p_ruleID nvarchar(100) /* = NULL */)
BEGIN
	-- Delete all specified rule Actions
	delete from lsGlobalMessageRuleActions where (RuleID = p_ruleID);

	delete from lsGlobalMessageRules where (RuleID = p_ruleID);
END;


//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_DeleteGlobalMessageRuleAction (
	p_ruleID   nvarchar(100) /* = NULL */,
	p_actionID nvarchar(100) /* = NULL */)
BEGIN
	delete from lsGlobalMessageRuleActions where (RuleID = p_ruleID AND ActionID = p_actionID);
END;


//

DELIMITER ;




/*  Implementation notes:
      Decsription:
	    Deletes user group
	  Returns:
		If successful returns nothing, otherwise returns 1 row with error text in column 'ErrorText'.

	  Implementation:
		*) Ensure that group ID exist. Return error text.
        *) Delete group members.
        *) Delete group.
		 
*/

DELIMITER //

CREATE PROCEDURE lspr_DeleteGroup (
	p_groupID     nvarchar(100) /* = NULL */)
sp_lbl:

BEGIN
	-- Ensure that group ID exist.
	if(not exists(select * from lsGroups where (GroupID = p_groupID)))
	then
		select CONCAT('Invalid group ID, specified group ID ''' , p_groupID , ''' doesn''t exist !') as ErrorText;
		leave sp_lbl;
	end if;

	-- Delete group members.
	delete from lsGroupMembers where (GroupID = p_groupID);

	-- Delete group.
	delete from lsGroups where (GroupID = p_groupID);

	select null as ErrorText;
END;


//

DELIMITER ;




/*  Implementation notes:
      Decsription:
	    Deletes user group member
	  Returns:
		If successful returns nothing, otherwise returns 1 row with error text in column 'ErrorText'.

	  Implementation:
		*) Ensure that group exists. Return error text.
        *) Ensure that group member does exist. Return error text.
        *) Delete group member.
		 
*/

DELIMITER //

CREATE PROCEDURE lspr_DeleteGroupMember (
	p_groupName   nvarchar(100) /* = NULL */,
	p_userOrGroup nvarchar(100) /* = NULL */)
sp_lbl:

BEGIN
	-- Ensure that group exists.
	if(not exists(select * from lsGroups where (GroupName = p_groupName)))
	then
		select CONCAT('Invalid group name, specified group ''' , p_groupName , ''' doesn''t exist !') as ErrorText;
		leave sp_lbl;
	end if;

	-- Ensure that group member does exist.
	if(not exists(select * from lsGroupMembers where (UserOrGroup = p_userOrGroup)))
	then
		select CONCAT('Invalid group member, specified group member ''' , p_userOrGroup , ''' already exists !') as ErrorText;
		leave sp_lbl;
	end if;

	-- Delete group members.
	delete from lsGroupMembers where (UserOrGroup = p_userOrGroup);

	select null as ErrorText;
END;


//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_DeleteMailingList (
	p_MailingListID	nvarchar(100) /* = NULL */)
BEGIN

delete from lsMailingListAcl where (MailingListID=p_MailingListID);
delete from lsMailingListAddresses where (MailingListID=p_MailingListID);
delete from lsMailingLists where (MailingListID=p_MailingListID);



END;
//

DELIMITER ;




/*  Implementation notes:
      Decsription:
	    Deletes specified mailing list ACL entry.
	  Returns:
		If successful returns nothing, otherwise returns 1 row with error text in column 'ErrorText'.

	  Implementation:
		*) Ensure that mailing list exists.
        *) Delete ACL entry.
		 
*/

DELIMITER //

CREATE PROCEDURE lspr_DeleteMailingListACL (
	p_mailingListName nvarchar(100) /* = NULL */,
	p_userOrGroup     nvarchar(100) /* = NULL */)
sp_lbl:

BEGIN
	declare v_mailingListID nvarchar(100);
    
	-- Ensure that mailing list exists.
	if(not exists(select * from lsMailingLists where (MailingListName = p_mailingListName)))
	then
		select CONCAT('Invalid mailing list name, specified mailing list ''' , p_mailingListName , ''' doesn''t exist !') as ErrorText;
		leave sp_lbl;
	end if;

	-- Get mailing list ID
	select (select MailingListID from lsMailingLists where (MailingListName = p_mailingListName));

	-- Delete ACL entry.
	delete from lsMailingListACL where (MailingListID = v_mailingListID AND UserOrGroup = p_userOrGroup);

	select null as ErrorText;
END;


//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_DeleteMailingListAddress (
	p_AddressID nvarchar(100) /* = NULL */)
BEGIN

delete from lsMailingListAddresses where (AddressID=p_AddressID);



END;
//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_DeleteMessage ( 
    p_MessageID char(36) /* = NULL */,
    p_Mailbox   nvarchar(100)    /* = NULL */,
    p_Folder    nvarchar(100)    /* = NULL */)
BEGIN
    delete from lsMailStore 
        where MessageID = p_MessageID AND Mailbox = p_Mailbox AND Folder = p_Folder;
END;


//

DELIMITER ;




/* Deletes specified recycle bin message.
    @messageID - Message ID which to delete.
*/
DELIMITER //

CREATE PROCEDURE lspr_DeleteRecycleBinMessage (
	p_messageID nvarchar(100) /* = NULL */)
BEGIN
	delete from lsRecycleBin where(MessageID = p_messageID);
END;


//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_DeleteRoute (
	p_RouteID nvarchar(100) /* = NULL */)
BEGIN

delete from lsRouting where (RouteID=p_RouteID);



END;
//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_DeleteSecurityEntry (
	p_SecurityID nvarchar(100) /* = NULL */)
BEGIN

delete from lsIPSecurity where (ID=p_SecurityID);



END;
//

DELIMITER ;




/*	Implementation notes:
      Decsription:
	    Deletes shared folder root.
	  Returns:
		If successful returns nothing, otherwise returns 1 row with error text in column 'ErrorText'.

	  Implementation:
		*) Ensure that root ID exist. Return error text.
        *) Delete root folder.
		 
*/

DELIMITER //

CREATE PROCEDURE lspr_DeleteSharedFolderRoot (
	p_rootID nvarchar(100) /* = NULL */)
sp_lbl:

BEGIN
	-- Ensure that root ID exist.
	if(not exists(select * from lsSharedFoldersRoots where (RootID = p_rootID)))
	then
		select CONCAT('Invalid root ID, specified root ID ''' , p_rootID , ''' doesn''t exist !') as ErrorText;
		leave sp_lbl;
	end if;

	-- Delete group.
	delete from lsSharedFoldersRoots where (RootID = p_rootID);

	select null as ErrorText;
END;


//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_DeleteUser (
	p_UserID	nvarchar(100) /* = NULL */)
BEGIN

delete from lsUserAddresses where (UserID=p_UserID);
delete from lsUserRemoteServers where (UserID=p_UserID);
delete from lsUserMessageRules where (UserID=p_UserID);
delete from lsIMAPSubscribedFolders where (UserID=p_UserID);
delete from lsUsers where (UserID=p_UserID);



END;
//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_DeleteUserAddress (
	p_emailAddress nvarchar(100) /* = NULL */)
BEGIN

delete from lsUserAddresses where (Address = p_emailAddress);



END;
//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_DeleteUserMessageRule (
	p_userID nvarchar(100) /* = NULL */,
	p_ruleID nvarchar(100) /* = NULL */)
BEGIN
	-- Delete all specified rule Actions
	delete from lsUserMessageRuleActions where (UserID = p_userID AND RuleID = p_ruleID);

	delete from lsUserMessageRules where (UserID = p_userID AND RuleID = p_ruleID);
END;


//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_DeleteUserMessageRuleAction (
	p_userID   nvarchar(100) /* = NULL */,
	p_ruleID   nvarchar(100) /* = NULL */,
	p_actionID nvarchar(100) /* = NULL */)
BEGIN
	delete from lsUserMessageRuleActions where (UserID = p_userID AND RuleID = p_ruleID AND ActionID = p_actionID);
END;


//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_DeleteUserRemoteServer (
	p_ServerID nvarchar(100) /* = NULL */)
BEGIN

delete from lsUserRemoteServers where (ServerID=p_ServerID);



END;
//

DELIMITER ;




/* Deletes users default folder.
    @folderName - Users default folder name which to delete.
*/
DELIMITER //

CREATE PROCEDURE lspr_DeleteUsersDefaultFolder (
	p_folderName nvarchar(200))
sp_lbl:

BEGIN
	-- Ensure that folder exist.
	if(not exists(select * from lsUsersDefaultFolders where (FolderName = p_folderName)))
	then
		select CONCAT('Users default folder with specified name ''' , p_folderName , ''' doesn''t exists !') as ErrorText;
		leave sp_lbl;
	end if;

	-- Delete folder.
	delete from lsUsersDefaultFolders where (FolderName = p_folderName);

	select null as ErrorText;
END;


//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_DomainExists (
	p_DomainName nvarchar(100) /* = NULL */)
BEGIN

select * from lsDomains where (DomainName=p_DomainName);



END;
//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_FolderExists (
	p_FolderName nvarchar(500) /* = NULL */,
	p_UserName   nvarchar(100) /* = NULL */)
BEGIN

if(exists (select * from lsIMAPFolders where (UserID=(select UserID from lsUsers where UserName = p_UserName)  AND FolderName = p_FolderName)))
then 
	select * from lsIMAPFolders where (UserID=(select UserID from lsUsers where UserName = p_UserName)  AND FolderName = p_FolderName);
else
	if(lower(p_FolderName) = 'inbox')
	then
		-- Create inbox, it's missing
		call lspr_CreateFolder( p_UserName,'Inbox');

		select * from lsIMAPFolders where (UserID=(select UserID from lsUsers where UserName = p_UserName)  AND FolderName = p_FolderName);
	end if;
end if;



END;
//

DELIMITER ;



DELIMITER //

CREATE PROCEDURE lspr_GetDomains()  
BEGIN

select * from lsDomains;



END;
//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_GetFilters()
BEGIN

select * from lsFilters;



END;
//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_GetFolderACL ( 
	p_FolderName nvarchar(500) /* = NULL */)
BEGIN

if(p_FolderName <> '')
then
	select * from lsIMAP_ACL where (Folder = p_FolderName);
else
	select * from lsIMAP_ACL;
end if;



END;
//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_GetFolders ( 
	p_UserName nvarchar(100))
BEGIN

if(exists (select * from lsIMAPFolders where UserID=(select UserID from lsUsers where UserName = p_UserName)))
then 
	select * from lsIMAPFolders where UserID=(select UserID from lsUsers where UserName = p_UserName);
else
	-- Create inbox, it's missing
	call lspr_CreateFolder( p_UserName,'Inbox');

	select 'Inbox' as FolderName;
end if;



END;
//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_GetGlobalMessageRuleActions (
	p_ruleID nvarchar(100) /* = NULL */)
BEGIN
	select * from lsGlobalMessageRuleActions where (RuleID = p_ruleID);
END;


//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_GetGlobalMessageRules()
BEGIN
	select * from lsGlobalMessageRules order by Cost ASC;
END;


//

DELIMITER ;




/*  Implementation notes:
      Decsription:
	     Gets user group members.
	  Returns:
		 Retruns user group members.
*/

DELIMITER //

CREATE PROCEDURE lspr_GetGroupMembers (
	p_groupName   nvarchar(100) /* = NULL */)	
BEGIN
	-- Get groupID
	declare v_groupID nvarchar(100);
	set v_groupID = (select GroupID from lsGroups where (GroupName = p_groupName));

	select * from lsGroupMembers where (GroupID = v_groupID);
END;


//

DELIMITER ;




/*  Implementation notes:
      Decsription:
	     Gets user groups.
	  Returns:
		 Retruns user groups.
*/

DELIMITER //

CREATE PROCEDURE lspr_GetGroups()	
BEGIN
	select * from lsGroups;
END;


//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_GetMailboxSize (
	p_UserName nvarchar(100) /* = NULL */)
BEGIN


declare v_Size int;
set  v_Size = 0;

-- Count mailbox size
if(exists(select MailBox from lsMailStore where Mailbox=p_UserName))
then
    set v_Size = (select sum(Size) from lsMailStore where Mailbox=p_UserName);
end if;

select v_Size as MailboxSize;



END;
//

DELIMITER ;




/*  Implementation notes:
      Decsription:
	     Gets mailing list ACL list.
	  Returns:
		 Retruns mailing list ACL list.
*/

DELIMITER //

CREATE PROCEDURE lspr_GetMailingListACL (	
	p_mailingListName nvarchar(100))
BEGIN
	select * from lsMailingListACL 
		where (MailingListID = (select MailingListID from lsMailingLists where (MailingListName = p_mailingListName)));
END;


//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_GetMailingListAddresses (
	p_MailingListName nvarchar(100) /* = NULL */)
BEGIN

	declare v_MailingListID nvarchar(100);
    
if(p_MailingListName <> '')
then
	set v_MailingListID = (select MailingListID from lsMailingLists where MailingListName=p_MailingListName);

	select * from lsMailingListAddresses where (MailingListID=v_MailingListID);
else
       select * from lsMailingListAddresses;
end if;



END;
//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_GetMailingListProperties (
	p_MailingListName nvarchar(100)	/* = NULL */)
BEGIN

select * from lsMailingLists where (MailingListName = p_MailingListName);



END;
//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_GetMailingLists (
	p_DomainName nvarchar(100) /* = NULL */)
BEGIN

if(p_DomainName <> '')
then
      select * from lsMailingLists where (DomainName=p_DomainName);
else
       select * from lsMailingLists;
end if;



END;
//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_GetMessage (
	p_MessageID char(36) /* = NULL */,
	p_Mailbox   nvarchar(100)    /* = NULL */,
	p_Folder    nvarchar(100)    /* = NULL */)
BEGIN

select Data from lsMailStore where MessageID = p_MessageID AND Mailbox = p_Mailbox AND Folder = p_Folder;



END;
//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_GetMessageInfo (
	p_Mailbox nvarchar(100)/* = NULL */,
	p_Folder	 nvarchar(100)/* = NULL */)
BEGIN

select MessageID,Size,Date,MessageFlags,UID  from lsMailStore where MAILBOX = p_Mailbox AND Folder = p_Folder;



END;
//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_GetMessageTopLines (
	p_MessageID char(36) /* = NULL */,
	p_Mailbox   nvarchar(100)    /* = NULL */,
	p_Folder    nvarchar(100)    /* = NULL */)
BEGIN
	select TopLines from lsMailStore where MessageID = p_MessageID AND  Mailbox = p_Mailbox AND Folder = p_Folder;
END;


//

DELIMITER ;




/* Gets recycle bin message.
    @messageID - Recycle bin message ID.
*/
DELIMITER //

CREATE PROCEDURE lspr_GetRecycleBinMessage (
    p_messageID char(36) /* = NULL */)
BEGIN
	select * from lsRecycleBin where (MessageID = p_messageID);
END;


//

DELIMITER ;




/* Gets reycle bin messages info.
    @userName  - User who's recyclebin messages to get or null if all users messages.
    @startDate - Messages from specified date.
    @endDate   - Messages to specified date.
*/
DELIMITER //

CREATE PROCEDURE lspr_GetRecycleBinMessagesInfo (
    p_userName  nvarchar(200) /* = NULL */,
    p_startDate datetime(3),
    p_endDate   datetime(3))
BEGIN
    IF p_userName is null
    THEN
        select MessageID,DeleteTime,`User`,Folder,`Size`,Envelope from lsRecycleBin where(p_startDate <= DATE_FORMAT(DeleteTime,'%Y%m%d') AND p_endDate >= DATE_FORMAT(DeleteTime,'%Y%m%d'));
    ELSE
        select MessageID,DeleteTime,`User`,Folder,`Size`,Envelope from lsRecycleBin where(`User` = p_userName AND p_startDate <= DATE_FORMAT(DeleteTime,'%Y%m%d') AND p_endDate >= DATE_FORMAT(DeleteTime,'%Y%m%d'));
    END IF;
END;


//

DELIMITER ;




/* Gets recycle bin settings.
*/
DELIMITER //

CREATE PROCEDURE lspr_GetRecycleBinSettings()	
BEGIN
	select * from lsRecycleBinSettings;
END;


//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_GetRoutes()
begin

select * from lsRouting;

end;


//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_GetSecurityList()
BEGIN

select * from lsIPSecurity;



END;
//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_GetSettings()  
BEGIN

select * from lsSettings;



END;
//

DELIMITER ;




/*  Implementation notes:
      Decsription:
	     Gets shared root folders.
	  Returns:
		 Retruns shared root folders.
*/

DELIMITER //

CREATE PROCEDURE lspr_GetSharedFolderRoots()	
BEGIN
	select * from lsSharedFoldersRoots;
END;


//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_GetSubscribedFolders ( 
	p_UserName nvarchar(100))
BEGIN

select * from lsIMAPSubscribedFolders where UserID=(select UserID from lsUsers where UserName = p_UserName);



END;
//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_GetUserAddresses (
	p_UserName nvarchar(100) /* = NULL */)
BEGIN

	declare v_UserID nvarchar(100);
    
if(p_UserName <> '')
then
	set v_UserID = (select UserID from lsUsers where UserName=p_UserName);

	select * from lsUserAddresses where (UserID=v_UserID);
else
       select * from lsUserAddresses;
end if;



END;
//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_GetUserMessageRuleActions (
	p_userID nvarchar(100) /* = NULL */,
	p_ruleID nvarchar(100) /* = NULL */)
BEGIN
	select * from lsUserMessageRuleActions where (UserID = p_userID AND RuleID = p_ruleID);
END;


//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_GetUserMessageRules (
	p_UserName nvarchar(100) /* = NULL */)
BEGIN
	declare v_UserID nvarchar(100);
if(p_UserName <> '')
then
	set v_UserID = (select UserID from lsUsers where UserName=p_UserName);

	select * from lsUserMessageRules where (UserID=v_UserID);
else
	select * from lsUserMessageRules;
end if;



END;
//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_GetUserProperties (
	p_UserName nvarchar(100)	/* = NULL */)
BEGIN

select * from lsUsers where (UserName = p_UserName);



END;
//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_GetUserRemoteServers (
	p_UserName nvarchar(100) /* = NULL */)
BEGIN
	declare v_UserID nvarchar(100);
if(p_UserName <> '')
then
	set v_UserID = (select UserID from lsUsers where UserName=p_UserName);

	select * from lsUserRemoteServers where (UserID=v_UserID);
else
	select * from lsUserRemoteServers;
end if;



END;
//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_GetUsers (
	p_DomainName nvarchar(100) /* = NULL */)
BEGIN

if(p_DomainName <> '')
then
      select * from lsUsers where (DomainName=p_DomainName);
else
       select * from lsUsers;
end if;



END;
//

DELIMITER ;




/*  Implementation notes:
      Decsription:
	     Gets users default folders.
	  Returns:
		 Retruns users default folders.
*/

DELIMITER //

CREATE PROCEDURE lspr_GetUsersDefaultFolders()	
BEGIN
	select * from lsUsersDefaultFolders;
END;


//

DELIMITER ;




/*  Implementation notes:
      Decsription:
	     Checks if specified user group exists.
	  Returns:
		 Retruns specified group if it exists.
*/

DELIMITER //

CREATE PROCEDURE lspr_GroupExists (
	p_groupName nvarchar(100) /* = NULL */)
BEGIN
	select * from lsGroups where (GroupName = p_groupName);
END;


//

DELIMITER ;




/*	Implementation notes:
      Decsription:
	     Checks if specified user group member exists.
	  Returns:
		 Retruns specified group member if it exists.
*/

DELIMITER //

CREATE PROCEDURE lspr_GroupMemberExists (
	p_groupName   nvarchar(100) /* = NULL */,
	p_userOrGroup nvarchar(100) /* = NULL */)
BEGIN
	-- Get groupID
	declare v_groupID nvarchar(100);
	set v_groupID = (select GroupID from Groups where (GroupName = p_groupName));

	select * from lsGroupMembers where (GroupID = v_groupID AND UserOrGroup = p_userOrGroup);
END;


//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_MapUser (
	p_EmailAddress nvarchar(100) /* = NULL */)
BEGIN

declare v_UserID nvarchar(100);
set v_UserID = (select UserID from lsUserAddresses where Address=p_EmailAddress);

select UserName from lsUsers where (UserID=v_UserID);



END;
//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_RenameFolder ( 
	p_UserName  nvarchar(100),
	p_Folder    nvarchar(100),
	p_NewFolder nvarchar(100))
sp_lbl:
BEGIN

declare v_UserID char(36);
set v_UserID = (select UserID from lsUsers where UserName = p_UserName);

-- Check if destination folder exists
if exists(select * from  lsIMAPFolders where UserID = v_UserID AND FolderName = p_NewFolder)
then
	select CONCAT('Destination Folder(' , p_Folder  , ') already exists') as ErrorText;
	leave sp_lbl;
end if;

if exists(select * from  lsIMAPFolders where UserID = v_UserID AND FolderName = p_Folder)
then
	-- Rename mail store folder and it's subfolders
	update lsMailStore  set 
		Folder = (CONCAT(p_NewFolder , substring(Folder,char_length(rtrim(p_Folder)) + 1,char_length(rtrim(Folder)) - char_length(rtrim(p_Folder)))))
	where (Mailbox = p_UserName AND Folder LIKE (CONCAT(p_Folder , '%')));

	-- Rename folder and it's subfolders
	update lsIMAPFolders  set 
		FolderName = (CONCAT(p_NewFolder , substring(FolderName,char_length(rtrim(p_Folder)) + 1,char_length(rtrim(FolderName)) - char_length(rtrim(p_Folder)))))
	where (UserID = v_UserID AND FolderName LIKE (CONCAT(p_Folder , '%')));

	-- Rename folder and it's subfolders ACL
	update lsIMAP_ACL  set 
		Folder = (CONCAT(p_UserName , '/' , p_NewFolder , substring(Folder,char_length(rtrim(CONCAT(p_UserName , '/' , p_Folder))) + 1,char_length(rtrim(Folder)) - char_length(rtrim(CONCAT(p_UserName , '/' , p_NewFolder))) + 1))) 
	where (Folder LIKE (CONCAT(p_UserName , '/' , p_Folder , '%')));
else
	select CONCAT('Source Folder(' , p_Folder  , ') doesn''t exists') as ErrorText;	
end if;



END;
//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_SetFolderACL (
	p_FolderName  nvarchar(500) /* = NULL */,
	p_UserName    nvarchar(500) /* = NULL */,
	p_Permissions nvarchar(20)  /* = '' */)
BEGIN

if(exists(select * from lsIMAP_ACL where (Folder = p_FolderName AND `User` = p_UserName)))
then
	update lsIMAP_ACL set 
		`Permissions` = p_Permissions
	where  (Folder = p_FolderName AND `User` = p_UserName);
else
	insert lsIMAP_ACL (Folder,`User`,`Permissions`) 
	select (p_FolderName,p_UserName,p_Permissions);
end if;



END;
//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_StoreMessage (
	p_Mailbox       nvarchar(100) /* = NULL */,
	p_Folder        nvarchar(100) /* = NULL */,
	p_Data          longblob         /* = NULL */,
	p_Size          bigint        /* = 0 */,
	p_TopLines      longblob         /* = NULL */,
	p_Date          DateTime(3)	     /* = NULL */,
	p_MessageFlags nvarchar(1000)            /* = 0 */)
sp_lbl:
BEGIN

declare v_UserID nvarchar(100);
if(not exists (select * from lsIMAPFolders where UserID=(select UserID from lsUsers where UserName = p_Mailbox)))
then 
	if(lower(p_Folder) = 'inbox')
	then
		set v_UserID = (select UserID from lsUsers where UserName = p_Mailbox);

		insert into lsIMAPFolders (UserID,FolderName) values (v_UserID,'Inbox');
	else
		select (CONCAT('Folder ' , p_Folder , ' doesn''t exist')) as ErrorText;
		leave sp_lbl;
	end if;
end if;

insert lsMailStore (MessageID,Mailbox,Folder,Data,Size,TopLines,Date,MessageFlags) values (uuid(),p_Mailbox,p_Folder,p_Data,p_Size,p_TopLines,p_Date,p_MessageFlags);



END;
//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_StoreMessageFlags (
	p_MessageID    char(36) /* = NULL */,
	p_Mailbox      nvarchar(100)    /* = NULL */,
	p_Folder       nvarchar(100)    /* = NULL */,
	p_MessageFalgs nvarchar(1000)   /* = NULL */)
BEGIN

Update lsMailStore set MessageFlags = p_MessageFalgs where MessageID = p_MessageID AND Mailbox = p_Mailbox AND Folder = p_Folder;



END;
//

DELIMITER ;




/* Stores specified message to recycel bin.
    @messageID - Recycle bin message ID.
    @user      - User whos messge it is.
    @folder    - Original folder that contained message.
    @size      - Message size in bytes.
    @envelope  - Message IMAP Envelop string.
    @data      - Message data.
*/
DELIMITER //

CREATE PROCEDURE lspr_StoreRecycleBinMessage (
    p_messageID nvarchar(100)  /* = NULL */,
    p_user      nvarchar(200)  /* = NULL */,
    p_folder    nvarchar(500)  /* = NULL */,
	p_size      bigint         /* = 0 */,
    p_envelope  nvarchar(2000) /* = NULL */,
    p_data      longblob          /* = NULL */)
BEGIN
    insert into lsRecycleBin (MessageID,DeleteTime,`User`,Folder,`Size`,Envelope,Data)
        values(p_messageID,now(),p_user,p_folder,p_size,p_envelope,p_data);
END;


//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_SubscribeFolder ( 
	p_UserName nvarchar(100),
	p_Folder   nvarchar(100))
BEGIN

-- ToDo: check if exist, delete or just skip ???

declare v_UserID char(36);
set v_UserID = (select UserID from lsUsers where UserName = p_UserName);

insert into lsIMAPSubscribedFolders(UserID,FolderName) values (v_UserID,p_Folder);



END;
//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_UnSubscribeFolder ( 
	p_UserName nvarchar(100),
	p_Folder   nvarchar(100))
BEGIN

declare v_UserID char(36);
set v_UserID = (select UserID from lsUsers where UserName = p_UserName);

delete from lsIMAPSubscribedFolders where UserID =  v_UserID AND FolderName = p_Folder;



END;
//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_UpdateDomain (
    p_DomainID    nvarchar(100) /* = NULL */,
    p_DomainName  nvarchar(100) /* = NULL */,
    p_Description nvarchar(100) /* = NULL */)
sp_lbl:
BEGIN

declare v_oldDomainName varchar(200);
-- Ensure that domain with specified ID exists
IF(not exists(select * from lsDomains where (DomainID = p_DomainID)))
THEN
    select CONCAT('Specified @DomainID "' , p_DomainID , '" doesn''t exists !') as ErrorText;
    leave sp_lbl;
END IF;

-- Ensure that another domain haven't same domain name
IF(exists(select * from lsDomains where (DomainID != p_DomainID AND DomainName = p_DomainName)))
THEN
    select CONCAT('Domain with specified name "' , p_DomainName , '" already exists !') as ErrorText;
    leave sp_lbl;
END IF;

-- If domain name changed, rename user addresses and mailing lists
set v_oldDomainName = (select DomainName from lsDomains where (DomainID = p_DomainID));

IF(lower(v_oldDomainName) != lower(p_DomainName))
THEN
    -- Rename user addresses
    update lsUserAddresses set
        Address = concat(substring(Address,0,char_length(rtrim(Address)) - char_length(rtrim(v_oldDomainName)) + 1) , p_DomainName)
    where(Address LIKE ('%@' + v_oldDomainName));

    -- Rename mailing lists
    update lsMailingLists set
        MailingListName = concat(substring(MailingListName,0,char_length(rtrim(MailingListName)) - char_length(rtrim(v_oldDomainName)) + 1) , p_DomainName)
    where(MailingListName LIKE ('%@' + v_oldDomainName)) ;
END IF;

update lsDomains set 
    DomainName  = p_DomainName,
    Description = p_Description
where (DomainiD = p_DomainID);

select null as ErrorText;



END;
//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_UpdateFilter (
	p_FilterID    nvarchar(100) /* = NULL */,
	p_Description nvarchar(100) /* = NULL */,
	p_Type        nvarchar(100) /* = NULL */,
	p_Assembly    nvarchar(100) /* = NULL */,
	p_ClassName   nvarchar(100) /* = NULL */,
	p_Cost        bigint        /* = 0 */,
	p_Enabled     tinyint           /* = true */)
BEGIN

if(exists(select * from lsFilters where (FilterID=p_FilterID)))
then
	update lsFilters set 
		Description = p_Description,
		Type        = p_Type,
		Assembly    = p_Assembly,
		ClassName   = p_ClassName,
		Cost        = p_Cost,
		Enabled     = p_Enabled
	where  (FilterID=p_FilterID);

	select null as ErrorText;
else
	select CONCAT('Filter with specified ID "' , p_FilterID , '" doesn''t exist !') as ErrorText;
end if;



END;
//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_UpdateGlobalMessageRule (
	p_ruleID          nvarchar(100) /* = NULL */,
	p_cost            bigint        /* = NULL */,
	p_enabled         tinyint           /* = NULL */,
	p_checkNextRule   int           /* = NULL */,
	p_description     nvarchar(400) /* = NULL */,
	p_matchExpression longblob         /* = NULL */)
BEGIN
	if(exists(select * from lsGlobalMessageRules where (RuleID = p_ruleID)))
    then
		update lsGlobalMessageRules set
			RuleID          = p_ruleID,
			Cost            = p_cost,
			Enabled         = p_enabled,
			CheckNextRuleIf = p_checkNextRule,
			Description     = p_description,
			MatchExpression = p_matchExpression
		where  (RuleID = p_ruleID);

		select null as ErrorText;
    else
		select CONCAT('Rule with specified ID "' , p_ruleID , '" doesn''t exist !') as ErrorText;
	end if;
END;


//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_UpdateGlobalMessageRuleAction (
	p_ruleID          nvarchar(100) /* = NULL */,
	p_actionID        nvarchar(100) /* = NULL */,
	p_description     nvarchar(400) /* = NULL */,
	p_actionType      int           /* = NULL */,
	p_actionData      longblob         /* = NULL */)
BEGIN
	if(exists(select * from lsGlobalMessageRuleActions where (RuleID = p_ruleID AND ActionID = p_actionID)))
    then
		update lsGlobalMessageRuleActions set
			RuleID      = p_ruleID,
			ActionID    = p_actionID,
			Description = p_description,
			ActionType  = p_actionType,
			ActionData  = p_actionData
		where  (RuleID = p_ruleID AND ActionID = p_actionID);

		select null as ErrorText;
    else
		select CONCAT('Action with specified ID "' , p_actionID , '" doesn''t exist !') as ErrorText;
	end if;
END;


//

DELIMITER ;




/*  Implementation notes:
      Decsription:
	    Updates user group.
	  Returns:
		If successful returns nothing, otherwise returns 1 row with error text in column 'ErrorText'.

	  Implementation:
		*) Ensure that group with specified ID does exist. Return error text.
        *) If group name is changed, ensure that new group name won't conflict 
           any other group or user name. Return error text.                    
        *) Udpate group.
		 
*/

DELIMITER //

CREATE PROCEDURE lspr_UpdateGroup (
	p_groupID     nvarchar(100) /* = NULL */,
	p_groupName   nvarchar(100) /* = NULL */,
	p_description nvarchar(400) /* = NULL */,
	p_enabled     tinyint           /* = NULL */)
sp_lbl:

BEGIN
	declare v_currentGroupName nvarchar(100);

	-- Ensure that group with specified ID does exist.
	if(not exists(select * from lsGroups where (GroupID = p_groupID)))
	then
		select CONCAT('Invalid group ID, specified group ID ''' , p_groupID , ''' already exists !') as ErrorText;
		leave sp_lbl;
	end if;

	-- If group name is changed, ensure that new group name won't conflict 
	-- any other group or user name. Throw Exception if does.

	set v_currentGroupName = (select GroupName from lsGroups where (GroupID = p_groupID));
	if(v_currentGroupName != p_groupName) 
	then		
		-- Ensure that group name won't exist already.
		if(exists(select * from lsGroups where (GroupName = p_groupName)))
		then
			select CONCAT('Invalid group name, specified group ''' , p_groupName , ''' already exists !') as ErrorText;
			leave sp_lbl;
		-- Ensure that user name with groupName doen't exist.
		elseif exists(select * from lsUsers where (UserName = p_groupName))
		then
			select CONCAT('Invalid group name, user with specified name ''' , p_groupName , ''' already exists !') as ErrorText;
			leave sp_lbl;
		end if;
	end if;

	-- Insert group
	update lsGroups set
		GroupID     = p_groupID,
		GroupName   = p_groupName,
		Description = p_description,
		Enabled     = p_enabled	
	where (GroupID = p_groupID);
		
	select null as ErrorText;
END;


//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_UpdateMailingList (
	p_MailingListID	 varchar(100) /* = NULL */,
	p_MailingListName varchar(100) /* = NULL */,
	p_Description     varchar(100) /* = NULL */,
	p_DomainName      varchar(100) /* = NULL */,
	p_enabled         tinyint          /* = false */)
sp_lbl:
BEGIN

		declare v_MailingListOwnerID nvarchar(100);
        
if(exists(select * from lsMailingLists where (MailingListID=p_MailingListID)))
then
	-- If changeing mailing list name, ensure that anyone already haven't got it
	if(exists(select * from lsMailingLists where (MailingListName=p_MailingListName)))
	then

		set v_MailingListOwnerID = (select MailingListID from lsMailingLists where MailingListName=p_MailingListName);
		if(v_MailingListOwnerID != p_MailingListID)
		then
			select CONCAT('Mailing list with name "' , p_MailingListName , '" already exists !') as ErrorText;
			leave sp_lbl;
		end if;
	end if;

	update lsMailingLists set 
		MailingListName = p_MailingListName,
		Description     = p_Description,
		DomainName      = p_DomainName,
		Enabled         = p_enabled
	where  (MailingListID=p_MailingListID);

	select null as ErrorText;
else
	select CONCAT('Mailing list with specified ID "' , p_MailingListID , '" doesn''t exist !') as ErrorText;
end if;



END;
//

DELIMITER ;




/* Updates recycle bin settings.
    @deleteToRecycleBin  - Specifies if messages are deleted to recycle bin. 
    @deleteMessagesAfter - Specifies after what days messages will be deleted.
*/
DELIMITER //

CREATE PROCEDURE lspr_UpdateRecycleBinSettings (
	p_deleteToRecycleBin  tinyint /* = 0 */,
	p_deleteMessagesAfter int /* = 1 */)
BEGIN
    IF(exists(select * from lsRecycleBinSettings))
    THEN
	    update lsRecycleBinSettings set
            DeleteToRecycleBin  = p_deleteToRecycleBin,
            DeleteMessagesAfter = p_deleteMessagesAfter;
    ELSE
        insert into lsRecycleBinSettings (DeleteToRecycleBin,DeleteMessagesAfter)
            values (p_deleteToRecycleBin,p_deleteMessagesAfter);
    END IF;
END;


//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_UpdateRoute (
	p_routeID     varchar(100) /* = NULL */,
	p_cost        bigint       /* = NULL */,
	p_enabled     tinyint          /* = NULL */,
	p_description varchar(100) /* = NULL */,
	p_pattern     varchar(100) /* = NULL */,
	p_action      int          /* = NULL */,
	p_actionData  longblob        /* = NULL */)
sp_lbl:
BEGIN

		declare v_RouteOwnerID nvarchar(100);
        
if(exists(select * from lsRouting where (RouteID=p_routeID)))
then
	-- If changeing route pattern, ensure that it  doesn't exist already
	if(exists(select * from lsRouting where (Pattern=p_pattern)))
	then

		set v_RouteOwnerID = (select RouteID from lsRouting where Pattern=p_pattern);
		if(v_RouteOwnerID != p_routeID)
		then
			select CONCAT('Route with pattern "' , p_pattern , '" already exists !') as ErrorText;
			leave sp_lbl;
		end if;
	end if;

	update lsRouting set 
		Cost        = p_cost,
		Enabled     = p_enabled,
		Description = p_description,
		Pattern     = p_pattern,
		Action      = p_action,
		ActionData  = p_actionData
	where  (RouteID=p_routeID);

	select null as ErrorText;
else
	select CONCAT('Route with specified ID "' , p_routeID , '" doesn''t exist !') as ErrorText;
end if;



END;
//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_UpdateSecurityEntry (
	p_id          varchar(100) /* = NULL */,
	p_enabled     tinyint          /* = 1 */,
	p_description varchar(100) /* = NULL */,
	p_service     varchar(100) /* = NULL */,
	p_action      varchar(100) /* = NULL */,
	p_startIP     varchar(100) /* = 0 */,
	p_endIP       varchar(100) /* = 0 */)
BEGIN

if(exists(select * from lsIPSecurity where (ID=p_id)))
then
	update lsIPSecurity set 
		Enabled     = p_enabled,
		Description = p_description,
		Service     = p_service,
		Action      = p_action,
		StartIP     = p_startIP,
		EndIP       = p_endIP
	where (ID=p_id);

	select null as ErrorText;
else
	select CONCAT('Security entry with specified ID "' , p_id , '" doesn''t exist !') as ErrorText;
end if;



END;
//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_UpdateSettings (
	p_Settings longblob /* = NULL */)
BEGIN

if(exists(select * from lsSettings))
then
	update lsSettings set 
		Settings = p_Settings;
else
	insert into lsSettings (Settings) values (p_Settings);
end if;



END;
//

DELIMITER ;




/*  Implementation notes:
      Decsription:
	    Updates shared folder root.
	  Returns:
		If successful returns nothing, otherwise returns 1 row with error text in column 'ErrorText'.

	  Implementation:
		*) Ensure that root with specified ID does exist. Return error text.
        *) If root name is changed, ensure that new root name won't conflict 
           any other root name. Return error text.                    
        *) Udpate root folder.
		 
*/

DELIMITER //

CREATE PROCEDURE lspr_UpdateSharedFolderRoot (
	p_rootID        nvarchar(100) /* = NULL */,
	p_enabled       tinyint           /* = NULL */,
	p_folder        nvarchar(400) /* = NULL */,
	p_description   nvarchar(400) /* = NULL */,
	p_rootType      int           /* = NULL */,
	p_boundedUser   nvarchar(100) /* = NULL */,
	p_boundedFolder nvarchar(400) /* = NULL */)
sp_lbl:

BEGIN

	declare v_currentRootName nvarchar(100);
	-- Ensure that root with specified ID does exist.
	if(not exists(select * from lsSharedFoldersRoots where (RootID = p_rootID)))
	then
		select CONCAT('Invalid root ID, specified root ID ''' , p_rootID , ''' already exists !') as ErrorText;
		leave sp_lbl;
	end if;

	-- If root name is changed, ensure that new root name won't conflict 
    -- any other root name. Throw Exception if does.
	set v_currentRootName = (select Folder from lsSharedFoldersRoots where (RootID = p_rootID));
	if(v_currentRootName != p_folder) 
	then		
		-- Ensure that root name won't exist already.
		if(exists(select * from lsSharedFoldersRoots where (Folder = p_folder)))
		then
			select CONCAT('Invalid root name, specified root ''' , p_folder , ''' already exists !') as ErrorText;
			leave sp_lbl;
		end if;
	end if;

	-- Insert group
	update lsSharedFoldersRoots set
		Enabled       = p_enabled,
		Folder        = p_folder,
		Description   = p_description,
		RootType      = p_rootType,
		BoundedUser   = p_boundedUser,
		BoundedFolder = p_boundedFolder
	where (RootID = p_rootID);
		
	select null as ErrorText;
END;


//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_UpdateUser (
	p_UserID	     varchar(100) /* = NULL */,
	p_FullName    varchar(100) /* = NULL */,
	p_UserName    varchar(100) /* = NULL */,
	p_Password    varchar(100) /* = NULL */,
	p_Description varchar(100) /* = NULL */,
	p_DomainName  varchar(100) /* = NULL */,
	p_MailboxSize bigint	      /* = 0 */,
	p_Enabled     tinyint          /* = true */,
	p_permissions int          /* = 255 */)
sp_lbl:
BEGIN

declare v_UserNameOwnerID nvarchar(100);
        
if(exists(select * from lsUsers where (UserID=p_UserID)))
then
	-- If changeing username, ensure that anyone already haven't got it
	if(exists(select * from lsUsers where (UserName = p_UserName)))
	then

		set v_UserNameOwnerID = (select UserID from lsUsers where UserName=p_UserName);
		if(v_UserNameOwnerID != p_UserID)
		then
			select CONCAT('User with user name "' , p_UserName , '" already exists !') as ErrorText;
			leave sp_lbl;
		end if;
	end if;

	update lsUsers set 
		FullName      = p_FullName,
		UserName      = p_UserName,
		Password      = p_Password,
		Description   = p_Description,
		Mailbox_Size  = p_MailboxSize,
		DomainName    = p_DomainName,
		Enabled       = p_Enabled,
		`Permissions` = p_permissions
	where  (UserID=p_UserID);

	select null as ErrorText;
else
	select CONCAT('User with specified ID "' , p_UserID , '" doesn''t exist !') as ErrorText;
end if;



END;
//

DELIMITER ;




/* Updates user last login time.
    @userName - User name whos last login time to update.
*/
DELIMITER //

CREATE PROCEDURE lspr_UpdateUserLastLoginTime (
    p_userName nvarchar(100) /* = NULL */)
BEGIN
    update lsUsers set
        LastLoginTime = now()
    where (UserName = p_userName);
END;


//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_UpdateUserMessageRule (
	p_userID          nvarchar(100) /* = NULL */,
	p_ruleID          nvarchar(100) /* = NULL */,
	p_cost            bigint        /* = NULL */,
	p_enabled         tinyint           /* = NULL */,
	p_checkNextRule   int           /* = NULL */,
	p_description     nvarchar(400) /* = NULL */,
	p_matchExpression longblob         /* = NULL */)
BEGIN
	if(exists(select * from lsUserMessageRules where (UserID = p_userID AND RuleID = p_ruleID)))
    then
		update lsUserMessageRules set
			UserID          = p_userID,
			RuleID          = p_ruleID,
			Cost            = p_cost,
			Enabled         = p_enabled,
			CheckNextRuleIf = p_checkNextRule,
			Description     = p_description,
			MatchExpression = p_matchExpression
		where  (UserID = p_userID AND RuleID = p_ruleID);

		select null as ErrorText;
    else
		select CONCAT('Rule with specified ID "' , p_ruleID , '" doesn''t exist !') as ErrorText;
	end if;
END;


//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_UpdateUserMessageRuleAction (
	p_userID          nvarchar(100) /* = NULL */,
	p_ruleID          nvarchar(100) /* = NULL */,
	p_actionID        nvarchar(100) /* = NULL */,
	p_description     nvarchar(400) /* = NULL */,
	p_actionType      int           /* = NULL */,
	p_actionData      longblob         /* = NULL */)
BEGIN
	if(exists(select * from lsUserMessageRuleActions where (UserID = p_userID AND RuleID = p_ruleID AND ActionID = p_actionID)))
    then
		update lsUserMessageRuleActions set
			UserID      = p_userID,
			RuleID      = p_ruleID,
			ActionID    = p_actionID,
			Description = p_description,
			ActionType  = p_actionType,
			ActionData  = p_actionData
		where (UserID = p_userID AND RuleID = p_ruleID AND ActionID = p_actionID);

		select null as ErrorText;
    else
		select CONCAT('Action with specified ID "' , p_actionID , '" doesn''t exist !') as ErrorText;
	end if;
END;


//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_UpdateUserRemoteServer ( 
	p_ServerID       nvarchar(100) /* = NULL */,
	p_UserName       nvarchar(100) /* = NULL */,
	p_Description    nvarchar(100) /* = NULL */,
	p_RemoteServer   nvarchar(100) /* = NULL */,
	p_RemotePort     int           /* = NULL */,
	p_RemoteUserName nvarchar(100) /* = NULL */,
	p_RemotePassword nvarchar(100) /* = NULL */,
	p_UseSSL         tinyint           /* = NULL */,
	p_Enabled        tinyint           /* = NULL */)
BEGIN

	declare v_UserID nvarchar(100);
if(exists(select * from lsUserRemoteServers where (ServerID = p_ServerID)))
then
	-- Get userID
	set v_UserID = (select UserID from lsUsers where UserName=p_UserName);

	update lsUserRemoteServers set
		ServerID       = p_ServerID,
		UserID         = v_UserID,
		Description    = p_Description,
		RemoteServer   = p_RemoteServer,
		RemotePort     = p_RemotePort,
		RemoteUserName = p_RemoteUserName,
		RemotePassword = p_RemotePassword,
		UseSSL         = p_UseSSL,
		Enabled        = p_Enabled  
	where (ServerID = p_ServerID);

	select null as ErrorText;
else
	select CONCAT('User remote server with specified ID "' , p_ServerID , '" doesn''t exist !') as ErrorText;
end if;



END;
//

DELIMITER ;




DELIMITER //

CREATE PROCEDURE lspr_ValidateMailboxSize (
	p_UserName nvarchar(100) /* = NULL */)
sp_lbl:

BEGIN

    declare v_Size bigint ; declare v_AllowedSize bigint;
    set  v_Size = 0;
    set  v_AllowedSize = -1;

    -- Get mailbox size
    if(exists(select Mailbox_Size from lsUsers where UserName=p_UserName))
    then
        set v_AllowedSize = (select Mailbox_Size from lsUsers where UserName=p_UserName);
    end if;
	-- Unlimited mailbox size, don't calculate mailbox size.
    if(v_AllowedSize < 1)
	THEN
        select 1 as Validated;
        leave sp_lbl; 
	END IF;


    -- Count mailbox size
    if(exists(select MailBox from lsMailStore where Mailbox=p_UserName))
    then
        set v_Size = (select sum(Size) from lsMailStore where Mailbox=p_UserName);
    end if;


    if(v_Size < v_AllowedSize*1000000) then  -- Allowed size in mb, size is bytes
        select 1 as Validated;
    -- end if;
    else
      select 0  as Validated;
    end if;
END;
//

DELIMITER ;


DELIMITER //

CREATE PROCEDURE lspr_GetMessageList (

p_Mailbox	nvarchar(100)	/* =NULL */,
p_Folder	nvarchar(100)	/* =NULL */)
BEGIN

select MessageID,Size,Date,MessageFlags,UID  from lsMailStore where MAILBOX = p_Mailbox AND Folder = p_Folder;


END;
//

DELIMITER ;
