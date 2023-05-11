using CompanyManagement.Database;
using CompanyManagement.Database.Base;
using CompanyManagement.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CompanyManagement.Models
{
    public class SalaryRecord
    {
        private string id = "";
        private string employeeID = "";
        private DateTime monthYear = Utils.emptyDate;
        private int totalWorkDays = 0;
        private int totalOffDays = 0;
        private decimal totalBonuses = 0;
        private decimal income = 0;
        private Employee worker = new Employee();
        private Role workerRole = new Role();
        private Department wokerDept = new Department();

        public string ID
        {
            get => id;
            set => id = value;
        }

        public string EmployeeID
        {
            get => employeeID;
            set => employeeID = value;
        }

        public DateTime MonthYear
        {
            get => monthYear;
            set => monthYear = value;
        }

        public int TotalWorkDays
        {
            get => totalWorkDays;
            set => totalWorkDays = value;
        }

        public int TotalOffDays
        {
            get { 
                if(monthYear != Utils.emptyDate)
                    return DateTime.DaysInMonth(monthYear.Year, monthYear.Month) - totalWorkDays;
                return 0;
            }
        }

        public decimal TotalBonuses
        {
            get => Decimal.Round(totalBonuses, 0);
            set => totalBonuses = value;
        }

        public decimal Income
        {
            get => Decimal.Round(income, 0);
            set => income = value;
        }

        public Employee Worker
        {
            get => worker;
            set => worker = value;
        }
        
        public Role WorkerRole
        {
            get => workerRole;
            set => workerRole = value;
        }

        public Department WorkerDept
        {
            get => wokerDept;
            set => wokerDept = value;
        }

        public SalaryRecord() { }

        public SalaryRecord(string id, string employeeID, DateTime monthYear, int totalWorkDays, decimal totalBonuses, decimal income)
        {
            this.id = id;
            this.employeeID = employeeID;
            this.monthYear = monthYear;
            this.totalWorkDays = totalWorkDays;
            this.totalBonuses = totalBonuses;
            this.income = income;
        }

        public SalaryRecord(IDataRecord record)
        {
            try
            {
                id = Utils.GetString(record, BaseDao.salaryID);
                employeeID = Utils.GetString(record, BaseDao.salaryEmplID);
                monthYear = Utils.GetDateTime(record, BaseDao.salaryMonthYear);
                totalWorkDays = Utils.GetInt(record, BaseDao.salaryWorkdays);
                totalBonuses = Utils.GetDecimal(record, BaseDao.salaryBonus);
                income = Utils.GetDecimal(record, BaseDao.salaryIncome);
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(SalaryRecord), "CAST ERROR: " + ex.Message);
            }
        }

    }
}
