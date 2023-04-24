using System.Windows;
using CompanyManagement.ViewModels.Dialogs;

namespace CompanyManagement.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for AddTimeTrackingDialog.xaml
    /// </summary>
    public partial class AddTimeTrackingDialog : Window
    {
        public AddTimeTrackingDialog()
        {
            InitializeComponent();
            DataContext = new AddTimeTrackingViewModel();
        }
    }
}