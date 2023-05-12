using CompanyManagement.Models;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.Views.Dialogs.Interfaces;
using System.Windows;

namespace CompanyManagement.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for ResponseLeaveDialog.xaml
    /// </summary>
    public partial class ResponseLeaveDialog : Window, IInputDialog<LeaveRequest>
    {
        public IInputViewModel<LeaveRequest> ViewModel { get; }

        public ResponseLeaveDialog()
        {
            InitializeComponent();
            ViewModel = new ResponseLeaveViewModel();
            DataContext = ViewModel;
        }

        public void ShowInputDialog()
        {
            ShowDialog();
        }
    }
}
