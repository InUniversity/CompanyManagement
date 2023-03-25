namespace CompanyManagement.Models
{
    public class SingletonEmployee
    {

        private static SingletonEmployee instance = null;
        private  EmployeeAccount employeeAccount;
        
        public EmployeeAccount CurrentEmployeeAccount
        {
            get { return employeeAccount; }
            set { employeeAccount = value; }
        }
        
        public static SingletonEmployee Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SingletonEmployee();
                }
                return instance;
            }
        }
        
        private SingletonEmployee()
        {
            employeeAccount = new EmployeeAccount();
        }
    }
}
