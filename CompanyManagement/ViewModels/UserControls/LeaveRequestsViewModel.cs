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

    public class LeaveRequestsViewModel : BaseViewModel
    {
        private List<LeaveRequest> leaveRequests;
        public List<LeaveRequest> LeaveRequests 
        { get => leaveRequests; set { leaveRequests = value; OnPropertyChanged(); } }

        private List<LeaveRequest> unapprovedLeaveRequests;
        public List<LeaveRequest> UnapprovedLeaveRequests 
        { get => unapprovedLeaveRequests; set { unapprovedLeaveRequests = value; OnPropertyChanged(); } }

        private List<LeaveRequest> approvedLeaveRequests;
        public List<LeaveRequest> ApprovedLeaveRequests 
        { get => approvedLeaveRequests; set { approvedLeaveRequests = value; OnPropertyChanged(); } }

        private List<LeaveRequest> deniedLeaveRequests;
        public List<LeaveRequest>  DeniedLeaveRequests 
        { get => deniedLeaveRequests; set { deniedLeaveRequests = value; OnPropertyChanged(); } }
        
        private Visibility visibleLeaveRequestsExpander = Visibility.Collapsed;
        public Visibility VisibleLeaveRequestsExpander 
        { get => visibleLeaveRequestsExpander; set { visibleLeaveRequestsExpander = value; OnPropertyChanged(); } } 

        private Visibility visibleUnapprovedLeaveRequestsExpander = Visibility.Collapsed;
        public Visibility VisibleUnapprovedLeaveListExpander 
        { get => visibleUnapprovedLeaveRequestsExpander; set { visibleUnapprovedLeaveRequestsExpander = value; OnPropertyChanged(); } }

        private Visibility visibleApprovedLeaveRequesrsExpander = Visibility.Collapsed;
        public Visibility VisibleApprovedLeaveRequesrsExpander 
        { get => visibleApprovedLeaveRequesrsExpander; set { visibleApprovedLeaveRequesrsExpander = value; OnPropertyChanged(); } }

        private Visibility visibleDeniedLeaveRequestsExpander = Visibility.Collapsed;
        public Visibility VisibleDeniedLeaveRequestsExpander 
        { get => visibleDeniedLeaveRequestsExpander; set { visibleDeniedLeaveRequestsExpander = value; OnPropertyChanged(); } }

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

        public LeaveRequestsViewModel(ILeaveListStrategy leaveListStrategy)
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
            LeaveRequests = leaveDao.SearchByEmployeeID(currentEmployee.ID);

            var receivedLeaveRequests = (currentEmployee.RoleID == BaseDao.EMPLOYEE_ROLE_ID)
                ? leaveDao.SearchByEmployeeID(currentEmployee.ID)
                : leaveDao.SearchByApproverID(currentEmployee.ID);

            var unapprovedLeaveList = receivedLeaveRequests.Where(p => p.StatusID == BaseDao.UNAPPROVED
                                        && p.StartDate > DateTime.Now).OrderByDescending(p => p.CreatedDate).ToList();
            UnapprovedLeaveRequests = unapprovedLeaveList;

            var listApprovedLeaves = receivedLeaveRequests.Where(p => p.StatusID == BaseDao.APPROVED)
                                        .OrderByDescending(p => p.CreatedDate).ToList();
            ApprovedLeaveRequests = listApprovedLeaves;

            var listUnapprovedLeaves = receivedLeaveRequests.Where(p => p.StatusID == BaseDao.UNAPPROVED 
                                        || (p.StartDate <= DateTime.Now && p.StatusID == BaseDao.DENIED))
                                        .OrderByDescending(p => p.CreatedDate).ToList();
            DeniedLeaveRequests = listUnapprovedLeaves;
        }

        private void SearchDate()
        {
            LoadLeaveRequestList();
            var allItem = LeaveRequests;
            allItem = LeaveRequests
                    .Where(item => item.CreatedDate.Date == TimeCreateLeave.Date)
                    .ToList();
            LeaveRequests = new List<LeaveRequest>(allItem);
            
            Log.Instance.Information(nameof(LeaveRequestsViewModel), "selected date = " + timeCreateLeave.ToShortDateString());
        }

        private LeaveRequest CreateLeave()
        {
            return new LeaveRequest(AutoGenerateID(), "", "", "LS2", currentEmployee.ID, "");
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
