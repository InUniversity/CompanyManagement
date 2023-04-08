using System.Windows.Input;
using System.Windows;
using CompanyManagement.ViewModels.UserControls;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.ViewModels.UserControls.Interfaces;
using CompanyManagement.Views.Dialogs;
using System.Windows.Controls;
using System.Windows.Media;

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
            AlertDialog alertDialog = new AlertDialog();
            ((AlertDialogViewModel)alertDialog.DataContext).Message="Bạn chắc chắn muốn thêm dự án !";      
            alertDialog.Show();
            if (((AlertDialogViewModel)alertDialog.DataContext).YesSelection)
            {
                Project project = ProjectInputDataContext.CreateProjectInstance();
                ParentDataContext.AddToDB(project);              
            }
            alertDialog.Close();    
            inputWindow.Close();
        }
        
        public void Retrieve(object project)
        {
            ProjectInputDataContext.RetrieveProject(project as Project);
        }
    }
}
