namespace CompanyManagement.Models
{
    public class SingletonEmployee
    {

        private static SingletonEmployee instance = null;
        private  Employee employee;
        
        public Employee CurrentEmployee
        {
            get { return employee; }
            set { employee = value; }
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
            employee = new Employee();
        }
    }
}
