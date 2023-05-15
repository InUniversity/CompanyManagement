using CompanyManagement.ViewModels.UserControls;
using System.Windows;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.Models;
using CompanyManagement.Views.Dialogs.Interfaces;

namespace CompanyManagement.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for CheckInDialog.xaml
    /// </summary>
    public partial class CheckInDialog : Window, IInputDialog<TimeSheet>
    {
        public IInputViewModel<TimeSheet> ViewModel { get; }

        public CheckInDialog()
        {
            InitializeComponent();
            ViewModel = new CheckInViewModel();
            DataContext = ViewModel; 
        }

        public void ShowInputDialog()
        {
            ShowDialog();
        }
    }
}
