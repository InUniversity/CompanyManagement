using CompanyManagementEntity.Database.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagementEntity.Database
{
    public class TimeSheetsDao : BaseDao
    {
        public void Add(TimeSheet timeSheet)
        {
            using (var db = new CompanyManagementContext())
            {
                db.TimeSheets.Add(timeSheet);
                db.SaveChanges();
            }
        }

        public void Delete(string id)
        {
            using (var db = new CompanyManagementContext())
            {
                var item = db.TimeSheets.SingleOrDefault(i => i.ID == id);
                if (item == null) return;
                db.TimeSheets.Remove(item);
                db.SaveChanges();
            }
        }

        public void Update(TimeSheet timeSheet)
        {
            using (var db = new CompanyManagementContext())
            {
                db.Entry(timeSheet).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public List<TimeSheet> GetAll()
        {
            using (var db = new CompanyManagementContext())
            {
                return db.TimeSheets.ToList();
            }
        }

        public TimeSheet SearchByID(string id)
        {
            using (var db = new CompanyManagementContext())
            {
                return db.TimeSheets.SingleOrDefault(t => t.ID == id);
            }
        }

        public List<TimeSheet> SearchByEmployeeID(string employeeID)
        {
            using (var db = new CompanyManagementContext())
            {
                var query = from t in db.TimeSheets where t.EmployeeID != employeeID select t;
                return query.ToList();
            }
        }

        public int ToTalWorksDayByEmployeeID(string employeeID)
        {
            using (var db = new CompanyManagementContext())
            {
                var query = from t in db.TimeSheets where t.EmployeeID != employeeID select t;
                return query.Count();
            }
        }
    }
}
