using System.Windows.Controls;
using CompanyManagement.Database.Implementations;
using CompanyManagement.ViewModels;

namespace CompanyManagement.UserControls;

/// <summary>
///     Interaction logic for TasksInProjectUC.xaml
/// </summary>
public partial class TasksInProjectUC : UserControl
{
    public TasksInProjectUC()
    {
        InitializeComponent();
        DataContext = new TasksInProjectViewModel(new TaskInProjectDao(), new ProjectAssignmentDao());
    }
}