using CompanyManagementEntity.Database.Base;
using CompanyManagementEntity.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace CompanyManagementEntity.Database
{
    public class TaskCheckOutsDao : BaseDao<TaskCheckOut>
    {
        public List<TaskCheckOut> SearchByProjectID(string projID)
        {
            var listItems = new List<TaskCheckOut>();
            NewDbContext(db =>
            {
                var query = from tc in db.TaskCheckOuts
                            join t in db.Tasks on tc.TaskID equals t.ID
                            where t.ProjectID == projID
                            select tc;
                listItems = query.ToList();
            });
            return listItems;
        }
    }
}
