using System.Windows;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.Views.UserControls;
using System.Windows.Controls;
using System.Windows.Input;
using System;
using CompanyManagement.Services;
using CompanyManagement.Models;
using CompanyManagement.Database.Base;

namespace CompanyManagement.ViewModels.Windows
{
    public class MainViewModel : BaseViewModel
    {
        private ContentControl currentChildView;
        public ContentControl CurrentChildView { get => currentChildView; set { currentChildView = value; OnPropertyChanged(); } }

        private UserInformationUC userInformationUC = new UserInformationUC();
        private AssignmentUC assignmentUC = new AssignmentUC();
        private OrganizationUC organizationUC = new OrganizationUC();
        private LeaveRequestsUC leavesListUC = new LeaveRequestsUC();
        private ApproveLeaveRequestsUC approveLeaveRequestsUC = new ApproveLeaveRequestsUC();

        private Visibility visibilityAssignmentView;
        public Visibility VisibilityAssignmentView { get => visibilityAssignmentView; set { visibilityAssignmentView = value; OnPropertyChanged();} }

        private Visibility visibilityEmployeesView;
        public Visibility VisibilityEmployeesView { get => visibilityEmployeesView; set { visibilityEmployeesView = value; OnPropertyChanged(); } }

        private bool statusOrganizationView = false;
        public bool StatusOrganizationView { get => statusOrganizationView; set { statusOrganizationView = value; OnPropertyChanged(); } }

        private bool statusAssignmentView = false;
        public bool StatusAssignmentView { get => statusAssignmentView; set { statusAssignmentView = value; OnPropertyChanged(); } }

        private bool statusUserInformationView = false;
        public bool StatusUserInformationView { get => statusUserInformationView; set { statusUserInformationView = value; OnPropertyChanged(); } }

        private bool statusLeavesView = false;
        public bool StatusLeavesView { get => statusLeavesView; set { statusLeavesView = value; OnPropertyChanged(); } }

        public ICommand ShowUserInformationViewCommand { get; private set; }
        public ICommand ShowAssignmentViewCommand { get; private set; }
        public ICommand ShowOrganizationViewCommand { get; private set; }
        public ICommand ShowLeavesViewCommand { get; private set;}
        public ICommand ShowApproveLeaveRequestsViewCommand { get; private set; }
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
            ShowOrganizationViewCommand = new RelayCommand<object>(ExecuteShowOrganizationView);
            ShowLeavesViewCommand = new RelayCommand<object>(ExecuteShowLeavesViewCommand);
            ShowApproveLeaveRequestsViewCommand = new RelayCommand<Window>(ExecuteShowApproveLeaveRequestListView);
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

        private void ExecuteShowOrganizationView(object obj)
        {
            CurrentChildView = organizationUC;
            StatusOrganizationView = true;
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