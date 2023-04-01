using CompanyManagement.ViewModels.Dialogs;
using CompanyManagement.ViewModels.UserControls;
using System.Windows;

namespace CompanyManagement.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for UpdateTimeKeepingDialog.xaml
    /// </summary>
    public partial class UpdateTimeKeepingDialog : Window
    {
        public UpdateTimeKeepingDialog()
        {
            InitializeComponent();
            ITimeKeepingInput timeKeepingInput = new TimeKeepingInputViewModel();
            DataContext = new UpdateTimeKeepingViewModel(timeKeepingInput);
        }
    }
}
