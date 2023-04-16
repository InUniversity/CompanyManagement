using CompanyManagement.Models;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.ViewModels.Dialogs;
using CompanyManagement.Views.Dialogs.Interfaces;
using System.Windows;

namespace CompanyManagement.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for UpdateLeaveDialog.xaml
    /// </summary>
    public partial class UpdateLeaveDialog : Window, IInputDialog<Leave>
    {
        public IInputViewModel<Leave> ViewModel { get; }

        public UpdateLeaveDialog()
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
