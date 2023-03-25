using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CompanyManagement.Database;
using CompanyManagement.Database.Implementations;
using CompanyManagement.Database.Interfaces;
using CompanyManagement.Models;
using CompanyManagement.Utilities;

namespace CompanyManagement.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        
        private string username;
        public string Username { get => username; set { username = value; OnPropertyChanged(); } }
            
        private string password;
        
        public ICommand LoginCommand { get; set; }
        public ICommand ForgotPasswordCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }

        private IEmployeeDao employeeDao;
        
        public LoginViewModel(IEmployeeDao employeeDao)
        {
            this.employeeDao = employeeDao;
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
            Employee employee = employeeDao.SearchByUsername(Username);
            if (employee == null || !string.Equals(password, employee.EmplAccount.Password))
            {
                MessageBox.Show(Utils.INVALIDATE_USERNAME_PASSWORD_MESSAGE);
                return;
            }
            SingletonEmployee.Instance.CurrentEmployee = employee;
            Window nextWindow = string.Equals(employee.PositionID, BaseDao.MANAGERIAL_POSITION_ID) 
                ? new ManagerWindow() : new EmployeeWindow();
            nextWindow.Show();
            loginWindow.Close();
        }

        private void ExecuteForgotPasswordCommand(object p)
        {
            MessageBox.Show("Coming soon....");
        }
    }
}