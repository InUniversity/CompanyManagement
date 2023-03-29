using CompanyManagement.Models;
using CompanyManagement.ViewModels.UserControls;
using System.Windows.Input;
using System.Windows;
using CompanyManagement.ViewModels.Base;

namespace CompanyManagement.ViewModels.Dialogs
{
    public class UpdateTimeKeepingViewModel: BaseViewModel
    {

        public ICommand UpdateTimeKeepingCommand { get; set; }

        public TimeKeepingViewModel ParentDataContext { get; set; }
        public TimeKeepingInputViewModel TimeKeepingDataContext { get; set; }

        public UpdateTimeKeepingViewModel()
        {
            SetCommands();
        }

        private void SetCommands()
        {
            TimeKeepingDataContext = new TimeKeepingInputViewModel();
            UpdateTimeKeepingCommand = new RelayCommand<Window>(UpdateCommand);
        }

        private void UpdateCommand(Window inputwindow)
        {
            TimeKeepingDataContext.TrimAllTexts();
            TimeKeeping timeKeeping = TimeKeepingDataContext.CreateTimeKeepingInstance(); 
            ParentDataContext.Update(timeKeeping);
            inputwindow.Close();
        }
    }
}
