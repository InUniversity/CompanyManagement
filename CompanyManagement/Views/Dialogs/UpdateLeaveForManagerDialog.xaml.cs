using CompanyManagement.Models;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.Views.Dialogs.Interfaces;
using System.Windows;
using CompanyManagement.ViewModels.Dialogs;

namespace CompanyManagement.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for UpdateLeaveForManagerDialog.xaml
    /// </summary>
    public partial class UpdateLeaveForManagerDialog : Window, IInputDialog<LeaveRequest>
    {
        public IInputViewModel<LeaveRequest> ViewModel { get; }

        public UpdateLeaveForManagerDialog()
        {
            InitializeComponent();
            ViewModel = new UpdateLeaveViewModel();
            DataContext = ViewModel;
        }

        public void ShowInputDialog()
        {
            ShowDialog();
        }
    }
}
