using CompanyManagement.Database.Implementations;
using CompanyManagement.Models;
using CompanyManagement.ViewModels.UserControls;
using System;
using System.Windows.Input;
using System.Windows;

namespace CompanyManagement.ViewModels.Dialogs
{
    public class AddTimeKeepingViewModel
    {
        public ICommand AddTimeKeepingCommand { get; set; }

        public TimeKeepingViewModel ParentDataContext { get; set; }

        public TimeKeepingInputViewModel TimeKeepingInputDataContext { get; set; }

        public AddTimeKeepingViewModel()
        {
            SetCommands();
        }

        public void SetCommands()
        {
            TimeKeepingInputDataContext = new TimeKeepingInputViewModel();
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
