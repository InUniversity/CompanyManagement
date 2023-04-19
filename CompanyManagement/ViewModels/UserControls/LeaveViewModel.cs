using CompanyManagement.Database;
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

        public ICommand OpenLeaveInputCommand { get; set; }
        public ICommand DeleteLeaveCommand { get; set; }
        public ICommand UpdateLeaveCommand { get; set; }
        public ICommand ItemClickCommand { get; set; }

        public INavigateAssignmentView ParentDataContext { get; set; }

        private LeaveDao leaveDao;
        private DepartmentDao departmentDao;

        private Employee currentEmployee = CurrentUser.Instance.CurrentEmployee;

        public LeaveViewModel()
        {
            leaveDao = new LeaveDao();
            departmentDao = new DepartmentDao();
            LoadLeave();
            SetCommand();
        }

        private void LoadLeave()
        {
            Leaves = leaveDao.GetAll();
        }

        private void SetCommand()
        {
            OpenLeaveInputCommand = new RelayCommand<object>(ExecuteAddCommand);
            DeleteLeaveCommand = new RelayCommand<string>(ExecuteDeleteCommand);
            UpdateLeaveCommand = new RelayCommand<Leave>(ExecuteUpdateCommand);
        }

        private Leave CreateLeave()
        {
            return new Leave(AutoGenerateID(), currentEmployee.ID, "", "",DateTime.Now, DateTime.Now, "", 
                DateTime.Now, departmentDao.SearchManagerIDByEmployeeID(currentEmployee.ID), "");
        }

        private string AutoGenerateID()
        {
            string leaaveID;
            Random random = new Random();
            do
            {
                int number = random.Next(1000000);
                leaaveID = $"LV{number:000000}";
            } while (leaveDao.SearchByID(leaaveID) != null);
            return leaaveID;
        }

        private void Add(Leave leave)
        {
            leaveDao.Add(leave);
            LoadLeave();
        }

        private void Update(Leave leave)
        {
            leaveDao.Update(leave);
            LoadLeave();
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
                  LoadLeave();
              }, () => { });
            dialog.Show();
        }

        public void ExecuteUpdateCommand(Leave leave)
        {
            IInputDialog<Leave> updateInputDiaLog = CurrentUser.Instance.IsEmployee()
                ? new UpdateLeaveDialog()
                : new UpdateLeaveForManagerDialog();
            InputDialogService<Leave> inputDialogService =
                new InputDialogService<Leave>(updateInputDiaLog, leave, Update);
            inputDialogService.Show();
        }
    }
}
