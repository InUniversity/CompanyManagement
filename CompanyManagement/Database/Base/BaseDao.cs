namespace CompanyManagement.Database.Base
{
    public abstract class BaseDao
    {
        public const string MANAGER_POS_ID = "1";
        public const string DEPARTMENT_HEAD_POS_ID = "2";
        public const string EMPLOYEE_POS_ID = "3";

        public const string COMPLETED = "100";
        public const string APPROVAL = "LS2";
        public const string APPROVED = "LS1";
        public const string UNAPPROVED = "LS3";

        protected const string ACCOUNT_TABLE = "Account";
        public const string ACCOUNT_USERNAME = "AccountUsername";
        public const string ACCOUNT_PASSWORD = "AccountPassword";
        public const string ACCOUNT_EMPLOYEE_ID = "EmployeeID";

        protected const string DEPARTMENT_TABLE = "Department";
        public const string DEPARTMENT_ID = "DepartmentID";
        public const string DEPARTMENT_NAME = "DepartmentName";
        public const string DEPARTMENT_MANAGER_ID = "ManagerID";

        protected const string EMPLOYEE_TABLE = "Employee";
        public const string EMPLOYEE_ID = "EmployeeID";
        public const string EMPLOYEE_NAME = "EmployeeName";
        public const string EMPLOYEE_GENDER = "Gender";
        public const string EMPLOYEE_BIRTHDAY = "Birthday";
        public const string EMPLOYEE_IDENTIFY_CARD = "IdentifyCard";
        public const string EMPLOYEE_EMAIL = "Email";
        public const string EMPLOYEE_PHONE_NUMBER = "PhoneNumber";
        public const string EMPLOYEE_ADDRESS = "EmployeeAddress";
        public const string EMPLOYEE_DEPARTMENT_ID = "DepartmentID";
        public const string EMPLOYEE_POSITION_ID = "PositionID";
        public const string EMPLOYEE_SALARY = "BaseSalary";

        protected const string POSITION_TABLE = "Position";
        public const string POSITION_ID = "PositionID";
        public const string POSITION_NAME = "PositionName";

        protected const string PROJECT_ASSIGNMENT_TABLE = "ProjectAssignment";
        public const string PROJECT_ASSIGNMENT_PROJECT_ID = "ProjectID";
        public const string PROJECT_ASSIGNMENT_DEPARTMENT_ID = "DepartmentID";

        protected const string PROJECT_TABLE = "Project";
        public const string PROJECT_ID = "ProjectID";
        public const string PROJECT_NAME = "ProjectName";
        public const string PROJECT_START = "CreateDate";
        public const string PROJECT_END = "EndDate";
        public const string PROJECT_COMPLETED = "CompletedDate";
        public const string PROJECT_PROPRESS = "Progress";
        public const string PROJECT_STATUS_ID = "ProjectStatusID";
        public const string PROJECT_CREATE_BY = "CreateBy";
        public const string PROJECT_BONUS_SALARY = "BonusSalary";

        protected const string TASK_TABLE = "Task";
        public const string TASK_ID = "TaskID";
        public const string TASK_TITLE = "Title";
        public const string TASK_DESCRIPTION = "TaskDescription";
        public const string TASK_ASSIGN_DATE = "AssignDate";
        public const string TASK_DEADLINE = "Deadline";
        public const string TASK_CREATE_BY = "CreateBy";
        public const string TASK_PROGRESS = "Progress";
        public const string TASK_EMPLOYEE_ID = "EmployeeID";
        public const string TASK_PROJECT_ID = "ProjectID";
        public const string TASK_STATUS_ID = "TaskStatusID";

        protected const string PROJECT_STATUS_TABLE = "ProjectStatus";
        public const string PROJECT_STATUS_NAME = "ProjectStatusName";

        protected const string TASK_STATUS_TABLE = "TaskStatus";
        public const string TASK_STATUS_NAME = "TaskStatusName";

        protected const string TASK_PRIORITY_TABLE = "TaskPriority";
        public const string TASK_PRIORITY_ID = "TaskPriorityID";
        public const string TASK_PRIORITY_NAME = "TaskPriorityName";

        protected const string CHECK_IN_OUT_TABLE = "CheckInOut";
        public const string CHECK_IN_OUT_ID = "ID";
        public const string CHECK_IN_OUT_EMPLOYEE_ID = "EmployeeId";
        public const string CHECK_IN_OUT_CHECK_IN_TIME = "CheckInTime";
        public const string CHECK_IN_OUT_CHECK_OUT_TIME = "CheckOutTime";
        public const string CHECK_IN_OUT_TASK_CHECK_IN_ID = "TaskCheckInID";

        protected const string TASK_CHECK_OUT_TABLE = "TaskCheckOut";
        public const string TASK_CHECK_OUT_CHECK_OUT_IN_ID = "CheckInOutID";
        public const string TASK_CHECK_OUT_TASK_ID = "TaskID";
        public const string TASK_CHECK_OUT_UPDATE_DATE = "UpdateDate";
        public const string TASK_CHECK_OUT_PROGRESS = "Progress";

        protected const string LEAVE_TABLE = "Leave";
        public const string LEAVE_ID = "ID";
        public const string LEAVE_EMPLOYEE_ID = "EmployeeID";
        public const string LEAVE_TYPE_ID = "LeaveTypeID";
        public const string LEAVE_REASON = "LeaveReason";
        public const string LEAVE_START_DATE = "StartDate";
        public const string LEAVE_END_DATE = "EndDate";
        public const string LEAVE_STATUS_ID = "LeaveStatusID";
        public const string LEAVE_CREATED_DATE = "CreatedDate";
        public const string LEAVE_APPROVED_BY = "ApprovedBy";
        public const string LEAVE_NOTE = "Note";

        protected const string LEAVE_TYPE_TABLE = "LeaveType";
        public const string LEAVE_TYPE_NAME = "LeaveTypeName";
        
        protected const string LEAVE_STATUS_TABLE = "LeaveStatus";
        public const string LEAVE_STATUS_NAME = "LeaveStatusName";

        protected const string SALARY_TABLE = "Salary";
        public const string SALARY_EMPLOYEE_ID = "EmployeeID";
        public const string SALARY_TIME = "SalaryTime";
        public const string SALARY_TOTAL_WORK_DAY = "TotalWorkDay";
        public const string SALARY_BONUS = "Bonus";
        public const string SALARY_INCOME = "Income";

        protected const string KPI_TABLE = "KPI";
        public const string KPI_ID = "ID";
        public const string KPI_EMPLOYEE_ID = "EmployeeID";
        public const string KPI_TIME = "KPITime";
        public const string KPI_NUMBER_TARGET = "NumberTarget";
        public const string KPI_NUMBER_ACTUAL = "NumberActual";

        protected DBConnection dbConnection = new DBConnection();
    }
}