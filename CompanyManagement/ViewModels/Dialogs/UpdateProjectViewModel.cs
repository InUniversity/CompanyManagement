using System;
using System.Windows;
using System.Windows.Input;
using CompanyManagement.Services;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.ViewModels.Dialogs
{
    public class UpdateProjectViewModel : BaseViewModel, IInputViewModel<Project>
    {
        public ICommand UpdateProjectCommand { get; }

        public ProjectInputViewModel ProjectInputDataContext { get; }
        private Action<Project> submitObjectAction;

        public UpdateProjectViewModel()
        {
            ProjectInputDataContext = new ProjectInputViewModel();
            UpdateProjectCommand = new RelayCommand<Window>(ExecuteUpdateCommand);
        }

        private void ExecuteUpdateCommand(Window inputWindow)
        {
            ProjectInputDataContext.TrimAllTexts();
            if (!ProjectInputDataContext.CheckAllFields())
                return;
            AlertDialogService dialog = new AlertDialogService(
               "Cập nhật dự án",
               "Bạn chắc chắn muốn cập nhật dự án !",
               () =>
               {
                   Project project = ProjectInputDataContext.ProjectIns;
                   submitObjectAction?.Invoke(project);
                   inputWindow.Close();
               }, () => { });
            dialog.Show();
        }

        public void ReceiveObject(Project project)
        {
            ProjectInputDataContext.ProjectIns = project;
        }

        public void ReceiveSubmitAction(Action<Project> submitObjectAction)
        {
            this.submitObjectAction = submitObjectAction;
        }
    }
}
