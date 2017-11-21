CREATE TABLE attachment (
  id_attachment bigint(20) NOT NULL,
  id_message bigint(20) NOT NULL,
  data longblob NOT NULL,
  file_name varchar(500) NOT NULL,
  PRIMARY KEY (id_attachment)
);

CREATE TABLE configuration (
  id_configuration int(11) NOT NULL,
  fetch_seconds int(11) NOT NULL,
  email varchar(500) NOT NULL,
  smtp_url varchar(500) NOT NULL,
  smtp_port int(11) NOT NULL,
  smtp_usessl varchar(45) NOT NULL,
  pop3_url varchar(500) NOT NULL,
  pop3_port int(11) NOT NULL,
  pop3_usessl int(11) NOT NULL,
  email_password varchar(45) NOT NULL,
  display_name varchar(300) NOT NULL,
  bad_response_mail_subject varchar(256) NOT NULL,
  bad_response_mail_body longtext NOT NULL,
  PRIMARY KEY (id_configuration)
);

CREATE TABLE log (
  id_log bigint(20) NOT NULL AUTO_INCREMENT,
  action varchar(200) NOT NULL,
  data longtext NOT NULL,
  date datetime NOT NULL,
  PRIMARY KEY (id_log)
);

CREATE TABLE message (
  id_message bigint(20) NOT NULL,
  phone_number varchar(55) NOT NULL,
  title varchar(1000) NOT NULL,
  body longtext NOT NULL,
  sender_mail varchar(500) NOT NULL,
  date_fetched datetime NOT NULL,
  date_sent datetime NOT NULL,
  PRIMARY KEY (id_message)
);

CREATE TABLE safe_list (
  phone_number varchar(55) NOT NULL,
  sender_mail varchar(500) NOT NULL,
  token varchar(10) NOT NULL,
  active tinyint(1) NOT NULL,
  active_date datetime DEFAULT NULL,
  PRIMARY KEY (phone_number,sender_mail)
);

CREATE TABLE user (
  phone_number varchar(50) NOT NULL,
  date_modified datetime NOT NULL,
  PRIMARY KEY (phone_number)
);
