using System.Windows.Controls;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.Views.UserControls;

/// <summary>
///     Interaction logic for UCEmployee.xaml
/// </summary>
public partial class EmployeesUC : UserControl
{
    public EmployeesUC()
    {
        InitializeComponent();
        DataContext = new EmployeesViewModel();
    }
}