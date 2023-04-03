namespace CompanyManagement.Database
{
    public abstract class BaseDao
    {
        public const string MANAGERIAL_POSITION_ID = "1";
        
        protected const string ACCOUNT_TABLE = "Account";
        public const string ACCOUNT_USERNAME = "account_username";
        public const string ACCOUNT_PASSWORD = "account_password";
        public const string ACCOUNT_EMPLOYEE_ID = "employee_id";
        
        protected const string DEPARTMENT_TABLE = "Department";
        public const string DEPARTMENT_ID = "department_id";
        public const string DEPARTMENT_NAME = "department_name";
        public const string DEPARTMENT_MANAGER_ID = "manager_id";
        
        protected const string EMPLOYEE_TABLE = "Employee";
        public const string EMPLOYEE_ID = "employee_id";
        public const string EMPLOYEE_NAME = "employee_name";
        public const string EMPLOYEE_GENDER = "gender";
        public const string EMPLOYEE_BIRTHDAY = "birthday";
        public const string EMPLOYEE_IDENTIFY_CARD = "identify_card";
        public const string EMPLOYEE_EMAIL = "email";
        public const string EMPLOYEE_PHONE_NUMBER = "phone_number";
        public const string EMPLOYEE_ADDRESS = "employee_address";
        public const string EMPLOYEE_DEPARTMENT_ID = "department_id";
        public const string EMPLOYEE_POSITION_ID = "position_id";
        public const string EMPLOYEE_SALARY = "salary";
        
        protected const string POSITION_TABLE = "Position";
        public const string POSITION_ID = "position_id";
        public const string POSITION_NAME = "position_name";
        
        protected const string PROJECT_ASSIGNMENT_TABLE = "ProjectAssignment";
        public const string PROJECT_ASSIGNMENT_PROJECT_ID = "project_id";
        public const string PROJECT_ASSIGNMENT_DEPARTMENT_ID = "department_id";
        
        protected const string PROJECT_TABLE = "Project";
        public const string PROJECT_ID = "project_id";
        public const string PROJECT_NAME = "project_name";
        public const string PROJECT_START = "create_date";
        public const string PROJECT_END = "end_date";
        public const string PROJECT_COMPLETED = "completed_date";
        public const string PROJECT_PROPRESS = "progress";
        public const string PROJECT_STATUS_ID = "project_status_id";
        public const string PROJECT_CREATE_BY = "create_by";
        
        protected const string TASK_TABLE = "Task";
        public const string TASK_ID = "task_id";
        public const string TASK_TITLE = "title";
        public const string TASK_DESCRIPTION = "task_description";
        public const string TASK_ASSIGN_DATE = "assign_date";
        public const string TASK_DEADLINE = "deadline";
        public const string TASK_CREATE_BY = "create_by";
        public const string TASK_PROGRESS = "progress";
        public const string TASK_EMPLOYEE_ID = "employee_id";
        public const string TASK_PROJECT_ID = "project_id";
        public const string TASK_STATUS_ID = "task_status_id";

        protected const string TIME_KEEPING_TABLE = "TimeKeeping";
        public const string TIME_KEEPING_TASK_ID = "task_id";
        public const string TIME_KEEPING_START_TIME = "start_time";
        public const string TIME_KEEPING_END_TIME = "end_time";
        public const string TIME_KEEPING_EMPLOYEE_ID = "employee_id";
        public const string TIME_KEEPING_NOTES = "Notes";
        public const string TIME_KEEPING_CREATE_BY = "create_by";

        protected const string PROJECT_STATUS_TABLE = "ProjectStatus";
        public const string PROJECT_STATUS_NAME = "project_status_id";

        protected const string TASK_STATUS_TABLE = "TaskStatus";
        public const string TASK_STATUS_NAME = "task_status_id";
        
        protected DBConnection dbConnection = new DBConnection();
    }
}