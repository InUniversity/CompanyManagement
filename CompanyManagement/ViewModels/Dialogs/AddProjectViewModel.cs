using System.Windows.Input;
using System.Windows;
using CompanyManagement.ViewModels.UserControls;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.ViewModels.UserControls.Interfaces;

namespace CompanyManagement.ViewModels.Dialogs
{
    public class AddProjectViewModel : BaseViewModel, IDialogViewModel
    {

        public ICommand AddProjectCommand { get; }

        public IEditDBViewModel ParentDataContext { get; set; }
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
            ParentDataContext.AddToDB(project);
            inputWindow.Close();
        }
        public void Retrieve(object project)
        {
            ProjectInputDataContext.RetrieveProject(project as Project);
        }
    }
}
