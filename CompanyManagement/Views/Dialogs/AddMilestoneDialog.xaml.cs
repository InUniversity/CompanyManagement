using CompanyManagement.Models;
using CompanyManagement.ViewModels.Dialogs;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.Views.Dialogs.Interfaces;
using System.Windows;

namespace CompanyManagement.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for MilestoneInputDialog.xaml
    /// </summary>
    public partial class AddMilestoneDialog : Window, IInputDialog<Milestone>
    {
        public IInputViewModel<Milestone> ViewModel { get; }

        public AddMilestoneDialog()
        {
            InitializeComponent();
            ViewModel = new AddMilestoneViewModel();
            DataContext = ViewModel;
        }

        public void ShowInputDialog()
        {
            ShowDialog();
        }
    }
}
