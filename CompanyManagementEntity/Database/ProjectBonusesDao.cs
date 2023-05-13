using CompanyManagementEntity.Database.Base;
using System.Collections.Generic;
using System.Linq;

namespace CompanyManagementEntity.Database
{
    public class ProjectBonusesDao : BaseDao<ProjectBonus>
    {
        public void Delete(string id)
        {
            NewDbContext(db =>
            {
                var item = db.ProjectBonuses.SingleOrDefault(pb => pb.ID == id);
                if (item == null) return;
                db.ProjectBonuses.Remove(item);
                db.SaveChanges();
            });
        }

        public void DeleteByProjectID(string id)
        {
            NewDbContext(db =>
            {
                var item = db.ProjectBonuses.SingleOrDefault(pb => pb.ProjectID == id);
                if (item == null) return;
                db.ProjectBonuses.Remove(item);
                db.SaveChanges();
            });
        }

        public ProjectBonus SearchByID(string id)
        {
            var item = new ProjectBonus();
            NewDbContext(db =>
            {
                item = db.ProjectBonuses.SingleOrDefault(pb => pb.ID == id);
            });
            return item;
        }

        public List<ProjectBonus> SearchByProjectID(string id)
        {
            var listItems = new List<ProjectBonus>();
            NewDbContext(db =>
            {
                var query = from pb in db.ProjectBonuses where pb.ProjectID == id select pb;
                listItems = query.ToList();
            });
            return listItems;
        }

        public List<ProjectBonus> SearchByEmployeeID(string id)
        {
            var listItems = new List<ProjectBonus>();
            NewDbContext(db =>
            {
                var query = from pb in db.ProjectBonuses where pb.EmployeeID == id select pb;
                listItems = query.ToList();
            });
            return listItems;
        }

        public decimal ToTalBonusesOfEmployeeByTime(string employeeID, int month, int year)
        {
            var result = new decimal(0);
            NewDbContext(db =>
            {
                var query = from pb in db.ProjectBonuses
                            where pb.EmployeeID == employeeID
                            && pb.ReceivedDate.Value.Month == month
                            && pb.ReceivedDate.Value.Year == year
                            select pb;
                result = query.Sum(pb => pb.Amount).Value;
            });
            return result;
        }

        public List<ProjectBonus> GetOfEmployeeByTime(string employeeID, int month, int year)
        {
            var listItems = new List<ProjectBonus>();
            NewDbContext(db =>
            {
                var query = from pb in db.ProjectBonuses
                            where pb.EmployeeID == employeeID
                            && pb.ReceivedDate.Value.Month == month
                            && pb.ReceivedDate.Value.Year == year
                            select pb;
                listItems = query.ToList();
            });
            return listItems;
        }
    }
}
