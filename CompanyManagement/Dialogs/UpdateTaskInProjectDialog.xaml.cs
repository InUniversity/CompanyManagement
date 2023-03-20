using CompanyManagement.ViewModels;
using System.Windows;

namespace CompanyManagement.Dialogs
{
    /// <summary>
    /// Interaction logic for UpdateTaskInProject.xaml
    /// </summary>
    public partial class UpdateTaskInProjectDialog : Window
    {
        public UpdateTaskInProjectDialog()
        {
            InitializeComponent();
            DataContext = new UpdateTaskInProjectViewModel();
        }
    }
}
