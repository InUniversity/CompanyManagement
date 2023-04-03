using System.Windows;
using System.Windows.Input;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.ViewModels.Dialogs
{
    public interface IUpdateProject
    {
        IProjects ParentDataContext { set; }
        IProjectInput ProjectInputDataContext { get; }
    }
    
    public class UpdateProjectViewModel : BaseViewModel, IUpdateProject
    {
        
        public ICommand UpdateProjectCommand { get; }

        public IProjects ParentDataContext { get; set; }
        public IProjectInput ProjectInputDataContext { get; }

        public UpdateProjectViewModel(IProjectInput projectInput)
        {
            ProjectInputDataContext = projectInput;
            UpdateProjectCommand = new RelayCommand<Window>(ExecuteUpdateCommand);
        }

        private void ExecuteUpdateCommand(Window inputWindow)
        {
            ProjectInputDataContext.TrimAllTexts();
            if (!ProjectInputDataContext.CheckAllFields())
                return;
            Project project = ProjectInputDataContext.CreateProjectInstance();
            ParentDataContext.Update(project);
            inputWindow.Close();
        }
    }
}
