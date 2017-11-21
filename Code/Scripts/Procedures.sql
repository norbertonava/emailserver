DELIMITER //

CREATE PROCEDURE GetConfiguration()
BEGIN

SELECT 	fetch_seconds, 
		email,
		smtp_url,
		smtp_port, 
		smtp_usessl, 
		pop3_url, 
		pop3_port, 
		pop3_usessl,
		email_password,
        display_name,
        bad_response_mail_subject,
        bad_response_mail_body
FROM	configuration
WHERE 	id_configuration = 1;

END;
//

DELIMITER //

CREATE PROCEDURE GetUsers()
BEGIN

SELECT 	phone_number,
        date_modified
FROM	user;

END;
//

DELIMITER //

CREATE PROCEDURE SaveConfiguration(		p_fetch_seconds int(11),
										p_email varchar(500),
										p_smtp_url varchar(500),
										p_smtp_port int(11),
										p_smtp_usessl varchar(45),
										p_pop3_url varchar(500),
										p_pop3_port int(11),
										p_pop3_usessl int(11),
										p_email_password varchar(45),
                                        p_display_name varchar(256),
                                        p_bad_response_mail_subject varchar(256),
										p_bad_response_mail_body longtext)
BEGIN
	
    DELETE FROM configuration
    WHERE id_configuration = 1;

	INSERT INTO configuration
	(id_configuration,
	fetch_seconds,
	email,
	smtp_url,
	smtp_port,
	smtp_usessl,
	pop3_url,
	pop3_port,
	pop3_usessl,
	email_password,
    display_name,
	bad_response_mail_subject,
	bad_response_mail_body    
    )
	VALUES
	(1,
	p_fetch_seconds,
	p_email,
	p_smtp_url,
	p_smtp_port,
	p_smtp_usessl,
	p_pop3_url,
	p_pop3_port,
	p_pop3_usessl,
	p_email_password,
    p_display_name,
    p_bad_response_mail_subject,
	p_bad_response_mail_body);
END;
//

DELIMITER //

CREATE PROCEDURE SaveMessage(	p_phone_number varchar(55),
								p_title varchar(1000),
								p_body longtext,
								p_sender_mail varchar(500),
								p_date_sent datetime)
BEGIN

	DECLARE p_id_message BIGINT;
	
    SET p_id_message = (select max(id_message) from message);
    
    if(isnull(p_id_message) = 1)
    then
		SET p_id_message = 0;
    end if;
    
    SET p_id_message = p_id_message  + 1;
    
	INSERT INTO message
	(id_message,
	phone_number,
	title,
	body,
	sender_mail,
	date_fetched,
	date_sent)
	VALUES
	(p_id_message,
	p_phone_number,
	p_title,
	p_body,
	p_sender_mail,
	now(),
	p_date_sent);

    SELECT p_id_message;
END;
//


DELIMITER //

CREATE PROCEDURE IsPhoneNumberValid(	p_phone_number varchar(55))
BEGIN

	SELECT COUNT(1) As Exist
    FROM user
    WHERE phone_number = p_phone_number;
    
END;
//

DELIMITER //

CREATE PROCEDURE SaveAttachment(p_id_message int(11),
								  p_data 	longblob,
								  p_file_name varchar(500))
BEGIN

	DECLARE p_id_attachment BIGINT;
	
    SET p_id_attachment = (select max(id_attachment) from attachment);
    
    if(isnull(p_id_attachment) = 1)
    then
		SET p_id_attachment = 0;
    end if;
    
    SET p_id_attachment = p_id_attachment + 1;
    SELECT p_id_attachment;
	INSERT INTO attachment	(id_attachment,
							id_message,
							data,
							file_name)
	VALUES	(p_id_attachment,
			p_id_message,
			p_data,
			p_file_name);

    
END;
//

DELIMITER //

CREATE PROCEDURE SaveUser(p_phone_number varchar(50))
BEGIN

	INSERT INTO user
	(phone_number,
	date_modified)
	VALUES
	(p_phone_number,
	now());		

END;
//

DELIMITER //

CREATE PROCEDURE UserExists(p_phone_number varchar(50))
BEGIN

	DECLARE p_id INT(11);
	
    SET p_id = (select 1 from user where phone_number = p_phone_number);
    
    if(isnull(p_id) = 1)
    then
		select false as exist;
	else
		select true as exist;
    end if;

END;
//

DELIMITER //

CREATE PROCEDURE SaveLogEntry(  p_action varchar(200),
								p_data longtext)
BEGIN

INSERT INTO log (action,
				data,
				date)
				VALUES
				(p_action,
				p_data,
				now());

END;
//

DELIMITER //

CREATE PROCEDURE GetMaxLogId()
BEGIN

	DECLARE p_id_log BIGINT;
	
    SET p_id_log = (SELECT MAX(id_log) FROM log);
    
    if(isnull(p_id_log) = 1)
    then
		set p_id_log = 1;
    end if;	
    
    select p_id_log as id_log;
END;
//

DELIMITER //

CREATE PROCEDURE GetLogData(p_id_log BIGINT)
BEGIN

	SELECT id_log, action, data, date
    FROM log
    WHERE id_log > p_id_log;

END;
//

DELIMITER //

CREATE PROCEDURE GetSafeList(p_sender_mail	varchar(500))
BEGIN

	if(p_sender_mail= '')
    then
		SELECT phone_number,
			sender_mail,
			token,
			active,
			active_date
		FROM safe_list;
	else
		SELECT phone_number,
			sender_mail,
			token,
			active,
			active_date
		FROM safe_list
        where sender_mail = p_sender_mail;    
    end if;
    
END;
//

DELIMITER //

CREATE PROCEDURE IsAlreadyInSafeList(p_sender_mail	varchar(500))
BEGIN

    select phone_number AS p_phone_number
    from safe_list 
    where sender_mail = p_sender_mail
    AND active;
    
END;
//

DELIMITER //

CREATE PROCEDURE ActivateSafeList(p_sender_mail	varchar(500),
								p_phone_number varchar(50))
BEGIN
	UPDATE safe_list
		SET	active = true,
			active_date = now()
	WHERE phone_number = p_phone_number 
	AND sender_mail = p_sender_mail;
END;
//

DELIMITER //

CREATE PROCEDURE SaveSafeList( 	p_phone_number varchar(55),
								p_sender_mail	varchar(500),
								p_token		varchar(10))
BEGIN

	DECLARE p_id INT(11);
	
    SET p_id = (select 1 from safe_list where phone_number = p_phone_number and sender_mail = p_sender_mail);
    
    if(isnull(p_id) = 1)
    then
		INSERT INTO safe_list
		(phone_number,
		sender_mail,
		token,
		active,
		active_date)
		VALUES
		(p_phone_number,
		p_sender_mail,
		p_token,
		false,
		null);
	else
		UPDATE safe_list
			SET	token = p_token,
				active = false,
				active_date = null
		WHERE phone_number = p_phone_number 
        AND sender_mail = p_sender_mail;
    end if;

END;
//