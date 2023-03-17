using CompanyManagement.ViewModels;
using System.Windows;

namespace CompanyManagement.Dialogs
{
    /// <summary>
    /// Interaction logic for UpdateProjectDialog.xaml
    /// </summary>
    public partial class UpdateProjectDialog 
        : Window
    {
        public UpdateProjectDialog()
        {
            InitializeComponent();
            DataContext = new UpdateProjectViewModel();
            projectInputUC.DataContext = new ProjectInputViewModel();
            ((UpdateProjectViewModel)DataContext).ProjectInputDataContext = (ProjectInputViewModel)projectInputUC.DataContext;
        }
    }
}
