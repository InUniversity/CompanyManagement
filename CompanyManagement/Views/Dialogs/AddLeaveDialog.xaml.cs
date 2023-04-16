using CompanyManagement.Models;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.ViewModels.Dialogs;
using CompanyManagement.Views.Dialogs.Interfaces;
using System.Windows;

namespace CompanyManagement.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for AddLeaveDialog.xaml
    /// </summary>
    public partial class AddLeaveDialog : Window, IInputDialog<Leave>
    {
        public IInputViewModel<Leave> ViewModel { get; }

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
