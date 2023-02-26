CREATE TABLE Employee(
	ID varchar(20),
	FName varchar(100),
	LName varchar(100),
	Gentle varchar(3),
	Birthday date,
	Ssn varchar(20),
	Phone varchar(11),
	MgrID varchar(20),
	Isalary float,
	Light int
);
-- Quy ước
/* 
Trạng thái 1: Đang làm việc
Trạng thái 2: Đang công tác
Trạng thái 3: Đang nghỉ theo chế độ
*/

CREATE TABLE Account(
	Username varchar (100),
	Pass varchar(100),
	ID varchar(20)
);

CREATE TABLE Department(
	Dnumber varchar (20),
	Dname varchar (100),
	MgrID varchar(20)
);

CREATE TABLE Attendance(
	Anumber varchar (20),
	ID varchar (20),
	Workshift varchar (30),
	Checkin time,
	Checkout time,
	WorkDate date
);

CREATE TABLE AttendanceTable(
	ATnumber varchar(20),
	ID varchar (20),
	Workdate date,
	ToTalTime int,
	Light int,
);

CREATE TABLE WorkShift(
	WSnumber varchar(20),
	WSname varchar(20),
	Checkin time,
	Checkout time,
	ID varchar(20),
	WorkDate date
);

CREATE TABLE LeaveApplication(
	LFnumber varchar (20),
	ID varchar (20),
	LFdate date,
	Reason varchar (500),
	Light int
);

CREATE TABLE PayCheck(
	PCnumber varchar(20),
	ID varchar (20),
	Isalary float,
	WSsalary float,
	Asalary float, --Allowance
	Dsalary float, --Deduct
	Salary float
);

CREATE TABLE Tasks(
	Tnumber varchar (20),
	Criterianame varchar (100),
	Unit varchar (50),
	Light varchar (20)
);

CREATE TABLE Light(
	Tnumber varchar (20),
	Overcome int,
	Achive int, 
	TryHarder int,
	Fail int,
);

INSERT INTO Employee(ID, FName, LName, Gentle, Birthday, Ssn, Phone, MgrID, Isalary, Light)
VALUES ('MGR0001', 'Quang', 'Le Hai', 'Nam', '1985-02-13' ,'1234567890', '098273465', null, 30000000 ,1)

INSERT INTO Employee(ID, FName, LName, Gentle, Birthday, Ssn, Phone, MgrID, Isalary, Light)
VALUES ('MGR0002', 'Anh', 'Dang Thi', 'Nu', '1983-05-25','1234567890', '098273465', null, 30000000 ,1)

INSERT INTO Employee(ID, FName, LName, Gentle, Birthday, Ssn, Phone, MgrID, Isalary, Light)
VALUES ('WRK0001', 'Nhan', 'Le Hoang Ngoc', 'Nu', '2000-12-29','1234567890', '098273464', 'MGR0001', 15000000 ,1)

INSERT INTO Employee(ID, FName, LName, Gentle, Birthday, Ssn, Phone, MgrID, Isalary, Light)
VALUES ('WRK0002', 'Le', 'Do Thi Ngoc', 'Nu','1997-12-24', '1234567891', '098273466', 'MGR0001', 15000000 ,1)

INSERT INTO Employee(ID, FName, LName, Gentle, Birthday, Ssn, Phone, MgrID, Isalary, Light)
VALUES ('WRK0003', 'Chuc', 'Ho Thi Ngoc', 'Nu','1989-11-03', '1234567892', '098273467', 'MGR0001', 15000000 ,1)

INSERT INTO Employee(ID, FName, LName, Gentle, Birthday, Ssn, Phone, MgrID, Isalary, Light)
VALUES ('WRK0004', 'Xuan', 'Mai Thanh', 'Nam', '1998-01-22','1234567893', '098273468', 'MGR0001', 15000000 ,1)

INSERT INTO Employee(ID, FName, LName, Gentle, Birthday, Ssn, Phone, MgrID, Isalary, Light)
VALUES ('WRK0005', 'Nhat', 'Le Hoang', 'Nam', '1988-02-05','1234567894', '098273469', 'MGR0001', 15000000 ,1)

INSERT INTO Employee(ID, FName, LName, Gentle, Birthday, Ssn, Phone, MgrID, Isalary, Light)
VALUES ('WRK0006', 'Kieu', 'Hoang Thanh', 'Nu', '1991-10-05','1234567895', '098273470', 'MGR0001', 15000000 ,1)

INSERT INTO Employee(ID, FName, LName, Gentle, Birthday, Ssn, Phone, MgrID, Isalary, Light)
VALUES ('WRK0007', 'Hoang', 'Do Thi Ngoc', 'Nu', '1989-09-25','1234567896', '098273471', 'MGR0001', 15000000 ,1)

INSERT INTO Employee(ID, FName, LName, Gentle, Birthday, Ssn, Phone, MgrID, Isalary, Light)
VALUES ('WRK0008', 'Cuong', 'Hoang Van', 'Nam', '1987-06-13','1234567897', '098273472', 'MGR0001', 15000000 ,1)

INSERT INTO Employee(ID, FName, LName, Gentle, Birthday, Ssn, Phone, MgrID, Isalary, Light)
VALUES ('WRK0009', 'Kien', 'Tran Quoc', 'Nam', '1999-09-05','1234567898', '098273473', 'MGR0001', 15000000 ,1)

INSERT INTO Employee(ID, FName, LName, Gentle, Birthday, Ssn, Phone, MgrID, Isalary, Light)
VALUES ('WRK0010', 'Phat', 'Le Thanh', 'Nam', '1997-08-16','1234567899', '098273474', 'MGR0002', 13000000 ,1)

INSERT INTO Employee(ID, FName, LName, Gentle, Birthday, Ssn, Phone, MgrID, Isalary, Light)
VALUES ('WRK0011', 'Tien', 'Nguyen Thanh', 'Nam', '1999-05-12','1234567880', '098273475', 'MGR0002', 13000000 ,1)

INSERT INTO Employee(ID, FName, LName, Gentle, Birthday, Ssn, Phone, MgrID, Isalary, Light)
VALUES ('WRK0012', 'Thanh', 'Le Xuan', 'Nu', '1996-06-19','1234567881', '098273476', 'MGR0002', 13000000 ,1)

INSERT INTO Employee(ID, FName, LName, Gentle, Birthday, Ssn, Phone, MgrID, Isalary, Light)
VALUES ('WRK0013', 'Khanh', 'Bach Cong', 'Nam', '1995-07-29','1234567882', '098273477', 'MGR0002', 13000000 ,1)

INSERT INTO Employee(ID, FName, LName, Gentle, Birthday, Ssn, Phone, MgrID, Isalary, Light)
VALUES ('WRK0014', 'An', 'Le Hoang Khanh', 'Nam', '1994-04-24','1234567883', '098273478', 'MGR0002', 13000000 ,1)

Select * From Employee;

INSERT INTO Department (Dnumber, Dname, MgrID)
VALUES ('D001', 'Thiet Ke', 'MGR0001')

INSERT INTO Department (Dnumber, Dname, MgrID)
VALUES ('D002', 'Tu Van', 'MGR0002')

Select * From Department;

INSERT INTO Account (Username, Pass, ID)
VALUES ('mgr0001','lehaiquang', 'MGR0001')

INSERT INTO Account (Username, Pass, ID)
VALUES ('mgr0002','dangthianh', 'MGR0002')

INSERT INTO Account (Username, Pass, ID)
VALUES ('wrk0001','lehoangngocnhan', 'WRK0001')

INSERT INTO Account (Username, Pass, ID)
VALUES ('wrk0002','dothingocle', 'WRK0002')

INSERT INTO Account (Username, Pass, ID)
VALUES ('wrk0003','hothingocchuc', 'WRK0003')

INSERT INTO Account (Username, Pass, ID)
VALUES ('wrk0004','maithanhxuan', 'WRK0004')

INSERT INTO Account (Username, Pass, ID)
VALUES ('wrk0005','lehoangnhat', 'WRK0005')

INSERT INTO Account (Username, Pass, ID)
VALUES ('wrk0006','hoangthanhkieu', 'WRK0006')

INSERT INTO Account (Username, Pass, ID)
VALUES ('wrk0007','dothingochoang', 'WRK0007')

INSERT INTO Account (Username, Pass, ID)
VALUES ('wrk0008','hoangvancuong', 'WRK0008')

INSERT INTO Account (Username, Pass, ID)
VALUES ('wrk0009','tranquockien', 'WRK0009')

INSERT INTO Account (Username, Pass, ID)
VALUES ('wrk0010','lethanhphat', 'WRK0010')

INSERT INTO Account (Username, Pass, ID)
VALUES ('wrk0011','nguyenthanhtien', 'WRK0011')

INSERT INTO Account (Username, Pass, ID)
VALUES ('wrk0012','lexuanthanh', 'WRK0012')

INSERT INTO Account (Username, Pass, ID)
VALUES ('wrk0013','bachcongkhanh', 'WRK0013')

INSERT INTO Account (Username, Pass, ID)
VALUES ('wrk0014','lehoangkhanhan', 'WRK0014')

Select * From Account;
