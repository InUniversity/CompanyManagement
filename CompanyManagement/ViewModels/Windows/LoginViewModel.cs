using System;
using System.Windows;
using System.Windows.Controls;
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
            CurrentUser.Ins.EmployeeIns = employee;
            window.Hide();
            ShowMainWindow(employee.EmplRole.Perms);
            window.Show();
        }

        private void ShowMainWindow(EPermission perms)
        {
            try
            {
                var mainStrategy = MainStrategyFactory.Create(perms);
                var viewModel = new MainViewModel(mainStrategy);
                var nextWindow = new MainWindow { DataContext = viewModel };
                nextWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(LoginViewModel), ex.Message);
            }
        }

        private void RefreshAllText()
        {
            Username = "";
            Password = "";
        }
    }
}