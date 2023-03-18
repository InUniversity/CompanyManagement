namespace CompanyManagement.Models
{
    public class SingletonAccount
    {

        private static SingletonAccount instance = null;
        private  Account account;
        
        public Account CurrentAccount
        {
            get { return account; }
            set { account = value; }
        }
        
        public static SingletonAccount Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SingletonAccount();
                }
                return instance;
            }
        }
        
        private SingletonAccount()
        {
            account = new Account();
        }
    }
}
