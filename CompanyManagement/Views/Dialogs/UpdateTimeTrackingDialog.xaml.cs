using CompanyManagement.ViewModels.Dialogs;
using CompanyManagement.ViewModels.UserControls;
using System.Windows;

namespace CompanyManagement.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for UpdateTimeTrackingDialog.xaml
    /// </summary>
    public partial class UpdateTimeTrackingDialog : Window
    {
        public UpdateTimeTrackingDialog()
        {
            InitializeComponent();
            DataContext = new UpdateTimeTrackingViewModel();
        }
    }
}
