﻿CREATE TABLE Employee(
	employee_id varchar (20),
	employee_name nvarchar(100),
	gender nvarchar(4),
	birth_day date,
	identify_card varchar(12),
	phone_number varchar (10),
	manager_id varchar (20),
	salary int,
	employee_address nvarchar(255)
);

CREATE TABLE StatusWork(
	status_id varchar (20),
	status_name nvarchar(100)
);

CREATE TABLE StatusEmployee(
	employee_id varchar(20),
	status_id varchar(20)
);

CREATE TABLE Account(
	username_ varchar(100),
	password_ varchar(100),
	employee_id varchar (20)
);

CREATE TABLE Department(
	department_id varchar(20),
	department_name nvarchar(100),
	manager_id varchar(20)
);

CREATE TABLE Project(
	project_id varchar(20),
	project_name nvarchar(225),
	date_start date,
	date_end date,
	budget int,
	project_status varchar(50)
);

CREATE TABLE Task(
	task_id varchar(20),
	task_name nvarchar(20),
	date_start date,
	date_end date,
	project_id varchar(20)
);

CREATE TABLE LeaveApplication(
	leave_application_id varchar(20),
	employee_id varchar (20),
	date_time datetime,
	time_off datetime,
	reason nvarchar (255),
	leave_appication_status varchar (50)
);

