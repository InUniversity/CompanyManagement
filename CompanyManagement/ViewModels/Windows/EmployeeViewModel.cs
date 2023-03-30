using CompanyManagement.ViewModels.Base;
using CompanyManagement.Views.UserControls;
using System.Windows.Controls;
using System.Windows.Input;

namespace CompanyManagement.ViewModels.Windows
{
    public class EmployeeViewModel : BaseViewModel
    {
        
        private ContentControl currentChildView = new ProjectsUC();
        public ContentControl CurrentChildView { get => currentChildView; set { currentChildView = value; OnPropertyChanged(nameof(CurrentChildView)); } }

        public ICommand ShowProjectsView { get; }

        public EmployeeViewModel()
        {
            ShowProjectsView = new RelayCommand<object>(ExecuteShowProjectsView);
        }

        private void ExecuteShowProjectsView(object obj)
        {
            currentChildView.Content = new ProjectsUC();
        }
    }
}
