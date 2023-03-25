using System.Windows;
using System.Windows.Input;

namespace CompanyManagement.ViewModels
{
    public class UpdateProjectViewModel : BaseViewModel
    {
        public ICommand UpdateProjectCommand { get; set; }

        public ProjectsViewModel ParentDataContext { get; set; }
        public ProjectInputViewModel ProjectInputDataContext { get; set; }

        public UpdateProjectViewModel()
        {
            SetCommands();
        }

        private void SetCommands()
        {
            ProjectInputDataContext = new ProjectInputViewModel();          
            UpdateProjectCommand = new RelayCommand<Window>(ExecuteUpdateCommand);
        }

        private void ExecuteUpdateCommand(Window inputWindow)
        {
            ProjectInputDataContext.TrimAllTexts();
            Project proj = ProjectInputDataContext.CreateProjectInstance();          
            ParentDataContext.Update(proj);
            inputWindow.Close();
        }
    }
}
