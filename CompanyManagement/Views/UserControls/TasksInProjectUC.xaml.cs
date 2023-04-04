using System.Windows.Controls;
using CompanyManagement.Database.Implementations;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.Views.UserControls;

/// <summary>
///     Interaction logic for TasksInProjectUC.xaml
/// </summary>
public partial class TasksInProjectUC : UserControl
{
    public TasksInProjectUC()
    {
        InitializeComponent();
        DataContext = new TasksInProjectViewModel();
    }
}