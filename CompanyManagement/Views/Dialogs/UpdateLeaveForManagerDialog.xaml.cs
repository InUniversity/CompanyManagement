using CompanyManagement.Models;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.ViewModels.Dialogs;
using CompanyManagement.Views.Dialogs.Interfaces;
using System.Windows;

namespace CompanyManagement.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for UpdateLeaveForManagerDialog.xaml
    /// </summary>
    public partial class UpdateLeaveForManagerDialog : Window, IInputDialog<Leave>
    {
        public IInputViewModel<Leave> ViewModel { get; }

        public UpdateLeaveForManagerDialog()
        {
            InitializeComponent();
            // DataContext = new UpdateLeaveViewModel();
        }

        public void ShowInputDialog()
        {
            ShowDialog();
        }
    }
}
