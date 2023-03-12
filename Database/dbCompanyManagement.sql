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
	department_id varchar(20),
	position_id varchar(20),
	salary int
);

CREATE TABLE Position(
	position_id varchar(20),
	position_name nvarchar(50)
);

CREATE TABLE StatusEmployee(
	employee_id varchar(20),
	status_work_id varchar(20)
);

CREATE TABLE StatusWork(
	status_work_id varchar(20),
	status_work_name nvarchar(50)
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
	role_name nvarchar(30),
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
VALUES ('1', N'Trưởng Phòng')

INSERT INTO Position(position_id, position_name)
VALUES ('2', N'Nhân Viên')
---------------------------------------------------------------------------------------------------------------------------------------------------------

INSERT INTO Employee(employee_id, employee_name, gender, birthday, identify_card, email, phone_number, employee_address, department_id, position_id, salary)
VALUES ('EM001', N'Nguyễn Văn An', N'Nam', '15-10-2000', '98654234568', 'nguyenvanan1510@gmail.com', '0976458234', N'Tiền Giang', 'DPM001','2',15000)

INSERT INTO Employee(employee_id, employee_name, gender, birthday, identify_card, email, phone_number, employee_address, manager_id, position_id, salary)
VALUES ('EM002', N'Nguyễn Thúy An', N'Nữ', '19-03-1998', '98654234758', 'nguyenthuyan1903@gmail.com', '0987965423', N'Hậu Giang', 'DPM002','2',15000)

INSERT INTO Employee(employee_id, employee_name, gender, birthday, identify_card, email, phone_number, employee_address, manager_id, position_id, salary)
VALUES ('EM003', N'Lê Hoàng Bảo Phát', N'Nam', '07-11-1990', '9867584568', 'lehoangbaophat0711@gmail.com', '0786458234', N'Bến Tre', 'DPM001','2',15000)

INSERT INTO Employee(employee_id, employee_name, gender, birthday, identify_card, email, phone_number, employee_address, manager_id, position_id, salary)
VALUES ('EM004', N'Trần Hoàng Lan', N'Nữ', '19-09-1998', '9789834568', 'tranhoanglan1909@gmail.com', '0879458234', N'Tp. HCM', 'DPM002','2',15000)

INSERT INTO Employee(employee_id, employee_name, gender, birthday, identify_card, email, phone_number, employee_address, manager_id, position_id, salary)
VALUES ('EM005', N'Hà Giang', N'Nữ', '25-12-2002', '9789812345', 'hagiang2512@gmail.com', '094567312', N'Kiên Giang', 'DPM001','2',15000)

INSERT INTO Employee(employee_id, employee_name, gender, birthday, identify_card, email, phone_number, employee_address, manager_id, position_id, salary)
VALUES ('EM006', N'Phan Hoàng Giang', N'Nam', '05-04-1995', '9789812345', 'phanhoanggiang0504@gmail.com', '0765489123', N'Đồng Nai', 'DPM001','2',15000)

INSERT INTO Employee(employee_id, employee_name, gender, birthday, identify_card, email, phone_number, employee_address, manager_id, position_id, salary)
VALUES ('CD001', N'Trần Lâm Phát Linh', N'Nam', '18-08-1996', '9789834123', 'tranlamphatlinh1808@gmail.com', '0879458999', N'Tp. HCM', 'DPM001','1',15000)

INSERT INTO Employee(employee_id, employee_name, gender, birthday, identify_card, email, phone_number, employee_address, manager_id, position_id, salary)
VALUES ('CD002', N'Lê Trần Thúy Lan', N'Nữ', '11-11-1993', '9789123123', 'letranthuylan1111@gmail.com', '0879777777', N'Tp. HCM', 'DPM001','1',15000)
---------------------------------------------------------------------------------------------------------------------------------------------------------

INSERT INTO Department(department_id, department_name, manager_id)
VALUES ('DPM001', N'IT', 'CD001')

INSERT INTO Department(department_id, department_name, manager_id)
VALUES ('DPM002', N'Kế Toán', 'CD002')
---------------------------------------------------------------------------------------------------------------------------------------------------------

INSERT INTO StatusWork(status_work_id, status_work_name)
VALUES ('1', N'Đang làm việc')

INSERT INTO StatusWork(status_work_id, status_work_name)
VALUES ('2', N'Nghỉ có phép')

INSERT INTO StatusWork(status_work_id, status_work_name)
VALUES ('3', N'Nghỉ không phép')

INSERT INTO StatusWork(status_work_id, status_work_name)
VALUES ('4', N'Nghỉ theo biên chế')

INSERT INTO StatusWork(status_work_id, status_work_name)
VALUES ('5', N'Đang công tác')
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
VALUES('nguyenvanan1510','@123456', 'EM001')

INSERT INTO Account(account_username, account_password, employee_id)
VALUES('nguyenthuyan1903','@123456', 'EM002')

INSERT INTO Account(account_username, account_password, employee_id)
VALUES('lehoangbaophat0711','@123456', 'EM003')

INSERT INTO Account(account_username, account_password, employee_id)
VALUES('tranhoanglan1909','@123456', 'EM004')

INSERT INTO Account(account_username, account_password, employee_id)
VALUES('hagiang2512','@123456', 'EM005')

INSERT INTO Account(account_username, account_password, employee_id)
VALUES('phanhoanggiang0504','@123456', 'EM006')

INSERT INTO Account(account_username, account_password, employee_id)
VALUES('tranlamphatlinh1808','@123456', 'CD001')

INSERT INTO Account(account_username, account_password, employee_id)
VALUES('letranthuylan1111','@123456', 'CD002')
---------------------------------------------------------------------------------------------------------------------------------------------------------

INSERT INTO Project (project_id, project_name, create_time, end_time, progress)
VALUES 
('PRJ001', N'Website Design', '01-04-2023', '30-06-2023', 'Not started'),
('PRJ002', N'App Development', '15-05-2023', '31-08-2023', 'Not started'),
('PRJ003', N'Network Upgrade', '01-06-2023', '30-09-2023', 'Not started');
---------------------------------------------------------------------------------------------------------------------------------------------------------

INSERT INTO Task (task_id, title, task_description, assign_date, deadline, create_by, progress, employee_id, project_id)
VALUES
('TSK001', N'Design homepage', N'Design the layout and functionality of the homepage', '05-04-2023', '15-04-2023', 'CD001', 'Not started', 'EM001', 'PRJ001'),
('TSK002', N'Develop homepage', N'Develop the homepage using HTML, CSS, and JavaScript', '16-04-2023', '30-04-2023', 'CD001', 'Not started', 'EM002', 'PRJ001'),
('TSK003', N'Design product page', N'Design the layout and functionality of the product page', '06-04-2023', '16-04-2023', 'CD001', 'Not started', 'EM003', 'PRJ001'),
('TSK004', N'Develop product page', N'Develop the product page using HTML, CSS, and JavaScript', '17-04-2023', '01-05-2023', 'CD001', 'Not started', 'EM004', 'PRJ001'),
('TSK005', N'Design contact page', N'Design the layout and functionality of the contact page', '07-04-2023', '17-04-2023', 'CD001', 'Not started', 'EM005', 'PRJ001'),
('TSK006', N'Develop contact page', N'Develop the contact page using HTML, CSS, and JavaScript', '18-04-2023', '02-05-2023', 'CD001', 'Not started', 'EM006', 'PRJ001'),
('TSK007', N'Create database schema', N'Design the database schema for the website', '05-04-2023', '10-04-2023', 'CD001', 'Not started', 'EM001', 'PRJ002'),
('TSK008', N'Implement database schema', N'Implement the database schema using MySQL', '11-04-2023', '20-04-2023', 'CD001', 'Not started', 'EM002', 'PRJ002'),
('TSK009', N'Design user authentication', N'Design the user authentication system for the website', '12-04-2023', '20-04-2023', 'CD001', 'Not started', 'EM003', 'PRJ002'),
('TSK010', N'Implement user authentication', N'Implement the user authentication system using PHP', '21-04-2023', '30-04-2023', 'CD001', 'Not started', 'EM004', 'PRJ002'),
('TSK011', N'Design payment system', N'Design the payment system for the website', '13-04-2023', '23-04-2023', 'CD001', 'Not started', 'EM005', 'PRJ002'),
('TSK012', N'Implement payment system', N'Implement the payment system using Stripe', '24-04-2023', '04-05-2023', 'CD001', 'Not started', 'EM006', 'PRJ002'),
('TSK013', N'Upgrade network hardware', N'Upgrade network hardware to support increased traffic', '01-06-2023', '15-06-2023', 'CD001', 'Not started', 'EM001', 'PRJ003'),
('TSK014', N'Configure new switches', N'Configure new switches to work with existing network infrastructure', '16-06-2023', '30-06-2023', 'CD001', 'Not started', 'EM002', 'PRJ003'),
('TSK015', N'Upgrade network software', N'Upgrade network software to support new features', '01-07-2023', '15-07-2023', 'CD001', 'Not started', 'EM003', 'PRJ003'),
('TSK016', N'Test network', N'Test the network to ensure it is functioning correctly', '16-07-2023', '31-07-2023', 'CD001', 'Not started', 'EM004', 'PRJ003'),
('TSK017', N'Train employees', N'Train employees on new network features and procedures', '01-08-2023', '15-08-2023', 'CD001', 'Not started', 'EM005', 'PRJ003'),
('TSK018', N'Rollout new network', N'Roll out the new network to all locations', '16-08-2023', '30-08-2023', 'CD001', 'Not started', 'EM006', 'PRJ003');
---------------------------------------------------------------------------------------------------------------------------------------------------------

INSERT INTO ProjectAssignment (project_id, role_name, employee_id)
VALUES
('PRJ001', N'Web Designer', 'EM001'),
('PRJ001', N'Developer', 'EM002'),
('PRJ001', N'Web Designer', 'EM003'),
('PRJ001', N'Web Designer', 'EM004'),
('PRJ001', N'Web Designer', 'EM005'),
('PRJ001', N'Web Designer', 'EM006'),
('PRJ002', N'Database Developer', 'EM001'),
('PRJ002', N'Database Developer', 'EM002'),
('PRJ002', N'Authentication Designer', 'EM003'),
('PRJ002', N'Authentication Designer', 'EM004'),
('PRJ002', N'Payment System Designer', 'EM005'),
('PRJ002', N'Payment System Designer', 'EM006'),
('PRJ003', N'Network Engineer', 'EM001'),
('PRJ003', N'Network Engineer', 'EM002'),
('PRJ003', N'Network Engineer', 'EM003'),
('PRJ003', N'Network Tester', 'EM004'),
('PRJ003', N'Leadership Trainer', 'EM005'),
('PRJ003', N'Network Deployment Specialist', 'EM006');