using CompanyManagement.Database;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CompanyManagement.ViewModels.UserControls
{
    public class LeaveInputViewModel : BaseViewModel
    {
        private LeaveRequest leaveRequest;
        public LeaveRequest LeaveRequestIns { get => leaveRequest; set => leaveRequest = value; }

        public string ID { get => leaveRequest.ID; set { leaveRequest.ID = value; OnPropertyChanged(); } } 
        public string Reason { get => leaveRequest.Reason; set { leaveRequest.Reason = value; OnPropertyChanged(); } }
        public string Notes { get => leaveRequest.Notes; set { leaveRequest.Notes = value; OnPropertyChanged(); } }
        public DateTime CreatedDate { get => leaveRequest.CreatedDate; set { leaveRequest.CreatedDate = value; OnPropertyChanged(); } }
        public DateTime StartDate { get => leaveRequest.StartDate; set { leaveRequest.StartDate = value; OnPropertyChanged(); } }
        public DateTime EndDate { get => leaveRequest.EndDate; set { leaveRequest.EndDate = value; OnPropertyChanged(); } }
        public string StatusID { get => leaveRequest.StatusID; set { leaveRequest.StatusID = value; OnPropertyChanged(); } }
        public string EmployeeID { get => leaveRequest.EmployeeID; set { leaveRequest.EmployeeID = value; OnPropertyChanged(); } }
        public string ApproverID { get => leaveRequest.ApproverID; set { leaveRequest.ApproverID = value; OnPropertyChanged(); } }
        
        private string errorMessage = "";
        public string ErrorMessage { get => errorMessage; set { errorMessage = value; OnPropertyChanged(); } }

        private string textToSearch = "";
        public string TextToSearch { get => textToSearch; set { textToSearch = value; OnPropertyChanged(); SearchByName(); } }

        private string roleName = "";
        public string RoleName { get => roleName; set { roleName = value; OnPropertyChanged(); } }

        private bool isReadOnly = false;
        public bool IsReadOnly { get => isReadOnly; set { isReadOnly = value; OnPropertyChanged(); } }

        private List<Employee> searchedApprovers;
        public List<Employee> SearchedApprovers { get => searchedApprovers; set { searchedApprovers = value; OnPropertyChanged(); } }

        private List<Employee> approvers;
        public List<Employee> Approvers { get => approvers; set { approvers = value; OnPropertyChanged(); } }

        public List<LeaveStatus> LeaveStatuses { get; set;}

        public ICommand AddApproverCommand { get; private set; }
        public ICommand GetSelectedApproverCommand { get; private set; }

        private LeaveStatusesDao leaveStatusesDao = new LeaveStatusesDao();
        private EmployeesDao employeesDao = new EmployeesDao();
        private DepartmentsDao departmentsDao = new DepartmentsDao();
        private RolesDao rolesDao = new RolesDao();

        private Employee currentEmployee = CurrentUser.Ins.EmployeeIns;

        public LeaveInputViewModel()
        {
            SetCommands();
            SetAllComboBox();
            LoadApprovers();
        }

        private void SetCommands()
        {
            GetSelectedApproverCommand = new RelayCommand<ListView>(ExecuteGetSelectedApproverCommand);
        }

        private void ExecuteGetSelectedApproverCommand(ListView listView)
        {
            if (listView.SelectedItem == null) return;
            var selectedItem = listView.SelectedItem as Employee;
            ApproverID = selectedItem.ID;
            RoleName = rolesDao.SearchByID(selectedItem.RoleID).Title;
        }

        private void SetAllComboBox()
        {
            LeaveStatuses = leaveStatusesDao.GetAll();
        }

        private void LoadApprovers()
        {
            approvers = employeesDao.GetManagers();
            string headerDeptID = departmentsDao.DepartmentByEmployeeDeptID(CurrentUser.Ins.EmployeeIns.DepartmentID)?.DepartmentHeadID ?? "";
            Employee headerApprover = employeesDao.SearchByID(headerDeptID);
            SearchedApprovers = string.Equals(currentEmployee.RoleID,BaseDao.DEPARTMENT_HEAD_ROLE_ID)
                ? Approvers
                : Approvers.Concat(new List<Employee>() { headerApprover }).ToList();
        }

        private void SearchByName()
        {
            var searchedItems = approvers;
            if (!string.IsNullOrEmpty(textToSearch))
            {
                searchedItems = approvers
                    .Where(item => item.Name.ToLower().Contains(textToSearch.ToLower()))
                    .ToList();
            }
            SearchedApprovers = new List<Employee>(searchedItems);
        }

        public void TrimAllTexts()
        {
            ID = ID.Trim();
            Reason = Reason.Trim();
            Notes = Notes.Trim();
            EmployeeID = EmployeeID.Trim();
            ApproverID = ApproverID.Trim();
        }
    }
}
