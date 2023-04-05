using System.Windows;
using System.Windows.Input;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.ViewModels.UserControls;
using CompanyManagement.ViewModels.UserControls.Interfaces;

namespace CompanyManagement.ViewModels.Dialogs
{
    public class UpdateProjectViewModel : BaseViewModel, IDialogViewModel
    {
        
        public ICommand UpdateProjectCommand { get; }

        public IEditDBViewModel ParentDataContext { get; set; }
        public IProjectInput ProjectInputDataContext { get; }

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
            Project project = ProjectInputDataContext.CreateProjectInstance();
            ParentDataContext.UpdateToDB(project);
            inputWindow.Close();
        }
        
        public void Retrieve(object project)
        {
            ProjectInputDataContext.RetrieveProject(project as Project);
        }
    }
}
