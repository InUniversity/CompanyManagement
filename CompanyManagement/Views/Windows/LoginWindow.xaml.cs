using System.Windows;
using CompanyManagement.Database.Implementations;
using CompanyManagement.ViewModels.Windows;

namespace CompanyManagement.Views.Windows;

/// <summary>
///     Interaction logic for LoginWindow.xaml
/// </summary>
public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();
        DataContext = new LoginViewModel(new AccountDao(), new EmployeeDao());
    }
}