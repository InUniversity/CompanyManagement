using System.Windows;
using CompanyManagement.Models;
using CompanyManagement.ViewModels.Dialogs;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.Views.Dialogs.Interfaces;

namespace CompanyManagement.Views.Dialogs
{
    /// <summary>
    ///     Interaction logic for UpdateTaskInProject.xaml
    /// </summary>
    public partial class UpdateTaskDialog : Window, IInputDialog<TaskInProject>
    {
        public IInputViewModel<TaskInProject> ViewModel { get; }
        
        public UpdateTaskDialog()
        {
            InitializeComponent();
            ViewModel = new UpdateTaskViewModel();
            DataContext = ViewModel;
        }

        public void ShowInputDialog()
        {
            ShowDialog();
        }
    }
}