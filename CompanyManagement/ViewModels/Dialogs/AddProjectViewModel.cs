using System.Windows.Input;
using System.Windows;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.ViewModels.Dialogs
{
    public class AddProjectViewModel : BaseViewModel
    {

        public ICommand AddProjectCommand { get; set; }

        public ProjectsViewModel ParentDataContext { get; set; }

        public ProjectInputViewModel ProjectInputDataContext { get; set; }

        public AddProjectViewModel()
        {
            SetCommands();
        }

        private void SetCommands()
        {
            ProjectInputDataContext = new ProjectInputViewModel();
            AddProjectCommand = new RelayCommand<Window>(AddCommand, p => ProjectInputDataContext.CheckAllFields());// nó ko checkallfields đc
        }

        private void AddCommand(Window inputWindow)
        {
            ProjectInputDataContext.TrimAllTexts();
            Project proj = ProjectInputDataContext.CreateProjectInstance();
            ParentDataContext.Add(proj);
            inputWindow.Close();
        }
    }
}
