using System.Windows;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for CheckOutDialog.xaml
    /// </summary>
    public partial class CheckOutDialog : Window
    {
        public CheckOutViewModel ViewModel { get; }
        
        public CheckOutDialog()
        {
            InitializeComponent();
            ViewModel = new CheckOutViewModel();
            DataContext = ViewModel;
        }
    }
}
