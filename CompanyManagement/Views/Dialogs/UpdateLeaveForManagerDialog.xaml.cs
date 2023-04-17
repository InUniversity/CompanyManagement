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
    public partial class UpdateLeaveForManagerDialog : Window, IInputDialog<Leave>
    {
        public IInputViewModel<Leave> ViewModel { get; }

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
