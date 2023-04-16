using System.Windows;
using CompanyManagement.Models;
using CompanyManagement.ViewModels.Dialogs;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.Views.Dialogs.Interfaces;

namespace CompanyManagement.Views.Dialogs
{
    /// <summary>
    ///     Interaction logic for AddTaskInProject.xaml
    /// </summary>
    public partial class AddTaskDialog : Window, IInputDialog<TaskInProject>
    {
        public IInputViewModel<TaskInProject> ViewModel { get; }
        
        public AddTaskDialog()
        {
            InitializeComponent();
            ViewModel = new AddTaskViewModel();
            DataContext = ViewModel;
        }

        public void ShowInputDialog()
        {
            ShowDialog();
        }
    }
}