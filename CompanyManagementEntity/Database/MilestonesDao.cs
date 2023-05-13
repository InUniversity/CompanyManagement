using CompanyManagementEntity.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagementEntity.Database
{
    public class MilestonesDao 
    {
        public void Add(Milestone mile)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    db.Milestones.Add(mile);
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
                    var mile = db.Milestones.SingleOrDefault(m => m.ID == id);
                    if (mile == null) return;
                    db.Milestones.Remove(mile);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
            }
        }

        public void Update(Milestone mile)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    db.Entry(mile).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
            }           
        }

        public Milestone SearchByID(string id)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    return db.Milestones.SingleOrDefault(m => m.ID == id);
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
                return null;
            }
        }
        public List<Milestone> SearchByProjectID(string projID)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    var query = from m in db.Milestones where m.ProjectID == projID select m;
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
