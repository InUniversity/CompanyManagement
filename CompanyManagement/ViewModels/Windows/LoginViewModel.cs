using System;
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
            Username = "EM0010101";
            Password = "@1234567";
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
            ShowMainWindow();
            window.Show();
        }

        private void ShowMainWindow()
        {
            MainWindow nextWindow = new MainWindow { DataContext = new MainViewModel() };
            nextWindow.ShowDialog();
        }

        private void RefreshAllText()
        {
            Username = "";
            Password = "";
        }
    }
}