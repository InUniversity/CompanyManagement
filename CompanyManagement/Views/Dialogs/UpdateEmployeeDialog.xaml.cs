using System.Windows;
using CompanyManagement.ViewModels.Dialogs;

namespace CompanyManagement.Views.Dialogs;

/// <summary>
///     Interaction logic for UpdateEmployeeDialog.xaml
/// </summary>
public partial class UpdateEmployeeDialog : Window
{
    public UpdateEmployeeDialog()
    {
        InitializeComponent();
        DataContext = new UpdateEmployeeViewModel();
    }
}