using System;
using CompanyManagement.Database;
using CompanyManagement.Models;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.Views.UserControls;
using System.Windows.Controls;
using System.Windows.Input;
using CompanyManagement.Strategies.UserControls.DeptsView;
using CompanyManagement.Utilities;
using System.Windows;

namespace CompanyManagement.ViewModels.UserControls
{
    public interface IOrganization
    {
        void MoveToEmployeesInDepartmentView(Department department);
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

        private Visibility visibilityEmployees;
        public Visibility VisibilityEmployees { get => visibilityEmployees; set { visibilityEmployees = value; OnPropertyChanged(); } }

        public ICommand ShowDepartmentsViewCommand { get; private set; }
        public ICommand ShowEmployeesViewCommand { get; private set; }

        private EmployeesDao employeesDao = new EmployeesDao();

        public OrganizationViewModel()
        {
            InitDeptStrategy();
            ExecuteShowDepartmentsView(null);
            SetCommand();
            SetVisibilityViewEmployees();
        }

        private void SetVisibilityViewEmployees()
        {
            VisibilityEmployees = CurrentUser.Ins.EmployeeIns.EmplRole.Perms == Enums.EPermission.HR ? Visibility.Visible : Visibility.Collapsed;
        }    

        private void InitDeptStrategy()
        {
            try
            {
                var curEmpl = CurrentUser.Ins.EmployeeIns;
                var deptStrategy = DeptStrategyFactory.Create(curEmpl.EmplRole.Perms);
                var viewModel = new DepartmentsViewModel(deptStrategy)
                {
                    ParentDataContext = this
                };
                departmentsView.DataContext = viewModel;
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(OrganizationViewModel), ex.Message);
            }
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
            CurrentChildView = departmentsView;
            StatusDepartmentsView = true;
        }

        public void MoveToEmployeesInDepartmentView(Department department)
        {
            ((EmployeesInDepartmentViewModel)employeesInDepartmentView.DataContext).Department = department;
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
