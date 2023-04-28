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

namespace CompanyManagement.ViewModels.UserControls
{

    public class LeaveListViewModel : BaseViewModel
    {

        private List<LeaveRequest> leaves;
        public List<LeaveRequest> Leaves { get => leaves; set { leaves = value; OnPropertyChanged(); } }

        private Visibility visibleAddButton = Visibility.Collapsed;
        public Visibility VisibleAddButton { get => visibleAddButton; set { visibleAddButton = value; OnPropertyChanged(); } }

        private Visibility visibleDeleteButton = Visibility.Collapsed;
        public Visibility VisibleDeleteButton { get => visibleDeleteButton; set { visibleDeleteButton = value; OnPropertyChanged(); } }

        private Visibility visibleUpdateButton = Visibility.Collapsed;
        public Visibility VisibleUpdateButton { get => visibleUpdateButton; set { visibleUpdateButton = value; OnPropertyChanged(); } }

        private Visibility visibleApproveButton = Visibility.Collapsed;
        public Visibility VisibleApproveButton { get => visibleApproveButton; set { visibleApproveButton = value; OnPropertyChanged(); } }

        private DateTime timeCreateLeave = DateTime.Now;
        public DateTime TimeCreateLeave { get => timeCreateLeave; set { timeCreateLeave = value; OnPropertyChanged(); FilterDate(); } }

        public ICommand NextTimeLeaveCreateDate { get; set; }
        public ICommand BackTimeLeaveCreateDate { get; set; }
        public ICommand OpenLeaveInputCommand { get; set; }
        public ICommand DeleteLeaveCommand { get; set; }
        public ICommand UpdateLeaveCommand { get; set; }
        public ICommand ApproveLeaveCommand { get; set; }

        public INavigateAssignmentView ParentDataContext { get; set; }

        private LeaveRequestsDao leaveRequestsDao = new LeaveRequestsDao();
        private DepartmentsDao departmentsDao = new DepartmentsDao();
        private EmployeesDao employeesDao = new EmployeesDao();

        private Employee currentEmployee = CurrentUser.Ins.EmployeeIns;

        public LeaveListViewModel()
        {          
            SetVisible();
            FilterDate();
            NextTimeLeaveCreateDate = new RelayCommand<object>(ExecuteNextTimeLeaveCreateDate);
            BackTimeLeaveCreateDate = new RelayCommand<object>(ExecuteBackTimeLeaveCreateDate);
        }

        private void ExecuteBackTimeLeaveCreateDate(object obj)
        {
            TimeCreateLeave = timeCreateLeave.AddDays(-1);
        }

        private void ExecuteNextTimeLeaveCreateDate(object obj)
        {
            TimeCreateLeave = timeCreateLeave.AddDays(1);
        }

        private void LoadLeaveList()
        {
            Leaves = GetLeaveList();
        }

        private List<LeaveRequest> GetLeaveList()
        {
            if (string.Equals(currentEmployee.RoleID, BaseDao.MANAGER_ROLE_ID))
                return leaveRequestsDao.GetAll();
            if (string.Equals(currentEmployee.RoleID, BaseDao.DEPARTMENT_HEAD_ROLE_ID))
                return leaveRequestsDao.SearchByDeptHeaderID(currentEmployee.ID);
            return leaveRequestsDao.SearchByEmployeeID(currentEmployee.ID);
        }

        private void SetVisible()
        {
            if (string.Equals(currentEmployee.RoleID, BaseDao.MANAGER_ROLE_ID))
                SetVisibleManager();
            else if (string.Equals(currentEmployee.RoleID, BaseDao.DEPARTMENT_HEAD_ROLE_ID))
                SetVisibleDepartmentHead();
            else
                SetVisibleEmployee();
        }

        private void SetVisibleManager()
        {
            VisibilityManager();
            VisibilityManagerCommands();
        }

        private void SetVisibleDepartmentHead()
        {
            VisibilityCRUD();
            VisibilityManager();
            VisibilityCRUDCommands();
            VisibilityManagerCommands();
        }

        private void SetVisibleEmployee()
        {
            VisibilityCRUD();
            VisibilityCRUDCommands();
        }

        private void FilterDate()
        {
            LoadLeaveList();
            var allItem = Leaves;
            allItem = Leaves
                    .Where(item => item.CreatedDate.Date == TimeCreateLeave.Date)
                    .ToList();
            Leaves = new List<LeaveRequest>(allItem);
            Log.Instance.Information(nameof(LeaveListViewModel), "selected date = " + timeCreateLeave.ToShortDateString());
        }

        private void VisibilityManager()
        {
            visibleApproveButton = Visibility.Visible;
        }

        private void VisibilityManagerCommands()
        {
            ApproveLeaveCommand = new RelayCommand<LeaveRequest>(ExecuteApproveCommand);
        }    

        private void VisibilityCRUD()
        {
            visibleAddButton = Visibility.Visible;
            visibleDeleteButton = Visibility.Visible;
            visibleUpdateButton = Visibility.Visible;
        }

        private void VisibilityCRUDCommands()
        {
            OpenLeaveInputCommand = new RelayCommand<object>(ExecuteAddCommand);
            DeleteLeaveCommand = new RelayCommand<string>(ExecuteDeleteCommand);
            UpdateLeaveCommand = new RelayCommand<LeaveRequest>(ExecuteUpdateCommand);
        }

        private LeaveRequest CreateLeave()
        {
            string approveBy = string.Equals(currentEmployee.ID, BaseDao.EMPLOYEE_ROLE_ID) 
                ? departmentsDao.DepartmentByEmployeeDeptID(currentEmployee.DepartmentID).ManagerID
                : employeesDao.SearchByPositionID(BaseDao.MANAGER_ROLE_ID).ID;
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
            } while (leaveRequestsDao.SearchByID(leaaveID) != null);
            return leaaveID;
        }

        private void Add(LeaveRequest leaveRequest)
        {
            leaveRequestsDao.Add(leaveRequest);
            LoadLeaveList();
        }

        private void Update(LeaveRequest leaveRequest)
        {
            leaveRequestsDao.Update(leaveRequest);
            LoadLeaveList();
        }

        private void ExecuteAddCommand(object p)
        {
            var leave = CreateLeave();
            var inputDialogService = new InputDialogService<LeaveRequest>(new AddLeaveDialog(), leave, Add);
            inputDialogService.Show();
            FilterDate();
        }

        private void ExecuteDeleteCommand(string id)
        {
            var dialog = new AlertDialogService(
              "Xóa xin phép nghỉ",
              "Bạn chắc chắn muốn xóa xin phép nghỉ!",
              () =>
              {
                  leaveRequestsDao.Delete(id);
                  LoadLeaveList();
              }, null);
            dialog.Show();
            FilterDate();
        }

        private void ExecuteUpdateCommand(LeaveRequest leaveRequest)
        {
            var inputDialogService = new InputDialogService<LeaveRequest>(new UpdateLeaveDialog(), leaveRequest, Update);
            inputDialogService.Show();
        }

        private void ExecuteApproveCommand(LeaveRequest leaveRequest)
        {
            var inputDialogService = new InputDialogService<LeaveRequest>(new UpdateLeaveForManagerDialog(), leaveRequest, Update);
            inputDialogService.Show();
        }
    }
}
