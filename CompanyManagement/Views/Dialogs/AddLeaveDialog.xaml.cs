using CompanyManagement.Models;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.Views.Dialogs.Interfaces;
using System.Windows;
using CompanyManagement.ViewModels.Dialogs;

namespace CompanyManagement.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for AddLeaveDialog.xaml
    /// </summary>
    public partial class AddLeaveDialog : Window, IInputDialog<LeaveRequest>
    {
        public IInputViewModel<LeaveRequest> ViewModel { get; }

        public AddLeaveDialog()
        {
            InitializeComponent();
            ViewModel = new AddLeaveViewModel();
            DataContext = ViewModel;
        }

        public void ShowInputDialog()
        {
            ShowDialog();
        }
    }
}
