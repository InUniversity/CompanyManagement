using CompanyManagement.ViewModels;
using System.Windows.Controls;

namespace CompanyManagement.UserControls
{
    /// <summary>
    /// Interaction logic for TasksInProjectUC.xaml
    /// </summary>
    public partial class TasksInProjectUC : UserControl
    {
        public TasksInProjectUC()
        {
            InitializeComponent();
            DataContext = new TasksInProjectViewModel();
        }
    }
}
