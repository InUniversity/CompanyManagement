CREATE DATABASE CompanyManagement
GO
USE CompanyManagement
GO
-- department
CREATE TABLE Departments(
    ID varchar(20) PRIMARY KEY NOT NULL,
    DepartmentName nvarchar(100),
    DepartmentHead varchar(20) DEFAULT ''
);
GO
INSERT INTO Departments (ID, DepartmentName, DepartmentHead)
VALUES
    ('DPM001', 'Software Development Department', 'EM006'),
    ('DPM002', 'Web Development', 'EM025'),
    ('DPM003', 'Technology and Communication', 'EM035'),
    ('DPM004', 'Artificial Intelligence', 'EM045'),
    ('DPM005', 'Infrastructure Department', 'EM051');
GO

-- employee
CREATE TABLE Roles(
    ID varchar(20) PRIMARY KEY NOT NULL,
    Title nvarchar(50)
);
GO
INSERT INTO Roles(ID, Title)
VALUES
    ('ER01', N'Quản lý'),
    ('ER02', N'Trưởng phòng'),
    ('ER03', N'HR'),
    ('ER04', N'Nhân viên');
GO

CREATE TABLE Employees(
    ID varchar (20) PRIMARY KEY NOT NULL,
    FullName nvarchar(100),
    Gender nvarchar(10),
    Birthday date,
    IdentifyCard varchar(12),
    Email varchar(50),
    PhoneNumber varchar (10),
    EmployeeAddress nvarchar(255),
    BaseSalary DECIMAL(19,4),
    DepartmentID varchar(20) DEFAULT '',
    RoleID varchar(20) DEFAULT ''
);
GO
INSERT INTO Employees(ID, FullName, Gender, Birthday, IdentifyCard, Email, PhoneNumber, EmployeeAddress, BaseSalary, DepartmentID, RoleID)
VALUES
    ('EM001', N'Nguyễn Văn An', 'Nam', CONVERT(DATE, '01-01-1990', 105), '001234567890', 'an.nguyen@it.company.com', '0123456789', N'TP. Hồ Chí Minh', 15000000, '', 'ER01'),
    ('EM002', N'Trần Thị Bình', N'Nữ', CONVERT(DATE, '02-02-1991', 105), '001234567891', 'binh.tran@it.company.com', '0234567890', N'Bình Dương', 15000000, '', 'ER01'),
    ('EM003', N'Lê Văn Cường', 'Nam', CONVERT(DATE, '03-03-1992', 105), '001234567892', 'cuong.le@it.company.com', '0345678901', N'Đồng Nai', 15000000, '', 'ER01'),
    ('EM004', N'Nguyễn Thị Dung', N'Nữ', CONVERT(DATE, '04-04-1993', 105), '001234567893', 'dung.nguyen@it.company.com', '0456789012', N'TP. Hồ Chí Minh', 15000000, '', 'ER01'),
    ('EM005', N'Phạm Văn Duy', 'Nam', CONVERT(DATE, '05-05-1994', 105), '001234567894', 'duy.pham@it.company.com', '0567890123', N'Bình Dương', 15000000, '', 'ER01'),
    ('EM006', N'Lê Thị Hà', N'Nữ', CONVERT(DATE, '06-06-1995', 105), '001234567895', 'ha.le@it.company.com', '0678901234', N'TP. Hồ Chí Minh', 15000000, 'DPM001', 'ER02'),
    ('EM007', N'Nguyễn Văn Hoàng', 'Nam', CONVERT(DATE, '07-07-1996', 105), '001234567896', 'hoang.nguyen@it.company.com', '0789012345', N'Đồng Nai', 15000000, 'DPM001', 'ER04'),
    ('EM008', N'Trần Thị Hương', N'Nữ', CONVERT(DATE, '08-08-1997', 105), '001234567897', 'huong.tran@it.company.com', '0890123456', N'Bình Phước', 15000000, 'DPM001', 'ER04'),
    ('EM009', N'Nguyễn Văn Khoa', 'Nam', CONVERT(DATE, '09-09-1998', 105), '001234567898', 'khoa.nguyen@it.company.com', '0901234567', N'TP. Hồ Chí Minh', 15000000, 'DPM001', 'ER04'),
    ('EM010', N'Lê Thị Lan', N'Nữ', CONVERT(DATE, '10-10-1999', 105), '001234567899', 'lan.le@it.company.com', '0912345678', N'Bình Dương', 15000000, 'DPM001', 'ER04'),
    ('EM011', N'Phan Văn Minh', 'Nam', CONVERT(DATE, '11-11-2000', 105), '001234567900', 'minh.phan@it.company.com', '0923456789', N'Đồng Nai', 15000000, 'DPM001', 'ER04'),
    ('EM012', N'Nguyễn Thị Ngọc', N'Nữ', CONVERT(DATE, '12-12-2001', 105), '001234567901', 'ngoc.nguyen@it.company.com', '0934567890', N'TP. Hồ Chí Minh', 15000000, 'DPM001', 'ER04'),
    ('EM013', N'Trần Văn Phong', 'Nam', CONVERT(DATE, '13-01-1990', 105), '001234567902', 'phong.tran@it.company.com', '0945678901', N'Bình Dương', 15000000, 'DPM001', 'ER04'),
    ('EM014', N'Lê Thị Quỳnh', N'Nữ', CONVERT(DATE, '14-02-1991', 105), '001234567903', 'quynh.le@it.company.com', '0956789012', N'TP. Hồ Chí Minh', 15000000, 'DPM001', 'ER04'),
    ('EM015', N'Nguyễn Văn Sơn', 'Nam', CONVERT(DATE, '15-03-1992', 105), '001234567904', 'son.nguyen@it.company.com', '0967890123', N'Bình Phước', 15000000, 'DPM001', 'ER04'),
    ('EM016', N'Trần Văn Tâm', 'Nam', CONVERT(DATE, '16-04-1993', 105), '001234567905', 'tam.tran@it.company.com', '0978901234', N'Đồng Nai', 15000000, 'DPM001', 'ER04'),
    ('EM017', N'Phạm Thị Uyên', N'Nữ', CONVERT(DATE, '17-05-1994', 105), '001234567906', 'uyen.pham@it.company.com', '0089012345', N'TP. Hồ Chí Minh', 15000000, 'DPM001', 'ER04'),
    ('EM018', N'Lê Văn Vĩ', 'Nam', CONVERT(DATE, '18-06-1995', 105), '001234567907', 'vi.le@it.company.com', '0190123456', N'Bình Dương', 15000000, 'DPM001', 'ER04'),
    ('EM019', N'Nguyễn Thị Xuân', N'Nữ', CONVERT(DATE, '19-07-1996', 105), '001234567908', 'xuan.nguyen@it.company.com', '0201234567', N'Đồng Nai', 15000000, 'DPM001', 'ER04'),
    ('EM020', N'Trần Văn Yên', 'Nam', CONVERT(DATE, '20-08-1997', 105), '001234567909', 'yen.tran@it.company.com', '0212345678', N'Bình Nhước', 15000000, 'DPM001', 'ER04'),
    ('EM021', N'Nguyễn Thị Vân', N'Nữ', CONVERT(DATE, '21-09-1998', 105), '001234567910', 'van.nguyen@it.company.com', '0223456789', N'TP. Hồ Chí Minh', 15000000, 'DPM001', 'ER04'),
    ('EM022', N'Trần Thị Ánh', N'Nữ', CONVERT(DATE, '22-10-1999', 105), '001234567911', 'anh.tran@it.company.com', '0234567890', N'TP. Hồ Chí Minh', 15000000, 'DPM001', 'ER04'),
    ('EM023', N'Nguyễn Văn Bảo', 'Nam', CONVERT(DATE, '23-11-2000', 105), '001234567912', 'bao.nguyen@it.company.com', '0345678901', N'Bình Dương', 15000000, 'DPM001', 'ER04'),
    ('EM024', N'Lê Thị Cẩm', N'Nữ', CONVERT(DATE, '24-12-2001', 105), '001234567913', 'cam.le@it.company.com', '0456789012', N'Đồng Nai', 15000000, 'DPM002', 'ER04'),
    ('EM025', N'Phan Văn Đạt', 'Nam', CONVERT(DATE, '25-01-1990', 105), '001234567914', 'dat.phan@it.company.com', '0567890123', N'TP. Hồ Chí Minh', 15000000, 'DPM002', 'ER02'),
    ('EM026', N'Nguyễn Thị Eo', N'Nữ', CONVERT(DATE, '26-02-1991', 105), '001234567915', 'eo.nguyen@it.company.com', '0678901234', N'Bình Phước', 15000000, 'DPM002', 'ER04'),
    ('EM027', N'Trần Văn Phan', 'Nam', CONVERT(DATE, '27-03-1992', 105), '001234567916', 'phan.tran@it.company.com', '0789012345', N'TP. Hồ Chí Minh', 15000000, 'DPM002', 'ER04'),
    ('EM028', N'Lê Thị Gấm', N'Nữ', CONVERT(DATE, '28-04-1993', 105), '001234567917', 'gam.le@it.company.com', '0890123456', N'Bình Dương', 15000000, 'DPM002', 'ER04'),
    ('EM029', N'Nguyễn Văn Hải', 'Nam', CONVERT(DATE, '29-05-1994', 105), '001234567918', 'hai.nguyen@it.company.com', '0901234567', N'Đồng Nai', 15000000, 'DPM002', 'ER04'),
    ('EM030', N'Trần Thị Linh', N'Nữ', CONVERT(DATE, '30-06-1995', 105), '001234567919', 'linh.tran@it.company.com', '0912345678', N'TP. Hồ Chí Minh', 15000000, 'DPM002', 'ER04'),
    ('EM031', N'Nguyễn Văn Khôi', 'Nam', CONVERT(DATE, '01-07-1996', 105), '001234567920', 'khoi.nguyen@it.company.com', '0923456789', N'Bình Phước', 15000000, 'DPM002', 'ER04'),
    ('EM032', N'Lê Thị Lộc', N'Nữ', CONVERT(DATE, '02-08-1997', 105), '001234567921', 'loc.le@it.company.com', '0934567890', N'TP. Hồ Chí Minh', 15000000, 'DPM002', 'ER04'),
    ('EM033', N'Nguyễn Thị Mỹ', N'Nữ', CONVERT(DATE, '03-09-1998', 105), '001234567922', 'my.nguyen@it.company.com', '0945678901', N'Bình Dương', 15000000, 'DPM002', 'ER04'),
    ('EM034', N'Trần Văn Nghĩa', 'Nam', CONVERT(DATE, '04-10-1999', 105), '001234567923', 'nghia.tran@it.company.com', '0756789012', N'Đồng Nai', 15000000, 'DPM002', 'ER04'),
    ('EM035', N'Lê Thị Oanh', N'Nữ', CONVERT(DATE, '05-11-2000', 105), '001234567924', 'oanh.le@it.company.com', '0767890123', N'TP. Hồ Chí Minh', 15000000, 'DPM003', 'ER02'),
    ('EM036', N'Phan Văn Phúc', 'Nam', CONVERT(DATE, '06-12-1991', 105), '001234567925', 'phuc.phan@it.company.com', '0878901234', N'Bình Phước', 15000000, 'DPM003', 'ER04'),
    ('EM037', N'Nguyễn Thị Quyên', N'Nữ', CONVERT(DATE, '07-01-1992', 105), '001234567926', 'quyen.nguyen@it.company.com', '0989012345', N'TP. Hồ Chí Minh', 15000000, 'DPM003', 'ER04'),
    ('EM038', N'Trần Văn R', 'Nam', CONVERT(DATE, '08-02-1993', 105), '001234567927', 'r.tran@it.company.com', '0890123456', N'Bình Dương', 15000000, 'DPM003', 'ER04'),
    ('EM039', N'Lê Thị Sương', N'Nữ', CONVERT(DATE, '09-03-1994', 105), '001234567928', 'suong.le@it.company.com', '0701234567', N'Đồng Nai', 15000000, 'DPM003', 'ER04'),
    ('EM040', N'Phan Văn Tài', 'Nam', CONVERT(DATE, '10-04-1995', 105), '001234567929', 'tai.phan@it.company.com', '0312345678', N'TP. Hồ Chí Minh', 15000000, 'DPM003', 'ER04'),
    ('EM041', N'Nguyễn Thị Uyên', N'Nữ', CONVERT(DATE, '11-05-1996', 105), '001234567930', 'uyen.nguyen@it.company.com', '0923456789', N'Bình Phước', 15000000, 'DPM003', 'ER04'),
    ('EM042', N'Trần Văn Vượng', 'Nam', CONVERT(DATE, '12-06-1997', 105), '001234567931', 'vuong.tran@it.company.com', '0934567890', N'TP. Hồ Chí Minh', 15000000, 'DPM003', 'ER04'),
    ('EM043', N'Lê Thị Xuyến', N'Nữ', CONVERT(DATE, '13-07-1998', 105), '001234567932', 'xuyen.le@it.company.com', '0845678901', N'Bình Dương', 15000000, 'DPM003', 'ER04'),
    ('EM044', N'Nguyễn Thị Mĩ Diệu', N'Nữ', CONVERT(DATE, '03-09-1998', 105), '001234567922', 'my.nguyen@it.company.com', '0945678901', N'Bình Dương', 15000000, 'DPM003', 'ER04'),
    ('EM045', N'Trần Văn Nghĩa', 'Nam', CONVERT(DATE, '04-10-1999', 105), '001234567923', 'nghia.tran@it.company.com', '0756789012', N'Đồng Nai', 15000000, 'DPM004', 'ER02'),
    ('EM046', N'Lê Thị Oanh', N'Nữ', CONVERT(DATE, '05-11-2000', 105), '001234567924', 'oanh.le@it.company.com', '0967890123', N'TP. Hồ Chí Minh', 15000000, 'DPM004', 'ER04'),
    ('EM047', N'Phan Văn Phúc', 'Nam', CONVERT(DATE, '06-12-1991', 105), '001234567925', 'phuc.phan@it.company.com', '0378901234', N'Bình Phước', 15000000, 'DPM004', 'ER04'),
    ('EM048', N'Nguyễn Thị Quyên', N'Nữ', CONVERT(DATE, '07-01-1992', 105), '001234567926', 'quyen.nguyen@it.company.com', '0389012345', N'TP. Hồ Chí Minh', 15000000, 'DPM004', 'ER04'),
    ('EM049', N'Trần Văn Giang', 'Nam', CONVERT(DATE, '08-02-1993', 105), '001234567927', 'giang.tran@it.company.com', '0990123456', N'Bình Dương', 15000000, 'DPM004', 'ER04'),
    ('EM050', N'Trần Văn Tài', N'Nam', CONVERT(DATE, '01-01-1990', 105), '123456789012', 'tai.tran@it.company.com', '0123456789', N'Hồ Chí Minh', 15000000, 'DPM005', 'ER04'),
    ('EM051', N'Lê Thị Hồng', N'Nữ', CONVERT(DATE, '02-02-1995', 105), '123456789013', 'hong.le@it.company.com', '0234567890', N'Cần Thơ', 15000000, 'DPM005',  'ER02'),
    ('EM052', N'Nguyễn Đình Huy', N'Nam', CONVERT(DATE, '03-03-1992', 105), '123456789014', 'huy.nguyen@it.company.com', '0345678901', N'Vũng Tàu', 15000000, 'DPM005', 'ER04'),
    ('EM053', N'Phạm Thị Ngọc', N'Nữ', CONVERT(DATE, '04-04-1991', 105), '123456789015', 'ngoc.pham@it.company.com', '0456789012', N'Hồ Chí Minh', 15000000, 'DPM005', 'ER04'),
    ('EM054', N'Lương Thị Vân', N'Nữ', CONVERT(DATE, '05-05-1994', 105), '123456789016', 'van.luong@it.company.com', '0567890123', N'Cà Mau', 15000000, 'DPM005', 'ER04'),
    ('EM055', N'Đặng Văn Đức', N'Nam', CONVERT(DATE, '06-06-1993', 105), '123456789017', 'duc.dang@it.company.com', '0678901234', N'Đồng Nai', 15000000, '', 'ER03');
GO
CREATE TABLE Accounts(
    Username varchar(100) NOT NULL UNIQUE,
    PasswordHash varchar(100) NOT NULL,
    EmployeeID varchar(20) PRIMARY KEY DEFAULT ''
);
GO
INSERT INTO Accounts (Username, PasswordHash, EmployeeID)
SELECT CONCAT(ID, SUBSTRING(CONVERT(varchar, Birthday, 103), 1, 2), SUBSTRING(CONVERT(varchar, Birthday, 103), 4, 2)),
       '@1234567', ID
FROM Employees;
GO

-- projects
CREATE TABLE ProjectStatuses(
    ID varchar(10) PRIMARY KEY NOT NULL,
    StatusName nvarchar(50)
);
GO
INSERT INTO ProjectStatuses(ID, StatusName)
VALUES
    ( 'PST1', N'Đang triển khai'),
    ( 'PST2', N'Hoàn thành'),
    ( 'PST3', N'Quá hạn'),
    ( 'PST4', N'Đang chờ thanh toán'),
    ( 'PST5', N'Lên kế hoạch');
GO
CREATE TABLE Projects(
    ID varchar(20) PRIMARY KEY NOT NULL,
    ProjectName nvarchar(225),
    Details nvarchar(1500),
    CreatedDate SMALLDATETIME,
    StartDate SMALLDATETIME,
    EndDate SMALLDATETIME,
    CompletedDate SMALLDATETIME DEFAULT '2000-01-01 00:00:00',
    Progress varchar(4),
    StatusID varchar(10) DEFAULT '',
    OwnerID varchar(20) DEFAULT '',
    BonusSalary DECIMAL(19,4)
);
GO
INSERT INTO Projects(ID, ProjectName, Details, CreatedDate, StartDate, EndDate, Progress, StatusID, OwnerID, BonusSalary)
VALUES
    ('PRJ001', 'Website Development', '', CONVERT(SMALLDATETIME, '01-01-2023 08:00 AM', 105), CONVERT(SMALLDATETIME, '01-03-2023 08:00 AM', 105), CONVERT(SMALLDATETIME, '30-06-2023 05:00 PM', 105), '50','PST1', 'EM001', 100000000),
    ('PRJ002', 'Mobile App Development', '', CONVERT(SMALLDATETIME, '01-02-2023 09:30 AM', 105), CONVERT(SMALLDATETIME, '01-02-2023 09:30 AM', 105), CONVERT(SMALLDATETIME, '31-08-2023 07:00 PM', 105),'35', 'PST1', 'EM002', 100234000),
    ('PRJ003', 'Database Management System', '', CONVERT(SMALLDATETIME, '01-03-2023 10:15 AM', 105), CONVERT(SMALLDATETIME, '01-03-2023 10:15 AM', 105), CONVERT(SMALLDATETIME, '31-10-2023 04:30 PM', 105),'10', 'PST1', 'EM003', 100056700),
    ('PRJ004', 'Artificial Intelligence Research', '', CONVERT(SMALLDATETIME, '01-04-2023 01:00 PM', 105), CONVERT(SMALLDATETIME, '01-04-2023 01:00 PM', 105), CONVERT(SMALLDATETIME, '31-03-2024 11:00 AM', 105),'0', 'PST1', 'EM004', 112300000),
    ('PRJ005', 'Cloud Computing Migration', '', CONVERT(SMALLDATETIME, '01-05-2023 02:45 PM', 105), CONVERT(SMALLDATETIME, '01-05-2023 02:45 PM', 105), CONVERT(SMALLDATETIME, '30-11-2023 10:30 AM', 105),'0', 'PST1', 'EM005', 112300456);
GO

CREATE TABLE ProjectAssignments(
    ProjectID varchar(20) DEFAULT '',
    DepartmentID varchar(20) DEFAULT '',
    PRIMARY KEY (ProjectID, DepartmentID)
);
GO
INSERT INTO ProjectAssignments (ProjectID, DepartmentID)
VALUES
    ('PRJ001', 'DPM001'),
    ('PRJ002', 'DPM002'),
    ('PRJ003', 'DPM003'),
    ('PRJ004', 'DPM004'),
    ('PRJ005', 'DPM005');
GO

-- tasks
CREATE TABLE TaskStatuses(
    ID varchar(10) PRIMARY KEY NOT NULL,
    StatusName nvarchar(50)
);
GO
INSERT INTO TaskStatuses(ID, StatusName)
VALUES
    ( 'TS1', N'Đang thực hiện'),
    ( 'TS2', N'Đã hoàn thành'),
    ( 'TS3', N'Quá hạn'),
    ( 'TS4', N'Đang xem xét');
GO
CREATE TABLE Tasks(
    ID varchar(20) PRIMARY KEY NOT NULL,
    Title nvarchar(50),
    Explanation nvarchar(255),
    StartDate SMALLDATETIME,
    Deadline SMALLDATETIME,
    Progress varchar(4),
    OwnerID varchar(20) DEFAULT '',
    EmployeeID varchar(20) DEFAULT '',
    ProjectID varchar(20) DEFAULT '',
    StatusID varchar(10) DEFAULT ''
);
GO
INSERT INTO Tasks(ID, Title, Explanation, StartDate, Deadline, Progress, OwnerID, EmployeeID, ProjectID, StatusID)
VALUES
    ('T000001', N'Website Development - Design', N'Thiết kế giao diện website cho khách hàng ABC', CONVERT(SMALLDATETIME, '01-03-2023 09:00 AM', 105), CONVERT(SMALLDATETIME, '15-03-2023 05:00 PM', 105), '50', 'EM002', 'EM007', 'PRJ001', 'TS3'),
    ('T000002', N'Website Development - Front-end', N'Lập trình phần front-end cho website khách hàng ABC', CONVERT(SMALLDATETIME, '16-03-2023 08:00 AM', 105), CONVERT(SMALLDATETIME, '31-03-2023 05:00 PM', 105), '30', 'EM002', 'EM007', 'PRJ001','TS3'),
    ('T000003', N'Website Development - Back-end', N'Lập trình phần back-end cho website khách hàng ABC', CONVERT(SMALLDATETIME, '01-04-2023 08:00 AM', 105), CONVERT(SMALLDATETIME, '15-04-2023 05:00 PM', 105), '10', 'EM002', 'EM007', 'PRJ001', 'TS3'),
    ('T000004', N'Website Development - Testing', N'Kiểm thử và sửa lỗi cho website khách hàng ABC', CONVERT(SMALLDATETIME, '16-04-2023 08:00 AM', 105), CONVERT(SMALLDATETIME, '30-04-2023 05:00 PM', 105), '10', 'EM002', 'EM013', 'PRJ001', 'TS1'),
    ('T000005', N'Website Development - Deployment', N'Triển khai website khách hàng ABC trên server', CONVERT(SMALLDATETIME, '01-05-2023 08:00 AM', 105), CONVERT(SMALLDATETIME, '15-05-2023 05:00 PM', 105), '0', 'EM002', 'EM017', 'PRJ001', 'TS1'),
    
    ('T000006', N'Develop mobile app UI design', N'Phát triển thiết kế giao diện người dùng cho ứng dụng di động', CONVERT(SMALLDATETIME, '20-04-2023 02:30 PM', 105), CONVERT(SMALLDATETIME, '20-05-2023 05:00 PM', 105), '0', 'EM001', 'EM027', 'PRJ002', 'TS1'),
    ('T000007', N'Develop mobile app backend', N'Tạo backend cho ứng dụng di động', CONVERT(SMALLDATETIME, '15-05-2023 10:00 AM', 105), CONVERT(SMALLDATETIME, '30-06-2023 01:30 PM', 105), '0', 'EM001', 'EM026', 'PRJ002', 'TS1'),
    ('T000008', N'Develop mobile app frontend', N'Tạo frontend cho ứng dụng di động', CONVERT(SMALLDATETIME, '01-06-2023 09:15 AM', 105), CONVERT(SMALLDATETIME, '15-07-2023 11:45 AM', 105), '0', 'EM001', 'EM028', 'PRJ002', 'TS1'),
    ('T000009', N'Test mobile app', N'Kiểm thử ứng dụng di động và báo cáo lỗi', CONVERT(SMALLDATETIME, '20-07-2023 02:00 PM', 105), CONVERT(SMALLDATETIME, '15-08-2023 04:30 PM', 105), '0', 'EM001', 'EM031', 'PRJ002', 'TS1'),
    ('T000010', N'Deploy mobile app', N'Triển khai ứng dụng di động lên cửa hàng ứng dụng', CONVERT(SMALLDATETIME, '01-09-2023 10:30 AM', 105), CONVERT(SMALLDATETIME, '30-09-2023 03:00 PM', 105), '0', 'EM001', 'EM030', 'PRJ002', 'TS1'),
    
    ('T000011', N'Create new database', N'Tạo cơ sở dữ liệu cho hệ thống quản lý nhân viên', CONVERT(SMALLDATETIME, '01-03-2023 10:15 AM', 105), CONVERT(SMALLDATETIME, '15-03-2023 04:30 PM', 105), '0', 'EM003', 'EM035', 'PRJ003', 'TS3'),
    ('T000012', N'Optimize database', N'Tối ưu hóa cơ sở dữ liệu cho hệ thống quản lý nhân viên', CONVERT(DATETIME, '05-03-2023 10:15 AM', 105), CONVERT(DATETIME, '20-03-2023 04:30 PM', 105), '0', 'EM003', 'EM036', 'PRJ003', 'TS3'),
    ('T000013', N'Collect information', N'Thu thập thông tin về các nhân viên trong công ty', CONVERT(SMALLDATETIME, '10-03-2023 10:15 AM', 105), CONVERT(SMALLDATETIME, '25-03-2023 04:30 PM', 105), '0', 'EM003', 'EM037', 'PRJ003', 'TS3'),
    ('T000014', N'Analyze data', N'Phân tích dữ liệu về các nhân viên trong công ty', CONVERT(SMALLDATETIME, '15-03-2023 10:15 AM', 105), CONVERT(SMALLDATETIME, '30-03-2023 04:30 PM', 105), '0', 'EM003', 'EM038', 'PRJ003', 'TS3'),
    ('T000015', N'Perform data check', N'Thực hiện kiểm tra dữ liệu đã thu thập và phân tích', CONVERT(SMALLDATETIME, '20-03-2023 10:15 AM', 105), CONVERT(SMALLDATETIME, '31-10-2023 04:30 PM', 105), '0', 'EM003', 'EM039', 'PRJ003', 'TS1'),
    
    ('T000016', N'Cloud Computing Migration', N'Migrate các ứng dụng và dữ liệu hiện có đến nền tảng Cloud Computing', CONVERT(SMALLDATETIME, '01-05-2023 02:45 PM', 105), CONVERT(SMALLDATETIME, '30-11-2023 10:30 AM', 105), '0', 'EM004', 'EM045', 'PRJ004', 'TS1'),
    ('T000017', N'Assess current infrastructure', N'Đánh giá cơ sở hạ tầng công nghệ thông tin hiện tại và xác định các khu vực cần được di chuyển đến đám mây.', CONVERT(SMALLDATETIME, '05-05-2023 02:45 PM', 105), CONVERT(SMALLDATETIME, '15-05-2023 04:30 PM', 105), '0', 'EM004', 'EM046', 'PRJ004', 'TS1'),
    ('T000018', N'Select cloud provIDer', N'Nghiên cứu và lựa chọn nhà cung cấp đám mây phù hợp cho công ty', CONVERT(SMALLDATETIME, '10-05-2023 02:45 PM', 105), CONVERT(SMALLDATETIME, '25-05-2023 04:30 PM', 105), '0', 'EM004', 'EM047', 'PRJ004', 'TS1'),
    ('T000019', N'Migrate applications', N'Migrate các ứng dụng hiện có đến nền tảng đám mây', CONVERT(SMALLDATETIME, '15-05-2023 02:45 PM', 105), CONVERT(SMALLDATETIME, '30-11-2023 10:30 AM', 105), '0', 'EM004', 'EM048', 'PRJ004', 'TS1'),
    ('T000020', N'Migrate data', N'Transfer dữ liệu của công ty đến nền tảng đám mây', CONVERT(SMALLDATETIME, '20-05-2023 02:45 PM', 105), CONVERT(SMALLDATETIME, '30-11-2023 10:30 AM', 105), '0', 'EM004', 'EM049', 'PRJ004', 'TS1'),
    
    ('T000021', N'Infrastructure Department', N'Triển khai và quản lý cơ sở hạ tầng công nghệ thông tin cho công ty', CONVERT(SMALLDATETIME, '01-05-2023 02:45 PM', 105), CONVERT(SMALLDATETIME, '30-11-2023 10:30 AM', 105), '0', 'EM051', 'EM050', 'PRJ005', 'TS1'),
    ('T000022', N'Set up network infrastructure', N'Cài đặt và cấu hình cơ sở hạ tầng mạng cho công ty', CONVERT(SMALLDATETIME, '05-05-2023 02:45 PM', 105), CONVERT(SMALLDATETIME, '15-05-2023 04:30 PM', 105), '0', 'EM051', 'EM050', 'PRJ005', 'TS1'),
    ('T000023', N'Implement server infrastructure', N'Cài đặt và cấu hình cơ sở hạ tầng máy chủ cho công ty', CONVERT(SMALLDATETIME, '10-05-2023 02:45 PM', 105), CONVERT(SMALLDATETIME, '25-05-2023 04:30 PM', 105), '0', 'EM051', 'EM052', 'PRJ005', 'TS1'),
    ('T000024', N'Set up security infrastructure', N'Cài đặt và cấu hình cơ sở hạ tầng bảo mật cho công ty', CONVERT(SMALLDATETIME, '15-05-2023 02:45 PM', 105), CONVERT(SMALLDATETIME, '30-11-2023 10:30 AM', 105), '0', 'EM051', 'EM053', 'PRJ005', 'TS1'),
    ('T000025', N'Implement backup infrastructure', N'Cài đặt và cấu hình cơ sở hạ tầng sao lưu cho công ty', CONVERT(SMALLDATETIME, '20-05-2023 02:45 PM', 105), CONVERT(SMALLDATETIME, '30-11-2023 10:30 AM', 105), '0', 'EM051', 'EM054', 'PRJ005', 'TS1'),
    ('T000026', N'Manage IT infrastructure', N'Quản lý và duy trì cơ sở hạ tầng công nghệ thông tin cho công ty', CONVERT(SMALLDATETIME, '25-05-2023 02:45 PM', 105), CONVERT(SMALLDATETIME, '30-11-2023 10:30 AM', 105), '0', 'EM051', 'EM054', 'PRJ005', 'TS1');
GO


-- leave requests
CREATE TABLE LeaveStatuses(
    ID varchar(20) PRIMARY KEY NOT NULL,
    StatusName nvarchar(50)
)
GO
INSERT INTO LeaveStatuses(ID, StatusName)
VALUES
    ('LS1', N'Chấp nhận'),
    ('LS2', N'Chưa giải quyết'),
    ('LS3', N'Từ chối');
GO
CREATE TABLE LeaveRequests(
    ID varchar(20) PRIMARY KEY NOT NULL,
    Reason nvarchar(255),
    Notes nvarchar(255),
    CreatedDate date,
    StartDate date, -- at least 7 days before the creation date
    EndDate date,
    StatusID varchar(20) DEFAULT '',
    EmployeeID varchar(20) DEFAULT '',
    ApproverID varchar(20) DEFAULT ''
)
GO
INSERT INTO LeaveRequests(ID, EmployeeID, Reason, Notes, CreatedDate, StartDate, EndDate, StatusID, ApproverID)
VALUES
    ('LEA0001', 'EM007', N'Nghỉ do bị ốm', N'ghi chú 1', '2023-04-01', '2023-04-08', '2023-04-09', 'LS1', 'EM006'),
    ('LEA0002', 'EM008', N'Nghỉ đi khám bệnh', N'ghi chú 2', '2023-04-01', '2023-04-10', '2023-04-06', 'LS1', 'EM006');
GO

-- check-in-out
CREATE TABLE TimeSheets(
    ID varchar(20) PRIMARY KEY NOT NULL,
    CheckInTime SMALLDATETIME NOT NULL,
    CheckOutTime SMALLDATETIME,
    EmployeeID varchar(20) DEFAULT '',
    TaskCheckInID varchar(20) DEFAULT ''
)
GO
INSERT INTO TimeSheets(ID, CheckInTime, CheckOutTime, EmployeeID, TaskCheckInID)
VALUES
    ('TSH00001', '2023-04-10 08:30:00', '2023-04-10 12:00:00', 'EM007', 'T000001'),
    ('TSH00002', '2023-04-11 13:30:00', '2023-04-11 16:00:00', 'EM008', 'T000002');
GO
CREATE TABLE TaskCheckOuts(
    UpdateDate SMALLDATETIME NOT NULL,
    Progress varchar(4) NOT NULL,
    TimeSheetID varchar(20) DEFAULT '',
    TaskID varchar(20) DEFAULT '',
    PRIMARY KEY (TimeSheetID, TaskID)
);
GO
INSERT INTO TaskCheckOuts(UpdateDate, Progress, TimeSheetID, TaskID)
VALUES
    ('2023-04-10 01:30 PM', '50', 'TSH00001', 'T000001'),
    ('2023-04-10 11:30 PM', '30', 'TSH00002', 'T000002');
GO


-- set KPI
-- set by month
CREATE TABLE KPIs(
    ID varchar(20) PRIMARY KEY NOT NULL,
    MonthYear date,
    RequiredTasksCount int,
    ActualTasksCount int,
    EmployeeID varchar(20) DEFAULT ''
);
GO
INSERT INTO KPIs(ID, MonthYear, RequiredTasksCount, ActualTasksCount, EmployeeID)
VALUES
    ('KPI00001', '2023-04-01', 10, 0, 'EM007'),
    ('KPI00002', '2023-04-01', 5, 0, 'EM008');
GO

-- salary (store salary of each employee by month)
CREATE TABLE ProjectBonuses(
    ID varchar(20) PRIMARY KEY NOT NULL,
    Amount DECIMAL(19,4),
    ReceivedDate SMALLDATETIME,
    EmployeeID varchar(20) DEFAULT '',
    ProjectID varchar(20) DEFAULT ''
);
GO
INSERT INTO ProjectBonuses(ID, Amount, ReceivedDate, EmployeeID, ProjectID)
VALUES
    ('PB000001', 10000.00, '2023-3-15', 'EM006', 'PRJ001');
GO
GO

CREATE TABLE SalaryRecords(
    ID varchar(20) PRIMARY KEY NOT NULL,
    EmployeeID varchar(20) NOT NULL,
    MonthYear date,
    TotalWorkdays int,
    TotalBonus DECIMAL(19,4),
    Income DECIMAL(19,4) --Income = BaseSalary (from Employees) * (TotalWorkdays/30) + Bonus
);
GO
INSERT INTO SalaryRecords(ID, EmployeeID, MonthYear, TotalWorkdays, TotalBonus, Income)
VALUES
    ('SR00001', 'EM006', '2023-3-1', '30', 10000.00, 15010000.00);
GO

-- -- Project plans
CREATE TABLE Milestones(
    ID varchar(20) PRIMARY KEY,
    Title nvarchar(255),
    Explanation nvarchar(1500),
    StartDate SMALLDATETIME,
    EndDate SMALLDATETIME,
    CompletedDate SMALLDATETIME,
    OwnerID varchar(20) DEFAULT '',
    ProjectID varchar(20) DEFAULT ''
);
GO
CREATE TABLE MileTasks(
    MileID varchar(20),
    TaskID varchar(20), -- task in ProjectID of Milestones table
    PRIMARY KEY (MileID, TaskID)
)
GO

INSERT INTO Milestones(ID, Title, Explanation, StartDate, EndDate, CompletedDate, OwnerID, ProjectID)
VALUES
    ('MST0001', N'Thiết kế giải pháp', N'Giao đoạn quan trọng', CONVERT(SMALLDATETIME, '01-01-2023 08:00 AM', 105), CONVERT(SMALLDATETIME, '01-04-2023 05:00 PM', 105), '2000-01-01 00:00:00', 'EM006', 'PRJ001');
GO
INSERT INTO MileTasks(MileID, TaskID)
VALUES
    ('MST0001', 'T000001'),
    ('MST0001', 'T000002');
GO
