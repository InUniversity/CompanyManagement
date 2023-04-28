﻿using System;
using System.Windows;
using System.Windows.Input;
using CompanyManagement.Database;
using CompanyManagement.Services;
using CompanyManagement.Utilities;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.ViewModels.Dialogs
{
    public class AddEmployeeViewModel : BaseViewModel, IInputViewModel<Employee>
    {
        public ICommand AddEmployeeCommand { get; }

        public EmployeeInputViewModel EmployeeInputDataContext { get; }
        private Action<Employee> submitObjectAction;

        private EmployeesDao employeesDao = new EmployeesDao();

        public AddEmployeeViewModel()
        {
            EmployeeInputDataContext = new EmployeeInputViewModel();
            AddEmployeeCommand = new RelayCommand<Window>(AddCommand);
        }

        private void AddCommand(Window inputWindow)
        {
            EmployeeInputDataContext.TrimAllTexts();
            if (!CheckAllFields()) 
                return;
          
        }

        private bool CheckAllFields()
        {
            if (!EmployeeInputDataContext.CheckAllFields())
                return false;
            if (employeesDao.SearchByID(EmployeeInputDataContext.ID) != null)
            {
                EmployeeInputDataContext.ErrorMessage = Utils.EXIST_ID_MESSAGE;
                return false;
            }
            if (employeesDao.SearchByIdentifyCard(EmployeeInputDataContext.IdentifyCard) != null)
            {
                EmployeeInputDataContext.ErrorMessage = Utils.EXIST_IDENTIFY_CARD_MESSAGE;
                return false;
            }
            if (employeesDao.SearchByPhoneNumber(EmployeeInputDataContext.PhoneNumber) != null)
            {
                EmployeeInputDataContext.ErrorMessage = Utils.EXIST_PHONE_NUMBER_MESSAGE;
                return false;
            }
            return true;
        }

        public void ReceiveObject(Employee employee)
        {
            employee = EmployeeInputDataContext.EmployeeIns;
        }

        public void ReceiveSubmitAction(Action<Employee> submitObjectAction)
        {
            this.submitObjectAction = submitObjectAction;
        }
    }
}
