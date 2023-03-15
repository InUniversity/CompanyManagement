using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

namespace CompanyManagement.ViewModels
{
    public class ProjectDetailsDialogViewModel:BaseViewModel
    {
        public ICommand CloseWindowCommand { get; set; }
        public ProjectDetailsDialogViewModel()
        {
            SetCommands();
        }
        private void SetCommands()
        {
            CloseWindowCommand = new RelayCommand<Window>(ExecuteCloseWindow);           
        }

        private void ExecuteCloseWindow(Window p)
        {
            p.Close();
        }

        
    }
}
