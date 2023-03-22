using System.Windows;
using CompanyManagement.ViewModels;

namespace CompanyManagement.Dialogs;

/// <summary>
///     Interaction logic for AddTaskInProject.xaml
/// </summary>
public partial class AddTaskDialog : Window
{
    public AddTaskDialog()
    {
        InitializeComponent();
        DataContext = new AddTaskViewModel();
    }
}