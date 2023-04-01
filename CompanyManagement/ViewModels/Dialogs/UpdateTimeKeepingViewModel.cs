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

        public ITimeKeeping ParentDataContext { get; set; }

        public ITimeKeepingInput TimeKeepingInputDataContext { get; set; }

        public UpdateTimeKeepingViewModel(ITimeKeepingInput timeKeepingInputDataContext)
        {
            TimeKeepingInputDataContext = timeKeepingInputDataContext;
            UpdateTimeKeepingCommand = new RelayCommand<Window>(UpdateCommand);
        }

        private void UpdateCommand(Window inputwindow)
        {
            TimeKeepingInputDataContext.TrimAllTexts();
            TimeKeeping timeKeeping = TimeKeepingInputDataContext.CreateTimeKeepingInstance(); 
            ParentDataContext.Update(timeKeeping);
            inputwindow.Close();
        }
    }
}
