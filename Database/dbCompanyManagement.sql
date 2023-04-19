CREATE DATABASE CompanyManagement
GO
USE CompanyManagement
GO

CREATE TABLE Position(
                         PositionID varchar(20) PRIMARY KEY,
                         PositionName nvarchar(50)
);
GO
CREATE TABLE Employee(
                         EmployeeID varchar (20) PRIMARY KEY,
                         EmployeeName nvarchar(100),
                         Gender nvarchar(10),
                         Birthday date,
                         IdentifyCard varchar(12),
                         Email varchar(50),
                         PhoneNumber varchar (10),
                         EmployeeAddress nvarchar(255),
                         DepartmentID varchar(20),
                         PositionID varchar(20),
                         Salary int
);
GO
ALTER TABLE Employee ADD CONSTRAINT FK_EmployeePosition FOREIGN KEY(PositionID) REFERENCES Position(PositionID)
GO
CREATE TABLE Account(
                        AccountUsername varchar(100),
                        AccountPassword varchar(100),
                        EmployeeID varchar(20) PRIMARY KEY,
);
GO
ALTER TABLE Account ADD CONSTRAINT FK_AccountEmployee FOREIGN KEY(EmployeeID) REFERENCES Employee(EmployeeID)
GO
CREATE TABLE Department(
                           DepartmentID varchar(20) PRIMARY KEY,
                           DepartmentName nvarchar(100),
                           ManagerID varchar(20)
);
GO
ALTER TABLE Department ADD CONSTRAINT FK_DepartmentEmployee
    FOREIGN KEY(ManagerID) REFERENCES Employee(EmployeeID)
GO
CREATE TABLE ProjectStatus(
                              ProjectStatusID varchar(10) PRIMARY KEY,
                              ProjectStatusName nvarchar(50)
);
GO
CREATE TABLE Project(
                        ProjectID varchar(20) PRIMARY KEY,
                        ProjectName nvarchar(225),
                        CreateDate SMALLDATETIME,
                        EndDate SMALLDATETIME,
                        CompletedDate SMALLDATETIME,
                        Progress varchar(4),
                        ProjectStatusID varchar(10),
                        CreateBy varchar(20)
);
GO
ALTER TABLE Project ADD CONSTRAINT FK_ProjectProjectStatus
    FOREIGN KEY(ProjectStatusID) REFERENCES ProjectStatus(ProjectStatusID)
GO
ALTER TABLE Project ADD CONSTRAINT FK_ProjectEmployee
    FOREIGN KEY(CreateBy) REFERENCES Employee(EmployeeID)
GO
CREATE TABLE ProjectAssignment(
                                  ProjectID varchar(20),
                                  DepartmentID varchar(20),
                                  PRIMARY KEY(ProjectID, DepartmentID)
);
GO
ALTER TABLE ProjectAssignment ADD CONSTRAINT FK_ProjectAssignmentProject
    FOREIGN KEY(ProjectID) REFERENCES Project(ProjectID)
GO
ALTER TABLE ProjectAssignment ADD CONSTRAINT FK_ProjectAssignmentDepartment
    FOREIGN KEY(DepartmentID) REFERENCES Department(DepartmentID)
GO
CREATE TABLE TaskStatus(
                           TaskStatusID varchar(10) PRIMARY KEY,
                           TaskStatusName nvarchar(50)
);
GO
CREATE TABLE TaskPriority(
                             TaskPriorityID varchar(20) PRIMARY KEY,
                             TaskPriorityName varchar(20)
);
GO
CREATE TABLE Task(
                     TaskID varchar(20) PRIMARY KEY,
                     Title nvarchar(50),
                     TaskDescription nvarchar(255),
                     AssignDate SMALLDATETIME,
                     Deadline SMALLDATETIME,
                     CreateBy varchar(20),
                     Progress varchar(4),
                     EmployeeID varchar(20),
                     ProjectID varchar(20),
                     TaskPriorityID varchar(20),
                     TaskStatusID varchar(10)
);
GO
ALTER TABLE Task ADD CONSTRAINT FK_TaskManager
    FOREIGN KEY(CreateBy) REFERENCES Employee(EmployeeID)
GO
ALTER TABLE Task ADD CONSTRAINT FK_TaskEmployee
    FOREIGN KEY(EmployeeID) REFERENCES Employee(EmployeeID)
GO
ALTER TABLE Task ADD CONSTRAINT FK_TaskProject
    FOREIGN KEY(ProjectID) REFERENCES Project(ProjectID)
GO
ALTER TABLE Task ADD CONSTRAINT FK_TaskPriority
    FOREIGN KEY(TaskPriorityID) REFERENCES TaskPriority(TaskPriorityID)
GO
ALTER TABLE Task ADD CONSTRAINT FK_TaskStatus
    FOREIGN KEY(TaskStatusID) REFERENCES TaskStatus(TaskStatusID)
GO

-- check-in-out
CREATE TABLE CheckInOut(
                           ID varchar(20) PRIMARY KEY,
                           EmployeeID varchar(20),
                           CheckInTime SMALLDATETIME,
                           CheckOutTime SMALLDATETIME,
                           CheckOutStatus BIT DEFAULT 0,
                           TaskID varchar(20)
)
GO
ALTER TABLE CheckInOut ADD CONSTRAINT FK_CheckInOutEmployee
    FOREIGN KEY(EmployeeID) REFERENCES Employee(EmployeeID)
GO
ALTER TABLE CheckInOut ADD CONSTRAINT FK_CheckInOutTask
    FOREIGN KEY(TaskID) REFERENCES Task(TaskID)
GO
CREATE TABLE CompletedTask(
                              CheckInOutID varchar(20),
                              TaskID varchar(20) PRIMARY KEY
)
GO
ALTER TABLE CompletedTask ADD CONSTRAINT FK_CompletedTaskCheckInOut
    FOREIGN KEY(CheckInOutID) REFERENCES CheckInOut(ID)
GO
ALTER TABLE CompletedTask ADD CONSTRAINT FK_CompletedTaskTask
    FOREIGN KEY(TaskID) REFERENCES Task(TaskID)
GO

-- request for leave
CREATE TABLE LeaveType(
                          LeaveTypeID varchar(20) PRIMARY KEY,
                          LeaveTypeName nvarchar(20),
)
GO
CREATE TABLE LeaveStatus(
                            LeaveStatusID varchar(20) PRIMARY KEY,
                            LeaveStatusName nvarchar(50)
)
GO
CREATE TABLE Leave(
                      ID varchar(20) PRIMARY KEY,
                      EmployeeID varchar(20),
                      LeaveTypeID varchar(20),
                      LeaveReason nvarchar(255),
                      StartDate SMALLDATETIME,
                      EndDate SMALLDATETIME,
                      LeaveStatusID varchar(20),
                      CreatedDate SMALLDATETIME,
                      ApprovedBy varchar(20),
                      Notes nvarchar(255)
)
GO
ALTER TABLE Leave ADD CONSTRAINT FK_LeaveType
    FOREIGN KEY(LeaveTypeID) REFERENCES LeaveType(LeaveTypeID)
GO
ALTER TABLE Leave ADD CONSTRAINT FK_LeaveStatus
    FOREIGN KEY(LeaveStatusID) REFERENCES LeaveStatus(LeaveStatusID)
GO
ALTER TABLE Leave ADD CONSTRAINT FK_LeaveEmployee
    FOREIGN KEY(ApprovedBy) REFERENCES Employee(EmployeeID)
GO
CREATE TABLE TaskDailyProgress(
					  TaskID varchar(20),
					  ProgressDate Date,
					  CompletionPercentage varchar(4),
					  PRIMARY KEY (TaskID, ProgressDate)
);
GO
ALTER TABLE TaskDailyProgress ADD CONSTRAINT FK_TaskDailyProgress
    FOREIGN KEY(TaskID) REFERENCES Task(TaskID)
---------------------------------------------------------------------------------------------------------------------------------------------------------
GO
INSERT INTO Position(PositionID, PositionName)
VALUES
    ('1', N'Quản lý'),
    ('2', N'Trưởng phòng'),
    ('3', N'Nhân viên')
GO
---------------------------------------------------------------------------------------------------------------------------------------------------------
INSERT INTO Employee (EmployeeID, EmployeeName, Gender, Birthday, IdentifyCard, Email, PhoneNumber, EmployeeAddress, DepartmentID, PositionID, Salary)
VALUES
    ('EM001', N'Nguyễn Văn An', 'Nam', CONVERT(DATE, '01-01-1990', 105), '001234567890', 'an.nguyen@it.company.com', '0123456789', N'TP. Hồ Chí Minh', '', '1', 15000000),
    ('EM002', N'Trần Thị Bình', N'Nữ', CONVERT(DATE, '02-02-1991', 105), '001234567891', 'binh.tran@it.company.com', '0234567890', N'Bình Dương', '', '1', 15000000),
    ('EM003', N'Lê Văn Cường', 'Nam', CONVERT(DATE, '03-03-1992', 105), '001234567892', 'cuong.le@it.company.com', '0345678901', N'Đồng Nai', '', '1', 15000000),
    ('EM004', N'Nguyễn Thị Dung', N'Nữ', CONVERT(DATE, '04-04-1993', 105), '001234567893', 'dung.nguyen@it.company.com', '0456789012', N'TP. Hồ Chí Minh', '', '1', 15000000),
    ('EM005', N'Phạm Văn Duy', 'Nam', CONVERT(DATE, '05-05-1994', 105), '001234567894', 'duy.pham@it.company.com', '0567890123', N'Bình Dương', '', '1', 15000000),
    ('EM006', N'Lê Thị Hà', N'Nữ', CONVERT(DATE, '06-06-1995', 105), '001234567895', 'ha.le@it.company.com', '0678901234', N'TP. Hồ Chí Minh', 'DPM001', '2', 8000000),
    ('EM007', N'Nguyễn Văn Hoàng', 'Nam', CONVERT(DATE, '07-07-1996', 105), '001234567896', 'hoang.nguyen@it.company.com', '0789012345', N'Đồng Nai', 'DPM001', '3', 8000000),
    ('EM008', N'Trần Thị Hương', N'Nữ', CONVERT(DATE, '08-08-1997', 105), '001234567897', 'huong.tran@it.company.com', '0890123456', N'Bình Phước', 'DPM001', '3', 8000000),
    ('EM009', N'Nguyễn Văn Khoa', 'Nam', CONVERT(DATE, '09-09-1998', 105), '001234567898', 'khoa.nguyen@it.company.com', '0901234567', N'TP. Hồ Chí Minh', 'DPM001', '3', 8000000),
    ('EM010', N'Lê Thị Lan', N'Nữ', CONVERT(DATE, '10-10-1999', 105), '001234567899', 'lan.le@it.company.com', '0912345678', N'Bình Dương', 'DPM001', '3', 8000000),
    ('EM011', N'Phan Văn Minh', 'Nam', CONVERT(DATE, '11-11-2000', 105), '001234567900', 'minh.phan@it.company.com', '0923456789', N'Đồng Nai', 'DPM001', '3', 8000000),
    ('EM012', N'Nguyễn Thị Ngọc', N'Nữ', CONVERT(DATE, '12-12-2001', 105), '001234567901', 'ngoc.nguyen@it.company.com', '0934567890', N'TP. Hồ Chí Minh', 'DPM001', '3', 8000000),
    ('EM013', N'Trần Văn Phong', 'Nam', CONVERT(DATE, '13-01-1990', 105), '001234567902', 'phong.tran@it.company.com', '0945678901', N'Bình Dương', 'DPM001', '3', 8000000),
    ('EM014', N'Lê Thị Quỳnh', N'Nữ', CONVERT(DATE, '14-02-1991', 105), '001234567903', 'quynh.le@it.company.com', '0956789012', N'TP. Hồ Chí Minh', 'DPM001', '3', 8000000),
    ('EM015', N'Nguyễn Văn Sơn', 'Nam', CONVERT(DATE, '15-03-1992', 105), '001234567904', 'son.nguyen@it.company.com', '0967890123', N'Bình Phước', 'DPM001', '3', 8000000),
    ('EM016', N'Trần Văn Tâm', 'Nam', CONVERT(DATE, '16-04-1993', 105), '001234567905', 'tam.tran@it.company.com', '0978901234', N'Đồng Nai', 'DPM001', '3', 8000000),
    ('EM017', N'Phạm Thị Uyên', N'Nữ', CONVERT(DATE, '17-05-1994', 105), '001234567906', 'uyen.pham@it.company.com', '0089012345', N'TP. Hồ Chí Minh', 'DPM001', '3', 8000000),
    ('EM018', N'Lê Văn Vĩ', 'Nam', CONVERT(DATE, '18-06-1995', 105), '001234567907', 'vi.le@it.company.com', '0190123456', N'Bình Dương', 'DPM001', '3', 8000000),
    ('EM019', N'Nguyễn Thị Xuân', N'Nữ', CONVERT(DATE, '19-07-1996', 105), '001234567908', 'xuan.nguyen@it.company.com', '0201234567', N'Đồng Nai', 'DPM001', '3', 8000000),
    ('EM020', N'Trần Văn Yên', 'Nam', CONVERT(DATE, '20-08-1997', 105), '001234567909', 'yen.tran@it.company.com', '0212345678', N'Bình Nhước', 'DPM001', '3', 8000000),
    ('EM021', N'Nguyễn Thị Vân', N'Nữ', CONVERT(DATE, '21-09-1998', 105), '001234567910', 'van.nguyen@it.company.com', '0223456789', N'TP. Hồ Chí Minh', 'DPM001', '3', 8000000),
    ('EM022', N'Trần Thị Ánh', N'Nữ', CONVERT(DATE, '22-10-1999', 105), '001234567911', 'anh.tran@it.company.com', '0234567890', N'TP. Hồ Chí Minh', 'DPM001', '3', 8000000),
    ('EM023', N'Nguyễn Văn Bảo', 'Nam', CONVERT(DATE, '23-11-2000', 105), '001234567912', 'bao.nguyen@it.company.com', '0345678901', N'Bình Dương', 'DPM001', '3', 8000000),
    ('EM024', N'Lê Thị Cẩm', N'Nữ', CONVERT(DATE, '24-12-2001', 105), '001234567913', 'cam.le@it.company.com', '0456789012', N'Đồng Nai', 'DPM002', '3', 8000000),
    ('EM025', N'Phan Văn Đạt', 'Nam', CONVERT(DATE, '25-01-1990', 105), '001234567914', 'dat.phan@it.company.com', '0567890123', N'TP. Hồ Chí Minh', 'DPM002', '2', 8000000),
    ('EM026', N'Nguyễn Thị Eo', N'Nữ', CONVERT(DATE, '26-02-1991', 105), '001234567915', 'eo.nguyen@it.company.com', '0678901234', N'Bình Phước', 'DPM002', '3', 8000000),
    ('EM027', N'Trần Văn Phan', 'Nam', CONVERT(DATE, '27-03-1992', 105), '001234567916', 'phan.tran@it.company.com', '0789012345', N'TP. Hồ Chí Minh', 'DPM002', '3', 8000000),
    ('EM028', N'Lê Thị Gấm', N'Nữ', CONVERT(DATE, '28-04-1993', 105), '001234567917', 'gam.le@it.company.com', '0890123456', N'Bình Dương', 'DPM002', '3', 8000000),
    ('EM029', N'Nguyễn Văn Hải', 'Nam', CONVERT(DATE, '29-05-1994', 105), '001234567918', 'hai.nguyen@it.company.com', '0901234567', N'Đồng Nai', 'DPM002', '3', 8000000),
    ('EM030', N'Trần Thị Linh', N'Nữ', CONVERT(DATE, '30-06-1995', 105), '001234567919', 'linh.tran@it.company.com', '0912345678', N'TP. Hồ Chí Minh', 'DPM002', '3', 8000000),
    ('EM031', N'Nguyễn Văn Khôi', 'Nam', CONVERT(DATE, '01-07-1996', 105), '001234567920', 'khoi.nguyen@it.company.com', '0923456789', N'Bình Phước', 'DPM002', '3', 8000000),
    ('EM032', N'Lê Thị Lộc', N'Nữ', CONVERT(DATE, '02-08-1997', 105), '001234567921', 'loc.le@it.company.com', '0934567890', N'TP. Hồ Chí Minh', 'DPM002', '3', 8000000),
    ('EM033', N'Nguyễn Thị Mỹ', N'Nữ', CONVERT(DATE, '03-09-1998', 105), '001234567922', 'my.nguyen@it.company.com', '0945678901', N'Bình Dương', 'DPM002', '3', 8000000),
    ('EM034', N'Trần Văn Nghĩa', 'Nam', CONVERT(DATE, '04-10-1999', 105), '001234567923', 'nghia.tran@it.company.com', '0756789012', N'Đồng Nai', 'DPM002', '3', 8000000),
    ('EM035', N'Lê Thị Oanh', N'Nữ', CONVERT(DATE, '05-11-2000', 105), '001234567924', 'oanh.le@it.company.com', '0767890123', N'TP. Hồ Chí Minh', 'DPM003', '2', 8000000),
    ('EM036', N'Phan Văn Phúc', 'Nam', CONVERT(DATE, '06-12-1991', 105), '001234567925', 'phuc.phan@it.company.com', '0878901234', N'Bình Phước', 'DPM003', '3', 8000000),
    ('EM037', N'Nguyễn Thị Quyên', N'Nữ', CONVERT(DATE, '07-01-1992', 105), '001234567926', 'quyen.nguyen@it.company.com', '0989012345', N'TP. Hồ Chí Minh', 'DPM003', '3', 8000000),
    ('EM038', N'Trần Văn R', 'Nam', CONVERT(DATE, '08-02-1993', 105), '001234567927', 'r.tran@it.company.com', '0890123456', N'Bình Dương', 'DPM003', '3', 8000000),
    ('EM039', N'Lê Thị Sương', N'Nữ', CONVERT(DATE, '09-03-1994', 105), '001234567928', 'suong.le@it.company.com', '0701234567', N'Đồng Nai', 'DPM003', '3', 8000000),
    ('EM040', N'Phan Văn Tài', 'Nam', CONVERT(DATE, '10-04-1995', 105), '001234567929', 'tai.phan@it.company.com', '0312345678', N'TP. Hồ Chí Minh', 'DPM003', '3', 8000000),
    ('EM041', N'Nguyễn Thị Uyên', N'Nữ', CONVERT(DATE, '11-05-1996', 105), '001234567930', 'uyen.nguyen@it.company.com', '0923456789', N'Bình Phước', 'DPM003', '3', 8000000),
    ('EM042', N'Trần Văn Vượng', 'Nam', CONVERT(DATE, '12-06-1997', 105), '001234567931', 'vuong.tran@it.company.com', '0934567890', N'TP. Hồ Chí Minh', 'DPM003', '3', 8000000),
    ('EM043', N'Lê Thị Xuyến', N'Nữ', CONVERT(DATE, '13-07-1998', 105), '001234567932', 'xuyen.le@it.company.com', '0845678901', N'Bình Dương', 'DPM003', '3', 8000000),
    ('EM044', N'Nguyễn Thị Mĩ Diệu', N'Nữ', CONVERT(DATE, '03-09-1998', 105), '001234567922', 'my.nguyen@it.company.com', '0945678901', N'Bình Dương', 'DPM003', '3', 8000000),
    ('EM045', N'Trần Văn Nghĩa', 'Nam', CONVERT(DATE, '04-10-1999', 105), '001234567923', 'nghia.tran@it.company.com', '0756789012', N'Đồng Nai', 'DPM004', '2', 8000000),
    ('EM046', N'Lê Thị Oanh', N'Nữ', CONVERT(DATE, '05-11-2000', 105), '001234567924', 'oanh.le@it.company.com', '0967890123', N'TP. Hồ Chí Minh', 'DPM004', '3', 8000000),
    ('EM047', N'Phan Văn Phúc', 'Nam', CONVERT(DATE, '06-12-1991', 105), '001234567925', 'phuc.phan@it.company.com', '0378901234', N'Bình Phước', 'DPM004', '3', 8000000),
    ('EM048', N'Nguyễn Thị Quyên', N'Nữ', CONVERT(DATE, '07-01-1992', 105), '001234567926', 'quyen.nguyen@it.company.com', '0389012345', N'TP. Hồ Chí Minh', 'DPM004', '3', 8000000),
    ('EM049', N'Trần Văn Giang', 'Nam', CONVERT(DATE, '08-02-1993', 105), '001234567927', 'giang.tran@it.company.com', '0990123456', N'Bình Dương', 'DPM004', '3', 8000000),
    ('EM050', N'Trần Văn Tài', N'Nam', CONVERT(DATE, '01-01-1990', 105), '123456789012', 'tai.tran@it.company.com', '0123456789', N'Hồ Chí Minh', 'DPM005', '1', 15000000),
    ('EM051', N'Lê Thị Hồng', N'Nữ', CONVERT(DATE, '02-02-1995', 105), '123456789013', 'hong.le@it.company.com', '0234567890', N'Cần Thơ', 'DPM005', '2', 8000000),
    ('EM052', N'Nguyễn Đình Huy', N'Nam', CONVERT(DATE, '03-03-1992', 105), '123456789014', 'huy.nguyen@it.company.com', '0345678901', N'Vũng Tàu', 'DPM005', '3', 8000000),
    ('EM053', N'Phạm Thị Ngọc', N'Nữ', CONVERT(DATE, '04-04-1991', 105), '123456789015', 'ngoc.pham@it.company.com', '0456789012', N'Hồ Chí Minh', 'DPM005', '3', 8000000),
    ('EM054', N'Lương Thị Vân', N'Nữ', CONVERT(DATE, '05-05-1994', 105), '123456789016', 'van.luong@it.company.com', '0567890123', N'Cà Mau', 'DPM005', '3', 8000000),
    ('EM055', N'Đặng Văn Đức', N'Nam', CONVERT(DATE, '06-06-1993', 105), '123456789017', 'duc.dang@it.company.com', '0678901234', N'Đồng Nai', 'DPM005', '3', 8000000);
GO
---------------------------------------------------------------------------------------------------------------------------------------------------------
INSERT INTO Department (DepartmentID, DepartmentName, ManagerID)
VALUES
    ('DPM001', 'Software Development Department', 'EM001'),
    ('DPM002', 'Web Development', 'EM002'),
    ('DPM003', 'Technology and Communication', 'EM003'),
    ('DPM004', 'Artificial Intelligence', 'EM004'),
    ('DPM005', 'Infrastructure Department', 'EM005'),
    ('DPM006', 'Finance and Accounting', 'EM005');
GO
---------------------------------------------------------------------------------------------------------------------------------------------------------
INSERT INTO Account (AccountUsername, AccountPassword, EmployeeID)
SELECT CONCAT(EmployeeID, SUBSTRING(CONVERT(varchar, Birthday, 103), 1, 2), SUBSTRING(CONVERT(varchar, Birthday, 103), 4, 2)),
       '@1234567', EmployeeID
FROM Employee;
GO
---------------------------------------------------------------------------------------------------------------------------------------------------------
INSERT INTO ProjectStatus(ProjectStatusID, ProjectStatusName)
VALUES
    ( '0', N'Đã hủy'),
    ( '1', N'Đang thu thập yêu cầu'),
    ( '2', N'Đang thiết kế và phát triển'),
    ( '3', N'Đang kiểm thử và đánh giá'),
    ( '4', N'Đang triển khai'),
    ( '5', N'Đang bảo trì và hỗ trợ'),
    ( '6', N'Đang nâng cấp và cải tiến'),
    ( '7', N'Đang đình chỉ');
GO
---------------------------------------------------------------------------------------------------------------------------------------------------------
INSERT INTO Project (ProjectID, ProjectName, CreateDate, EndDate, CompletedDate, Progress, ProjectStatusID, CreateBy)
VALUES
    ('PRJ001', 'Website Development', CONVERT(SMALLDATETIME, '01-01-2023 08:00 AM', 105), CONVERT(SMALLDATETIME, '30-06-2023 05:00 PM', 105), CONVERT(SMALLDATETIME, '01-01-2000 00:00 AM', 105), '50','4', 'EM001'),
    ('PRJ002', 'Mobile App Development', CONVERT(SMALLDATETIME, '01-02-2023 09:30 AM', 105), CONVERT(SMALLDATETIME, '31-08-2023 07:00 PM', 105), CONVERT(SMALLDATETIME, '01-01-2000 00:00 AM', 105),'35', '4', 'EM002'),
    ('PRJ003', 'Database Management System', CONVERT(SMALLDATETIME, '01-03-2023 10:15 AM', 105), CONVERT(SMALLDATETIME, '31-10-2023 04:30 PM', 105), CONVERT(SMALLDATETIME, '01-01-2000 00:00 AM', 105),'10', '4', 'EM003'),
    ('PRJ004', 'Artificial Intelligence Research', CONVERT(SMALLDATETIME, '01-04-2023 01:00 PM', 105), CONVERT(SMALLDATETIME, '31-03-2024 11:00 AM', 105), CONVERT(SMALLDATETIME, '01-01-2000 00:00 AM', 105),'0', '1', 'EM004'),
    ('PRJ005', 'Cloud Computing Migration', CONVERT(SMALLDATETIME, '01-05-2023 02:45 PM', 105), CONVERT(SMALLDATETIME, '30-11-2023 10:30 AM', 105), CONVERT(SMALLDATETIME, '01-01-2000 00:00 AM', 105),'0', '1', 'EM050');
GO
---------------------------------------------------------------------------------------------------------------------------------------------------------

INSERT INTO ProjectAssignment (ProjectID, DepartmentID)
VALUES
    ('PRJ001', 'DPM001'),
    ('PRJ002', 'DPM002'),
    ('PRJ003', 'DPM003'),
    ('PRJ004', 'DPM004'),
    ('PRJ005', 'DPM005');
GO
---------------------------------------------------------------------------------------------------------------------------------------------------------
INSERT INTO TaskStatus(TaskStatusID, TaskStatusName)
VALUES
    ( '0', N'Đã hủy'),
    ( '1', N'Mở'),
    ( '2', N'Đang tiến hành'),
    ( '3', N'Đang xem lại'),
    ( '4', N'Sẽ được kiểm tra'),
    ( '5', N'Đang chờ'),
    ( '6', N'Đang hoãn'),
    ( '7', N'Đã đóng');
GO
---------------------------------------------------------------------------------------------------------------------------------------------------------
INSERT INTO TaskPriority(TaskPriorityID, TaskPriorityName)
VALUES
    ('1', N'Cao'),
    ('2', N'Trung bình'),
    ('3', N'Thấp');
GO
---------------------------------------------------------------------------------------------------------------------------------------------------------
INSERT INTO Task (TaskID, Title, TaskDescription, AssignDate, Deadline, CreateBy, Progress, EmployeeID, ProjectID, TaskPriorityID, TaskStatusID)
VALUES
    ('T000001', N'Website Development - Design', N'Thiết kế giao diện website cho khách hàng ABC', CONVERT(SMALLDATETIME, '01-03-2023 09:00 AM', 105), CONVERT(SMALLDATETIME, '15-03-2023 05:00 PM', 105), 'EM002', '50', 'EM003', 'PRJ001', '1', '2'),
    ('T000002', N'Website Development - Front-end', N'Lập trình phần front-end cho website khách hàng ABC', CONVERT(SMALLDATETIME, '16-03-2023 08:00 AM', 105), CONVERT(SMALLDATETIME, '31-03-2023 05:00 PM', 105), 'EM002', '30', 'EM007', 'PRJ001','1', '2'),
    ('T000003', N'Website Development - Back-end', N'Lập trình phần back-end cho website khách hàng ABC', CONVERT(SMALLDATETIME, '01-04-2023 08:00 AM', 105), CONVERT(SMALLDATETIME, '15-04-2023 05:00 PM', 105), 'EM002', '10', 'EM009', 'PRJ001', '1', '2'),
    ('T000004', N'Website Development - Testing', N'Kiểm thử và sửa lỗi cho website khách hàng ABC', CONVERT(SMALLDATETIME, '16-04-2023 08:00 AM', 105), CONVERT(SMALLDATETIME, '30-04-2023 05:00 PM', 105), 'EM002', '10', 'EM013', 'PRJ001', '2', '2'),
    ('T000005', N'Website Development - Deployment', N'Triển khai website khách hàng ABC trên server', CONVERT(SMALLDATETIME, '01-05-2023 08:00 AM', 105), CONVERT(SMALLDATETIME, '15-05-2023 05:00 PM', 105), 'EM002', '0', 'EM017', 'PRJ001', '1', '1'),
    ('T000006', N'Develop mobile app UI design', N'Phát triển thiết kế giao diện người dùng cho ứng dụng di động', CONVERT(SMALLDATETIME, '20-04-2023 02:30 PM', 105), CONVERT(SMALLDATETIME, '20-05-2023 05:00 PM', 105), 'EM001', '0', 'EM027', 'PRJ002', '1', '1'),
    ('T000007', N'Develop mobile app backend', N'Tạo backend cho ứng dụng di động', CONVERT(SMALLDATETIME, '15-05-2023 10:00 AM', 105), CONVERT(SMALLDATETIME, '30-06-2023 01:30 PM', 105), 'EM001', '0', 'EM026', 'PRJ002', '1', '1'),
    ('T000008', N'Develop mobile app frontend', N'Tạo frontend cho ứng dụng di động', CONVERT(SMALLDATETIME, '01-06-2023 09:15 AM', 105), CONVERT(SMALLDATETIME, '15-07-2023 11:45 AM', 105), 'EM001', '0', 'EM028', 'PRJ002','1', '1'),
    ('T000009', N'Test mobile app', N'Kiểm thử ứng dụng di động và báo cáo lỗi', CONVERT(SMALLDATETIME, '20-07-2023 02:00 PM', 105), CONVERT(SMALLDATETIME, '15-08-2023 04:30 PM', 105), 'EM001', '0', 'EM031', 'PRJ002','2', '1'),
    ('T000010', N'Deploy mobile app', N'Triển khai ứng dụng di động lên cửa hàng ứng dụng', CONVERT(SMALLDATETIME, '01-09-2023 10:30 AM', 105), CONVERT(SMALLDATETIME, '30-09-2023 03:00 PM', 105), 'EM001', '0', 'EM030', 'PRJ002','2', '1'),
    ('T000011', N'Create new database', N'Tạo cơ sở dữ liệu cho hệ thống quản lý nhân viên', CONVERT(SMALLDATETIME, '01-03-2023 10:15 AM', 105), CONVERT(SMALLDATETIME, '15-03-2023 04:30 PM', 105), 'EM003', '0', 'EM035', 'PRJ003','2', '1'),
    ('T000012', N'Optimize database', N'Tối ưu hóa cơ sở dữ liệu cho hệ thống quản lý nhân viên', CONVERT(DATETIME, '05-03-2023 10:15 AM', 105), CONVERT(DATETIME, '20-03-2023 04:30 PM', 105), 'EM003', '0', 'EM036', 'PRJ003','3', '1'),
    ('T000013', N'Collect information', N'Thu thập thông tin về các nhân viên trong công ty', CONVERT(SMALLDATETIME, '10-03-2023 10:15 AM', 105), CONVERT(SMALLDATETIME, '25-03-2023 04:30 PM', 105), 'EM003', '0', 'EM037', 'PRJ003', '1', '1'),
    ('T000014', N'Analyze data', N'Phân tích dữ liệu về các nhân viên trong công ty', CONVERT(SMALLDATETIME, '15-03-2023 10:15 AM', 105), CONVERT(SMALLDATETIME, '30-03-2023 04:30 PM', 105), 'EM003', '0', 'EM038', 'PRJ003','1', '1'),
    ('T000015', N'Perform data check', N'Thực hiện kiểm tra dữ liệu đã thu thập và phân tích', CONVERT(SMALLDATETIME, '20-03-2023 10:15 AM', 105), CONVERT(SMALLDATETIME, '31-10-2023 04:30 PM', 105), 'EM003', '0', 'EM039', 'PRJ003', '1', '1'),
    ('T000016', N'Cloud Computing Migration', N'Migrate các ứng dụng và dữ liệu hiện có đến nền tảng Cloud Computing', CONVERT(SMALLDATETIME, '01-05-2023 02:45 PM', 105), CONVERT(SMALLDATETIME, '30-11-2023 10:30 AM', 105), 'EM004', '0', 'EM045', 'PRJ004','1', '1'),
    ('T000017', N'Assess current infrastructure', N'Đánh giá cơ sở hạ tầng công nghệ thông tin hiện tại và xác định các khu vực cần được di chuyển đến đám mây.', CONVERT(SMALLDATETIME, '05-05-2023 02:45 PM', 105), CONVERT(SMALLDATETIME, '15-05-2023 04:30 PM', 105), 'EM004', '0', 'EM046', 'PRJ004','1', '1'),
    ('T000018', N'Select cloud provIDer', N'Nghiên cứu và lựa chọn nhà cung cấp đám mây phù hợp cho công ty', CONVERT(SMALLDATETIME, '10-05-2023 02:45 PM', 105), CONVERT(SMALLDATETIME, '25-05-2023 04:30 PM', 105), 'EM004', '0', 'EM047', 'PRJ004','1', '1'),
    ('T000019', N'Migrate applications', N'Migrate các ứng dụng hiện có đến nền tảng đám mây', CONVERT(SMALLDATETIME, '15-05-2023 02:45 PM', 105), CONVERT(SMALLDATETIME, '30-11-2023 10:30 AM', 105), 'EM004', '0', 'EM048', 'PRJ004', '1', '1'),
    ('T000020', N'Migrate data', N'Transfer dữ liệu của công ty đến nền tảng đám mây', CONVERT(SMALLDATETIME, '20-05-2023 02:45 PM', 105), CONVERT(SMALLDATETIME, '30-11-2023 10:30 AM', 105), 'EM004', '0', 'EM049', 'PRJ004', '1', '1'),
    ('T000021', N'Infrastructure Department', N'Triển khai và quản lý cơ sở hạ tầng công nghệ thông tin cho công ty', CONVERT(SMALLDATETIME, '01-05-2023 02:45 PM', 105), CONVERT(SMALLDATETIME, '30-11-2023 10:30 AM', 105), 'EM050', '0', 'EM050', 'PRJ005', '1', '1'),
    ('T000022', N'Set up network infrastructure', N'Cài đặt và cấu hình cơ sở hạ tầng mạng cho công ty', CONVERT(SMALLDATETIME, '05-05-2023 02:45 PM', 105), CONVERT(SMALLDATETIME, '15-05-2023 04:30 PM', 105), 'EM050', '0', 'EM051', 'PRJ005', '1', '1'),
    ('T000023', N'Implement server infrastructure', N'Cài đặt và cấu hình cơ sở hạ tầng máy chủ cho công ty', CONVERT(SMALLDATETIME, '10-05-2023 02:45 PM', 105), CONVERT(SMALLDATETIME, '25-05-2023 04:30 PM', 105), 'EM050', '0', 'EM052', 'PRJ005', '1', '1'),
    ('T000024', N'Set up security infrastructure', N'Cài đặt và cấu hình cơ sở hạ tầng bảo mật cho công ty', CONVERT(SMALLDATETIME, '15-05-2023 02:45 PM', 105), CONVERT(SMALLDATETIME, '30-11-2023 10:30 AM', 105), 'EM050', '0', 'EM053', 'PRJ005', '1', 1),
    ('T000025', N'Implement backup infrastructure', N'Cài đặt và cấu hình cơ sở hạ tầng sao lưu cho công ty', CONVERT(SMALLDATETIME, '20-05-2023 02:45 PM', 105), CONVERT(SMALLDATETIME, '30-11-2023 10:30 AM', 105), 'EM050', '0', 'EM054', 'PRJ005', '1', '1'),
    ('T000026', N'Manage IT infrastructure', N'Quản lý và duy trì cơ sở hạ tầng công nghệ thông tin cho công ty', CONVERT(SMALLDATETIME, '25-05-2023 02:45 PM', 105), CONVERT(SMALLDATETIME, '30-11-2023 10:30 AM', 105), 'EM050', '0', 'EM055', 'PRJ005', '1', '1');

INSERT INTO CheckInOut(ID, EmployeeID, CheckInTime, CheckOutTime, CheckOutStatus, TaskID)
VALUES
    ('CI00001', 'EM007', '2023-04-10 08:30:00', '2023-04-10 12:00:00', 1, 'T000001'),
    ('CI00002', 'EM008', '2023-04-11 13:30:00', '2023-04-11 16:00:00', 0, 'T000002');
GO
INSERT INTO CompletedTask(CheckInOutID, TaskID)
VALUES
    ('CI00001', 'T000001'),
    ('CI00001', 'T000002');
GO

INSERT INTO LeaveType(LeaveTypeID, LeaveTypeName)
VALUES
    ('LT1', N'Nghỉ bệnh'),
    ('LT2', N'Nghỉ cá nhân'),
    ('LT3', N'Nghỉ phép');
GO
INSERT INTO LeaveStatus(LeaveStatusID, LeaveStatusName)
VALUES
    ('LS1', N'chấp nhận'),
    ('LS2', N'chưa giải quyết'),
    ('LS3', N'từ chối');
GO
-- when taking a leave, one must request permission from 'department head'
INSERT INTO Leave(ID, EmployeeID, LeaveTypeID, LeaveReason, StartDate, EndDate, LeaveStatusID, CreatedDate, ApprovedBy, Notes)
VALUES
    ('LEA0001', 'EM007', 'LT1', N'Nghỉ do bị ốm', '2023-04-01', '2023-04-05', 'LS1', '2023-04-06', 'EM001', N'ghi chú 1'),
    ('LEA0002', 'EM008', 'LT2', N'Nghỉ đi khám bệnh', '2023-04-01', '2023-04-10', 'LS1', '2023-04-06', 'EM001', N'ghi chú 2');


