using CompanyManagement.Models;
using CompanyManagement.ViewModels.UserControls;
using System.Windows.Input;
using System.Windows;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.ViewModels.UserControls.Interfaces;

namespace CompanyManagement.ViewModels.Dialogs
{
    public class UpdateTimeKeepingViewModel : BaseViewModel, IDialogViewModel
    {

        public ICommand UpdateTimeKeepingCommand { get; set; }

        public IEditDBViewModel ParentDataContext { get; set; }
        public ITimeKeepingInput TimeKeepingInputDataContext { get; set; }

        public UpdateTimeKeepingViewModel()
        {
            TimeKeepingInputDataContext = new TimeKeepingInputViewModel() { InformationLockStatus=false};
            UpdateTimeKeepingCommand = new RelayCommand<Window>(UpdateCommand);
        }

        private void UpdateCommand(Window inputWindow)
        {
            TimeKeepingInputDataContext.TrimAllTexts();
            TimeKeeping timeKeeping = TimeKeepingInputDataContext.CreateTimeKeepingInstance(); 
            ParentDataContext.UpdateToDB(timeKeeping);
            inputWindow.Close();
        }

        public void Retrieve(object timeKeeping)
        {
            TimeKeepingInputDataContext.Retrieve(timeKeeping as TimeKeeping);
        }
    }
}
