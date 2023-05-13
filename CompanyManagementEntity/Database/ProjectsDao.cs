using CompanyManagementEntity.Utilities;
using System;
using System.Data.Entity;
using System.Linq;

namespace CompanyManagementEntity.Database
{
    public class ProjectsDao
    {
        public void Add(Project proj)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    db.Projects.Add(proj);
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
                    var proj = db.Projects.SingleOrDefault(p => p.ID == id);
                    if (proj == null) return;
                    db.Projects.Remove(proj);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
            }        
        }

        public void Update(Project proj)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    db.Entry(proj).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
            }        
        }

        public Project SearchByID(string id)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    return db.Projects.SingleOrDefault(p => p.ID == id);
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
