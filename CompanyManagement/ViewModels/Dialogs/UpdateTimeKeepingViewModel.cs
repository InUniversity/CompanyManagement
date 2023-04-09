using CompanyManagement.Models;
using CompanyManagement.ViewModels.UserControls;
using System.Windows.Input;
using System.Windows;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.ViewModels.UserControls.Interfaces;
using CompanyManagement.Views.Dialogs;
using CompanyManagement.Services;

namespace CompanyManagement.ViewModels.Dialogs
{
    public class UpdateTimeKeepingViewModel : BaseViewModel, IDialogViewModel
    {

        public ICommand UpdateTimeKeepingCommand { get; set; }

        public IEditDBViewModel ParentDataContext { get; set; }
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
              "Bạn chắc chắn muốn cập nhật \n bảng chấm công !",
              () =>
              {
                  TimeKeeping timeKeeping = TimeKeepingInputDataContext.CreateTimeKeepingInstance();
                  ParentDataContext.UpdateToDB(timeKeeping);
              }, () => { });
            dialog.Show();
            inputWindow.Close();
        }

        public void Retrieve(object timeKeeping)
        {
            TimeKeepingInputDataContext.Retrieve(timeKeeping as TimeKeeping);
        }
    }
}
