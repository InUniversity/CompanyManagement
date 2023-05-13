using CompanyManagementEntity.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace CompanyManagementEntity.Database
{
    public class SalaryRecordsDao 
    {
        public void Add(SalaryRecord salaryRecord)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    db.SalaryRecords.Add(salaryRecord);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
            }           
        }

        public void Delete(string id)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    var salary = db.SalaryRecords.SingleOrDefault(s => s.ID == id);
                    if (salary == null) return;
                    db.SalaryRecords.Remove(salary);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
            }        
        }

        public void DeleteByEmployee(string employeeID, DateTime monthYear)
        {
            try
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
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
            }        
        }

        public void DeleteByMonthYear(int month, int year)
        {
            try
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
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
            }        
        }

        public SalaryRecord SearchByID(string id)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    return db.SalaryRecords.SingleOrDefault(s => s.ID == id);
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
                return null;
            }         
        }

        public List<SalaryRecord> GetByTime(int month, int year)
        {
            try
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
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
                return null;
            }   
        }

        public List<SalaryRecord> GetByDepartmentID(string departmentID, int month, int year)
        {
            try
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
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
                return null;
            }        
        }

        public List<SalaryRecord> GetByEmployeeID(string employeeID)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    var query = from s in db.SalaryRecords
                                where s.EmployeeID == employeeID
                                select s;
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
                return null;
            }         
        }
    }
}
