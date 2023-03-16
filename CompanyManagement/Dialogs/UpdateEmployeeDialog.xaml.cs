using CompanyManagement.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace CompanyManagement.Dialogs
{
    /// <summary>
    /// Interaction logic for UpdateEmployeeDialog.xaml
    /// </summary>
    public partial class UpdateEmployeeDialog : Window
    {

        public UpdateEmployeeDialog()
        {
            InitializeComponent();
            DataContext = new UpdateEmployeeViewModel();
            ((UpdateEmployeeViewModel)DataContext).EmployeeInputDataContext = (EmployeeInputViewModel)employeeInputUC.DataContext;
        }
    }
}
