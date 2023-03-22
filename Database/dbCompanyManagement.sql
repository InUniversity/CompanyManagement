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
	create_time varchar(25),
	end_time varchar(25),
	progress varchar(30)
);

CREATE TABLE ProjectAssignment(
	project_id varchar(20),
	role_name nvarchar(50),
	department_id varchar(20)
);

CREATE TABLE Task(
	task_id varchar(20),
	title nvarchar(50),
	task_description nvarchar(255),
	assign_date varchar(20),
	deadline varchar(25),
	create_by varchar(25),
	progress varchar(30),
	employee_id varchar(20),
	project_id varchar(20)
);

INSERT INTO Position(position_id, position_name)
VALUES	
('1', N'Trưởng phòng'),
('2', N'Nhân Viên')
---------------------------------------------------------------------------------------------------------------------------------------------------------
INSERT INTO Employee (employee_id, employee_name, gender, birthday, identify_card, email, phone_number, employee_address, department_id, position_id, salary)
VALUES
('EM001', N'Nguyễn Văn An', 'Nam', '01/01/1990', '001234567890', 'an.nguyen@it.company.com', '0123456789', N'TP. Hồ Chí Minh', 'DPM001', '1', 15000000),
('EM002', N'Trần Thị Bình', N'Nữ', '02/02/1991', '001234567891', 'binh.tran@it.company.com', '0234567890', N'Bình Dương', 'DPM002', '1', 15000000),
('EM003', N'Lê Văn Cường', 'Nam', '03/03/1992', '001234567892', 'cuong.le@it.company.com', '0345678901', N'Đồng Nai', 'DPM003', '1', 15000000),
('EM004', N'Nguyễn Thị Dung', N'Nữ', '04/04/1993', '001234567893', 'dung.nguyen@it.company.com', '0456789012', N'TP. Hồ Chí Minh', 'DPM004', '1', 15000000),
('EM005', N'Phạm Văn Duy', 'Nam', '05/05/1994', '001234567894', 'duy.pham@it.company.com', '0567890123', N'Bình Dương', 'DPM005', '1', 15000000),
('EM006', N'Lê Thị Hà', N'Nữ', '06/06/1995', '001234567895', 'ha.le@it.company.com', '0678901234', N'TP. Hồ Chí Minh', 'DPM001', '2', 8000000),
('EM007', N'Nguyễn Văn Hoàng', 'Nam', '07/07/1996', '001234567896', 'hoang.nguyen@it.company.com', '0789012345', N'Đồng Nai', 'DPM001', '2', 8000000),
('EM008', N'Trần Thị Hương', N'Nữ', '08/08/1997', '001234567897', 'huong.tran@it.company.com', '0890123456', N'Bình Phước', 'DPM001', '2', 8000000),
('EM009', N'Nguyễn Văn Khoa', 'Nam', '09/09/1998', '001234567898', 'khoa.nguyen@it.company.com', '0901234567', N'TP. Hồ Chí Minh', 'DPM001', '2', 8000000),
('EM010', N'Lê Thị Lan', N'Nữ', '10/10/1999', '001234567899', 'lan.le@it.company.com', '0912345678', N'Bình Dương', 'DPM001', '2', 8000000),
('EM011', N'Phan Văn Minh', 'Nam', '11/11/2000', '001234567900', 'minh.phan@it.company.com', '0923456789', N'Đồng Nai', 'DPM001', '2', 8000000),
('EM012', N'Nguyễn Thị Ngọc', N'Nữ', '12/12/2001', '001234567901', 'ngoc.nguyen@it.company.com', '0934567890', N'TP. Hồ Chí Minh', 'DPM001', '2', 8000000),
('EM013', N'Trần Văn Phong', 'Nam', '13/01/1990', '001234567902', 'phong.tran@it.company.com', '0945678901', N'Bình Dương', 'DPM001', '2', 8000000),
('EM014', N'Lê Thị Quỳnh', N'Nữ', '14/02/1991', '001234567903', 'quynh.le@it.company.com', '0956789012', N'TP. Hồ Chí Minh', 'DPM001', '2', 8000000),
('EM015', N'Nguyễn Văn Sơn', 'Nam', '15/03/1992', '001234567904', 'son.nguyen@it.company.com', '0967890123', N'Bình Phước', 'DPM001', '2', 8000000), 
('EM016', N'Trần Văn Tâm', 'Nam', '16/04/1993', '001234567905', 'tam.tran@it.company.com', '0978901234', N'Đồng Nai', 'DPM001', '2', 8000000),
('EM017', N'Phạm Thị Uyên', N'Nữ', '17/05/1994', '001234567906', 'uyen.pham@it.company.com', '0089012345', N'TP. Hồ Chí Minh', 'DPM001', '2', 8000000),
('EM018', N'Lê Văn Vĩ', 'Nam', '18/06/1995', '001234567907', 'vi.le@it.company.com', '0190123456', N'Bình Dương', 'DPM001', '2', 8000000),
('EM019', N'Nguyễn Thị Xuân', N'Nữ', '19/07/1996', '001234567908', 'xuan.nguyen@it.company.com', '0201234567', N'Đồng Nai', 'DPM001', '2', 8000000),
('EM020', N'Trần Văn Yên', 'Nam', '20/08/1997', '001234567909', 'yen.tran@it.company.com', '0212345678', N'Bình Nhước', 'DPM001', '2', 8000000),
('EM021', N'Nguyễn Thị Vân', N'Nữ', '21/09/1998', '001234567910', 'van.nguyen@it.company.com', '0223456789', N'TP. Hồ Chí Minh', 'DPM001', '2', 8000000),
('EM022', N'Trần Thị Ánh', N'Nữ', '22/10/1999', '001234567911', 'anh.tran@it.company.com', '0234567890', N'TP. Hồ Chí Minh', 'DPM001', '2', 8000000),
('EM023', N'Nguyễn Văn Bảo', 'Nam', '23/11/2000', '001234567912', 'bao.nguyen@it.company.com', '0345678901', N'Bình Dương', 'DPM001', '2', 8000000),
('EM024', N'Lê Thị Cẩm', N'Nữ', '24/12/2001', '001234567913', 'cam.le@it.company.com', '0456789012', N'Đồng Nai', 'DPM002', '2', 8000000),
('EM025', N'Phan Văn Đạt', 'Nam', '25/01/1990', '001234567914', 'dat.phan@it.company.com', '0567890123', N'TP. Hồ Chí Minh', 'DPM002', '2', 8000000),
('EM026', N'Nguyễn Thị Eo', N'Nữ', '26/02/1991', '001234567915', 'eo.nguyen@it.company.com', '0678901234', N'Bình Phước', 'DPM002', '2', 8000000),
('EM027', N'Trần Văn Phan', 'Nam', '27/03/1992', '001234567916', 'phan.tran@it.company.com', '0789012345', N'TP. Hồ Chí Minh', 'DPM002', '2', 8000000),
('EM028', N'Lê Thị Gấm', N'Nữ', '28/04/1993', '001234567917', 'gam.le@it.company.com', '0890123456', N'Bình Dương', 'DPM002', '2', 8000000),
('EM029', N'Nguyễn Văn Hải', 'Nam', '29/05/1994', '001234567918', 'hai.nguyen@it.company.com', '0901234567', N'Đồng Nai', 'DPM002', '2', 8000000),
('EM030', N'Trần Thị Linh', N'Nữ', '30/06/1995', '001234567919', 'linh.tran@it.company.com', '0912345678', N'TP. Hồ Chí Minh', 'DPM002', '2', 8000000),
('EM031', N'Nguyễn Văn Khôi', 'Nam', '01/07/1996', '001234567920', 'khoi.nguyen@it.company.com', '0923456789', N'Bình Phước', 'DPM002', '2', 8000000),
('EM032', N'Lê Thị Lộc', N'Nữ', '02/08/1997', '001234567921', 'loc.le@it.company.com', '0934567890', N'TP. Hồ Chí Minh', 'DPM002', '2', 8000000),
('EM033', N'Nguyễn Thị Mỹ', N'Nữ', '03/09/1998', '001234567922', 'my.nguyen@it.company.com', '0945678901', N'Bình Dương', 'DPM002', '2', 8000000),
('EM034', N'Trần Văn Nghĩa', 'Nam', '04/10/1999', '001234567923', 'nghia.tran@it.company.com', '0756789012', N'Đồng Nai', 'DPM002', '2', 8000000),
('EM035', N'Lê Thị Oanh', N'Nữ', '05/11/2000', '001234567924', 'oanh.le@it.company.com', '0767890123', N'TP. Hồ Chí Minh', 'DPM003', '2', 8000000),
('EM036', N'Phan Văn Phúc', 'Nam', '06/12/1991', '001234567925', 'phuc.phan@it.company.com', '0878901234', N'Bình Phước', 'DPM003', '2', 8000000),
('EM037', N'Nguyễn Thị Quyên', N'Nữ', '07/01/1992', '001234567926', 'quyen.nguyen@it.company.com', '0989012345', N'TP. Hồ Chí Minh', 'DPM003', '2', 8000000),
('EM038', N'Trần Văn R', 'Nam', '08/02/1993', '001234567927', 'r.tran@it.company.com', '0890123456', N'Bình Dương', 'DPM003', '2', 8000000),
('EM039', N'Lê Thị Sương', N'Nữ', '09/03/1994', '001234567928', 'suong.le@it.company.com', '0701234567', N'Đồng Nai', 'DPM003', '2', 8000000),
('EM040', N'Phan Văn Tài', 'Nam', '10/04/1995', '001234567929', 'tai.phan@it.company.com', '0312345678', N'TP. Hồ Chí Minh', 'DPM003', '2', 8000000),
('EM041', N'Nguyễn Thị Uyên', N'Nữ', '11/05/1996', '001234567930', 'uyen.nguyen@it.company.com', '0923456789', N'Bình Phước', 'DPM003', '2', 8000000),
('EM042', N'Trần Văn Vượng', 'Nam', '12/06/1997', '001234567931', 'vuong.tran@it.company.com', '0934567890', N'TP. Hồ Chí Minh', 'DPM003', '2', 8000000),
('EM043', N'Lê Thị Xuyến', N'Nữ', '13/07/1998', '001234567932', 'xuyen.le@it.company.com', '0845678901', N'Bình Dương', 'DPM003', '2', 8000000),
('EM044', N'Nguyễn Thị Mỹ', N'Nữ', '03/09/1998', '001234567922', 'my.nguyen@it.company.com', '0945678901', N'Bình Dương', 'DPM003', '2', 8000000),
('EM045', N'Trần Văn Nghĩa', 'Nam', '04/10/1999', '001234567923', 'nghia.tran@it.company.com', '0756789012', N'Đồng Nai', 'DPM004', '2', 8000000),
('EM046', N'Lê Thị Oanh', N'Nữ', '05/11/2000', '001234567924', 'oanh.le@it.company.com', '0967890123', N'TP. Hồ Chí Minh', 'DPM004', '2', 8000000),
('EM047', N'Phan Văn Phúc', 'Nam', '06/12/1991', '001234567925', 'phuc.phan@it.company.com', '0378901234', N'Bình Phước', 'DPM004', '2', 8000000),
('EM048', N'Nguyễn Thị Quyên', N'Nữ', '07/01/1992', '001234567926', 'quyen.nguyen@it.company.com', '0389012345', N'TP. Hồ Chí Minh', 'DPM004', '2', 8000000),
('EM049', N'Trần Văn Giang', 'Nam', '08/02/1993', '001234567927', 'giang.tran@it.company.com', '0990123456', N'Bình Dương', 'DPM004', '2', 8000000),
('EM050', N'Trần Văn Tài', N'Nam', '01-01-1990', '123456789012', 'tai.tran@it.company.com', '0123456789', N'Hồ Chí Minh', 'DPM005', '1', 15000000),
('EM051', N'Lê Thị Hồng', N'Nữ', '02-02-1995', '123456789013', 'hong.le@it.company.com', '0234567890', N'Cần Thơ', 'DPM005', '2', 8000000),
('EM052', N'Nguyễn Đình Huy', N'Nam', '03-03-1992', '123456789014', 'huy.nguyen@it.company.com', '0345678901', N'Vũng Tàu', 'DPM005', '2', 8000000),
('EM053', N'Phạm Thị Ngọc', N'Nữ', '04-04-1991', '123456789015', 'ngoc.pham@it.company.com', '0456789012', N'Hồ Chí Minh', 'DPM005', '2', 8000000),
('EM054', N'Lương Thị Vân', N'Nữ', '05-05-1994', '123456789016', 'van.luong@it.company.com', '0567890123', N'Cà Mau', 'DPM005', '2', 8000000),
('EM055', N'Đặng Văn Đức', N'Nam', '06-06-1993', '123456789017', 'duc.dang@it.company.com', '0678901234', N'Đồng Nai', 'DPM005', '2', 8000000);
---------------------------------------------------------------------------------------------------------------------------------------------------------

INSERT INTO Department (department_id, department_name, manager_id) 
VALUES
('DPM001', 'Software Development Department', 'EM001'),
('DPM002', 'Web Development', 'EM002'),
('DPM003', 'Technology and Communication', 'EM003'),
('DPM004', 'Artificial Intelligence', 'EM004'),
('DPM005', 'Infrastructure Department', 'EM005'),
('DPM006', 'Finance and Accounting', 'EM005');
---------------------------------------------------------------------------------------------------------------------------------------------------------

INSERT INTO StatusWork(status_work_id, status_work_name)
VALUES	
('1', N'Đang làm việc'),
('2', N'Nghỉ có phép'),
('3', N'Nghỉ không phép'),
('4', N'Nghỉ theo biên chế'),
('5', N'Đang công tác')
---------------------------------------------------------------------------------------------------------------------------------------------------------
INSERT INTO StatusEmployee(employee_id, status_work_id)
VALUES
    ('EM001', '1'),
    ('EM002', '1'),
    ('EM003', '1'),
    ('EM004', '1'),
    ('EM005', '1'),
    ('EM006', '1'),
    ('EM007', '1'),
    ('EM008', '1'),
    ('EM009', '1'),
    ('EM010', '1'),
    ('EM011', '1'),
    ('EM012', '1'),
    ('EM013', '1'),
    ('EM014', '1'),
    ('EM015', '1'),
    ('EM016', '1'),
    ('EM017', '1'),
    ('EM018', '1'),
    ('EM019', '1'),
    ('EM020', '1'),
    ('EM021', '1'),
    ('EM022', '1'),
    ('EM023', '1'),
    ('EM024', '1'),
    ('EM025', '1'),
    ('EM026', '1'),
    ('EM027', '1'),
    ('EM028', '1'),
    ('EM029', '1'),
    ('EM030', '1'),
    ('EM031', '1'),
    ('EM032', '1'),
    ('EM033', '1'),
    ('EM034', '1'),
    ('EM035', '1'),
    ('EM036', '1'),
    ('EM037', '1'),
    ('EM038', '1'),
	('EM039', '1'),	
	('EM040', '1'),	
	('EM041', '1'),	
	('EM042', '1'),	
	('EM043', '1'),	
	('EM044', '1'),	
	('EM045', '1'),	
	('EM046', '1'),	
	('EM047', '1'),	
	('EM048', '1'),
	('EM049', '1'),
	('EM050', '1'),
	('EM051', '1'),
	('EM052', '1'),
	('EM053', '1'),
	('EM054', '1'),
	('EM055', '1');	
---------------------------------------------------------------------------------------------------------------------------------------------------------
INSERT INTO Account (account_username, account_password, employee_id)
SELECT CONCAT(REPLACE(LOWER(
TRANSLATE(employee_name, N'áàảãạăắằẳẵặâấầẩẫậéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵđ'
					   , N'aaaaaaaaaaaaaaaaaeeeeeeeeeeeiiiiiooooooooooooooooouuuuuuuuuuuyyyyyd')),' ', ''), 
					   SUBSTRING(birthday, 1, 2), SUBSTRING(birthday, 4, 2)), 
					   '@1234567', employee_id
FROM Employee;
---------------------------------------------------------------------------------------------------------------------------------------------------------

INSERT INTO Project (project_id, project_name, create_time, end_time, progress)
VALUES 
('PRJ001', 'Website Development', '01-01-2023 08:00 AM', '30-06-2023 05:00 PM', '50%'),
('PRJ002', 'Mobile App Development', '01-02-2023 09:30 AM', '31-08-2023 07:00 PM', '35%'),
('PRJ003', 'Database Management System', '01-03-2023 10:15 AM', '31-10-2023 04:30 PM', '10%'),
('PRJ004', 'Artificial Intelligence Research', '01-04-2023 01:00 PM', '31-03-2024 11:00 AM', 'Not started'),
('PRJ005', 'Cloud Computing Migration', '01-05-2023 02:45 PM', '30-11-2023 10:30 AM', 'Not started');
---------------------------------------------------------------------------------------------------------------------------------------------------------

INSERT INTO ProjectAssignment (project_id, role_name, department_id)
VALUES
('PRJ001', 'Web Designer', 'DPM001'),
('PRJ002', 'Mobile App Developmenent', 'DPM002'),
('PRJ003', 'Database Management System', 'DPM003'),
('PRJ004', 'Artificial Intelligence Research', 'DPM004'),
('PRJ005', 'Cloud Computing Migration', 'DPM005');
---------------------------------------------------------------------------------------------------------------------------------------------------------

INSERT INTO Task (task_id, title, task_description, assign_date, deadline, create_by, progress, employee_id, project_id)
VALUES
('T000001', N'Website Development - Design', N'Thiết kế giao diện website cho khách hàng ABC', '01-03-2023 09:00 AM', '15-03-2023 05:00 PM', 'EM002', '50%', 'EM001', 'PRJ001'),
('T000002', N'Website Development - Front-end', N'Lập trình phần front-end cho website khách hàng ABC', '16-03-2023 08:00 AM', '31-03-2023 05:00 PM', 'EM002', '30%', 'EM007', 'PRJ001'),
('T000003', N'Website Development - Back-end', N'Lập trình phần back-end cho website khách hàng ABC', '01-04-2023 08:00 AM', '15-04-2023 05:00 PM', 'EM002', '10%', 'EM009', 'PRJ001'),
('T000004', N'Website Development - Testing', N'Kiểm thử và sửa lỗi cho website khách hàng ABC', '16-04-2023 08:00 AM', '30-04-2023 05:00 PM', 'EM002', '5%', 'EM013', 'PRJ001'),
('T000005', N'Website Development - Deployment', N'Triển khai website khách hàng ABC trên server', '01-05-2023 08:00 AM', '15-05-2023 05:00 PM', 'EM002', '0%', 'EM017', 'PRJ001'),
('T000006', N'Develop mobile app UI design', N'Phát triển thiết kế giao diện người dùng cho ứng dụng di động', '20-04-2023 02:30 PM', '20-05-2023 05:00 PM', 'EM001', '0%', 'EM027', 'PRJ002'),
('T000007', N'Develop mobile app backend', N'Tạo backend cho ứng dụng di động', '15-05-2023 10:00 AM', '30-06-2023 01:30 PM', 'EM001', '0%', 'EM026', 'PRJ002'),
('T000008', N'Develop mobile app frontend', N'Tạo frontend cho ứng dụng di động', '01-06-2023 09:15 AM', '15-07-2023 11:45 AM', 'EM001', '0%', 'EM028', 'PRJ002'),
('T000009', N'Test mobile app', N'Kiểm thử ứng dụng di động và báo cáo lỗi', '20-07-2023 02:00 PM', '15-08-2023 04:30 PM', 'EM001', '0%', 'EM031', 'PRJ002'),
('T000010', N'Deploy mobile app', N'Triển khai ứng dụng di động lên cửa hàng ứng dụng', '01-09-2023 10:30 AM', '30-09-2023 03:00 PM', 'EM001', '0%', 'EM030', 'PRJ002'),
('T000011', N'Create new database', N'Tạo cơ sở dữ liệu cho hệ thống quản lý nhân viên', '01-03-2023 10:15 AM', '15-03-2023 04:30 PM', 'EM003', '0%', 'EM035', 'PRJ003'),
('T000012', N'Optimize database', N'Tối ưu hóa cơ sở dữ liệu cho hệ thống quản lý nhân viên', '05-03-2023 10:15 AM', '20-03-2023 04:30 PM', 'EM003', '0%', 'EM036', 'PRJ003'),
('T000013', N'Collect information', N'Thu thập thông tin về các nhân viên trong công ty', '10-03-2023 10:15 AM', '25-03-2023 04:30 PM', 'EM003', '0%', 'EM037', 'PRJ003'),
('T000014', N'Analyze data', N'Phân tích dữ liệu về các nhân viên trong công ty', '15-03-2023 10:15 AM', '30-03-2023 04:30 PM', 'EM003', '0%', 'EM038', 'PRJ003'),
('T000015', N'Perform data check', N'Thực hiện kiểm tra dữ liệu đã thu thập và phân tích', '20-03-2023 10:15 AM', '31-10-2023 04:30 PM', 'EM003', '0%', 'EM039', 'PRJ003'),
('T000016', N'Cloud Computing Migration', N'Migrate các ứng dụng và dữ liệu hiện có đến nền tảng Cloud Computing', '01-05-2023 02:45 PM', '30-11-2023 10:30 AM', 'EM004', '0%', 'EM045', 'PRJ004'),
('T000017', N'Assess current infrastructure', N'Đánh giá cơ sở hạ tầng công nghệ thông tin hiện tại và xác định các khu vực cần được di chuyển đến đám mây.', '05-05-2023 02:45 PM', '15-05-2023 04:30 PM', 'EM004', '0%', 'EM046', 'PRJ004'),
('T000018', N'Select cloud provider', N'Nghiên cứu và lựa chọn nhà cung cấp đám mây phù hợp cho công ty', '10-05-2023 02:45 PM', '25-05-2023 04:30 PM', 'EM004', '0%', 'EM047', 'PRJ004'),
('T000019', N'Migrate applications', N'Migrate các ứng dụng hiện có đến nền tảng đám mây', '15-05-2023 02:45 PM', '30-11-2023 10:30 AM', 'EM004', '0%', 'EM048', 'PRJ004'),
('T000020', N'Migrate data', N'Transfer dữ liệu của công ty đến nền tảng đám mây', '20-05-2023 02:45 PM', '30-11-2023 10:30 AM', 'EM004', '0%', 'EM049', 'PRJ004'),
('T000021', N'Infrastructure Department', N'Triển khai và quản lý cơ sở hạ tầng công nghệ thông tin cho công ty', '01-05-2023 02:45 PM', '30-11-2023 10:30 AM', 'EM050', '0%', 'EM050', 'PRJ005'),
('T000022', N'Set up network infrastructure', N'Cài đặt và cấu hình cơ sở hạ tầng mạng cho công ty', '05-05-2023 02:45 PM', '15-05-2023 04:30 PM', 'EM050', '0%', 'EM051', 'PRJ005'),
('T000023', N'Implement server infrastructure', N'Cài đặt và cấu hình cơ sở hạ tầng máy chủ cho công ty', '10-05-2023 02:45 PM', '25-05-2023 04:30 PM', 'EM050', '0%', 'EM052', 'PRJ005'),
('T000024', N'Set up security infrastructure', N'Cài đặt và cấu hình cơ sở hạ tầng bảo mật cho công ty', '15-05-2023 02:45 PM', '30-11-2023 10:30 AM', 'EM050', '0%', 'EM053', 'PRJ005'),
('T000025', N'Implement backup infrastructure', N'Cài đặt và cấu hình cơ sở hạ tầng sao lưu cho công ty', '20-05-2023 02:45 PM', '30-11-2023 10:30 AM', 'EM050', '0%', 'EM054', 'PRJ005'),
('T000026', N'Manage IT infrastructure', N'Quản lý và duy trì cơ sở hạ tầng công nghệ thông tin cho công ty', '25-05-2023 02:45 PM', '30-11-2023 10:30 AM', 'EM050', '0%', 'EM055', 'PRJ005');