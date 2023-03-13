using System.Windows.Controls;
using CompanyManagement.Dialogs;
using CompanyManagement.Services;
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
        EmployeeInputDialogService.RegisterDialog<AddEmployeeDialog, EmployeeInputViewModel>();
    }
}