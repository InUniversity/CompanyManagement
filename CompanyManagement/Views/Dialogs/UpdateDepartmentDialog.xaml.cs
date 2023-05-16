using CompanyManagement.Models;
using CompanyManagement.ViewModels.Dialogs;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.Views.Dialogs.Interfaces;
using System.Windows;

namespace CompanyManagement.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for UpdateDepartmentDialog.xaml
    /// </summary>
    public partial class UpdateDepartmentDialog : Window, IInputDialog<Department>
    {
        public IInputViewModel<Department> ViewModel { get; }

        public UpdateDepartmentDialog()
        {
            InitializeComponent();
            ViewModel = new UpdateDepartmentViewModel();
            DataContext = ViewModel;
        }

        public void ShowInputDialog()
        {
            ShowDialog();
        }
    }
}
