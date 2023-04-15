using CompanyManagement.Models;
using CompanyManagement.ViewModels.UserControls;
using System.Windows.Input;
using System.Windows;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.Services;

namespace CompanyManagement.ViewModels.Dialogs
{
    public class UpdateTimeKeepingViewModel : BaseViewModel
    {
        public ICommand UpdateTimeKeepingCommand { get; set; }

        public ITimeKeepingInput TimeKeepingInputDataContext { get; set; }

        public UpdateTimeKeepingViewModel()
        {
            TimeKeepingInputDataContext = new TimeKeepingInputViewModel();
            UpdateTimeKeepingCommand = new RelayCommand<Window>(UpdateCommand);
        }

        private void UpdateCommand(Window inputWindow)
        {
            TimeKeepingInputDataContext.TrimAllTexts();
            AlertDialogService dialog = new AlertDialogService(
              "Cập nhật bảng chấm công",
              "Bạn chắc chắn muốn cập nhật bảng chấm công !",
              () =>
              {
                  TimeKeeping timeKeeping = TimeKeepingInputDataContext.CreateTimeKeepingInstance();
                  // ParentDataContext.UpdateToDB(timeKeeping); 
                  inputWindow.Close();
              }, () => { });
            dialog.Show();
        }

        public void Retrieve(object timeKeeping)
        {
            TimeKeepingInputDataContext.Receive(timeKeeping as TimeKeeping);
        }
    }
}
