using System.Windows;
using System.Windows.Input;
using CompanyManagement.Database;
using CompanyManagement.Models;

namespace CompanyManagement.ViewModels;

public class LoginViewModel : BaseViewModel
{
    
    private string username;
    public string Username { get => username; set { username = value; OnPropertyChanged(); } }
        
    private string password;
    public string Password { get => password; set { password = value; OnPropertyChanged(); } }
    
    public ICommand LoginCommand { get; set; }
    public ICommand ForgotPasswordCommand { get; set; }

    private AccountDao accDao = new AccountDao();
    
    public LoginViewModel()
    {
        SetCommands();
    }

    private void SetCommands()
    {
        LoginCommand = new ReplayCommand<object>(p => OnClickLogin(p));
        ForgotPasswordCommand = new ReplayCommand<object>(p => OnClickForgotPassword(p));
    }

    private void OnClickLogin(object p)
    {
        MessageBox.Show($"Username: {Username}, Password: {Password}");
        Account account = accDao.SearchByUsername(Username);
        if (account == null || !string.Equals(Password, account.Password))
        {
            MessageBox.Show("Username or password not valid");
            return;
        }
        var mainWindow = new MainWindow();
        ((Window) p).Hide();
        mainWindow.ShowDialog();
        ((Window) p).Show();
    }

    private void OnClickForgotPassword(object p)
    {
        MessageBox.Show("Coming soon....");
    }
}