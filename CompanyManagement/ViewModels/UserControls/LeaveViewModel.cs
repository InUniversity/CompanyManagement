using CompanyManagement.Database;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;
using CompanyManagement.Services;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.Views.Dialogs;
using CompanyManagement.Views.Dialogs.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace CompanyManagement.ViewModels.UserControls
{

    public class LeaveViewModel : BaseViewModel
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

        public ICommand OpenLeaveInputCommand { get; set; }
        public ICommand DeleteLeaveCommand { get; set; }
        public ICommand UpdateLeaveCommand { get; set; }
        public ICommand ApproveLeaveCommand { get; set; }

        public INavigateAssignmentView ParentDataContext { get; set; }

        private LeaveDao leaveDao = new LeaveDao();
        private DepartmentDao departmentDao = new DepartmentDao();
        private EmployeeDao employeeDao = new EmployeeDao();

        private Employee currentEmployee = CurrentUser.Instance.CurrentEmployee;

        public LeaveViewModel()
        {
            LoadLeaves();
            SetVisible();
        }

        private void LoadLeaves()
        {
            var leaves = CurrentUser.Instance.IsEmployee()
                ? leaveDao.SearchByEmployeeID(currentEmployee.ID)
                : (CurrentUser.Instance.IsManager()
                ? leaveDao.GetAll()
                : leaveDao.SearchByDeptHeaderID(currentEmployee.ID));
            Leaves = leaves;
        }

        private void SetVisible()
        {

            if (CurrentUser.Instance.IsManager())
            {
                VisibilityManager();
                VisibilityManagerCommands();
                return;
            }

            if (CurrentUser.Instance.IsEmployee())
            {
                VisibilityCRUD();
                VisibilityCRUDCommands();
                return;
            }
 
            if(CurrentUser.Instance.IsDepartmentHead())
            {
                VisibilityCRUD();
                VisibilityManager();
                VisibilityCRUDCommands();
                VisibilityManagerCommands();
            }    
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
            string approveBy = CurrentUser.Instance.IsEmployee()
                ? departmentDao.DepartmentByEmployeeDeptID(currentEmployee.DepartmentID).ManagerID
                : employeeDao.SearchByPositionID(BaseDao.MANAGER_POS_ID).ID;
            return new Leave(AutoGenerateID(), currentEmployee.ID, "", "", DateTime.Now, DateTime.Now, "LS2",
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
            LoadLeaves();
        }

        private void Update(Leave leave)
        {
            leaveDao.Update(leave);
            LoadLeaves();
        }

        public void ExecuteAddCommand(object p)
        {
            Leave leave = CreateLeave();
            var inputDialogService = new InputDialogService<Leave>(new AddLeaveDialog(), leave, Add);
            inputDialogService.Show();
        }

        public void ExecuteDeleteCommand(string id)
        {
            AlertDialogService dialog = new AlertDialogService(
              "Xóa xin phép nghỉ",
              "Bạn chắc chắn muốn xóa xin phép nghỉ!",
              () =>
              {
                  leaveDao.Delete(id);
                  LoadLeaves();
              }, () => { });
            dialog.Show();
        }

        public void ExecuteUpdateCommand(Leave leave)
        {
            var inputDialogService =
                 new InputDialogService<Leave>(new UpdateLeaveDialog(), leave, Update);
            inputDialogService.Show();
        }

        public void ExecuteApproveCommand(Leave leave)
        {
            var inputDialogService =
                new InputDialogService<Leave>(new UpdateLeaveForManagerDialog(), leave, Update);
            inputDialogService.Show();
        }
    }
}
