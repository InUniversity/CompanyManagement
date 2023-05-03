using CompanyManagement.ViewModels.Base;
using CompanyManagement.ViewModels.UserControls;
using System.Windows.Input;
using System.Windows;

namespace CompanyManagement.ViewModels.Dialogs
{
    public class UpdateDepartmentViewModel : BaseViewModel
    {
        public ICommand UpdateDeptCommand { get; set; }

        public DepartmentInputViewModel DeptInputVM { get; set; }

        public UpdateDepartmentViewModel()
        {
            DeptInputVM = new DepartmentInputViewModel();
            UpdateDeptCommand = new RelayCommand<Window>(ExecuteUpdateDept);
        }

        private void ExecuteUpdateDept(Window inputWindow)
        {
            DeptInputVM.TrimAllTexts();
            var department = DeptInputVM.DeptIns;
            inputWindow.Close();
        }
    }
}