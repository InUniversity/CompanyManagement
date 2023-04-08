using CompanyManagement.Models;
using System.Windows;
using System.Windows.Input;
using CompanyManagement.ViewModels.UserControls;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.ViewModels.UserControls.Interfaces;
using CompanyManagement.Views.Dialogs;

namespace CompanyManagement.ViewModels.Dialogs
{
    public class UpdateTaskViewModel : BaseViewModel, IDialogViewModel
    {

        public ICommand UpdateTaskCommand { get; set; }

        public IEditDBViewModel ParentDataContext { get; set; }
        public ITaskInput TaskInputDataContext { get; set; }

        public UpdateTaskViewModel()
        {
            TaskInputDataContext = new TaskInputViewModel();
            SetCommands();
        }

        private void SetCommands()
        {
            UpdateTaskCommand = new RelayCommand<Window>(UpdateCommand);
        }

        private void UpdateCommand(Window inputWindow)
        {
            TaskInputDataContext.TrimAllTexts();
            if (!TaskInputDataContext.CheckAllFields())
                return;
            AlertDialog alertDialog = new AlertDialog();
            ((AlertDialogViewModel)alertDialog.DataContext).Message = "       Bạn chắc chắn muốn \n cập nhật dữ liệu nhiệm vụ !";
            alertDialog.ShowDialog();
            if (((AlertDialogViewModel)alertDialog.DataContext).YesSelection)
            {
                TaskInProject task = TaskInputDataContext.CreateTaskInProjectInstance();
                ParentDataContext.UpdateToDB(task);
            }
            inputWindow.Close();
        }
        
        public void Retrieve(object task)
        {
            TaskInputDataContext.RetrieveTask(task as TaskInProject);
        }
    }
}
