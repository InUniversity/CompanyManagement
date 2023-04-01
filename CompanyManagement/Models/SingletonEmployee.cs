namespace CompanyManagement.Models
{
    public class SingletonEmployee
    {
        private static SingletonEmployee instance;
        private Employee employee;
        private Account account;

        private Employee CurrentEmployee
        {
            get { return employee; }
            set { employee = value; }
        }
        
        public Account CurrentAccount
        {
            get { return account; }
            set { account = value; }
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
            account = new Account();
        }
    }
}
