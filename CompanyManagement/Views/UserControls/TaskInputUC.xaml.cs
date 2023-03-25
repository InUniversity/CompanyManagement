using System.Windows.Controls;
using CompanyManagement.Database.Implementations;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.Views.UserControls;

/// <summary>
///     Interaction logic for TaskInputUC.xaml
/// </summary>
public partial class TaskInputUC : UserControl
{
    public TaskInputUC()
    {
        InitializeComponent();
        DataContext = new TaskInputViewModel(new ProjectAssignmentDao());
    }
}