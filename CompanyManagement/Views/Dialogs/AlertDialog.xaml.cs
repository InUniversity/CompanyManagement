using System.Windows;
using CompanyManagement.ViewModels.Dialogs;

namespace CompanyManagement.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for AlertDialog.xaml
    /// </summary>
    public partial class AlertDialog : Window
    {
        public AlertDialogViewModel ViewModel { get; }

        public AlertDialog()
        {
            InitializeComponent();
            ViewModel = new AlertDialogViewModel();
            DataContext = ViewModel;
        }
    }
}
