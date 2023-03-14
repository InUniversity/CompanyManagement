using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CompanyManagement.Database;
using CompanyManagement.Models;

namespace CompanyManagement.ViewModels;

public class LoginViewModel : BaseViewModel
{
    public bool IsLogin { get; set; }

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
        IsLogin = false;
        LoginCommand = new RelayCommand<Window>(p => OnClickLogin(p));
        ForgotPasswordCommand = new RelayCommand<object>(p => OnClickForgotPassword(p));
        PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { password = p.Password; });
    }

    private void OnClickLogin(Window p)
    {
        Account account = accDao.SearchByUsername(Username);
        if (account == null || !string.Equals(Password, account.Password))
        {
            MessageBox.Show("Username or password not valid");
            IsLogin = false;
            return;
        }
        IsLogin = true;
        p.Close();
    }

    private void OnClickForgotPassword(object p)
    {
        MessageBox.Show("Coming soon....");
    }
}