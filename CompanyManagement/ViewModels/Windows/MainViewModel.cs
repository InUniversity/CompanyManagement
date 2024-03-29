﻿using System.Windows;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.Views.UserControls;
using System.Windows.Controls;
using System.Windows.Input;
using CompanyManagement.Services;
using CompanyManagement.Strategies.Windows.MainView;

namespace CompanyManagement.ViewModels.Windows
{
    public class MainViewModel : BaseViewModel
    {
        private ContentControl currentChildView;
        public ContentControl CurrentChildView { get => currentChildView; set { currentChildView = value; OnPropertyChanged(); } }

        private UserInformationUC userInformationUC;
        private AssignmentUC assignmentUC;
        private OrganizationUC organizationUC;
        private LeaveRequestsUC leavesUC;
        private ApproveLeaveRequestsUC approveLeavesUC;
        private SalaryRecordsUC salaryRecordsUC;

        private bool statusOrganizationView = false;
        public bool StatusOrganizationView { get => statusOrganizationView; set { statusOrganizationView = value; OnPropertyChanged(); } }

        private bool statusAssignmentView = false;
        public bool StatusAssignmentView { get => statusAssignmentView; set { statusAssignmentView = value; OnPropertyChanged(); } }

        private bool statusUserInformationView = false;
        public bool StatusUserInformationView { get => statusUserInformationView; set { statusUserInformationView = value; OnPropertyChanged(); } }

        private bool statusLeavesView = false;
        public bool StatusLeavesView { get => statusLeavesView; set { statusLeavesView = value; OnPropertyChanged(); } }
        
        private bool statusApproveLeavesView = false;
        public bool StatusApproveLeavesView { get => statusApproveLeavesView; set { statusApproveLeavesView = value; OnPropertyChanged(); } }
        
        private bool statusSalaryRecordsView = false;
        public bool StatusSalaryRecordsView { get => statusSalaryRecordsView; set { statusSalaryRecordsView = value; OnPropertyChanged(); } }

        private Visibility visibilityAssignment;
        public Visibility VisibilityAssignment { get => visibilityAssignment; set { visibilityAssignment = value; OnPropertyChanged();} }

        private Visibility visibilityOrganization;
        public Visibility VisibilityOrganization { get => visibilityOrganization; set { visibilityOrganization = value; OnPropertyChanged(); } }
        
        private Visibility visibilityApproveLeaves;
        public Visibility VisibilityApproveLeaves { get => visibilityApproveLeaves; set { visibilityApproveLeaves = value; OnPropertyChanged(); } }
        
        private Visibility visibilitySalaryRecords;
        public Visibility VisibilitySalaryRecords { get => visibilitySalaryRecords; set { visibilitySalaryRecords = value; OnPropertyChanged(); } }
        
        public ICommand ShowUserInformationViewCommand { get; private set; }
        public ICommand ShowAssignmentViewCommand { get; private set; }
        public ICommand ShowOrganizationViewCommand { get; private set; }
        public ICommand ShowLeavesViewCommand { get; private set;}
        public ICommand ShowApproveLeaveRequestsViewCommand { get; private set; }
        public ICommand ShowSalaryRecordsViewCommand { get; private set; }
        public ICommand LogOutCommand { get; private set; }

        private IMainStrategy mainStrategy;
        public IMainStrategy MainStrategy
        {
            get => mainStrategy;
            set
            {
                mainStrategy = value; 
                mainStrategy.SetVisible(this);
            }
        }

        public MainViewModel(IMainStrategy mainStrategy)
        {
            MainStrategy = mainStrategy;
            InitAllView();
            ExecuteShowUserInformationViewCommand(null);
            SetCommands();
        }

        private void InitAllView()
        {
            userInformationUC = new UserInformationUC();
            assignmentUC = new AssignmentUC();
            organizationUC = new OrganizationUC();
            leavesUC = new LeaveRequestsUC();
            approveLeavesUC = new ApproveLeaveRequestsUC();
            salaryRecordsUC = new SalaryRecordsUC();
        }

        private void SetCommands()
        {
            ShowUserInformationViewCommand = new RelayCommand<object>(ExecuteShowUserInformationViewCommand);
            ShowAssignmentViewCommand = new RelayCommand<object>(ExecuteShowAssignmentView);
            ShowOrganizationViewCommand = new RelayCommand<object>(ExecuteShowOrganizationView);
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

        private void ExecuteShowOrganizationView(object obj)
        {
            CurrentChildView = organizationUC;
            StatusOrganizationView = true;
        }

        private void ExecuteShowLeavesViewCommand(object obj)
        {
            CurrentChildView = leavesUC;
            StatusLeavesView = true;
        }

        private void ExecuteShowApproveLeaveRequestListView(Window obj)
        {
            CurrentChildView = approveLeavesUC;
            StatusApproveLeavesView = true;
        }
        
        private void ExecuteShowSalaryRecordsView(object obj)
        {
            CurrentChildView = salaryRecordsUC;
            StatusSalaryRecordsView = true;
        }

        private void ExecuteLogOutCommand(Window window)
        {
            var dialog = new AlertDialogService( 
                "Đăng xuất", 
                "Bạn chắc chắn đăng xuất khỏi tài khoản!", 
                window.Close, null); 
            dialog.Show();
        }
    }
}
