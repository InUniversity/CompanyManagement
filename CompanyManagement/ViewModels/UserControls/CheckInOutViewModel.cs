using System;
using System.Collections.ObjectModel;
using CompanyManagement.Database;
using CompanyManagement.Models;
using CompanyManagement.ViewModels.Base;

namespace CompanyManagement.ViewModels.UserControls
{
    public class CheckInOutViewModel : BaseViewModel
    {
        private ObservableCollection<CheckInOut> checkInOutList;
        public ObservableCollection<CheckInOut> CheckInOutList { get => checkInOutList; set { checkInOutList = value; OnPropertyChanged(); } }

        private CheckInOutDao checkInOutDao = new CheckInOutDao();

        public CheckInOutViewModel()
        {
            CheckInOutList = new ObservableCollection<CheckInOut>(checkInOutDao.GetAll());
        }
    }
}