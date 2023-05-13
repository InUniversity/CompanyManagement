using CompanyManagement.Enums;
using CompanyManagementEntity.Database.Base;
using CompanyManagementEntity.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyManagementEntity.Database
{
    public class TasksDao : BaseDao<Task>
    {
        public void Delete(string id)
        {
            NewDbContext(db =>
            {
                var tsk = db.Tasks.SingleOrDefault(t => t.ID == id);
                if (tsk == null) return;
                db.Tasks.Remove(tsk);
                db.SaveChanges();
            });
        }

        public Task SearchByID(string id)
        {
            var item = new Task();
            NewDbContext(db =>
            {
                item = db.Tasks.SingleOrDefault(t => t.ID == id);
            });
            return item;
        }

        public List<Task> SearchByProjectID(string projID)
        {
            var listItems = new List<Task>();
            NewDbContext(db =>
            {
                var query = from t in db.Tasks where t.ProjectID == projID select t;
                listItems = query.ToList();
            });
            return listItems;
        }

        public List<Task> SearchByEmployeeID(string projID, string emplID)
        {
            var listItems = new List<Task>();
            NewDbContext(db =>
            {
                var query = from t in db.Tasks where t.ProjectID == projID && t.EmployeeID == emplID select t;
                listItems = query.ToList();
            });
            return listItems;
        }

        public List<Task> SearchTasksCheckOut(string emplID, DateTime curDate)
        {
            var listItems = new List<Task>();
            NewDbContext(db =>
            {
                var query = from t in db.Tasks
                            where t.StartDate <= curDate
                            && t.EmployeeID == emplID
                            && t.StatusID != (int)ETaskStatus.Completed
                            select t;
                listItems = query.ToList();
            });
            return listItems;
        }
    }
}
