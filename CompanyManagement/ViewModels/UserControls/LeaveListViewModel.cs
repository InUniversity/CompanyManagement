using CompanyManagement.Database;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;
using CompanyManagement.Services;
using CompanyManagement.Utilities;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.Views.Dialogs;
using CompanyManagement.Views.Dialogs.Interfaces;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using CompanyManagement.Strategies.UserControls.LeaveListView;

namespace CompanyManagement.ViewModels.UserControls
{

    public class LeaveListViewModel : BaseViewModel
    {
        private List<LeaveRequest> leaveRequestList;
        public List<LeaveRequest> LeaveRequestList 
        { get => leaveRequestList; set { leaveRequestList = value; OnPropertyChanged(); } }

        private List<LeaveRequest> unapprovedLeaveRequestList;
        public List<LeaveRequest> UnapprovedLeaveRequestList 
        { get => unapprovedLeaveRequestList; set { unapprovedLeaveRequestList = value; OnPropertyChanged(); } }

        private List<LeaveRequest> approvedLeaveRequestList;
        public List<LeaveRequest> ApprovedLeaveRequestList 
        { get => approvedLeaveRequestList; set { approvedLeaveRequestList = value; OnPropertyChanged(); } }

        private List<LeaveRequest> deniedLeaveRequestList;
        public List<LeaveRequest>  DeniedLeaveRequestList 
        { get => deniedLeaveRequestList; set { deniedLeaveRequestList = value; OnPropertyChanged(); } }
        
        private Visibility visibleLeaveRequestListExpander = Visibility.Collapsed;
        public Visibility VisibleLeaveRequestListExpander 
        { get => visibleUnapprovedLeaveListExpander; set { visibleUnapprovedLeaveListExpander = value; OnPropertyChanged(); } } 

        private Visibility visibleUnapprovedLeaveListExpander = Visibility.Collapsed;
        public Visibility VisibleUnapprovedLeaveListExpander 
        { get => visibleUnapprovedLeaveListExpander; set { visibleUnapprovedLeaveListExpander = value; OnPropertyChanged(); } }

        private Visibility visibleApprovedLeaveListExpander = Visibility.Collapsed;
        public Visibility VisibleApprovedLeaveListExpander 
        { get => visibleApprovedLeaveListExpander; set { visibleApprovedLeaveListExpander = value; OnPropertyChanged(); } }

        private Visibility visibleDeniedLeaveListExpander = Visibility.Collapsed;
        public Visibility VisibleDeniedLeaveListExpander 
        { get => visibleDeniedLeaveListExpander; set { visibleDeniedLeaveListExpander = value; OnPropertyChanged(); } }

        private DateTime timeCreateLeave = DateTime.Now;
        public DateTime TimeCreateLeave { get => timeCreateLeave; set { timeCreateLeave = value; OnPropertyChanged(); SearchDate(); } }

        public ICommand BackDateCommand { get; private set; }
        public ICommand NextDateCommand { get; private set; }
        public ICommand OpenAddLeaveDialogCommand { get; private set; }
        public ICommand DeleteLeaveCommand { get; private set; }
        public ICommand OpenUpdateLeaveDialogCommand { get; private set; }
        public ICommand ApproveLeaveCommand { get; private set; }
        
        private ILeaveListStrategy leaveListStrategy;
        public ILeaveListStrategy LeaveListStrategy 
        { 
            get => leaveListStrategy;
            set
            {
                if (leaveListStrategy == value) return;
                leaveListStrategy = value;
                leaveListStrategy.SetVisible(this);
            }
        }

        public INavigateAssignmentView ParentDataContext { get; set; }

        private LeaveRequestsDao leaveDao = new LeaveRequestsDao();
        private DepartmentsDao departmentDao = new DepartmentsDao();
        private EmployeesDao employeeDao = new EmployeesDao();

        private Employee currentEmployee = CurrentUser.Ins.EmployeeIns;

        public LeaveListViewModel(ILeaveListStrategy leaveListStrategy)
        {
            LeaveListStrategy = leaveListStrategy;
            SetCommands();
            SearchDate();
        }

        private void SetCommands()
        {
            BackDateCommand = new RelayCommand<object>(ExecuteBackTimeLeaveCreateDate);
            NextDateCommand = new RelayCommand<object>(ExecuteNextTimeLeaveCreateDate);
            OpenAddLeaveDialogCommand = new RelayCommand<object>(ExecuteAddCommand);
            DeleteLeaveCommand = new RelayCommand<string>(ExecuteDeleteCommand);
            OpenUpdateLeaveDialogCommand = new RelayCommand<LeaveRequest>(ExecuteUpdateCommand);
            ApproveLeaveCommand = new RelayCommand<LeaveRequest>(ExecuteApproveCommand);
        }

        private void ExecuteBackTimeLeaveCreateDate(object obj)
        {
            TimeCreateLeave = timeCreateLeave.AddDays(-1);
        }

        private void ExecuteNextTimeLeaveCreateDate(object obj)
        {
            TimeCreateLeave = timeCreateLeave.AddDays(1);
        }

        private void LoadLeaveRequestList()
        {
            LeaveRequestList = leaveDao.SearchByEmployeeID(currentEmployee.ID);
            
            var receivedLeaveRequests = leaveDao.SearchByDeptHeaderID(currentEmployee.ID);

            var unapprovedLeaveList = receivedLeaveRequests.Where(p => p.StatusID == BaseDao.APPROVAL).ToList();
            UnapprovedLeaveRequestList = unapprovedLeaveList;

            var listApprovedLeaves = receivedLeaveRequests.Where(p => p.StatusID == BaseDao.APPROVED).ToList();
            ApprovedLeaveRequestList = listApprovedLeaves;

            var listUnapprovedLeaves = receivedLeaveRequests.Where(p => p.StatusID == BaseDao.UNAPPROVED).ToList();
            DeniedLeaveRequestList = listUnapprovedLeaves;
        }

        private void SearchDate()
        {
            LoadLeaveRequestList();
            var allItem = LeaveRequestList;
            allItem = LeaveRequestList
                    .Where(item => item.CreatedDate.Date == TimeCreateLeave.Date)
                    .ToList();
            LeaveRequestList = new List<LeaveRequest>(allItem);
            
            Log.Instance.Information(nameof(LeaveListViewModel), "selected date = " + timeCreateLeave.ToShortDateString());
        }

        private LeaveRequest CreateLeave()
        {
            string approveBy = string.Equals(currentEmployee.ID, BaseDao.EMPLOYEE_ROLE_ID) 
                ? departmentDao.DepartmentByEmployeeDeptID(currentEmployee.DepartmentID).DepartmentHeadID
                : employeeDao.SearchByPositionID(BaseDao.MANAGER_ROLE_ID).ID;
            return new LeaveRequest(AutoGenerateID(), "", "", "LS2", currentEmployee.ID, approveBy);
        }

        private string AutoGenerateID()
        {
            string leaaveID;
            Random random = new Random();
            do
            {
                int number = random.Next(10000);
                leaaveID = $"LEA{number:0000}";
            } while (leaveDao.SearchByID(leaaveID) != null);
            return leaaveID;
        }

        private void ExecuteAddCommand(object p)
        {
            var leave = CreateLeave();
            var inputDialogService = new InputDialogService<LeaveRequest>(new AddLeaveDialog(), leave, Add);
            inputDialogService.Show();
            SearchDate();
        }

        private void Add(LeaveRequest leave)
        {
            leaveDao.Add(leave);
            LoadLeaveRequestList();
        }

        private void ExecuteDeleteCommand(string id)
        {
            var dialog = new AlertDialogService( 
                "Xóa xin phép nghỉ", 
                "Bạn chắc chắn muốn xóa xin phép nghỉ!",
                () =>
                {
                    leaveDao.Delete(id); 
                    LoadLeaveRequestList();
                }, null); 
            dialog.Show();
            LoadLeaveRequestList();
        }

        private void ExecuteUpdateCommand(LeaveRequest leave)
        {
            var inputDialogService = new InputDialogService<LeaveRequest>(new UpdateLeaveDialog(), leave, Update);
            inputDialogService.Show();
        }
        
        private void Update(LeaveRequest leave)
        {
            leaveDao.Update(leave);
            LoadLeaveRequestList();
        }

        private void ExecuteApproveCommand(LeaveRequest leave)
        {
            Update(leave);
        }

        private void ExecuteDenyCommand(LeaveRequest leave)
        {
            Update(leave);
        }
    }
}
