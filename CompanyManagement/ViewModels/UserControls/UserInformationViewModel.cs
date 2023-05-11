using CompanyManagement.ViewModels.Base;
using CompanyManagement.Views.UserControls;
using System.Windows.Controls;
using CompanyManagement.ViewModels.Windows;
using CompanyManagement.Models;
using System.Windows.Input;
using System;
using CompanyManagement.Database;

namespace CompanyManagement.ViewModels.UserControls
{
    public class UserInformationViewModel : BaseViewModel 
    {
        private TimeSheetUC WorkingView = new TimeSheetUC();
        private EmployeeInputUC MyInformationView = new EmployeeInputUC();
        private PersonalSalaryUC HistorySalaryView = new PersonalSalaryUC();

        private bool statusWorkingView=false;
        public bool StatusWorkingView { get => statusWorkingView; set { statusWorkingView = value; OnPropertyChanged(); } }

        private bool statusMyInformationView = false;
        public bool StatusMyInformationView { get => statusMyInformationView; set { statusMyInformationView = value; OnPropertyChanged(); } }
        
        private bool statusPersonalSalaryView = false;
        public bool StatusPersonalSalaryView { get => statusPersonalSalaryView; set { statusPersonalSalaryView = value; OnPropertyChanged(); } }

        private ContentControl currentChildView = new ContentControl();
        public ContentControl CurrentChildView { get => currentChildView; set { currentChildView = value; OnPropertyChanged(); } }

        public ICommand ShowWorkingView { get; private set; }
        public ICommand ShowMyInformationView { get; private set; }
        public ICommand ShowPersonalSalaryView { get; private set; }

        public UserInformationViewModel() 
        {
            SetCommands();
            ExcuteShowMyInformationView(null);
        }

        private void SetCommands()
        {
            ShowWorkingView = new RelayCommand<object>(ExcuteShowWorkingView);
            ShowMyInformationView = new RelayCommand<object>(ExcuteShowMyInformationView);
            ShowPersonalSalaryView = new RelayCommand<object>(ExcuteShowPersonalSalaryView);
        }

        private void ExcuteShowMyInformationView(object obj)
        {
            Employee currentEmployee = CurrentUser.Ins.EmployeeIns;
            currentEmployee.EmplRole = (new RolesDao()).SearchByID(currentEmployee.RoleID);
            ((EmployeeInputViewModel)MyInformationView.DataContext).EmployeeIns = currentEmployee;
            CurrentChildView = MyInformationView;
            StatusMyInformationView= true; 
        }

        private void ExcuteShowWorkingView(object obj)
        {
            CurrentChildView = WorkingView;
            StatusWorkingView= true;
        }

        private void ExcuteShowPersonalSalaryView(object obj)
        {
            CurrentChildView = HistorySalaryView;
            StatusPersonalSalaryView = true;
        }
    }
}
