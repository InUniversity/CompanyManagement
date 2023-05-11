using CompanyManagement.Database;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;
using CompanyManagement.Services;
using CompanyManagement.Utilities;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.ViewModels.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CompanyManagement.ViewModels.Dialogs
{
    public class AddDepartmentViewModel : BaseDao, IInputViewModel<Department>
    {
        public  DepartmentInputViewModel DepartmentInputDataContext { get; set; }

        private Action<Department> submitObjectAction;

        public ICommand AddDepartmentCommand { get; private set; }
        public ICommand CloseDialogCommand { get; private set; }

        private DepartmentsDao departmentsDao = new DepartmentsDao();
        public AddDepartmentViewModel() 
        {
            DepartmentInputDataContext = new DepartmentInputViewModel();
            SetCommands();
        } 

        private void SetCommands()
        {
            AddDepartmentCommand = new RelayCommand<Window>(ExecuteAddDepartmentCommand);
            CloseDialogCommand = new RelayCommand<Window>(ExecuteCloseDialogCommand);
        }

        private void ExecuteCloseDialogCommand(Window window)
        {
            window.Close();
        }

        private void ExecuteAddDepartmentCommand(Window window)
        {
            DepartmentInputDataContext.TrimAllTexts();
            if (!CheckAllFields())
                return;
            var dialog = new AlertDialogService(
                "Thêm phòng ban",
                "Bạn chắc chắn muốn thêm phòng ban !",
                () =>
                {
                    var dept = DepartmentInputDataContext.DeptIns;
                    submitObjectAction?.Invoke(dept);
                    window.Close();
                }, null);
            dialog.Show();
        }

        private bool CheckAllFields()
        {
            if (!DepartmentInputDataContext.CheckAllFields())
                return false;
            if (departmentsDao.SearchByID(DepartmentInputDataContext.ID) != null)
            {
                DepartmentInputDataContext.ErrorMessage = Utils.invalidIDMess;
                return false;
            }
            return true;
        }

        public void ReceiveObject(Department department)
        {
            DepartmentInputDataContext.DeptIns = department;
        }

        public void ReceiveSubmitAction(Action<Department> submitObjectAction)
        {
            this.submitObjectAction = submitObjectAction;
        }
    }
}
