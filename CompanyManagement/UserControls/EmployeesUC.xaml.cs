using System.Windows.Controls;
using CompanyManagement.Database.Implementations;
using CompanyManagement.ViewModels;

namespace CompanyManagement.UserControls;

/// <summary>
///     Interaction logic for UCEmployee.xaml
/// </summary>
public partial class EmployeesUC : UserControl
{
    public EmployeesUC()
    {
        InitializeComponent();
        DataContext = new EmployeesViewModel(new EmployeeAccountDao());
    }
}