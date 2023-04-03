using CompanyManagement.ViewModels.Base;
using CompanyManagement.Views.UserControls;
using System.Windows.Controls;
using CompanyManagement.ViewModels.Windows;
using CompanyManagement.Models;

namespace CompanyManagement.ViewModels.UserControls
{
    public class UserInformationViewModel : BaseViewModel 
    {

        private EmployeeInputUC employeeInputUC = new EmployeeInputUC();

        private ContentControl currentChildView = new ContentControl();
        public ContentControl CurrentChildView { get => currentChildView; set { currentChildView = value; OnPropertyChanged(); } }

        public UserInformationViewModel() 
        {
            ((EmployeeInputViewModel)employeeInputUC.DataContext).Retrieve(SingletonEmployee.Instance.CurrentEmployee);
            currentChildView = employeeInputUC;
        }
    }
}
