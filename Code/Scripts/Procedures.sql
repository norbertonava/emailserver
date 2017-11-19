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
		email_password
FROM	configuration
WHERE 	id_configuration = 1;

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
										p_email_password varchar(45))
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
	email_password)
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
	p_email_password);
END;
//

DELIMITER //

CREATE PROCEDURE SaveMessage(	p_phone_number varchar(55),
								p_title varchar(1000),
								p_body longtext,
								p_sender_mail varchar(500),
								p_date_sent datetime)
BEGIN

	DECLARE p_id_message INT(11);
	
    SET p_id_message = (select max(id_message) from message);
    
    if(isnull(p_id_message) = 1)
    then
		SET p_id_message = 1;
    end if;
    
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

    
END;
//