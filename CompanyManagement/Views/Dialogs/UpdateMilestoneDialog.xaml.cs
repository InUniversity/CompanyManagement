using CompanyManagement.Models;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.ViewModels.Dialogs;
using System.Windows;
using CompanyManagement.Views.Dialogs.Interfaces;

namespace CompanyManagement.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for UpdateMilestoneDialog.xaml
    /// </summary>
    public partial class UpdateMilestoneDialog : Window, IInputDialog<Milestone>
    {
        public IInputViewModel<Milestone> ViewModel { get; }

        public UpdateMilestoneDialog()
        {
            InitializeComponent();
            ViewModel = new UpdateMilestoneViewModel();
            DataContext = ViewModel;
        }

        public void ShowInputDialog()
        {
            ShowDialog();
        }
    }
}
