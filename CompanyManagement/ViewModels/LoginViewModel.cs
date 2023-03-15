using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CompanyManagement.Database;
using CompanyManagement.Models;

namespace CompanyManagement.ViewModels;

public class LoginViewModel : BaseViewModel
{
    public int IsLogin { get; set; }

    private string username;
    public string Username { get => username; set { username = value; OnPropertyChanged(); } }
        
    private string password;
    public string Password { get => password; set { password = value; OnPropertyChanged(); } }
    
    public ICommand LoginCommand { get; set; }
    public ICommand ForgotPasswordCommand { get; set; }
    public ICommand PasswordChangedCommand { get; set; }

    private AccountDao accDao = new AccountDao();
    
    public LoginViewModel()
    {
        SetCommands();
    }

    private void SetCommands()
    {
        IsLogin = 0;
        LoginCommand = new ReplayCommand<Window>(p => OnClickLogin(p));
        ForgotPasswordCommand = new ReplayCommand<object>(p => OnClickForgotPassword(p));
        PasswordChangedCommand = new ReplayCommand<PasswordBox>((p) => { password = p.Password; });
    }

    private void OnClickLogin(Window p)
    {
        Account account = accDao.SearchByUsername(Username);
        if (account == null || !string.Equals(Password, account.Password))
        {
            MessageBox.Show("Username or password not valid");
            IsLogin = 0;
            return;
        }
        if (account.EmployeeId.IndexOf("CD") >= 0)
            IsLogin = 1;
        else
            IsLogin = 2;
        p.Close();
    }

    private void OnClickForgotPassword(object p)
    {
        MessageBox.Show("Coming soon....");
    }
}