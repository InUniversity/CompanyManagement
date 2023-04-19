using CompanyManagement.ViewModels.Base;
using CompanyManagement.Views.UserControls;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace CompanyManagement.ViewModels.Windows
{
    public class EmployeeViewModel : BaseViewModel
    {
        private ContentControl currentChildView;
        public ContentControl CurrentChildView { get => currentChildView; set { currentChildView = value; OnPropertyChanged(); } }

        private AssignmentUC assignmentUC = new AssignmentUC();
        private LeaveUC leaveUC = new LeaveUC();
        private UserInformationUC userInformationUC = new UserInformationUC();
        
        private bool statusAssignmentView = false;
        public bool StatusAssignmentView { get => statusAssignmentView; set { statusAssignmentView = value; OnPropertyChanged(); } }

        private bool statusLeaveView = false;
        public bool StatusLeaveView { get => statusLeaveView; set { statusLeaveView = value; OnPropertyChanged(); } }

        private bool statusUserInformationView = false;
        public bool StatusUserInformationView { get => statusUserInformationView; set { statusUserInformationView = value; OnPropertyChanged(); } }

        public ICommand ShowAssignmentViewCommand { get; set; }
        public ICommand ShowLeaveViewCommand { get; set; }

        public ICommand ShowUserInformationViewCommand { get; set; }

        public EmployeeViewModel()
        {
            ExecuteShowUserInformationView(null);
            CurrentChildView = assignmentUC;
            SetCommands();
        }

        private void SetCommands()
        {
            ShowAssignmentViewCommand = new RelayCommand<object>(ExecuteShowAssignView);
            ShowLeaveViewCommand = new RelayCommand<object>(ExecuteShowLeaveView);
            ShowUserInformationViewCommand = new RelayCommand<object>(ExecuteShowUserInformationView);
        }

        private void ExecuteShowUserInformationView(object obj)
        {
            CurrentChildView = userInformationUC;
            StatusUserInformationView = true;
        }

        private void ExecuteShowAssignView(object obj)
        {
            CurrentChildView = assignmentUC;
            StatusAssignmentView = true;
        }
        
        private void ExecuteShowLeaveView(object obj)
        {
            CurrentChildView = leaveUC;
            StatusLeaveView = true;
        }
    }
}
