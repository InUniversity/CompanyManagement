using CompanyManagementEntity.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyManagementEntity.Database
{
    public class TaskCheckOutsDao
    {
        public void Add(TaskCheckOut tskCheckOut)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    db.TaskCheckOuts.Add(tskCheckOut);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
            }       
        }

        public List<TaskCheckOut> SearchByProjectID(string projID)
        {
            try
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
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
                return null;
            }       
        }
    }
}
