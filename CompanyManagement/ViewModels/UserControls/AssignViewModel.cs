using CompanyManagement.ViewModels.Base;
using CompanyManagement.Views.UserControls;
using System.Windows.Controls;
using System.Windows.Input;

namespace CompanyManagement.ViewModels.UserControls
{
    public class AssignViewModel : BaseViewModel
    {
        private ContentControl currentChildView = new ProjectsUC();

        public ContentControl CurrentChildView { get => currentChildView; set { currentChildView = value; OnPropertyChanged(nameof(CurrentChildView)); } }
      

        private ContentControl currentMenu ;

        public ContentControl CurrentMenu { get => currentMenu; set { currentMenu = value; OnPropertyChanged(nameof(CurrentChildView)); } }

        public ICommand ShowProjectsView { get; }

        public ICommand ShowTasksView { get; }

        

        public AssignViewModel() 
        {
            ShowProjectsView = new RelayCommand<object>(ExecuteShowProjectsView);
            ShowTasksView = new RelayCommand<object>((p) => { ExecuteShowTasksView(); }, p => { return (((ProjectsViewModel)currentChildView.DataContext).SelectedProject != null) ; }) ;
        }

        private void ExecuteShowTasksView()
        {        
              
            TasksInProjectUC tasksInProjectUC = new TasksInProjectUC();
            tasksInProjectUC.DataContext = ((ProjectsViewModel)currentChildView.DataContext).TasksDataContext;
            currentChildView.Content = tasksInProjectUC;
            currentMenu.Content = new MenuInProjectUC();
   
        }

        private void ExecuteShowProjectsView(object obj)
        {
            CurrentChildView= new ProjectsUC();
            
        }
    }
}
