using CompanyManagement.Models;
using CompanyManagement.ViewModels.Dialogs;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.Views.Dialogs.Interfaces;
using System.Windows;

namespace CompanyManagement.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for AddDepartmentDialog.xaml
    /// </summary>
    public partial class AddDepartmentDialog : Window, IInputDialog<Department>
    {
        public IInputViewModel<Department> ViewModel { get; }
        public AddDepartmentDialog()
        {
            InitializeComponent();
            ViewModel = new AddDepartmentViewModel();
            DataContext = ViewModel;
        }
        public void ShowInputDialog()
        {
            ShowDialog();
        }
    }
}
