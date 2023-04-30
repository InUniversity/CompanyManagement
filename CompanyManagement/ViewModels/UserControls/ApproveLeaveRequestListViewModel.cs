using CompanyManagement.Database.Base;
using CompanyManagement.Database;
using CompanyManagement.Models;
using CompanyManagement.Services;
using CompanyManagement.Strategies.UserControls.LeaveListView;
using CompanyManagement.Utilities;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using CompanyManagement.Views.UserControls;

namespace CompanyManagement.ViewModels.UserControls
{
    public class ApproveLeaveRequestListViewModel : BaseViewModel
    {
        private List<LeaveRequest> ReceivedLeaveRequests;

        private List<LeaveRequest> unapprovedLeaveRequests;
        public List<LeaveRequest> UnapprovedLeaveRequests
        { get => unapprovedLeaveRequests; set { unapprovedLeaveRequests = value; OnPropertyChanged(); } }

        private List<LeaveRequest> approvedLeaveRequests;
        public List<LeaveRequest> ApprovedLeaveRequests
        { get => approvedLeaveRequests; set { approvedLeaveRequests = value; OnPropertyChanged(); } }

        private List<LeaveRequest> deniedLeaveRequests;
        public List<LeaveRequest> DeniedLeaveRequests
        { get => deniedLeaveRequests; set { deniedLeaveRequests = value; OnPropertyChanged(); } }

        private DateTime timeCreateLeave = DateTime.Now;
        public DateTime TimeCreateLeave { get => timeCreateLeave; set { timeCreateLeave = value; OnPropertyChanged(); SearchDate(); } }

        public ICommand BackDateCommand { get; private set; }
        public ICommand NextDateCommand { get; private set; }
        public ICommand ApproveLeaveCommand { get; private set; }
        public ICommand DenyLeaveCommand { get; private set; }
        public ICommand RestoreLeaveCommand { get; private set; }

        private LeaveRequestsDao leaveDao = new LeaveRequestsDao();
        private DepartmentsDao departmentDao = new DepartmentsDao();
        private EmployeesDao employeeDao = new EmployeesDao();

        private Employee currentEmployee = CurrentUser.Ins.EmployeeIns;

        public ApproveLeaveRequestListViewModel()
        {
            SetCommands();
            LoadLeaveRequestList();
        }

        private void SetCommands()
        {
            BackDateCommand = new RelayCommand<object>(ExecuteBackTimeLeaveCreateDate);
            NextDateCommand = new RelayCommand<object>(ExecuteNextTimeLeaveCreateDate);
            ApproveLeaveCommand = new RelayCommand<LeaveRequest>(ExecuteApproveCommand);
            DenyLeaveCommand = new RelayCommand<LeaveRequest>(ExecuteDenyLeaveCommand);
            RestoreLeaveCommand = new RelayCommand<LeaveRequest>(ExecuteRestoreLeaveCommand);
        }

        private void ExecuteRestoreLeaveCommand(LeaveRequest obj)
        {
            throw new NotImplementedException();
        }

        private void ExecuteDenyLeaveCommand(LeaveRequest obj)
        {
            throw new NotImplementedException();
        }
        private void ExecuteApproveCommand(LeaveRequest leave)
        {
            throw new NotImplementedException();
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

            ReceivedLeaveRequests = leaveDao.SearchByApproverID(currentEmployee.ID);
    
            var listUnapprovedLeaves = ReceivedLeaveRequests.Where(p => p.StatusID == BaseDao.UNAPPROVED).ToList();
            UnapprovedLeaveRequests = listUnapprovedLeaves;

            var listApprovedLeaves = ReceivedLeaveRequests.Where(p => p.StatusID == BaseDao.APPROVED).ToList();
            ApprovedLeaveRequests = listApprovedLeaves;

            var listDeniedLeaveRequests = ReceivedLeaveRequests.Where(p => p.StatusID == BaseDao.DENIED).ToList();
            DeniedLeaveRequests = listDeniedLeaveRequests;
        }

        private void SearchDate()
        {
            LoadLeaveRequestList();
            var allItem = ReceivedLeaveRequests;
            allItem = ReceivedLeaveRequests
                    .Where(item => item.CreatedDate.Date == TimeCreateLeave.Date)
                    .ToList();
            ReceivedLeaveRequests = new List<LeaveRequest>(allItem);

            Log.Instance.Information(nameof(LeaveRequestsViewModel), "selected date = " + timeCreateLeave.ToShortDateString());
        }

  
    }
}
