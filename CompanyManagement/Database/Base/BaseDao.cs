namespace CompanyManagement.Database.Base
{
    public abstract class BaseDao
    {
        public const string MANAGER_ROLE_ID = "ER01";
        public const string DEPARTMENT_HEAD_ROLE_ID = "ER02";
        public const string HR_ROLE_ID = "ER03";
        public const string EMPLOYEE_ROLE_ID = "ER04";

        public const string COMPLETED = "100";
        public const string APPROVAL = "LS2";
        public const string APPROVED = "LS1";
        public const string UNAPPROVED = "LS3";

        protected const string ACCOUNTS_TABLE = "Accounts";
        public const string ACCOUNTS_USERNAME = "Username";
        public const string ACCOUNTS_PASSWORD = "PasswordHash";
        public const string ACCOUNTS_EMPLOYEE_ID = "EmployeeID";

        protected const string DEPARTMENTS_TABLE = "Departments";
        public const string DEPARTMENTS_ID = "ID";
        public const string DEPARTMENTS_NAME = "DepartmentName";
        public const string DEPARTMENTS_DEPARTMENT_HEAD = "DepartmentHead";

        protected const string EMPLOYEE_TABLE = "Employees";
        public const string EMPLOYEE_ID = "ID";
        public const string EMPLOYEE_NAME = "FullName";
        public const string EMPLOYEE_GENDER = "Gender";
        public const string EMPLOYEE_BIRTHDAY = "Birthday";
        public const string EMPLOYEE_IDENTIFY_CARD = "IdentifyCard";
        public const string EMPLOYEE_EMAIL = "Email";
        public const string EMPLOYEE_PHONE_NUMBER = "PhoneNumber";
        public const string EMPLOYEE_ADDRESS = "EmployeeAddress";
        public const string EMPLOYEE_SALARY = "BaseSalary";
        public const string EMPLOYEE_DEPARTMENT_ID = "DepartmentID";
        public const string EMPLOYEE_POSITION_ID = "RoleID";

        protected const string ROLES_TABLE = "Roles";
        public const string ROLES_ID = "ID";
        public const string ROLES_NAME = "Title";

        protected const string PROJECT_ASSIGNMENT_TABLE = "ProjectAssignments";
        public const string PROJECT_ASSIGNMENT_PROJECT_ID = "ProjectID";
        public const string PROJECT_ASSIGNMENT_DEPARTMENT_ID = "DepartmentID";
        
        protected const string PROJECT_STATUSES_TABLE = "ProjectStatus";
        public const string PROJECT_STATUSES_ID = "ID";
        public const string PROJECT_STATUSES_NAME = "StatusName";

        protected const string PROJECTS_TABLE = "Projects";
        public const string PROJECTS_ID = "ID";
        public const string PROJECTS_NAME = "ProjectName";
        public const string PROJECTS_CREATED = "CreatedDate";
        public const string PROJECTS_START = "StartDate";
        public const string PROJECTS_END = "EndDate";
        public const string PROJECTS_COMPLETED = "CompletedDate";
        public const string PROJECTS_PROPRESS = "Progress";
        public const string PROJECTS_STATUS_ID = "StatusID";
        public const string PROJECTS_OWNER_ID = "OwnerID";
        public const string PROJECTS_BONUS_SALARY = "BonusSalary";

        protected const string TASK_STATUSES_TABLE = "TaskStatuses";
        public const string TASK_STATUSES_ID = "ID";
        public const string TASK_STATUSES_NAME = "StatusName";

        protected const string TASKS_TABLE = "Tasks";
        public const string TASKS_ID = "ID";
        public const string TASKS_TITLE = "Title";
        public const string TASKS_EXPLANATION = "Explanation";
        public const string TASKS_START_DATE = "StartDate";
        public const string TASKS_DEADLINE = "Deadline";
        public const string TASKS_PROGRESS = "Progress";
        public const string TASKS_OWNER_ID = "OwnerID";
        public const string TASKS_EMPLOYEE_ID = "EmployeeID";
        public const string TASKS_PROJECT_ID = "ProjectID";
        public const string TASKS_STATUS_ID = "StatusID";

        protected const string TIME_SHEETS_TABLE = "CheckInOut";
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

        protected const string SALARY_TABLE = "SalaryRecords";
        public const string SALARY_EMPLOYEE_ID = "EmployeeID";
        public const string SALARY_TIME = "SalaryTime";
        public const string SALARY_TOTAL_WORK_DAY = "TotalWorkDay";
        public const string SALARY_BONUS = "Bonus";
        public const string SALARY_INCOME = "Income";

        protected const string KPIS_TABLE = "KPIs";
        public const string KPIS_ID = "ID";
        public const string KPIS_EMPLOYEE_ID = "EmployeeID";
        public const string KPIS_TIME = "KPITime";
        public const string KPIS_NUMBER_TARGET = "NumberTarget";
        public const string KPIS_NUMBER_ACTUAL = "NumberActual";

        protected DBConnection dbConnection = new DBConnection();
    }
}