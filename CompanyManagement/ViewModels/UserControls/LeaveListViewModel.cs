using CompanyManagement.Database;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;
using CompanyManagement.Services;
using CompanyManagement.Utilities;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.Views.Dialogs;
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
        private List<Leave> leaveRequestList;
        public List<Leave> LeaveRequestList { get => leaveRequestList; set { leaveRequestList = value; OnPropertyChanged(); } }

        private List<Leave> unapprovedLeaveRequestList;
        public List<Leave> UnapprovedLeaveRequestList { get => unapprovedLeaveRequestList; set { unapprovedLeaveRequestList = value; OnPropertyChanged(); } }

        private List<Leave> approvedLeaveRequestList;
        public List<Leave> ApprovedLeaveRequestList { get => approvedLeaveRequestList; set { approvedLeaveRequestList = value; OnPropertyChanged(); } }

        private List<Leave> deniedLeaveRequestList;
        public List<Leave>  DeniedLeaveRequestList { get => deniedLeaveRequestList; set { deniedLeaveRequestList = value; OnPropertyChanged(); } }
        
        private Visibility visibleLeaveRequestListExpander = Visibility.Collapsed;
        public Visibility VisibleLeaveRequestListExpander { get => visibleUnapprovedLeaveListExpander; set { visibleUnapprovedLeaveListExpander = value; OnPropertyChanged(); } } 

        private Visibility visibleUnapprovedLeaveListExpander = Visibility.Collapsed;
        public Visibility VisibleUnapprovedLeaveListExpander { get => visibleUnapprovedLeaveListExpander; set { visibleUnapprovedLeaveListExpander = value; OnPropertyChanged(); } }

        private Visibility visibleApprovedLeaveListExpander = Visibility.Collapsed;
        public Visibility VisibleApprovedLeaveListExpander { get => visibleApprovedLeaveListExpander; set { visibleApprovedLeaveListExpander = value; OnPropertyChanged(); } }

        private Visibility visibleDeniedLeaveListExpander = Visibility.Collapsed;
        public Visibility VisibleDeniedLeaveListExpander { get => visibleDeniedLeaveListExpander; set { visibleDeniedLeaveListExpander = value; OnPropertyChanged(); } }

        private DateTime timeCreateLeave = DateTime.Now;
        public DateTime TimeCreateLeave { get => timeCreateLeave; set { timeCreateLeave = value; OnPropertyChanged(); SearchDate(); } }

        public ICommand NextTimeCommand { get; private set; }
        public ICommand BackTimeCommand { get; private set; }
        public ICommand OpenAddLeaveRequestCommand { get; private set; }
        public ICommand DeleteLeaveRequestCommand { get; private set; }
        public ICommand OpenUpdateLeaveRequestCommand { get; private set; }
        public ICommand ApproveLeaveRequestCommand { get; private set; }
        public ICommand DenyLeaveRequestCommand { get; private set; }
        
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

        private LeaveDao leaveDao = new LeaveDao();
        private DepartmentsDao departmentsDao = new DepartmentsDao();
        private EmployeesDao employeesDao = new EmployeesDao();

        private Employee currentEmployee = CurrentUser.Ins.EmployeeIns;

        public LeaveListViewModel(ILeaveListStrategy leaveListStrategy)
        {
            LeaveListStrategy = leaveListStrategy;
            SetCommands();
            SearchDate();
        }

        private void SetCommands()
        {
            NextTimeCommand = new RelayCommand<object>(ExecuteNextTimeLeaveCreateDate);
            BackTimeCommand = new RelayCommand<object>(ExecuteBackTimeLeaveCreateDate);
            OpenAddLeaveRequestCommand = new RelayCommand<object>(ExecuteAddCommand);
            DeleteLeaveRequestCommand = new RelayCommand<string>(ExecuteDeleteCommand);
            OpenUpdateLeaveRequestCommand = new RelayCommand<Leave>(ExecuteUpdateCommand);
            ApproveLeaveRequestCommand = new RelayCommand<Leave>(ExecuteApproveCommand);
            DenyLeaveRequestCommand = new RelayCommand<Leave>(ExecuteDenyCommand);
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
            
            var receivedLeaveRequests = leaveDao.SearchByApprovedBy(currentEmployee.ID);

            var unapprovedLeaveList = receivedLeaveRequests.Where(p => p.LeaveStatusID == BaseDao.APPROVAL).ToList();
            UnapprovedLeaveRequestList = unapprovedLeaveList;

            var listApprovedLeaves = receivedLeaveRequests.Where(p => p.LeaveStatusID == BaseDao.APPROVED).ToList();
            ApprovedLeaveRequestList = listApprovedLeaves;

            var listUnapprovedLeaves = receivedLeaveRequests.Where(p => p.LeaveStatusID == BaseDao.UNAPPROVED).ToList();
            DeniedLeaveRequestList = listUnapprovedLeaves;
        }

        private List<Leave> GetLeaveList()
        {
            if (string.Equals(currentEmployee.RoleID, BaseDao.MANAGER_ROLE_ID))
                return leaveDao.GetAll();
            if (string.Equals(currentEmployee.RoleID, BaseDao.DEPARTMENT_HEAD_ROLE_ID))
                return leaveDao.SearchByApprovedBy(currentEmployee.ID);
            return leaveDao.SearchByEmployeeID(currentEmployee.ID);
        }

        private void SearchDate()
        {
            LoadLeaveRequestList();
            var allItem = LeaveRequestList;
            allItem = LeaveRequestList
                    .Where(item => item.CreateDate.Date == TimeCreateLeave.Date)
                    .ToList();
            LeaveRequestList = new List<Leave>(allItem);
            
            Log.Instance.Information(nameof(LeaveListViewModel), "selected date = " + timeCreateLeave.ToShortDateString());
        }

        private Leave CreateLeave()
        {
            string approveBy = string.Equals(currentEmployee.ID, BaseDao.EMPLOYEE_ROLE_ID) 
                ? departmentsDao.DepartmentByEmployeeDeptID(currentEmployee.DepartmentID).ManagerID
                : employeesDao.SearchByPositionID(BaseDao.MANAGER_ROLE_ID).ID;
            return new Leave(AutoGenerateID(), currentEmployee.ID, "LS2", "", DateTime.Now, DateTime.Now, "LS2",
                DateTime.Now, approveBy , "");
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
            var inputDialogService = new InputDialogService<Leave>(new AddLeaveDialog(), leave, Add);
            inputDialogService.Show();
            SearchDate();
        }

        private void Add(Leave leave)
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

        private void ExecuteUpdateCommand(Leave leave)
        {
            var inputDialogService = new InputDialogService<Leave>(new UpdateLeaveDialog(), leave, Update);
            inputDialogService.Show();
        }
        
        private void Update(Leave leave)
        {
            leaveDao.Update(leave);
            LoadLeaveRequestList();
        }

        private void ExecuteApproveCommand(Leave leave)
        {
            Update(leave);
        }

        private void ExecuteDenyCommand(Leave leave)
        {
            Update(leave);
        }
    }
}
