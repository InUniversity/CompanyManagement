using CompanyManagement.Database;
using CompanyManagement.Models;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CompanyManagement.ViewModels.UserControls
{
    public interface IOrganization
    {
        void MoveToEmployeesInDepartmentView(Department dempartment);
        void MoveToDepartmentsView();
    }
    public class OrganizationViewModel : BaseViewModel , IOrganization
    {
        private ContentControl currentChildView;
        public ContentControl CurrentChildView { get => currentChildView; set { currentChildView = value; OnPropertyChanged(); } }

        private DepartmentsUC departmentsView = new DepartmentsUC();
        private EmployeesUC allEmployeesView = new EmployeesUC();
        private EmployeesInDepartmentUC employeesInDepartmentView = new EmployeesInDepartmentUC();

        private bool statusDepartmentsView = false;
        public bool StatusDepartmentsView { get => statusDepartmentsView; set { statusDepartmentsView = value; OnPropertyChanged(); } }

        private bool statusEmployeesView = false;
        public bool StatusEmployeesView { get => statusEmployeesView; set { statusEmployeesView = value; OnPropertyChanged(); } }

        public ICommand ShowDepartmentsViewCommand { get; private set; }
        public ICommand ShowEmployeesViewCommand { get; private set; }

        private EmployeesDao employeesDao = new EmployeesDao();

        public OrganizationViewModel()
        {
            ExecuteShowDepartmentsView(null);
            SetCommand();
        }

        private void SetCommand()
        {
            ShowDepartmentsViewCommand = new RelayCommand<object>(ExecuteShowDepartmentsView);
            ShowEmployeesViewCommand = new RelayCommand<object>(ExecuteShowEmployeesView);
        }

        private void ExecuteShowEmployeesView(object obj)
        {
            CurrentChildView = allEmployeesView;
            StatusEmployeesView = true;
        }

        private void ExecuteShowDepartmentsView(object obj)
        {
            ((DepartmentsViewModel)departmentsView.DataContext).ParentDataContext = this;
            CurrentChildView = departmentsView;
            StatusDepartmentsView = true;
        }

        public void MoveToEmployeesInDepartmentView(Department dempartment)
        {
            ((EmployeesInDepartmentViewModel)employeesInDepartmentView.DataContext).Department = dempartment;
            ((EmployeesInDepartmentViewModel)employeesInDepartmentView.DataContext).ParentDataContext = this;
            ((EmployeesInDepartmentViewModel)employeesInDepartmentView.DataContext).LoadEmployees();
            CurrentChildView = employeesInDepartmentView;
        }

        public void MoveToDepartmentsView()
        {
            ExecuteShowDepartmentsView(null);
        }
    }
}
