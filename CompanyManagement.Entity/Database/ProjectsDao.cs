using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Entity.Database
{
    public class ProjectsDao : BaseDao
    {
        public void Add(Project proj)
        {
            using (var db = new CompanyManagementContext())
            {
                db.Projects.Add(proj);
                db.SaveChanges();
            }
        }

        public void Delete(string id)
        {
            using (var db = new CompanyManagementContext())
            {
                var proj = db.Projects.SingleOrDefault(p => p.ID == id);
                if (proj == null) return;
                db.Projects.Remove(proj);
                db.SaveChanges();
            }
        }

        public void Update(Project proj)
        {
            using (var db = new CompanyManagementContext())
            {
                db.Entry(proj).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public Project SearchByID(string id)
        {
            using (var db = new CompanyManagementContext())
            {
                return db.Projects.SingleOrDefault(p => p.ID == id);
            }
        }
    }
}
