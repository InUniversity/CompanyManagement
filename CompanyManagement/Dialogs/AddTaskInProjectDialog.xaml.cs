using CompanyManagement.ViewModels;
using System.Windows;

namespace CompanyManagement.Dialogs
{
    /// <summary>
    /// Interaction logic for AddTaskInProject.xaml
    /// </summary>
    public partial class AddTaskInProjectDialog : Window
    {
        public AddTaskInProjectDialog()
        {
            InitializeComponent();
            DataContext = new AddTaskInProjectViewModel();
        }
    }
}
