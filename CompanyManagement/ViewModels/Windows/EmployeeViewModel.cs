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
        private UserInformationUC userInformationUC = new UserInformationUC();
        private LeaveUC leaveUC = new LeaveUC();
        
        private bool statusAssignmentView = false;
        public bool StatusAssignmentView { get => statusAssignmentView; set { statusAssignmentView = value; OnPropertyChanged(); } }
        
        private bool statusUserInformationView = false;
        public bool StatusUserInformationView { get => statusUserInformationView; set { statusUserInformationView = value; OnPropertyChanged(); } }

        private bool statusLeaveView = false;
        public bool StatusLeaveView { get => statusLeaveView; set { statusLeaveView = value; OnPropertyChanged(); } }

        public ICommand ShowAssignmentViewCommand { get; private set; }
        public ICommand ShowUserInformationViewCommand { get; private set; }
        public ICommand ShowLeaveViewCommand { get; private set; }

        public EmployeeViewModel()
        {
            ExecuteShowUserInformationView(null);
            SetCommands();
        }

        private void SetCommands()
        {
            ShowAssignmentViewCommand = new RelayCommand<object>(ExecuteShowAssignView);
            ShowUserInformationViewCommand = new RelayCommand<object>(ExecuteShowUserInformationView);
            ShowLeaveViewCommand = new RelayCommand<object>(ExecuteShowLeaveView);
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
