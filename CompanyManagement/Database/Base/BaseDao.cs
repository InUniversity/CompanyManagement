namespace CompanyManagement.Database.Base
{
    public abstract class BaseDao
    {
        public const string managerRole = "ER01";
        public const string deptHeadRole = "ER02";
        public const string hrRole = "ER03";
        public const string regularEmplRole = "ER04";

        public const string completed = "100";
        public const string leavRequestUpapproved = "LS2";
        public const string leavRequesApproved = "LS1";
        public const string leavRequesDenied = "LS3";
        public const string projRunningID = "PST1";
        public const string projCompletedID = "PST2";
        public const string projOverdueID = "PST3";
        public const string projPendingPayID = "PST4";
        public const string projInProgressID = "PST5";

        public const string completedTask = "TS2";
        public const string overdueTask = "TS3";
        public const string underConsiderableTask = "TS4";
        public const string ongoingTask = "TS1";

        protected const string deptTbl = "Departments";
        public const string deptID = "ID";
        public const string deptName = "DepartmentName";
        public const string deptHead = "DepartmentHead";

        protected const string roleTbl = "Roles";
        public const string roleID = "ID";
        public const string roleName = "Title";

        protected const string emplTbl = "Employees";
        public const string emplID = "ID";
        public const string emplName = "FullName";
        public const string emplGender = "Gender";
        public const string emplBirthday = "Birthday";
        public const string emplIdentCard = "IdentifyCard";
        public const string emplEmail = "Email";
        public const string emplPhoneNo = "PhoneNumber";
        public const string emplAddress = "EmployeeAddress";
        public const string emplSalary = "BaseSalary";
        public const string emplDeptID = "DepartmentID";
        public const string emplRoleID = "RoleID";

        protected const string accTbl = "Accounts";
        public const string accName = "Username";
        public const string accPass = "PasswordHash";
        public const string accEmplID = "EmployeeID";
        
        protected const string projStasTbl = "ProjectStatuses";
        public const string projStasID = "ID";
        public const string projStasName = "StatusName";

        protected const string projTbl = "Projects";
        public const string projID = "ID";
        public const string projName = "ProjectName";
        public const string projDetails = "Details";
        public const string projCreated = "CreatedDate";
        public const string projStart = "StartDate";
        public const string projEnd = "EndDate";
        public const string projCompleted = "CompletedDate";
        public const string projProgress = "Progress";
        public const string projStatusID = "StatusID";
        public const string projOwnerID = "OwnerID";
        public const string projBonus = "BonusSalary";

        protected const string projAssignTbl = "ProjectAssignments";
        public const string projAssignID = "ProjectID";
        public const string projAssignDeptID = "DepartmentID";

        protected const string taskStasTbl = "TaskStatuses";
        public const string taskStasID = "ID";
        public const string taskStasName = "StatusName";

        protected const string taskTbl = "Tasks";
        public const string taskID = "ID";
        public const string taskTitle = "Title";
        public const string taskExplanation = "Explanation";
        public const string taskStart = "StartDate";
        public const string taskDeadline = "Deadline";
        public const string taskProgress = "Progress";
        public const string taskOwnerID = "OwnerID";
        public const string taskEmplID = "EmployeeID";
        public const string taskProjID = "ProjectID";
        public const string taskStatusID = "StatusID";

        protected const string leavStasTbl = "LeaveStatuses";
        public const string leavStasID = "ID";
        public const string leavStasName = "StatusName";
        
        protected const string leavTbl = "LeaveRequests";
        public const string leavID = "ID";
        public const string leavReason = "Reason";
        public const string leavNotes = "Notes";
        public const string leavCreated = "CreatedDate";
        public const string leavStart = "StartDate";
        public const string leavEnd = "EndDate";
        public const string leavStatusID = "StatusID";
        public const string leavEmplID = "EmployeeID";
        public const string leavApproverID = "ApproverID";
        
        protected const string timeShtTbl = "TimeSheets";
        public const string timeShtID = "ID";
        public const string timeShtInTime = "CheckInTime";
        public const string timeShtOutTime = "CheckOutTime";
        public const string timeShtEmplID = "EmployeeID";
        public const string timeShtTaskInID = "TaskCheckInID";
        
        protected const string taskOutTbl = "TaskCheckOuts";
        public const string taskOutUpdate = "UpdateDate";
        public const string taskOutProgress = "Progress";
        public const string taskOutTimeShtID = "TimeSheetID";
        public const string taskOutTaskID = "TaskID";

        protected const string kpiTbl = "KPIs";
        public const string kpiID = "ID";
        public const string kpiMonthYear = "MonthYear";
        public const string kpiRequireTaskCnt = "RequiredTasksCount";
        public const string kpiActualTaskCnt = "ActualTasksCount";
        public const string kpiEmplID = "EmployeeID";

        protected const string projBonusTbl = "ProjectBonuses";
        public const string projBonusID = "ID";
        public const string projBonusAmount = "Amount";
        public const string projBonusReceiveDate = "ReceivedDate";
        public const string projBonusEmplID = "EmployeeID";
        public const string projBonusProjID = "ProjectID";

        protected const string salaryTbl = "SalaryRecords";
        public const string salaryEmplID = "EmployeeID";
        public const string salaryMonthYear = "MonthYear";
        public const string salaryWorkdays = "TotalWorkdays";
        public const string salaryBonus = "TotalBonus";
        public const string salaryIncome = "Income";
        
        protected const string mileTbl = "Milestones";
        public const string mileID = "ID";
        public const string mileTitle = "Title";
        public const string mileExplanation = "Explanation";
        public const string mileStart = "StartDate";
        public const string mileEnd = "EndDate";
        public const string mileCompleted = "CompletedDate";
        public const string mileOwnerID = "OwnerID";
        public const string mileProjID = "ProjectID";
        
        protected const string mileTsksTbl = "MileTasks";
        public const string mileTskID = "MileID";
        public const string mileTskTskID = "TaskID";
        
        protected DBConnection dbConnection = new DBConnection();
    }
}