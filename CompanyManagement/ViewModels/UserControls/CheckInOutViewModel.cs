using System.Collections.ObjectModel;
using System.Windows.Input;
using CompanyManagement.Database;
using CompanyManagement.Models;
using CompanyManagement.Services;
using CompanyManagement.ViewModels.Base;

namespace CompanyManagement.ViewModels.UserControls
{
    public class CheckInOutViewModel : BaseViewModel
    {
        private bool isToggled;
        public bool IsToggled { get => isToggled; set { isToggled = value; OnPropertyChanged(); } }
        

        private ObservableCollection<CheckInOut> checkInOutList;
        public ObservableCollection<CheckInOut> CheckInOutList { get => checkInOutList; set { checkInOutList = value; OnPropertyChanged(); } }

        private CheckInOutDao checkInOutDao = new CheckInOutDao();
        
        public ICommand ToggledCommand { get; }

        public CheckInOutViewModel()
        {
            CheckInOutList = new ObservableCollection<CheckInOut>(checkInOutDao.GetAll());
            ToggledCommand = new RelayCommand<object>(Toggled);
        }

        private void Toggled(object obj)
        {
            AlertDialogService dialog = new AlertDialogService(
                "Is toggled",
                "IsToggled: " + isToggled,
                () => 
                {

                },
                () => 
                {
                    IsToggled = !IsToggled;
                });
            dialog.Show();
        }
    }
}