namespace CompanyManagement.Models
{
    public class CurrentUser
    {
        private static CurrentUser instance;
        
        private Employee employee;
        public Employee EmployeeIns { get => employee; set => employee = value; }
        
        public static CurrentUser Ins
        {
            get
            {
                if (instance == null)
                {
                    instance = new CurrentUser();
                }
                return instance;
            }
        }
        
        private CurrentUser()
        {
            employee = new Employee();
        }

        private void Logout()
        {
            employee = new Employee();
        }
    }
}
