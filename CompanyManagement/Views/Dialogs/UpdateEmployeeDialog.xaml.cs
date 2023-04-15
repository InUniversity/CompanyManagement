using System.Windows;
using CompanyManagement.ViewModels.Dialogs;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.Views.Dialogs.Interfaces;

namespace CompanyManagement.Views.Dialogs
{
    /// <summary>
    ///     Interaction logic for UpdateEmployeeDialog.xaml
    /// </summary>
    public partial class UpdateEmployeeDialog : Window, IInputDialog<Employee>
    {
        public IInputViewModel<Employee> ViewModel { get; }
        
        public UpdateEmployeeDialog()
        {
            InitializeComponent();
            ViewModel = new UpdateEmployeeViewModel();
            DataContext = ViewModel;
        }

        public void ShowInputDialog()
        {
            ShowDialog();
        }
    }
}