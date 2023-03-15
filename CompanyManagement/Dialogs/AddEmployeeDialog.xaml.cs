using CompanyManagement.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace CompanyManagement.Dialogs;

/// <summary>
///     Interaction logic for EmployeeInputWindow.xaml
/// </summary>
public partial class AddEmployeeDialog : Window
{
    public AddEmployeeDialog()
    {
        InitializeComponent();
        DataContext = new AddEmployeeViewModel();
        employeeInputUC.DataContext = new EmployeeInputViewModel();
        ((AddEmployeeViewModel)DataContext).EmployeeInputDataContext = (EmployeeInputViewModel)employeeInputUC.DataContext;
    }
}