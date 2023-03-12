using CompanyManagement.Database;
using CompanyManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CompanyManagement.ViewModels
{
    public class TasksInProjectViewModel: BaseViewModel
    {
        private string id;
        public string ID { get => id; set { id = value; OnPropertyChanged(); } }

        private string name;
        public string Name { get => name; set { name = value; OnPropertyChanged(); } }

        private string start;
        public string Start { get => start; set { start = value; OnPropertyChanged(); } }

        private string end;
        public string End { get => end; set { end = value; OnPropertyChanged(); } }

        private string employeeID;
        public string EmployeeID { get => employeeID; set { employeeID = value; OnPropertyChanged(); } }

        private TaskInProjectDao taskinprojectdao = new TaskInProjectDao();

        ICommand AddTaskInProjectCommand { get; set; }
        ICommand DeleteTaskInProjectCommand { get; set; }
        ICommand SaveTaskInProjectCommand { get; set; }
        
        public TasksInProjectViewModel()
        {
            SetCommand();
        }
        private void SetCommand()
        {
            AddTaskInProjectCommand = new ReplayCommand<Window>(p => OnClickAdd(p)) ;
            DeleteTaskInProjectCommand = new ReplayCommand<Window>(p => OnClickDelete(p));
            SaveTaskInProjectCommand = new ReplayCommand<Window>(p => OnClickSave(p));
        }

        private void OnClickAdd(Window p)
        {
            TaskInProject taskinproject = new TaskInProject(/*Thêm id, name, start, end, empID*/);
            taskinprojectdao.Add(taskinproject);
        }
        
        private void OnClickDelete(Window p)
        {
            TaskInProject taskinproject = new TaskInProject(/*Thêm id, name, start, end, empID*/);
            taskinprojectdao.Delete(taskinproject);
        }

        private void OnClickSave(Window p)
        {
            TaskInProject taskinproject = new TaskInProject(/*Thêm id, name, start, end, empID*/);
            taskinprojectdao.Save(taskinproject);
        }
    }
}
