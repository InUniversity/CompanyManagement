using System.Windows.Input;
using System.Windows;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.ViewModels.Dialogs
{
    public class AddProjectViewModel : BaseViewModel
    {

        public ICommand AddProjectCommand { get; set; }

        public IProjects ParentDataContext { get; set; }
        public IProjectInput ProjectInputDataContext { get; set; }

        public AddProjectViewModel(IProjectInput projectInput)
        {
            ProjectInputDataContext = projectInput;
            SetCommands();
        }

        private void SetCommands()
        {
            AddProjectCommand = new RelayCommand<Window>(AddCommand, CheckAllFields);
        }

        private void AddCommand(Window inputWindow)
        {
            ProjectInputDataContext.TrimAllTexts();
            Project project = ProjectInputDataContext.CreateProjectInstance();
            ParentDataContext.Add(project);
            inputWindow.Close();
        }

        private bool CheckAllFields(object p)
        {
            return ProjectInputDataContext.CheckAllFields();
        }
    }
}
