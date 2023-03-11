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
	status_work_id  varchar(20)
);

CREATE TABLE StatusWork(
	status_work_id varchar(20),
	status_work_name varchar(50)
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

INSERT INTO Position(position_id, position_name)
VALUES ('1', 'Trưởng Phòng')

INSERT INTO Position(position_id, position_name)
VALUES ('2', 'Nhân Viên')
---------------------------------------------------------------------------------------------------------------------------------------------------------

INSERT INTO Employee(employee_id, employee_name, gender, birthday, identify_card, email, phone_number, employee_address, manager_id, position_id, salary)
VALUES ('EM001','Nguyễn Văn An', 'Nam', '15-10-2000', '98654234568', 'nguyenvanan1510@gmail.com', '0976458234', 'Tiền Giang', 'CD001','2',15000)

INSERT INTO Employee(employee_id, employee_name, gender, birthday, identify_card, email, phone_number, employee_address, manager_id, position_id, salary)
VALUES ('EM002','Nguyễn Thúy An', 'Nữ', '19-03-1998', '98654234758', 'nguyenthuyan1903@gmail.com', '0987965423', 'Hậu Giang', 'CD002','2',15000)

INSERT INTO Employee(employee_id, employee_name, gender, birthday, identify_card, email, phone_number, employee_address, manager_id, position_id, salary)
VALUES ('EM003','Lê Hoàng Bảo Phát', 'Nam', '07-11-1990', '9867584568', 'lehoangbaophat0711@gmail.com', '0786458234', 'Bến Tre', 'CD001','2',15000)

INSERT INTO Employee(employee_id, employee_name, gender, birthday, identify_card, email, phone_number, employee_address, manager_id, position_id, salary)
VALUES ('EM004','Trần Hoàng Lan', 'Nữ', '19-09-1998', '9789834568', 'tranhoanglan1909@gmail.com', '0879458234', 'Tp. HCM', 'CD002','2',15000)

INSERT INTO Employee(employee_id, employee_name, gender, birthday, identify_card, email, phone_number, employee_address, manager_id, position_id, salary)
VALUES ('EM005','Hà Giang', 'Nữ', '25-12-2002', '9789812345', 'hagiang2512@gmail.com', '094567312', 'Kiên Giang', 'CD001','2',15000)

INSERT INTO Employee(employee_id, employee_name, gender, birthday, identify_card, email, phone_number, employee_address, manager_id, position_id, salary)
VALUES ('EM006','Phan Hoàng Giang', 'Nam', '05-04-1995', '9789812345', 'phanhoanggiang0504@gmail.com', '0765489123', 'Đồng Nai', 'CD001','2',15000)

INSERT INTO Employee(employee_id, employee_name, gender, birthday, identify_card, email, phone_number, employee_address, manager_id, position_id, salary)
VALUES ('CD001','Trần Lâm Phát Linh', 'Nam', '18-08-1996', '9789834123', 'tranlamphatlinh1808@gmail.com', '0879458999', 'Tp. HCM', null ,'1',15000)

INSERT INTO Employee(employee_id, employee_name, gender, birthday, identify_card, email, phone_number, employee_address, manager_id, position_id, salary)
VALUES ('CD002','Lê Trần Thúy Lan', 'Nữ', '11-11-1993', '9789123123', 'letranthuylan1111@gmail.com', '0879777777', 'Tp. HCM', null ,'1',15000)
---------------------------------------------------------------------------------------------------------------------------------------------------------

INSERT INTO Department(department_id, department_name, manager_id)
VALUES ('DPM001', 'IT', 'CD001')

INSERT INTO Department(department_id, department_name, manager_id)
VALUES ('DPM002', 'Kế Toán', 'CD002')
---------------------------------------------------------------------------------------------------------------------------------------------------------

INSERT INTO StatusWork(status_work_id, status_work_name)
VALUES ('1', 'Đang làm việc')

INSERT INTO StatusWork(status_work_id, status_work_name)
VALUES ('2', 'Nghỉ có phép')

INSERT INTO StatusWork(status_work_id, status_work_name)
VALUES ('3', 'Nghỉ không phép')

INSERT INTO StatusWork(status_work_id, status_work_name)
VALUES ('4', 'Nghỉ theo biên chế')

INSERT INTO StatusWork(status_work_id, status_work_name)
VALUES ('5', 'Đang công tác')
---------------------------------------------------------------------------------------------------------------------------------------------------------

INSERT INTO StatusEmployee(employee_id, status_work_id)
VALUES('EM001','1')

INSERT INTO StatusEmployee(employee_id, status_work_id)
VALUES('EM002','1')

INSERT INTO StatusEmployee(employee_id, status_work_id)
VALUES('EM003','1')

INSERT INTO StatusEmployee(employee_id, status_work_id)
VALUES('EM004','1')

INSERT INTO StatusEmployee(employee_id, status_work_id)
VALUES('EM005','1')

INSERT INTO StatusEmployee(employee_id, status_work_id)
VALUES('EM006','1')

INSERT INTO StatusEmployee(employee_id, status_work_id)
VALUES('CD001','1')

INSERT INTO StatusEmployee(employee_id, status_work_id)
VALUES('CD002','1')
---------------------------------------------------------------------------------------------------------------------------------------------------------

INSERT INTO Account(account_username, account_password, employee_id)
VALUES('nguyenvanan1510@gmail.com','@123456', 'EM001')

INSERT INTO Account(account_username, account_password, employee_id)
VALUES('nguyenthuyan1903@gmail.com','@123456', 'EM002')

INSERT INTO Account(account_username, account_password, employee_id)
VALUES('lehoangbaophat0711@gmail.com','@123456', 'EM003')

INSERT INTO Account(account_username, account_password, employee_id)
VALUES('tranhoanglan1909@gmail.com','@123456', 'EM004')

INSERT INTO Account(account_username, account_password, employee_id)
VALUES('hagiang2512@gmail.com','@123456', 'EM005')

INSERT INTO Account(account_username, account_password, employee_id)
VALUES('phanhoanggiang0504@gmail.com','@123456', 'EM006')

INSERT INTO Account(account_username, account_password, employee_id)
VALUES('tranlamphatlinh1808@gmail.com','@123456', 'CD001')

INSERT INTO Account(account_username, account_password, employee_id)
VALUES('letranthuylan1111@gmail.com','@123456', 'CD002')