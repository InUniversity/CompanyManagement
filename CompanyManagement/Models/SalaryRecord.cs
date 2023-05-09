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

namespace CompanyManagement.Models
{
    public class SalaryRecord
    {
        private string id;
        private string employeeID;
        private DateTime monthYear;
        private int totalWorkDays;
        private int totalOffDays;
        private decimal totalBonuses;
        private decimal income;
        private Employee worker;

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
            get => totalOffDays;
            set => totalOffDays = value;
        }

        public decimal TotalBonuses
        {
            get => totalBonuses;
            set => totalBonuses = Decimal.Round(value,0);
        }

        public decimal Income
        {
            get => income;
            set => income = Decimal.Round(value, 0);
        }

        public Employee Worker
        {
            get => worker;
            set => worker = value;
        }

        public Role WorkerRole
        {
            get => (new RolesDao()).SearchByID(Worker.RoleID);
        }

        public Department WorkerDepartment
        {
            get => (new DepartmentsDao()).SearchByID(Worker.DepartmentID)??(new Department("", "Management", ""));
        }

        public SalaryRecord() { }

        public SalaryRecord(string id, string employeeID, DateTime monthYear, int totalWorkDays, int totalOffDays, decimal totalBonuses, decimal income)
        {
            this.id = id;
            this.employeeID = employeeID;
            this.monthYear = monthYear;
            this.totalWorkDays = totalWorkDays;
            this.totalOffDays = totalOffDays;
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
                Log.Instance.Error(nameof(Milestone), "CAST ERROR: " + ex.Message);
            }
        }

    }
}
