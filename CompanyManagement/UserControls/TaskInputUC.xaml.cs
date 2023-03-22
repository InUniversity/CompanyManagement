using System.Windows.Controls;
using CompanyManagement.ViewModels;

namespace CompanyManagement.UserControls
{
    /// <summary>
    /// Interaction logic for TaskInputUC.xaml
    /// </summary>
    public partial class TaskInputUC : UserControl
    {
        public TaskInputUC()
        {
            InitializeComponent();
            DataContext = new TasksInProjectViewModel();
        }
    }
}
