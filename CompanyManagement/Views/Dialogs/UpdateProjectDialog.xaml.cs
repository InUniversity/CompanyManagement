using System.Windows;
using CompanyManagement.ViewModels.Dialogs;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.Views.Dialogs.Interfaces;

namespace CompanyManagement.Views.Dialogs
{
    /// <summary>
    ///     Interaction logic for UpdateProjectDialog.xaml
    /// </summary>
    public partial class UpdateProjectDialog : Window, IInputDialog<Project>
    {
        public IInputViewModel<Project> ViewModel { get; }
        
        public UpdateProjectDialog()
        {
            InitializeComponent();
            ViewModel = new UpdateProjectViewModel();
            DataContext = ViewModel;
        }

        public void ShowInputDialog()
        {
            ShowDialog();
        }
    }
}