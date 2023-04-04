using System.Windows.Input;
using System.Windows;
using CompanyManagement.Database.Implementations;
using CompanyManagement.ViewModels.UserControls;
using CompanyManagement.ViewModels.Base;

namespace CompanyManagement.ViewModels.Dialogs
{
    public interface IAddProject
    {
        IProjects ParentDataContext { set; }
        IProjectInput ProjectInputDataContext { get; }
    }

    public class AddProjectViewModel : BaseViewModel, IAddProject
    {

        public ICommand AddProjectCommand { get; }

        public IProjects ParentDataContext { get; set; }
        public IProjectInput ProjectInputDataContext { get; }

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
            Project project = ProjectInputDataContext.CreateProjectInstance();
            ParentDataContext.Add(project);
            inputWindow.Close();
        }
    }
}
