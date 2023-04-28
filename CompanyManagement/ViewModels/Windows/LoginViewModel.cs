using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CompanyManagement.Database;
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
        public string Password { get => password; set { password = value; OnPropertyChanged(); } }

        public ICommand LoginCommand { get; private set; }
        public ICommand ForgotPasswordCommand { get; private set; }
        public ICommand PasswordChangedCommand { get; private set; }
  
        private AccountsDao accountsDao = new AccountsDao();
        private EmployeesDao employeesDao = new EmployeesDao();

        public LoginViewModel()
        {
            SetCommands();
        }

        private void SetCommands()
        {
            LoginCommand = new RelayCommand<Window>(ExecuteLoginCommand);
            ForgotPasswordCommand = new RelayCommand<object>(ExecuteForgotPasswordCommand);
            PasswordChangedCommand = new RelayCommand<PasswordBox>(p => { password = p.Password; });
        }

        private void ExecuteLoginCommand(Window window)
        {
            var account = accountsDao.SearchByUsername(Username);
            if (account == null || !string.Equals(password, account.Password))
            {
                MessageBox.Show(Utils.INVALIDATE_USERNAME_PASSWORD_MESSAGE);
                return;
            }
            RefreshAllText();
            var employee = employeesDao.SearchByID(account.EmployeeID);
            employee.MyAccount = account;
            CurrentUser.Ins.EmployeeIns = employee;
            window.Hide();
            ShowMainWindow();
            window.Show();
        }

        private void ShowMainWindow()
        {
            Window nextWindow = new MainWindow();
            nextWindow.ShowDialog();
        }

        private void RefreshAllText()
        {
            Username = "";
            Password = "";
        }

        private void ExecuteForgotPasswordCommand(object p)
        {
            Log.Instance.Information(nameof(LoginViewModel), "click ForgotPassword: Coming soon....");
        }
    }
}