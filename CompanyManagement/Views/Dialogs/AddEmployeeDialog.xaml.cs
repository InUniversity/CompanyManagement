using System.Windows;
using CompanyManagement.ViewModels.Dialogs;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.Views.Dialogs.Interfaces;

namespace CompanyManagement.Views.Dialogs
{
    /// <summary>
    ///     Interaction logic for EmployeeInputWindow.xaml
    /// </summary>
    public partial class AddEmployeeDialog : Window, IInputDialog<Employee>
    {
        public IInputViewModel<Employee> ViewModel { get; }

        public AddEmployeeDialog()
        {
            InitializeComponent();
            ViewModel = new AddEmployeeViewModel();
            DataContext = ViewModel;
        }

        public void ShowInputDialog()
        {
            ShowDialog();
        }
    }
}