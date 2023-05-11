using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CompanyManagement.Database;
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
            CurrentUser.Ins.EmployeeIns = employee;
            window.Hide();
            ShowMainWindow(employee.PermsID);
            window.Show();
        }

        private void ShowMainWindow(string roleID)
        {
            try
            {
                var mainStrategy = MainStrategyFactory.Create(roleID);
                var nextWindow = new MainWindow { DataContext = new MainViewModel(mainStrategy) };
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