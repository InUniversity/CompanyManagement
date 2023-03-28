using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CompanyManagement.Database;
using CompanyManagement.Database.Interfaces;
using CompanyManagement.Models;
using CompanyManagement.Utilities;
using CompanyManagement.Views.Windows;

namespace CompanyManagement.ViewModels.UserControls
{
    public class LoginViewModel : BaseViewModel
    {

        private string username;
        public string Username { get => username; set { username = value; OnPropertyChanged(); } }

        private string password;

        public ICommand LoginCommand { get; set; }
        public ICommand ForgotPasswordCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }

        private IEmployeeAccountDao employeeAccountDao;

        public LoginViewModel(IEmployeeAccountDao employeeAccountDao)
        {
            this.employeeAccountDao = employeeAccountDao;
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
            // EmployeeAccount employeeAccount = employeeAccountDao.SearchByUsername(Username);
            // if (employeeAccount == null || !string.Equals(password, employeeAccount.EmplAccount.Password))
            // {
            //     MessageBox.Show(Utils.INVALIDATE_USERNAME_PASSWORD_MESSAGE);
            //     return;
            // }
            // SingletonEmployee.Instance.CurrentEmployeeAccount = employeeAccount;
            // Window nextWindow = string.Equals(employeeAccount.PositionID, BaseDao.MANAGERIAL_POSITION_ID)
            //     ? new ManagerWindow() : new EmployeeWindow();
            // nextWindow.Show();
            new ManagerWindow().Show();
            loginWindow.Close();
        }

        private void ExecuteForgotPasswordCommand(object p)
        {
            MessageBox.Show("Coming soon....");
        }
    }
}