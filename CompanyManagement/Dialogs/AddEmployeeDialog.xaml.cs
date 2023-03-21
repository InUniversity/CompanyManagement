using CompanyManagement.ViewModels;
using System.Windows;
using CompanyManagement.Database.Implementations;

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