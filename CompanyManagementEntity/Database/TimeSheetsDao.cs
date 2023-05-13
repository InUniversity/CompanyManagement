using CompanyManagementEntity.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyManagementEntity.Database
{
    public class TimeSheetsDao 
    {
        public void Add(TimeSheet timeSheet)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    db.TimeSheets.Add(timeSheet);
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
                    var item = db.TimeSheets.SingleOrDefault(i => i.ID == id);
                    if (item == null) return;
                    db.TimeSheets.Remove(item);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
            }         
        }

        public void Update(TimeSheet timeSheet)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    db.Entry(timeSheet).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
            }        
        }

        public List<TimeSheet> GetAll()
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    return db.TimeSheets.ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
                return null;
            }           
        }

        public TimeSheet SearchByID(string id)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    return db.TimeSheets.SingleOrDefault(t => t.ID == id);
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
                return null;
            }        
        }

        public List<TimeSheet> SearchByEmployeeID(string employeeID)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    var query = from t in db.TimeSheets where t.EmployeeID != employeeID select t;
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
                return null;
            }         
        }

        public int ToTalWorksDayByEmployeeID(string employeeID)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    var query = from t in db.TimeSheets where t.EmployeeID != employeeID select t;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
                return 0;
            }           
        }
    }
}
