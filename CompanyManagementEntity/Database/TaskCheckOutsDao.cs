using CompanyManagementEntity.Database.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagementEntity.Database
{
    public class TaskCheckOutsDao : BaseDao
    {
        public void Add(TaskCheckOut tskCheckOut)
        {
            using (var db = new CompanyManagementContext())
            {
                db.TaskCheckOuts.Add(tskCheckOut);
                db.SaveChanges();
            }
        }

        public List<TaskCheckOut> SearchByProjectID(string projID)
        {
            using (var db = new CompanyManagementContext())
            {
                var query = from tc in db.TaskCheckOuts
                            join t in db.Tasks on tc.TaskID equals t.ID
                            where t.ProjectID == projID
                            select tc;
                return query.ToList();
            }
        }
    }
}
