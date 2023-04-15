using System.Windows;
using CompanyManagement.Models;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.ViewModels.UserControls;
using CompanyManagement.Views.Dialogs.Interfaces;

namespace CompanyManagement.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for CheckOutDialog.xaml
    /// </summary>
    public partial class CheckOutDialog : Window, IInputDialog<CheckInOut>
    {
        public IInputViewModel<CheckInOut> ViewModel { get; }

        public CheckOutDialog()
        {
            InitializeComponent();
            ViewModel = new CheckOutViewModel();
            DataContext = ViewModel;
        }

        public void ShowInputDialog()
        {
            ShowDialog();
        }
    }
}
