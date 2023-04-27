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

        private List<Leave> leaves;
        public List<Leave> Leaves { get => leaves; set { leaves = value; OnPropertyChanged(); } }

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

        private LeaveDao leaveDao = new LeaveDao();
        private DepartmentDao departmentDao = new DepartmentDao();
        private EmployeeDao employeeDao = new EmployeeDao();

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

        private List<Leave> GetLeaveList()
        {
            if (string.Equals(currentEmployee.RoleID, BaseDao.MANAGER_ROLE_ID))
                return leaveDao.GetAll();
            if (string.Equals(currentEmployee.RoleID, BaseDao.DEPARTMENT_HEAD_ROLE_ID))
                return leaveDao.SearchByDeptHeaderID(currentEmployee.ID);
            return leaveDao.SearchByEmployeeID(currentEmployee.ID);
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
                    .Where(item => item.CreateDate.Date == TimeCreateLeave.Date)
                    .ToList();
            Leaves = new List<Leave>(allItem);
            Log.Instance.Information(nameof(LeaveListViewModel), "selected date = " + timeCreateLeave.ToShortDateString());
        }

        private void VisibilityManager()
        {
            visibleApproveButton = Visibility.Visible;
        }

        private void VisibilityManagerCommands()
        {
            ApproveLeaveCommand = new RelayCommand<Leave>(ExecuteApproveCommand);
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
            UpdateLeaveCommand = new RelayCommand<Leave>(ExecuteUpdateCommand);
        }

        private Leave CreateLeave()
        {
            string approveBy = string.Equals(currentEmployee.ID, BaseDao.EMPLOYEE_ROLE_ID) 
                ? departmentDao.DepartmentByEmployeeDeptID(currentEmployee.DepartmentID).ManagerID
                : employeeDao.SearchByPositionID(BaseDao.MANAGER_ROLE_ID).ID;
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

        private void Add(Leave leave)
        {
            leaveDao.Add(leave);
            LoadLeaveList();
        }

        private void Update(Leave leave)
        {
            leaveDao.Update(leave);
            LoadLeaveList();
        }

        private void ExecuteAddCommand(object p)
        {
            var leave = CreateLeave();
            var inputDialogService = new InputDialogService<Leave>(new AddLeaveDialog(), leave, Add);
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
                  leaveDao.Delete(id);
                  LoadLeaveList();
              }, null);
            dialog.Show();
            FilterDate();
        }

        private void ExecuteUpdateCommand(Leave leave)
        {
            var inputDialogService = new InputDialogService<Leave>(new UpdateLeaveDialog(), leave, Update);
            inputDialogService.Show();
        }

        private void ExecuteApproveCommand(Leave leave)
        {
            var inputDialogService = new InputDialogService<Leave>(new UpdateLeaveForManagerDialog(), leave, Update);
            inputDialogService.Show();
        }
    }
}
