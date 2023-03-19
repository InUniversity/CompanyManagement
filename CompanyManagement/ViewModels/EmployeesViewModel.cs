using System.Collections.ObjectModel;
using CompanyManagement.Database;
using System.Windows.Input;
using CompanyManagement.Dialogs;

namespace CompanyManagement.ViewModels
{
    public class EmployeesViewModel : BaseViewModel, IEmployees
    {

        private ObservableCollection<Employee> employees;
        public ObservableCollection<Employee> Employees { get => employees; set { employees = value; OnPropertyChanged(); } }

        public ICommand OpenInputDialogCommand { get; set; }
        public ICommand DeleteEmployeeCommand { get; set; }
        public ICommand UpdateEmployeeCommand { get; set; }

        private EmployeeDao employeeDao = new EmployeeDao();

        public EmployeesViewModel()
        {
            LoadEmployees();
            SetCommands();
        }

        private void LoadEmployees()
        {
            Employees = new ObservableCollection<Employee>(employeeDao.GetAll());
        }

        private void SetCommands()
        {
            OpenInputDialogCommand = new RelayCommand<object>(OpenEmployeeInputDialog);
            DeleteEmployeeCommand = new RelayCommand<string>(ExecuteDeleteCommand);
            UpdateEmployeeCommand = new RelayCommand<Employee>(ExecuteUpdateCommand);
        }

        private void OpenEmployeeInputDialog(object p)
        {
            AddEmployeeDialog addEmployeeDialog = new AddEmployeeDialog();
            AddEmployeeViewModel addEmployeeVM = (AddEmployeeViewModel)addEmployeeDialog.DataContext;
            addEmployeeVM.ParentDataContext = this;
            addEmployeeDialog.ShowDialog();
        }

        private void ExecuteDeleteCommand(string id)
        {
            employeeDao.Delete(id);
            LoadEmployees();
        }

        private void ExecuteUpdateCommand(Employee employee)
        {
            UpdateEmployeeDialog updateEmployeeDialog = new UpdateEmployeeDialog();
            UpdateEmployeeViewModel updateEmployeeVM = (UpdateEmployeeViewModel)updateEmployeeDialog.DataContext;
            updateEmployeeVM.ParentDataContext = this;
            updateEmployeeVM.EmployeeInputDataContext.Retrieve(employee);
            updateEmployeeDialog.ShowDialog();
        }

        public void Add(Employee employee)
        {
            employeeDao.Add(employee);
            LoadEmployees();
        }

        public void Update(Employee employee)
        {
            employeeDao.Update(employee);
            LoadEmployees();
        }
    }

    public interface IEmployees
    {
        void Add(Employee employee);
        void Update(Employee employee);                                
    }
}
