using CompanyManagementEntity.Database.Base;
using System.Collections.Generic;
using System.Linq;

namespace CompanyManagementEntity.Database
{
    public class MilestonesDao : BaseDao<Milestone>
    {
        public void Delete(string id) 
        {
            NewDbContext(db =>
            {
                var mile = db.Milestones.SingleOrDefault(m => m.ID == id);
                if (mile == null) return;
                db.Milestones.Remove(mile);
                db.SaveChanges();
            });
        }

        public Milestone SearchByID(string id)
        {
            var item = new Milestone();
            NewDbContext(db =>
            {
                item = db.Milestones.SingleOrDefault(m => m.ID == id);
            });
            return item;
        }
        public List<Milestone> SearchByProjectID(string projID)
        {
            var listItems = new List<Milestone>();
            NewDbContext(db =>
            {
                var query = from m in db.Milestones where m.ProjectID == projID select m;
                listItems = query.ToList();
            });
            return listItems;
        }
    }
}
