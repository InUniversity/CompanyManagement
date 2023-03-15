﻿using CompanyManagement.Database;
using System.Windows.Input;
using System.Windows;

namespace CompanyManagement.ViewModels
{
    class UpdateEmployeeViewModel : BaseViewModel
    {

        public ICommand UpdateEmployeeCommand { get; set; }

        public EmployeesViewModel ParentDataContext { get; set; }
        public EmployeeInputViewModel EmployeeInputDataContext { get; set; }

        private PositionDao positionDao = new PositionDao();
        private DepartmentDao departmentDao = new DepartmentDao();

        public UpdateEmployeeViewModel()
        {
            EmployeeInputDataContext = new EmployeeInputViewModel();
            UpdateEmployeeCommand = new RelayCommand<Window>(UpdateCommand, p => CheckAllFiles());
        }

        private void UpdateCommand(Window inputWindow)
        {
            EmployeeInputDataContext.TrimAllTexts();
            Employee empl = EmployeeInputDataContext.CreateEmployeeInstance();
            ParentDataContext.Update(empl);
            EmployeeInputDataContext.ClearAllTexts();
            inputWindow.Close();
        }

        private bool CheckAllFiles()
        {
            if (!EmployeeInputDataContext.CheckAllFields())
                return false;
            return true;
        }
    }
}
