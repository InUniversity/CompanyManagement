CREATE DATABASE CompanyManagement
GO
USE CompanyManagement
GO

CREATE TABLE Employee(
	employee_id varchar (20),
	employee_name nvarchar(100),
	gender nvarchar(10),
	birthday varchar(10),
	identify_card varchar(12),
	email varchar(50),
	phone_number varchar (10),
	employee_address nvarchar(255),
	manager_id varchar(20),
	position_id varchar(20),
	salary int
);

CREATE TABLE Position(
	position_id varchar(20),
	position_name nvarchar(50)
);

CREATE TABLE StatusEmployee(
	employee_id varchar(20),
	status_id varchar(20)
);

CREATE TABLE Account(
	account_username varchar(100),
	account_password varchar(100),
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
	create_time varchar(10),
	end_time varchar(10),
	progress varchar(30)
);

CREATE TABLE ProjectAssignment(
	project_id varchar(20),
	role_name varchar(20),
	employee_id varchar(20)
);

CREATE TABLE Task(
	task_id varchar(20),
	title nvarchar(50),
	task_description nvarchar(255),
	assign_date varchar(20),
	deadline varchar(10),
	create_by varchar(20),
	progress varchar(30),
	employee_id varchar(20),
	project_id varchar(20)
);

Insert into Account (account_username, account_password, employee_id)
Values ('abcd', '123456', '21110171')

Select * From Account;