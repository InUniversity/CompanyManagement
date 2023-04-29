using CompanyManagement.Models;
using System;
using System.Collections.Generic;
using CompanyManagement.Database;
using CompanyManagement.ViewModels.Base;
using System.Windows.Input;
using System.Linq;
using System.Windows.Controls;

namespace CompanyManagement.ViewModels.UserControls
{
    public interface ITaskInput
    {
        bool CheckAllFields();
        void TrimAllTexts();
        public TaskInProject TaskInProjectIns { get;set; }
    }

    public class TaskInputViewModel : BaseViewModel, ITaskInput
    {
        private TaskInProject task;
        public TaskInProject TaskInProjectIns 
        { 
            get => task;
            set
            {
                task = value; 
                Employees = assignmentsDao.GetEmployeesInProject(task.ProjectID); 
                SearchByName();
            } 
        }

        public string ID { get => task.ID; set { task.ID = value; OnPropertyChanged(); } }
        public string Title { get => task.Title; set { task.Title = value; OnPropertyChanged(); } }
        public string Explanation { get => task.Explanation; set { task.Explanation = value; OnPropertyChanged(); } }
        public DateTime StartDate { get => task.StartDate; set { task.StartDate = value; OnPropertyChanged(); } }
        public DateTime Deadline { get => task.Deadline; set { task.Deadline = value; OnPropertyChanged(); } }
        public string Progress { get => task.Progress; set { task.Progress = value; OnPropertyChanged(); } }
        public string EmployeeID { get => task.EmployeeID; set { task.EmployeeID = value; OnPropertyChanged(); } }
        public string ProjectID { get => task.ProjectID; set { task.ProjectID = value; OnPropertyChanged(); } }
        public string StatusID { get => task.StatusID; set { task.StatusID = value; OnPropertyChanged(); } }

        private string errorMessage = "";
        public string ErrorMessage { get => errorMessage; set { errorMessage = value; OnPropertyChanged(); } }

        private List<Employee> employees;
        public List<Employee> Employees { get => employees; set { employees = value; OnPropertyChanged(); } }

        private List<TaskStatus> taskStatuses;
        public List<TaskStatus> TaskStatuses { get => taskStatuses; set { taskStatuses = value; OnPropertyChanged(); } }

        private List<Employee> searchedEmployeesCanAssign;
        public List<Employee> SearchedEmployeesCanAssign { get => searchedEmployeesCanAssign; set { searchedEmployeesCanAssign = value; OnPropertyChanged(); } }

        private string textToSearch = "";
        public string TextToSearch { get => textToSearch; set { textToSearch = value; OnPropertyChanged(); SearchByName(); } }

        public ICommand GetSelectedEmployeeCommand { get; private set; }

        private TaskStatusesDao taskStatusesDao = new TaskStatusesDao();
        private ProjectAssignmentsDao assignmentsDao = new ProjectAssignmentsDao();

        public TaskInputViewModel()
        {
            SetCommands();
            SetAllComboBox();
        }

        private void SetCommands()
        {
            GetSelectedEmployeeCommand = new RelayCommand<ListView>(ExecuteGetSelectedEmployeeCommand);       
        }

        private void ExecuteGetSelectedEmployeeCommand(ListView listView)
        {
            if (listView.SelectedItem == null) return;
            var selectedItem = listView.SelectedItem as Employee;
            EmployeeID = selectedItem.ID;
        }

        private void SetAllComboBox()
        {
            TaskStatuses = taskStatusesDao.GetAll();
        }

        public bool CheckAllFields()
        {
            ErrorMessage = "";
            if (string.IsNullOrWhiteSpace(Title) || string.IsNullOrWhiteSpace(EmployeeID))
            {
                ErrorMessage = "Các thông tin không được để trống!!!";
                return false;
            }
            if (Deadline < StartDate)
            {
                ErrorMessage = "Thời gian kết thúc không hợp lệ!!!";
                return false;
            }
            return true;
        }
        
        private void SearchByName()
        {
            var searchedItems = employees;
            if (!string.IsNullOrEmpty(textToSearch))
            {
                searchedItems = employees
                    .Where(item => item.Name.ToLower().Contains(textToSearch.ToLower()))
                    .ToList();
            }       
            SearchedEmployeesCanAssign = new List<Employee>(searchedItems);
        }

        public void TrimAllTexts()
        {
            Title = Title.Trim();
            Explanation = Explanation.Trim();
        }
    }
}
