namespace CompanyManagement.Models
{
    public class CurrentUser
    {
        private static CurrentUser instance;
        private Employee employee;
        private Account account;

        public Employee CurrentEmployee
        {
            get { return employee; }
            set { employee = value; }
        }
        
        public Account CurrentAccount
        {
            get { return account; }
            set { account = value; }
        }
        
        public static CurrentUser Instance
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
            account = new Account();
            employee = new Employee();
        }
    }
}
