using System.Windows;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for CheckOutDialog.xaml
    /// </summary>
    public partial class CheckOutDialog : Window
    {
        public IDialogViewModel ViewModel { get; }
        
        public CheckOutDialog()
        {
            InitializeComponent();
            ViewModel = new CheckOutViewModel();
            DataContext = ViewModel;
        }
    }
}
