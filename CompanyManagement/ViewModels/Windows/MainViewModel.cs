using System.Windows;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.Views.UserControls;
using System.Windows.Controls;
using System.Windows.Input;
using System;
using CompanyManagement.Services;
using CompanyManagement.Models;
using CompanyManagement.Database.Base;
using System.Data.SqlClient;

namespace CompanyManagement.ViewModels.Windows
{
    public class MainViewModel : BaseViewModel
    {
        private ContentControl currentChildView;
        public ContentControl CurrentChildView { get => currentChildView; set { currentChildView = value; OnPropertyChanged(); } }

        private UserInformationUC userInformationUC = new UserInformationUC();
        private AssignmentUC assignmentUC = new AssignmentUC();
        private EmployeesUC employeesUC = new EmployeesUC();
        private LeaveRequestsUC leavesListUC = new LeaveRequestsUC();
        private ApproveLeaveRequestsUC approveLeaveRequestsUC = new ApproveLeaveRequestsUC();
        private SalaryRecordsUC salaryRecordsUC = new SalaryRecordsUC();

        private Visibility visibilityAssignmentView;
        public Visibility VisibilityAssignmentView { get => visibilityAssignmentView; set { visibilityAssignmentView = value; OnPropertyChanged();} }

        private Visibility visibilityEmployeesView;
        public Visibility VisibilityEmployeesView { get => visibilityEmployeesView; set { visibilityEmployeesView = value; OnPropertyChanged(); } }

        private bool statusEmployeesView = false;
        public bool StatusEmployeesView { get => statusEmployeesView; set { statusEmployeesView = value; OnPropertyChanged(); } }

        private bool statusAssignmentView = false;
        public bool StatusAssignmentView { get => statusAssignmentView; set { statusAssignmentView = value; OnPropertyChanged(); } }

        private bool statusUserInformationView = false;
        public bool StatusUserInformationView { get => statusUserInformationView; set { statusUserInformationView = value; OnPropertyChanged(); } }

        private bool statusLeavesView = false;
        public bool StatusLeavesView { get => statusLeavesView; set { statusLeavesView = value; OnPropertyChanged(); } }

        private bool statusSalaryRecordsView = false;
        public bool StatusSalaryRecordsView { get => statusSalaryRecordsView; set { statusSalaryRecordsView = value; OnPropertyChanged(); } }

        public ICommand ShowUserInformationViewCommand { get; private set; }
        public ICommand ShowAssignmentViewCommand { get; private set; }
        public ICommand ShowEmployeesViewCommand { get; private set; }
        public ICommand ShowLeavesViewCommand { get; private set;}
        public ICommand ShowApproveLeaveRequestsViewCommand { get; private set; }
        public ICommand ShowSalaryRecordsViewCommand { get; private set; }
        public ICommand LogOutCommand { get; private set; }

        public MainViewModel()
        {
            ExecuteShowUserInformationViewCommand(null);
            SetCommands();
        }

        private void SetCommands()
        {
            ShowUserInformationViewCommand = new RelayCommand<object>(ExecuteShowUserInformationViewCommand);
            ShowAssignmentViewCommand = new RelayCommand<object>(ExecuteShowAssignmentView);
            ShowEmployeesViewCommand = new RelayCommand<object>(ExecuteShowEmployeesView);
            ShowLeavesViewCommand = new RelayCommand<object>(ExecuteShowLeavesViewCommand);
            ShowApproveLeaveRequestsViewCommand = new RelayCommand<Window>(ExecuteShowApproveLeaveRequestListView);
            ShowSalaryRecordsViewCommand = new RelayCommand<object>(ExecuteShowSalaryRecordsView);
            LogOutCommand = new RelayCommand<Window>(ExecuteLogOutCommand);
        }

        private void ExecuteShowUserInformationViewCommand(object obj)
        {
            CurrentChildView = userInformationUC;
            StatusUserInformationView = true;
        }

        private void ExecuteShowAssignmentView(object obj)
        {
            // TODO
            // To refresh tasks view (Bad approve)
            assignmentUC = new AssignmentUC();
            CurrentChildView = assignmentUC;
            StatusAssignmentView = true;
        }

        private void ExecuteShowEmployeesView(object obj)
        {
            CurrentChildView = employeesUC;
            StatusEmployeesView = true;
        }

        private void ExecuteShowLeavesViewCommand(object obj)
        {
            CurrentChildView = leavesListUC;
            StatusLeavesView = true;
        }

        private void ExecuteShowApproveLeaveRequestListView(Window obj)
        {
            CurrentChildView = approveLeaveRequestsUC;
        }

        private void ExecuteShowSalaryRecordsView(object obj)
        {
            CurrentChildView = salaryRecordsUC;
            StatusSalaryRecordsView = true;
        }

        private void ExecuteLogOutCommand(Window window)
        {
            AlertDialogService dialog = new AlertDialogService(
              "Đăng xuất",
              "Bạn chắc chắn đăng xuất khỏi tài khoản!",
              () =>
              {
                  window.Close();
              }, null);
            dialog.Show();
        }
    }
}