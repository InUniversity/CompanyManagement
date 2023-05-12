using CompanyManagement.Models;
using System;
using System.Collections.Generic;
using CompanyManagement.Database;
using CompanyManagement.ViewModels.Base;
using System.Windows.Input;
using System.Linq;
using System.Windows.Controls;
using CompanyManagement.Database.Base;
using CompanyManagement.Enums;
using CompanyManagement.Utilities;

namespace CompanyManagement.ViewModels.UserControls
{
    public interface ITaskInput
    {
        public TaskInProject TaskInProjectIns { get; set; }
        bool CheckAllFields();
        void TrimAllTexts();
    }

    public class TaskInputViewModel : BaseViewModel, ITaskInput
    {
        private TaskInProject task = new TaskInProject();
        public TaskInProject TaskInProjectIns 
        { 
            get => task;
            set
            {
                task = value;
                LoadEmployeeCanAssign();
                SearchByName();
            } 
        }

        public string ID { get => task.ID; set { task.ID = value; OnPropertyChanged(); } }
        public string Title { get => task.Title; set { task.Title = value; OnPropertyChanged(); } }
        public string Explanation { get => task.Explanation; set { task.Explanation = value; OnPropertyChanged(); } }
        public DateTime StartDate { get => task.Start; set { task.Start = value; OnPropertyChanged(); } }
        public DateTime Deadline { get => task.Deadline; set { task.Deadline = value; OnPropertyChanged(); } }

        public string Progress
        {
            get => task.Progress;
            set
            {
                task.Progress = value;
                UpdateStatus();
                OnPropertyChanged();
            }
        }
        
        public Employee Owner
        {
            get => task.Owner;
            set
            {
                task.Owner = value;
                task.OwnerID = task.Owner.ID;
                OnPropertyChanged();
            }
        }

        public Employee AssignedEmployee
        {
            get => task.AssignedEmployee;
            set
            {
                task.AssignedEmployee = value;
                task.EmployeeID = task.AssignedEmployee.ID;
                OnPropertyChanged();
            }
        }
        
        public string ProjectID { get => task.ProjectID; set { task.ProjectID = value; OnPropertyChanged(); } }
        
        public ETaskStatus Status 
        { 
            get => task.Status;
            set
            {
                task.Status = value;
                UpdateProgress();
                OnPropertyChanged();
            } 
        }

        private string errorMessage = "";
        public string ErrorMessage { get => errorMessage; set { errorMessage = value; OnPropertyChanged(); } }

        private List<ETaskStatus> taskStatuses;
        public List<ETaskStatus> TaskStatuses { get => taskStatuses; set { taskStatuses = value; OnPropertyChanged(); } }
        
        private List<Employee> employees;

        private List<Employee> searchedEmployeesCanAssign;
        public List<Employee> SearchedEmployeesCanAssign 
        { get => searchedEmployeesCanAssign; set { searchedEmployeesCanAssign = value; OnPropertyChanged(); } }

        private string textToSearch = "";
        public string TextToSearch { get => textToSearch; set { textToSearch = value; OnPropertyChanged(); SearchByName(); } }

        public ICommand GetSelectedEmployeeCommand { get; private set; }

        private ProjectAssignmentsDao assignmentsDao = new ProjectAssignmentsDao();
        private RolesDao roleDao = new RolesDao();
        
        public TaskInputViewModel()
        {
            SetCommands();
            SetAllComboBox();
        }

        private void LoadEmployeeCanAssign()
        {
            employees = assignmentsDao.GetEmployeesInProject(task.ProjectID);
            foreach (var employee in employees)
            {
                employee.EmplRole = roleDao.SearchByID(employee.RoleID);
            }
        }

        private void SetCommands()
        {
            GetSelectedEmployeeCommand = new RelayCommand<ListView>(ExecuteGetSelectedEmployeeCommand);       
        }

        private void ExecuteGetSelectedEmployeeCommand(ListView listView)
        {
            if (listView.SelectedItem == null) return;
            AssignedEmployee = listView.SelectedItem as Employee;
        }

        private void SetAllComboBox()
        {
            TaskStatuses = Enum.GetValues(typeof(ETaskStatus)).Cast<ETaskStatus>().ToList();
        }

        public bool CheckAllFields()
        {
            ErrorMessage = "";
            if (string.IsNullOrWhiteSpace(Title))
            {
                ErrorMessage = Utils.invalidEmptyMess;
                return false;
            }
            if (Deadline < StartDate)
            {
                ErrorMessage = Utils.invalidTimeline;
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

        private void UpdateProgress()
        {
            string result = GetProgressByStatus(Status, Progress);
            if (Progress == result) return;
            Progress = result;
        }
        
        private string GetProgressByStatus(ETaskStatus status, string progress)
        {
            return status switch
            {
                ETaskStatus.Completed => BaseDao.completed,
                ETaskStatus.Cancelled => "0",
                _ => progress
            };
        }
        
        private void UpdateStatus()
        {
            ETaskStatus result = GetStatusByProgress(Progress, Status);
            if (Status == result) return;
            Status = result;
        }

        private ETaskStatus GetStatusByProgress(string progress, ETaskStatus status)
        {
            return progress switch
            {
                BaseDao.completed => ETaskStatus.Completed,
                _ => status
            };
        }
    }
}
