using CompanyManagement.Models;
using CompanyManagement.ViewModels.UserControls;
using System.Windows.Input;
using System.Windows;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.Services;

namespace CompanyManagement.ViewModels.Dialogs
{
    public class AddTimeKeepingViewModel : BaseViewModel
    {
        public ICommand AddTimeKeepingCommand { get; set; }

        public ITimeKeepingInput TimeKeepingInputDataContext { get; set; }

        public AddTimeKeepingViewModel()
        {
            TimeKeepingInputDataContext = new TimeKeepingInputViewModel();
            AddTimeKeepingCommand = new RelayCommand<Window>(AddCommand);
        }

        private void AddCommand(Window inputWindow)
        {
            TimeKeepingInputDataContext.TrimAllTexts();
            AlertDialogService dialog = new AlertDialogService(
               "Thêm bảng chấm công",
               "Bạn chắc chắn muốn thêm bảng chấm công !",
               () =>
               {
                   TimeKeeping timeKeeping = TimeKeepingInputDataContext.CreateTimeKeepingInstance();
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
