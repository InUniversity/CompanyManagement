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

        protected const string DEPARTMENTS_TABLE = "Departments";
        public const string DEPARTMENTS_ID = "ID";
        public const string DEPARTMENTS_NAME = "DepartmentName";
        public const string DEPARTMENTS_HEAD = "DepartmentHead";

        protected const string ROLES_TABLE = "Roles";
        public const string ROLES_ID = "ID";
        public const string ROLES_NAME = "Title";

        protected const string EMPLOYEES_TABLE = "Employees";
        public const string EMPLOYEES_ID = "ID";
        public const string EMPLOYEES_NAME = "FullName";
        public const string EMPLOYEES_GENDER = "Gender";
        public const string EMPLOYEES_BIRTHDAY = "Birthday";
        public const string EMPLOYEES_IDENTIFY_CARD = "IdentifyCard";
        public const string EMPLOYEES_EMAIL = "Email";
        public const string EMPLOYEES_PHONE_NUMBER = "PhoneNumber";
        public const string EMPLOYEES_ADDRESS = "EmployeeAddress";
        public const string EMPLOYEES_SALARY = "BaseSalary";
        public const string EMPLOYEES_DEPARTMENT_ID = "DepartmentID";
        public const string EMPLOYEES_POSITION_ID = "RoleID";

        protected const string ACCOUNTS_TABLE = "Accounts";
        public const string ACCOUNTS_USERNAME = "Username";
        public const string ACCOUNTS_PASSWORD = "PasswordHash";
        public const string ACCOUNTS_EMPLOYEE_ID = "EmployeeID";
        
        protected const string PROJECT_STATUSES_TABLE = "ProjectStatuses";
        public const string PROJECT_STATUSES_ID = "ID";
        public const string PROJECT_STATUSES_NAME = "StatusName";

        protected const string PROJECTS_TABLE = "Projects";
        public const string PROJECTS_ID = "ID";
        public const string PROJECTS_NAME = "ProjectName";
        public const string PROJECTS_DETAILS = "Details";
        public const string PROJECTS_CREATED = "CreatedDate";
        public const string PROJECTS_START = "StartDate";
        public const string PROJECTS_END = "EndDate";
        public const string PROJECTS_COMPLETED = "CompletedDate";
        public const string PROJECTS_PROPRESS = "Progress";
        public const string PROJECTS_STATUS_ID = "StatusID";
        public const string PROJECTS_OWNER_ID = "OwnerID";
        public const string PROJECTS_BONUS_SALARY = "BonusSalary";

        protected const string PROJECT_ASSIGNMENTS_TABLE = "ProjectAssignments";
        public const string PROJECT_ASSIGNMENTS_PROJECT_ID = "ProjectID";
        public const string PROJECT_ASSIGNMENTS_DEPARTMENT_ID = "DepartmentID";

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

        protected const string LEAVE_STATUSES_TABLE = "LeaveStatuses";
        public const string LEAVE_STATUSES_ID = "ID";
        public const string LEAVE_STATUSES_NAME = "StatusName";
        
        protected const string LEAVE_TABLE = "LeaveRequests";
        public const string LEAVE_ID = "ID";
        public const string LEAVE_REASON = "Reason";
        public const string LEAVE_NOTES = "Notes";
        public const string LEAVE_CREATED_DATE = "CreatedDate";
        public const string LEAVE_START_DATE = "StartDate";
        public const string LEAVE_END_DATE = "EndDate";
        public const string LEAVE_STATUS_ID = "StatusID";
        public const string LEAVE_EMPLOYEE_ID = "EmployeeID";
        public const string LEAVE_APPROVER_ID = "ApproverID";
        
        protected const string TIME_SHEETS_TABLE = "TimeSheets";
        public const string TIME_SHEETS_ID = "ID";
        public const string TIME_SHEETS_CHECK_IN_TIME = "CheckInTime";
        public const string TIME_SHEETS_CHECK_OUT_TIME = "CheckOutTime";
        public const string TIME_SHEETS_EMPLOYEE_ID = "EmployeeID";
        public const string TIME_SHEETS_TASK_CHECK_IN_ID = "TaskCheckInID";
        
        protected const string TASK_CHECK_OUTS_TABLE = "TaskCheckOuts";
        public const string TASK_CHECK_OUTS_UPDATE_DATE = "UpdateDate";
        public const string TASK_CHECK_OUTS_PROGRESS = "Progress";
        public const string TASK_CHECK_OUTS_TIME_SHEET_ID = "TimeSheetID";
        public const string TASK_CHECK_OUTS_TASK_ID = "TaskID";

        protected const string KPIS_TABLE = "KPIs";
        public const string KPIS_ID = "ID";
        public const string KPIS_MONTH_YEAR = "MonthYear";
        public const string KPIS_REQUIRED_TASKS_COUNT = "RequiredTasksCount";
        public const string KPIS_ACTUAL_TASKS_COUNT = "ActualTasksCount";
        public const string KPIS_EMPLOYEE_ID = "EmployeeID";

        protected const string PROJECT_BONUSES_TABLE = "ProjectBonuses";
        public const string PROJECT_BONUSES_ID = "ID";
        public const string PROJECT_BONUSES_AMOUNT = "Amount";
        public const string PROJECT_BONUSES_RECEIVED_DATE = "ReceivedDate";
        public const string PROJECT_BONUSES_EMPLOYEE_ID = "EmployeeID";
        public const string PROJECT_BONUSES_PROJECT_ID = "ProjectID";

        protected const string SALARY_TABLE = "SalaryRecords";
        public const string SALARY_EMPLOYEE_ID = "EmployeeID";
        public const string SALARY_MONTH_YEAR = "MonthYear";
        public const string SALARY_TOTAL_WORK_DAYS = "TotalWorkdays";
        public const string SALARY_TOTAL_BONUS = "TotalBonus";
        public const string SALARY_INCOME = "Income";
        
        protected DBConnection dbConnection = new DBConnection();
    }
}