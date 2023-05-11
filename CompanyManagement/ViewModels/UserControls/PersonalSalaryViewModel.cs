using CompanyManagement.Database;
using CompanyManagement.Models;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.ViewModels.Dialogs;
using CompanyManagement.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CompanyManagement.ViewModels.UserControls
{
    public class PersonalSalaryViewModel : BaseViewModel
    {

        private List<SalaryRecord> salaryRecords = new List<SalaryRecord>();
        public List<SalaryRecord> SalaryRecords { get => salaryRecords; set { salaryRecords = value; OnPropertyChanged(); } }

        public ICommand OpenSalaryDetailsDialogCommand { get; private set; }

        private SalaryRecordsDao salaryRecordsDao = new SalaryRecordsDao();
        private EmployeesDao employeesDao = new EmployeesDao();
        private RolesDao rolesDao = new RolesDao();
        private DepartmentsDao departmentsDao = new DepartmentsDao();

        public PersonalSalaryViewModel()
        {
            SetCommand();
            LoadSalaryRecords();
        }

        private void SetCommand()
        {
            OpenSalaryDetailsDialogCommand = new RelayCommand<SalaryRecord>(ExecuteOpenSalaryDetailsDialog);
        }

        private void LoadSalaryRecords()
        {
            var tempSalaryRecords = salaryRecordsDao.GetByEmployeeID(CurrentUser.Ins.EmployeeIns.ID)
                .OrderByDescending(p => p.MonthYear).ToList();
            FillValue(tempSalaryRecords);
            SalaryRecords = new List<SalaryRecord>(tempSalaryRecords);
        }

        private void FillValue(List<SalaryRecord> list)
        {
            foreach(SalaryRecord salaryRecord in list)
            {
                salaryRecord.Worker = employeesDao.SearchByID(salaryRecord.EmployeeID);
                salaryRecord.WorkerRole = rolesDao.SearchByID(salaryRecord.Worker.RoleID);
                salaryRecord.WorkerDept = departmentsDao.SearchByID(salaryRecord.Worker.DepartmentID);
            }    
        }

        private void ExecuteOpenSalaryDetailsDialog(SalaryRecord salaryRecord)
        {
            SalaryDetailsDialog salaryDetailDiaLog = new SalaryDetailsDialog();
            SalaryDetailViewModel salaryDetailViewModel = new SalaryDetailViewModel();
            salaryDetailViewModel.salaryRecordIns = salaryRecord;
            salaryDetailDiaLog.DataContext = salaryDetailViewModel;
            salaryDetailDiaLog.Show();
        }
    }
}
