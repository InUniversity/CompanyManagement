using CompanyManagement.Database;
using CompanyManagement.Models;
using CompanyManagement.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using CompanyManagement.Utilities;

namespace CompanyManagement.ViewModels.UserControls
{
    public class LeaveInputViewModel : BaseViewModel
    {
        private LeaveRequest leaveRequest;
        public LeaveRequest LeaveRequestIns { get => leaveRequest; set => leaveRequest = value; }

        public string ID { get => leaveRequest.ID; set { leaveRequest.ID = value; OnPropertyChanged(); } } 
        public string Reason { get => leaveRequest.Reason; set { leaveRequest.Reason = value; OnPropertyChanged(); } }
        public string Notes { get => leaveRequest.Notes; set { leaveRequest.Notes = value; OnPropertyChanged(); } }
        public DateTime StartDate { get => leaveRequest.Start; set { leaveRequest.Start = value; OnPropertyChanged(); } }
        public DateTime EndDate { get => leaveRequest.End; set { leaveRequest.End = value; OnPropertyChanged(); } }
        public string EmployeeID { get => leaveRequest.RequesterID; set { leaveRequest.RequesterID = value; OnPropertyChanged(); } }
        public string ApproverID { get => leaveRequest.ApproverID; set { leaveRequest.ApproverID = value; OnPropertyChanged(); } }
        public string Response { get => leaveRequest.Response; set { leaveRequest.Response = value; OnPropertyChanged(); } }

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

        public ICommand AddApproverCommand { get; private set; }
        public ICommand GetSelectedApproverCommand { get; private set; }

        private EmployeesDao employeesDao = new EmployeesDao();
        private RolesDao rolesDao = new RolesDao();
        private CheckFormat checker = new CheckFormat();

        private Employee currentEmployee = CurrentUser.Ins.Empl;

        public LeaveInputViewModel()
        {
            SetCommands();
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
            RoleName = selectedItem.EmplRole.Title;
        }

        private void LoadApprovers()
        {
            approvers = employeesDao.GetRequestApprovers();
            foreach(Employee header in approvers)
            {
                header.EmplRole = rolesDao.SearchByID(header.RoleID);
            }
            SearchedApprovers = new List<Employee>(approvers);
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

        public bool CheckAllFields()
        {
            ErrorMessage = "";
            if (string.IsNullOrWhiteSpace(Reason) || string.IsNullOrWhiteSpace(ApproverID))
            {
                ErrorMessage = Utils.invalidEmptyMess;
                return false;
            }
            if (!checker.ValidateTimeline(StartDate, EndDate))
            {
                ErrorMessage = Utils.invalidTimeline;
                return false;
            }
            return true;
        }

        public void TrimAllTexts()
        {
            ID = ID.Trim();
            Reason = Reason.Trim();
            Notes = Notes.Trim();
        }
    }
}
