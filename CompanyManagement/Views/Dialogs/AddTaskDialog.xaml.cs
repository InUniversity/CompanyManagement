using System.Windows;
using CompanyManagement.Database.Implementations;
using CompanyManagement.ViewModels.Dialogs;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.Views.Dialogs;

/// <summary>
///     Interaction logic for AddTaskInProject.xaml
/// </summary>
public partial class AddTaskDialog : Window
{
    public AddTaskDialog()
    {
        InitializeComponent();
        DataContext = new AddTaskViewModel(new TaskInputViewModel(new ProjectAssignmentDao(), new TaskStatusDao()));
    }
}