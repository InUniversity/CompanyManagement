using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CompanyManagement.Database;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;
using CompanyManagement.Utilities;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.Views.Windows;

namespace CompanyManagement.ViewModels.Windows
{
    public class LoginViewModel : BaseViewModel
    {

        private string username;
        public string Username { get => username; set { username = value; OnPropertyChanged(); } }

        private string password;

        public ICommand LoginCommand { get; set; }
        public ICommand ForgotPasswordCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }

        private AccountDao accountDao;
        private EmployeeDao employeeDao;

        public LoginViewModel()
        {
            accountDao = new AccountDao();
            employeeDao = new EmployeeDao();
            SetCommands();
        }

        private void SetCommands()
        {
            LoginCommand = new RelayCommand<Window>(ExecuteLoginCommand);
            ForgotPasswordCommand = new RelayCommand<object>(ExecuteForgotPasswordCommand);
            PasswordChangedCommand = new RelayCommand<PasswordBox>(p => { password = p.Password; });
        }
        
        private void ExecuteLoginCommand(Window loginWindow)
        {
            Account account = accountDao.SearchByUsername(Username);
            if (account == null || !string.Equals(password, account.Password))
            {
                MessageBox.Show(Utils.INVALIDATE_USERNAME_PASSWORD_MESSAGE);
                return;
            }
            CurrentUser.Instance.CurrentAccount = account;
            Employee employee = employeeDao.SearchByID(account.EmployeeID);
            CurrentUser.Instance.CurrentEmployee = employee;
            Window nextWindow = CreateWindow(employee.PositionID);
            nextWindow.Show();
            loginWindow.Close();
        }

        private Window CreateWindow(string positionID)
        {
            return CurrentUser.Instance.IsManager()
                ? new ManagerWindow()
                : new EmployeeWindow();
        }

        private void ExecuteForgotPasswordCommand(object p)
        {
            Log.Instance.Information(nameof(LoginViewModel), "click ForgotPassword: Coming soon....");
        }
    }
}