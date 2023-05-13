using CompanyManagementEntity.Database.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagementEntity.Database
{
    public class SalaryRecordsDao : BaseDao
    {

        public void Add(SalaryRecord salaryRecord)
        {
            using (var db = new CompanyManagementContext())
            {
                db.SalaryRecords.Add(salaryRecord);
                db.SaveChanges();
            }
        }

        public void Delete(string id)
        {
            using (var db = new CompanyManagementContext())
            {
                var salary = db.SalaryRecords.SingleOrDefault(s => s.ID == id);
                if (salary == null) return;
                db.SalaryRecords.Remove(salary);
                db.SaveChanges();
            }
        }

        public void DeleteByEmployee(string employeeID, DateTime monthYear)
        {
            using (var db = new CompanyManagementContext())
            {
                var salary = db.SalaryRecords.
                            SingleOrDefault(s => s.EmployeeID == employeeID
                            && s.MonthYear == monthYear);
                if (salary == null) return;
                db.SalaryRecords.Remove(salary);
                db.SaveChanges();
            }
        }

        public void DeleteByMonthYear(int month, int year)
        {
            using (var db = new CompanyManagementContext())
            {
                var salary = db.SalaryRecords.
                            SingleOrDefault(s => s.MonthYear.Value.Month == month
                            && s.MonthYear.Value.Year == year);
                if (salary == null) return;
                db.SalaryRecords.Remove(salary);
                db.SaveChanges();
            }
        }

        public SalaryRecord SearchByID(string id)
        {
            using (var db = new CompanyManagementContext())
            {
                return db.SalaryRecords.SingleOrDefault(s => s.ID == id);
            }
        }

        public List<SalaryRecord> GetByTime(int month, int year)
        {
            using (var db = new CompanyManagementContext())
            {
                var query = from s in db.SalaryRecords
                            where s.MonthYear.Value.Month == month
                            && s.MonthYear.Value.Year == year
                            select s;
                return query.ToList();
            }
        }

        public List<SalaryRecord> GetByDepartmentID(string departmentID, int month, int year)
        {
            using (var db = new CompanyManagementContext())
            {
                var query = from s in db.SalaryRecords
                            join e in db.Employees on s.EmployeeID equals e.ID
                            where s.MonthYear.Value.Month == month
                            && s.MonthYear.Value.Year == year
                            && e.DepartmentID == departmentID
                            || departmentID == "MNG"
                            || departmentID == "ALL"
                            select s;
                return query.ToList();
            }
        }

        public List<SalaryRecord> GetByEmployeeID(string employeeID)
        {
            using (var db = new CompanyManagementContext())
            {
                var query = from s in db.SalaryRecords
                            where s.EmployeeID == employeeID
                            select s;
                return query.ToList();
            }
        }
    }
}
