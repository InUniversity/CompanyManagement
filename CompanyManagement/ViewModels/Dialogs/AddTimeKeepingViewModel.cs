using CompanyManagement.Models;
using CompanyManagement.ViewModels.UserControls;
using System.Windows.Input;
using System.Windows;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.Database.Interfaces;

namespace CompanyManagement.ViewModels.Dialogs
{
    public class AddTimeKeepingViewModel
    {
        public ICommand AddTimeKeepingCommand { get; set; }

        public ITimeKeeping ParentDataContext { get; set; }

        public ITimeKeepingInput TimeKeepingInputDataContext { get; set; }

        public AddTimeKeepingViewModel(ITimeKeepingInput timeKeepingInputDataContext)
        {
            TimeKeepingInputDataContext = timeKeepingInputDataContext;
            AddTimeKeepingCommand = new RelayCommand<Window>(AddCommand);
        }

        private void AddCommand(Window inputwindow)
        {
            TimeKeepingInputDataContext.TrimAllTexts();
            TimeKeeping timeKeeping = TimeKeepingInputDataContext.CreateTimeKeepingInstance();
            ParentDataContext.Add(timeKeeping);
            inputwindow.Close();
        }
    }
}
