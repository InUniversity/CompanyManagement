using System.Windows;
using System.Windows.Input;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.ViewModels.Dialogs
{
    public class UpdateProjectViewModel : BaseViewModel
    {
        public ICommand UpdateProjectCommand { get; set; }

        public IProjects ParentDataContext { get; set; }
        public IProjectInput ProjectInputInputDataContext { get; set; }

        public UpdateProjectViewModel(IProjectInput projectInput)
        {
            ProjectInputInputDataContext = projectInput;
            SetCommands();
        }

        private void SetCommands()
        {
            UpdateProjectCommand = new RelayCommand<Window>(ExecuteUpdateCommand);
        }

        private void ExecuteUpdateCommand(Window inputWindow)
        {
            ProjectInputInputDataContext.TrimAllTexts();
            Project project = ProjectInputInputDataContext.CreateProjectInstance();
            ParentDataContext.Update(project);
            inputWindow.Close();
        }
    }
}
