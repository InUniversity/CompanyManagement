using CompanyManagement.Models;
using CompanyManagement.ViewModels.Dialogs;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
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
            DataContext = new UpdateLeaveViewModel();
        }

        public void ShowInputDialog()
        {
            ShowDialog();
        }
    }
}
