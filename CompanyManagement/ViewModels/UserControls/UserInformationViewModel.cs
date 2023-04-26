using CompanyManagement.ViewModels.Base;
using CompanyManagement.Views.UserControls;
using System.Windows.Controls;
using CompanyManagement.ViewModels.Windows;
using CompanyManagement.Models;
using System.Windows.Input;
using System;

namespace CompanyManagement.ViewModels.UserControls
{
    public class UserInformationViewModel : BaseViewModel 
    {
        private CheckInOutUC WorkingView = new CheckInOutUC();
        private EmployeeInputUC MyInformationView = new EmployeeInputUC();

        private bool statusWorkingView=false;
        public bool StatusWorkingView { get => statusWorkingView; set { statusWorkingView = value; OnPropertyChanged(); } }

        private bool statusMyInformationView = false;
        public bool StatusMyInformationView { get => statusMyInformationView; set { statusMyInformationView = value; OnPropertyChanged(); } }

        private ContentControl currentChildView = new ContentControl();
        public ContentControl CurrentChildView { get => currentChildView; set { currentChildView = value; OnPropertyChanged(); } }

        public ICommand ShowWorkingView { get; set; }
        public ICommand ShowMyInformationView { get; set; }

        public UserInformationViewModel() 
        {
            ShowWorkingView = new RelayCommand<object>(ExcuteShowWorkingView);
            ShowMyInformationView = new RelayCommand<object>(ExcuteShowMyInformationView);
            ExcuteShowMyInformationView(null);
        }

        private void ExcuteShowMyInformationView(object obj)
        {
            ((EmployeeInputViewModel)MyInformationView.DataContext).EmployeeIns = CurrentUser.Ins.EmployeeIns;
            CurrentChildView = MyInformationView;
            StatusMyInformationView= true; 
        }

        private void ExcuteShowWorkingView(object obj)
        {
            CurrentChildView = WorkingView;
            StatusWorkingView= true;
        }
    }
}
