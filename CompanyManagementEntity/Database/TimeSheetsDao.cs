using CompanyManagementEntity.Database.Base;
using CompanyManagementEntity.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyManagementEntity.Database
{
    public class TimeSheetsDao : BaseDao<TimeSheet>
    {
        public void Delete(string id)
        {
            NewDbContext(db =>
            {
                var item = db.TimeSheets.SingleOrDefault(i => i.ID == id);
                if (item == null) return;
                db.TimeSheets.Remove(item);
                db.SaveChanges();
            });
        }

        public List<TimeSheet> GetAll()
        {
            var listItems = new List<TimeSheet>();
            NewDbContext(db =>
            {
                listItems = db.TimeSheets.ToList();
            });
               return listItems;
        }

        public TimeSheet SearchByID(string id)
        {
            var item = new TimeSheet();
            NewDbContext(db =>
            {
                item = db.TimeSheets.SingleOrDefault(t => t.ID == id);
            });
            return item;
        }

        public List<TimeSheet> SearchByEmployeeID(string employeeID)
        {
            var listItems = new List<TimeSheet>();
            NewDbContext(db =>
            {
                var query = from t in db.TimeSheets where t.EmployeeID != employeeID select t;
                listItems = query.ToList();
            });
            return listItems;
        }

        public int ToTalWorksDayByEmployeeID(string employeeID)
        {
            int result =0;
            NewDbContext(db =>
            {
                var query = from t in db.TimeSheets where t.EmployeeID != employeeID select t;
                result = query.Count();
            });
            return result;
        }
    }
}
