using System;
using System.Windows;
using System.Windows.Input;
using CompanyManagement.Database;
using CompanyManagement.Enums;
using CompanyManagement.Models;
using CompanyManagement.Strategies.Windows.MainView;
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
        public ICommand ExitCommand { get; private set; }

        private AccountsDao accountsDao = new AccountsDao();
        private EmployeesDao employeesDao = new EmployeesDao();
        private RolesDao rolesDao = new RolesDao();

        public LoginViewModel()
        {
            SetCommands();
        }

        private void SetCommands()
        {
            LoginCommand = new RelayCommand<Window>(ExecuteLoginCommand);
            ExitCommand = new RelayCommand<Window>(ExecuteExitCommand);
        }

        private void ExecuteExitCommand(Window window)
        {
            window.Close();
        }

        private void ExecuteLoginCommand(Window window)
        {
            var account = accountsDao.SearchByUsername(Username);
            if (account == null || !string.Equals(password, account.Password))
            {
                MessageBox.Show(Utils.invalidAccMess);
                return;
            }
            RefreshAllText();
            var employee = employeesDao.SearchByID(account.EmployeeID);
            employee.Acc = account;
            employee.EmplRole = rolesDao.SearchByID(employee.RoleID);
            CurrentUser.Ins.Empl = employee;
            window.Hide();
            ShowMainWindow(employee.EmplRole.Perms);
            window.Show();
        }

        private void ShowMainWindow(EPermission perms)
        {
            IMainStrategy strategy = new MainForEmployee();
            try
            {
                strategy = MainStrategyFactory.Create(perms);
            }
            catch (Exception ex)
            {
                Log.Ins.Error(nameof(LoginViewModel), ex.Message);
            }
            var viewModel = new MainViewModel(strategy);
            var nextWindow = new MainWindow { DataContext = viewModel };
            nextWindow.ShowDialog();
        }

        private void RefreshAllText()
        {
            Username = "";
            Password = "";
        }
    }
}