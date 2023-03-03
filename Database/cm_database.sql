CREATE TABLE Employee(
	employeeID varchar (20),
	employeeName nvarchar(100),
	gender nvarchar(4),
	birthDay date,
	ssn varchar(12),
	phoneNumber varchar (10),
	managerID varchar (20),
	salary int,
	addressEmp nvarchar(255)
);

CREATE TABLE StatusWork(
	statusID varchar (20),
	statusName nvarchar(100)
);

CREATE TABLE StatusEmployee(
	employeeID varchar(20),
	statusID varchar(20)
);

CREATE TABLE Account(
	username varchar(100),
	pass_word varchar(100),
	employeeID varchar (20)
);

CREATE TABLE Department(
	departmentID varchar(20),
	departmentName nvarchar(100),
	managerID varchar(20)
);

CREATE TABLE Project(
	projectID varchar(20),
	projectName nvarchar(225),
	dateStart date,
	dateEnd date,
	budget int,
	statusProject varchar(50)
);

CREATE TABLE Task(
	taskID varchar(20),
	taskName nvarchar(20),
	dateStart date,
	dateEnd date,
	projectId varchar(20)
);

CREATE TABLE LeaveApplication(
	leaveapp_id varchar(20),
	employee_id varchar (20),
	date_time datetime,
	time_off datetime,
	reason nvarchar (255),
	leaveapp_status varchar (50)
);

