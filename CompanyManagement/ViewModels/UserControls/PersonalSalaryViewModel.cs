using CompanyManagement.Database;
using CompanyManagement.Models;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.ViewModels.Dialogs;
using CompanyManagement.Views.Dialogs;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace CompanyManagement.ViewModels.UserControls
{
    public class PersonalSalaryViewModel : BaseViewModel
    {

        private List<SalaryRecord> salaryRecords = new List<SalaryRecord>();
        public List<SalaryRecord> SalaryRecords { get => salaryRecords; set { salaryRecords = value; OnPropertyChanged(); } }

        public ICommand OpenSalaryDetailsDialogCommand { get; private set; }

        private SalaryRecordsDao salaryRecordsDao = new SalaryRecordsDao();

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
            SalaryRecords = salaryRecordsDao.GetByEmployeeID(CurrentUser.Ins.Empl.ID).OrderByDescending(p => p.MonthYear).ToList();
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
