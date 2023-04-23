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

        private AccountDao accountDao = new AccountDao();
        private EmployeeDao employeeDao = new EmployeeDao();

        public LoginViewModel()
        {
            SetCommands();
        }

        private void SetCommands()
        {
            LoginCommand = new RelayCommand<PasswordBox>(ExecuteLoginCommand);
            ForgotPasswordCommand = new RelayCommand<object>(ExecuteForgotPasswordCommand);
            PasswordChangedCommand = new RelayCommand<PasswordBox>(p => { password = p.Password; });
        }
        
        private void ExecuteLoginCommand(PasswordBox passwordBox)
        {
            var account = accountDao.SearchByUsername(Username);
            if (account == null || !string.Equals(password, account.Password))
            {
                MessageBox.Show(Utils.INVALIDATE_USERNAME_PASSWORD_MESSAGE);
                return;
            }
            var employee = employeeDao.SearchByID(account.EmployeeID);
            employee.MyAccount = account;
            CurrentUser.Ins.EmployeeIns = employee;
            ShowMainWindow();
            passwordBox.Password = "";
            Username = "";
        }

        private void ShowMainWindow()
        {
            Window nextWindow = new MainWindow();
            nextWindow.Show();
        }

        private void ExecuteForgotPasswordCommand(object p)
        {
            Log.Instance.Information(nameof(LoginViewModel), "click ForgotPassword: Coming soon....");
        }
    }
}