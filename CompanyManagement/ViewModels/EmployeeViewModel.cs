using System.Windows.Input;

namespace CompanyManagement.ViewModels
{
    public class EmployeeViewModel : BaseViewModel
    {

        private ICommand addEmployee;
        public ICommand AddEmployee
        {
            get { return addEmployee; }
            set 
            { 
                addEmployee = value; 
                OnPropertyChanged(nameof(AddEmployee));
            }
        }

    }
}
