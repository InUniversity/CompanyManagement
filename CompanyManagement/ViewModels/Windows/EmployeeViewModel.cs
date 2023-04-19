using CompanyManagement.ViewModels.Base;
using CompanyManagement.Views.UserControls;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace CompanyManagement.ViewModels.Windows
{
    class EmployeeViewModel : BaseViewModel
    {
        
        private ContentControl currentChildView;
        public ContentControl CurrentChildView { get => currentChildView; set { currentChildView = value; OnPropertyChanged(); } }

        private UserInformationUC userInformationUC = new UserInformationUC();
        private AssignmentUC assignmentUC = new AssignmentUC();

        private bool statusAssignmentView = false;
        public bool StatusAssignmentView { get => statusAssignmentView; set { statusAssignmentView = value; OnPropertyChanged(); } }

        private bool statusUserInformationView = false;
        public bool StatusUserInformationView { get => statusUserInformationView; set { statusUserInformationView = value; OnPropertyChanged(); } }

        public ICommand ShowAssignViewCommand { get; }
        public ICommand ShowUserInformationViewCommand { get; set; }


        public EmployeeViewModel()
        {
            ExecuteShowUserInformationViewCommand(null);
            ShowAssignViewCommand = new RelayCommand<object>(ExecuteShowAssignViewCommand);
            ShowUserInformationViewCommand = new RelayCommand<object>(ExecuteShowUserInformationViewCommand);
        }

        private void ExecuteShowUserInformationViewCommand(object obj)
        {

            CurrentChildView = userInformationUC;
            StatusUserInformationView = true;
        }

        private void ExecuteShowAssignViewCommand(object obj)
        {
            CurrentChildView.Content = new AssignmentUC();
            StatusAssignmentView = true;
        }
    }
}
