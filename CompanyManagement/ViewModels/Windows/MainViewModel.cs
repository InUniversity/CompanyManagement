using System.Windows;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.Views.UserControls;
using System.Windows.Controls;
using System.Windows.Input;
using System;
using CompanyManagement.Services;

namespace CompanyManagement.ViewModels.Windows
{
    public class MainViewModel : BaseViewModel
    {
        private ContentControl currentChildView;
        public ContentControl CurrentChildView { get => currentChildView; set { currentChildView = value; OnPropertyChanged(); } }

        private AssignmentUC assignmentUC = new AssignmentUC();
        private EmployeesUC employeesUC = new EmployeesUC();
        private UserInformationUC userInformationUC = new UserInformationUC();
        private LeaveListUC leavesListUC = new LeaveListUC();
        private ApproveLeaveRequestListUC approveLeaveRequestListUC = new ApproveLeaveRequestListUC();

        private Visibility visibilityAssignmentView;
        public Visibility VisibilityAssignmentView { get => visibilityAssignmentView; set { visibilityAssignmentView = value; OnPropertyChanged();} }

        private Visibility visibilityEmployeesView;
        public Visibility VisibilityEmployeesView { get => visibilityEmployeesView; set { visibilityEmployeesView = value; OnPropertyChanged(); } }
        
        private Visibility visibilityWorkScheduleView;
        public Visibility VisibilityWorkScheduleView { get => visibilityWorkScheduleView; set { visibilityWorkScheduleView = value; OnPropertyChanged(); } }

        private bool statusEmployeesView = false;
        public bool StatusEmployeesView { get => statusEmployeesView; set { statusEmployeesView = value; OnPropertyChanged(); } }

        private bool statusAssignmentView = false;
        public bool StatusAssignmentView { get => statusAssignmentView; set { statusAssignmentView = value; OnPropertyChanged(); } }

        private bool statusUserInformationView = false;
        public bool StatusUserInformationView { get => statusUserInformationView; set { statusUserInformationView = value; OnPropertyChanged(); } }

        private bool statusLeavesView = false;
        public bool StatusLeavesView { get => statusLeavesView; set { statusLeavesView = value; OnPropertyChanged(); } }

        public ICommand ShowAssignmentViewCommand { get; set; }
        public ICommand ShowEmployeesViewCommand { get; set; }
        public ICommand ShowUserInformationViewCommand { get; set; }
        public ICommand ShowLeavesViewCommand { get; set;}
        public ICommand LogoutCommand { get; set; }
        public ICommand ShowApproveLeaveRequestListViewCommand { get; set; }

        public MainViewModel()
        {
            ExecuteShowUserInformationViewCommand(null);
            SetCommands();
        }

        private void SetCommands()
        {
            ShowAssignmentViewCommand = new RelayCommand<object>(ExecuteShowAssignmentView);
            ShowEmployeesViewCommand = new RelayCommand<object>(ExecuteShowEmployeesView);
            ShowUserInformationViewCommand = new RelayCommand<object>(ExecuteShowUserInformationViewCommand);
            ShowLeavesViewCommand = new RelayCommand<object>(ExecuteShowLeavesViewCommand);
            LogoutCommand = new RelayCommand<Window>(ExecuteLogoutCommand);
            ShowApproveLeaveRequestListViewCommand = new RelayCommand<Window>(ExecuteShowApproveLeaveRequestListView);
        }

        private void ExecuteShowApproveLeaveRequestListView(Window obj)
        {
            CurrentChildView = approveLeaveRequestListUC;
        }

        private void ExecuteLogoutCommand(Window window)
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

        private void ExecuteShowLeavesViewCommand(object obj)
        {
            CurrentChildView = leavesListUC;
            StatusLeavesView = true;
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
    }
}