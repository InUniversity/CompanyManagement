using System.Windows;
using CompanyManagement.Database.Implementations;
using CompanyManagement.ViewModels;

namespace CompanyManagement.Dialogs;

/// <summary>
///     Interaction logic for EmployeeInputWindow.xaml
/// </summary>
public partial class AddEmployeeDialog : Window
{
    public AddEmployeeDialog()
    {
        InitializeComponent();
        DataContext = new AddEmployeeViewModel(new EmployeeDao());
    }
}