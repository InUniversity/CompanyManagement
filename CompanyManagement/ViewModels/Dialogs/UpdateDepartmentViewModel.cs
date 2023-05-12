using CompanyManagement.ViewModels.Base;
using CompanyManagement.ViewModels.UserControls;
using System.Windows.Input;
using System.Windows;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.Models;
using System;
using CompanyManagement.Services;
using CompanyManagement.Utilities;

namespace CompanyManagement.ViewModels.Dialogs
{
    public class UpdateDepartmentViewModel : BaseViewModel, IInputViewModel<Department>
    {
        public ICommand UpdateDeptCommand { get; private set; }
        public ICommand CloseDialogCommand { get; private set; }

        public DepartmentInputViewModel DeptInputDataContext { get; }

        private Action<Department> submitObjectAction;

        public UpdateDepartmentViewModel()
        {
            DeptInputDataContext = new DepartmentInputViewModel();
            UpdateDeptCommand = new RelayCommand<Window>(ExecuteUpdateDeptCommand);
            CloseDialogCommand = new RelayCommand<Window>(ExecuteCloseDialogCommand);
        }

        private void ExecuteCloseDialogCommand(Window window)
        {
            window.Close();
        }
    

        private void ExecuteUpdateDeptCommand(Window inputWindow)
        {
            DeptInputDataContext.TrimAllTexts();
            if (!CheckAllFields()) return;
            var dialog = new AlertDialogService(
                "Cập nhật phòng ban",
                "Bạn chắc chắn muốn cập nhật phòng ban!",
                () =>
                {
                    Log.Instance.Information(nameof(UpdateDepartmentViewModel), "Update Department: " + DeptInputDataContext.DeptIns.Name);
                    var department = DeptInputDataContext.DeptIns;
                    submitObjectAction?.Invoke(department);
                    inputWindow.Close();
                }, null);
            dialog.Show();
        }


        private bool CheckAllFields()
        {
            return DeptInputDataContext.CheckAllFields();
        }


        public void ReceiveObject(Department request)
        {
            DeptInputDataContext.DeptIns = request;
        }

        public void ReceiveSubmitAction(Action<Department> submitObjectAction)
        {
            this.submitObjectAction = submitObjectAction;
        }
    }
}