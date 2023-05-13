using CompanyManagementEntity.Database.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagementEntity.Database
{
    public class MilestonesDao : BaseDao
    {
        public void Add(Milestone mile)
        {
            using (var db = new CompanyManagementContext())
            {
                db.Milestones.Add(mile);
                db.SaveChanges();
            }
        }

        public void Delete(string id)
        {
            using (var db = new CompanyManagementContext())
            {
                var mile = db.Milestones.SingleOrDefault(m => m.ID == id);
                if (mile == null) return;
                db.Milestones.Remove(mile);
                db.SaveChanges();
            }
        }

        public void Update(Milestone mile)
        {
            using (var db = new CompanyManagementContext())
            {
                db.Entry(mile).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public Milestone SearchByID(string id)
        {
            using (var db = new CompanyManagementContext())
            {
                return db.Milestones.SingleOrDefault(m => m.ID == id);
            }
        }
        public List<Milestone> SearchByProjectID(string projID)
        {
            using (var db = new CompanyManagementContext())
            {
                var query = from m in db.Milestones where m.ProjectID == projID select m;
                return query.ToList();
            }
        }
    }
}
