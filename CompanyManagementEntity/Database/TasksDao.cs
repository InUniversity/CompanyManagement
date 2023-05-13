using CompanyManagement.Enums;
using CompanyManagementEntity.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyManagementEntity.Database
{
    public class TasksDao 
    {
        public void Add(Task tsk)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    db.Tasks.Add(tsk);
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
                    var tsk = db.Tasks.SingleOrDefault(t => t.ID == id);
                    if (tsk == null) return;
                    db.Tasks.Remove(tsk);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
            }          
        }

        public void Update(Task tsk)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    db.Entry(tsk).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
            }          
        }

        public Task SearchByID(string id)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    return db.Tasks.SingleOrDefault(t => t.ID == id);
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
                return null;
            }     
        }

        public List<Task> SearchByProjectID(string projID)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    var query = from t in db.Tasks where t.ProjectID == projID select t;
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
                return null;
            }        
        }

        public List<Task> SearchByEmployeeID(string projID, string emplID)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    var query = from t in db.Tasks where t.ProjectID == projID && t.EmployeeID == emplID select t;
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
                return null;
            }         
        }

        public List<Task> SearchTasksCheckOut(string emplID, DateTime curDate)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    var query = from t in db.Tasks
                                where t.StartDate <= curDate
                                && t.EmployeeID == emplID
                                && t.StatusID != (int)ETaskStatus.Completed
                                select t;
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
