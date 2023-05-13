using CompanyManagementEntity.Database.Base;
using CompanyManagementEntity.Utilities;
using System;
using System.Data.Entity;
using System.Linq;

namespace CompanyManagementEntity.Database
{
    public class ProjectsDao : BaseDao<Project>
    {
        public void Delete(string id)
        {
            NewDbContext(db =>
            {
                var proj = db.Projects.SingleOrDefault(p => p.ID == id);
                if (proj == null) return;
                db.Projects.Remove(proj);
                db.SaveChanges();
            });
        }

        public Project SearchByID(string id)
        {
            var item = new Project();
            NewDbContext(db =>
            {
                item = db.Projects.SingleOrDefault(p => p.ID == id);
            });
            return item;
        }
    }
}
