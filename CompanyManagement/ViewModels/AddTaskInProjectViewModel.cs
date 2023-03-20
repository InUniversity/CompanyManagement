using CompanyManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CompanyManagement.ViewModels
{
    public class AddTaskInProjectViewModel: BaseViewModel
    {

        public ICommand AddTaskInProjectCommand { get; set; }

        public TasksInProjectViewModel ParentDataContext { get; set; }

        public TaskInProjectInputViewModel TaskInProjectInputDataContext { get; set; }

        public AddTaskInProjectViewModel()
        {
            SetCommands();
        }

        public void SetCommands()
        { 
            TaskInProjectInputDataContext = new TaskInProjectInputViewModel();
            AddTaskInProjectCommand = new RelayCommand<Window>(AddCommand, p => TaskInProjectInputDataContext.CheckAllFields());
        }

        private void AddCommand(Window inputwindow)
        {
            TaskInProjectInputDataContext.TrimmAllTexts();
            TaskInProject task = TaskInProjectInputDataContext.CreateTaskInProjectInstance();
            ParentDataContext.Add(task);
            inputwindow.Close();
        }
    }
}
