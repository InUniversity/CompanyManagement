using System.Windows;
using CompanyManagement.ViewModels.Dialogs;

namespace CompanyManagement.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for AddTimeKeepingDialog.xaml
    /// </summary>
    public partial class AddTimeKeepingDialog : Window
    {
        public AddTimeKeepingDialog()
        {
            InitializeComponent();
            DataContext = new AddTimeKeepingViewModel();
        }
    }
}