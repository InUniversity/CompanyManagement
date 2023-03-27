using System.Windows;
using System.Windows.Input;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.ViewModels.Dialogs
{
    public class UpdateProjectViewModel : BaseViewModel
    {
        
        public ICommand UpdateProjectCommand { get; set; }

        public IProjects ParentDataContext { get; set; }
        public IProjectInput ProjectInputDataContext { get; set; }

        public UpdateProjectViewModel(IProjectInput projectInput)
        {
            ProjectInputDataContext = projectInput;
            UpdateProjectCommand = new RelayCommand<Window>(ExecuteUpdateCommand);
        }

        private void ExecuteUpdateCommand(Window inputWindow)
        {
            ProjectInputDataContext.TrimAllTexts();
            Project project = ProjectInputDataContext.CreateProjectInstance();
            ParentDataContext.Update(project);
            inputWindow.Close();
        }
    }
}
