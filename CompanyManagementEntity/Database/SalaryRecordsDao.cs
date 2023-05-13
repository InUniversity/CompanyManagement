using CompanyManagementEntity.Database.Base;
using CompanyManagementEntity.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace CompanyManagementEntity.Database
{
    public class SalaryRecordsDao : BaseDao<SalaryRecord>
    {
        public void Delete(string id)
        {
            NewDbContext(db =>
            {
                var salary = db.SalaryRecords.SingleOrDefault(s => s.ID == id);
                if (salary == null) return;
                db.SalaryRecords.Remove(salary);
                db.SaveChanges();
            });
        }

        public void DeleteByEmployee(string employeeID, DateTime monthYear)
        {
            NewDbContext(db =>
            {
                var salary = db.SalaryRecords.
                               SingleOrDefault(s => s.EmployeeID == employeeID
                               && s.MonthYear == monthYear);
                if (salary == null) return;
                db.SalaryRecords.Remove(salary);
                db.SaveChanges();
            });
        }

        public void DeleteByMonthYear(int month, int year)
        {
            NewDbContext(db =>
            {
                var salary = db.SalaryRecords.
                                SingleOrDefault(s => s.MonthYear.Value.Month == month
                                && s.MonthYear.Value.Year == year);
                if (salary == null) return;
                db.SalaryRecords.Remove(salary);
                db.SaveChanges();
            });
        }

        public SalaryRecord SearchByID(string id)
        {
            var item = new SalaryRecord();
            NewDbContext(db =>
            {
                item = db.SalaryRecords.SingleOrDefault(s => s.ID == id);
            });
            return item;
        }

        public List<SalaryRecord> GetByTime(int month, int year)
        {
            var listItems = new List<SalaryRecord>();
            NewDbContext(db =>
            {
                var query = from s in db.SalaryRecords
                            where s.MonthYear.Value.Month == month
                            && s.MonthYear.Value.Year == year
                            select s;
                listItems = query.ToList();
            });
            return listItems;
        }

        public List<SalaryRecord> GetByDepartmentID(string departmentID, int month, int year)
        {
            var listItems = new List<SalaryRecord>();
            NewDbContext(db =>
            {
                var query = from s in db.SalaryRecords
                            join e in db.Employees on s.EmployeeID equals e.ID
                            where s.MonthYear.Value.Month == month
                            && s.MonthYear.Value.Year == year
                            && e.DepartmentID == departmentID
                            || departmentID == "MNG"
                            || departmentID == "ALL"
                            select s;
                listItems = query.ToList();
            });
            return listItems;
        }

        public List<SalaryRecord> GetByEmployeeID(string employeeID)
        {
            var listItems = new List<SalaryRecord>();
            NewDbContext(db =>
            {
                var query = from s in db.SalaryRecords
                            where s.EmployeeID == employeeID
                            select s;
                listItems = query.ToList();
            });
            return listItems;
        }
    }
}
