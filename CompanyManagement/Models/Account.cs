namespace CompanyManagement.Models
{
    public class Account
    {
        private string username;
        private string password;

        public string Username
        {
            get => username;
            set => username = value;
        }

        public string Password
        {
            get => password;
            set => password = value;
        }

        public Account() { }

        public Account(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
    }
}
