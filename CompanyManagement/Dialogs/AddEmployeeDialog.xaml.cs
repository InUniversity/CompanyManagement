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
        IEmployeeInput employeeInput = new EmployeeInputViewModel(new PositionDao(), new DepartmentDao());
        DataContext = new AddEmployeeViewModel(employeeInput, new EmployeeDao());
    }
}