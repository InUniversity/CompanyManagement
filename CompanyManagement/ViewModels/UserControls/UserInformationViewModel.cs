using CompanyManagement.ViewModels.Base;
using CompanyManagement.Views.UserControls;
using System.Windows.Controls;
using CompanyManagement.ViewModels.Windows;

namespace CompanyManagement.ViewModels.UserControls
{
    class UserInformationViewModel : BaseViewModel 
    {
        EmployeeInputUC employeeInputUC = new EmployeeInputUC();

        private ContentControl currentChildView = new ContentControl();
        public ContentControl CurrentChildView { get => currentChildView; set { currentChildView = value; OnPropertyChanged(); } }
        public UserInformationViewModel() 
        {
            ((EmployeeInputViewModel)employeeInputUC.DataContext).Retrieve(LoginViewModel.CurrentUser.CurrentEmployee);
            currentChildView = employeeInputUC;
        }
    }
}
