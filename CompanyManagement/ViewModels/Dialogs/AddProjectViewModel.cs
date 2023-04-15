using System;
using System.Windows.Input;
using System.Windows;
using CompanyManagement.ViewModels.UserControls;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.Services;

namespace CompanyManagement.ViewModels.Dialogs
{
    public class AddProjectViewModel : BaseViewModel, IInputViewModel<Project>
    {
        public ICommand AddProjectCommand { get; }

        public IProjectInput ProjectInputDataContext { get; }
        private Action<Project> submitObjectAction;

        public AddProjectViewModel()
        {
            ProjectInputDataContext = new ProjectInputViewModel();
            AddProjectCommand = new RelayCommand<Window>(AddCommand);
        }

        private void AddCommand(Window inputWindow)
        {
            ProjectInputDataContext.TrimAllTexts();
            if (!ProjectInputDataContext.CheckAllFields())
                return;
            AlertDialogService dialog = new AlertDialogService(
               "Thêm dự án",
               "Bạn chắc chắn muốn thêm dự án !",
               () =>
               {
                   Project project = ProjectInputDataContext.CreateProjectInstance(); 
                   submitObjectAction?.Invoke(project);
                   inputWindow.Close();
               }, () => { }); 
            dialog.Show();           
        }

        public void ReceiveObject(Project project)
        {
            ProjectInputDataContext.Receive(project);
        }

        public void ReceiveSubmitAction(Action<Project> submitObjectAction)
        {
            this.submitObjectAction = submitObjectAction;
        }
    }
}
