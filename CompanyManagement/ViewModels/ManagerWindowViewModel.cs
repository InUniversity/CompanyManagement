using CompanyManagement.ViewModels.UserControls;
using CompanyManagement.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace CompanyManagement.ViewModels
{
    class ManagerWindowViewModel : BaseViewModel
    {
        private ContentControl currentChildView= new AssignUC();

        public ContentControl CurrentChildView { get => currentChildView; set { currentChildView = value; OnPropertyChanged(nameof (CurrentChildView)); } }

        public ICommand ShowAssignView { get;}

        public ICommand ShowEmployeesView { get;}

        public ICommand ShowWorkSheduleView { get; }

        public ICommand ShowNotifytView { get; }    

        public ICommand ShowSettingsView { get; }

        public ManagerWindowViewModel()
        {
            ShowAssignView = new RelayCommand<object>(ExecuteShowAssignView);
            ShowEmployeesView = new RelayCommand<object>(ExecuteShowEmployeesView);
            ShowWorkSheduleView = new RelayCommand<object>(ExecuteShowWorkSheduleView);
            ShowNotifytView = new RelayCommand<object>(ExecuteShowNotifyView);
            ShowSettingsView = new RelayCommand<object>(ExecuteShowSettingsView);
        }

        private void ExecuteShowSettingsView(object obj)
        {
            currentChildView.Content = new SettingsUC();
        }

        private void ExecuteShowNotifyView(object obj)
        {
            currentChildView.Content = new NotifyUC();
        }

        private void ExecuteShowWorkSheduleView(object obj)
        {
            currentChildView.Content = new WorkSheduleUC();
        }

        private void ExecuteShowEmployeesView(object obj)
        {            
            currentChildView.Content = new EmployeesUC();
        }

        private void ExecuteShowAssignView(object obj)
        {
            currentChildView.Content = new AssignUC();

        }
    }
}
