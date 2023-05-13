using CompanyManagementEntity.Database.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagementEntity.Database
{
    public class TasksDao : BaseDao
    {
        public void Add(Task tsk)
        {
            using (var db = new CompanyManagementContext())
            {
                db.Tasks.Add(tsk);
                db.SaveChanges();
            }
        }

        public void Delete(string id)
        {
            using (var db = new CompanyManagementContext())
            {
                var tsk = db.Tasks.SingleOrDefault(t => t.ID == id);
                if (tsk == null) return;
                db.Tasks.Remove(tsk);
                db.SaveChanges();
            }
        }

        public void Update(Task tsk)
        {
            using (var db = new CompanyManagementContext())
            {
                db.Entry(tsk).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public Task SearchByID(string id)
        {
            using (var db = new CompanyManagementContext())
            {
                return db.Tasks.SingleOrDefault(t => t.ID == id);
            }
        }

        public List<Task> SearchByProjectID(string projID)
        {
            using (var db = new CompanyManagementContext())
            {
                var query = from t in db.Tasks where t.ProjectID == projID select t;
                return query.ToList();
            }
        }

        public List<Task> SearchByEmployeeID(string projID, string emplID)
        {
            using (var db = new CompanyManagementContext())
            {
                var query = from t in db.Tasks where t.ProjectID == projID && t.EmployeeID == emplID select t;
                return query.ToList();
            }
        }

        public List<Task> SearchTasksCheckOut(string emplID, DateTime curDate)
        {
            using (var db = new CompanyManagementContext())
            {
                var query = from t in db.Tasks
                            where t.StartDate <= curDate
                            && t.EmployeeID == emplID
                            && t.Progress != completed
                            select t;
                return query.ToList();
            }
        }
    }
}
