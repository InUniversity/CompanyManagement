using System.Windows;
using CompanyManagement.ViewModels.Dialogs;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.Views.Dialogs.Interfaces;

namespace CompanyManagement.Views.Dialogs
{
    /// <summary>
    ///     Interaction logic for AddProjectDialog.xaml
    /// </summary>
    public partial class AddProjectDialog : Window, IInputDialog<Project>
    {
        public IInputViewModel<Project> ViewModel { get; }
        
        public AddProjectDialog()
        {
            InitializeComponent();
            ViewModel = new AddProjectViewModel();
            DataContext = ViewModel;
        }

        public void ShowInputDialog()
        {
            ShowDialog();
        }
    }
}