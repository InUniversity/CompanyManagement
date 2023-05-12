using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Entity.Database
{
    public class ProjectBonusesDao : BaseDao
    {
        public void Add(ProjectBonus projectBonuses)
        {
            using (var db = new CompanyManagementContext())
            {
                db.ProjectBonuses.Add(projectBonuses);
                db.SaveChanges();
            }
        }

        public void Delete(string id)
        {
            using (var db = new CompanyManagementContext())
            {
                var item = db.ProjectBonuses.SingleOrDefault(pb => pb.ID == id);
                if (item == null) return;
                db.ProjectBonuses.Remove(item);
                db.SaveChanges();
            }
        }

        public void Update(ProjectBonus projectBonuses)
        {
            using (var db = new CompanyManagementContext())
            {
                db.Entry(projectBonuses).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public ProjectBonus SearchByID(string id)
        {
            using (var db = new CompanyManagementContext())
            {
                return db.ProjectBonuses.SingleOrDefault(pb => pb.ID == id);
            }
        }

        public List<ProjectBonus> SearchByProjectID(string id)
        {
            using (var db = new CompanyManagementContext())
            {
                var query = from pb in db.ProjectBonuses where pb.ProjectID == id select pb;
                return query.ToList();
            }
        }

        public List<ProjectBonus> SearchByEmployeeID(string id)
        {
            using (var db = new CompanyManagementContext())
            {
                var query = from pb in db.ProjectBonuses where pb.EmployeeID == id select pb;
                return query.ToList();
            }
        }

        public void DeleteByProjectID(string id)
        {
            using (var db = new CompanyManagementContext())
            {
                var item = db.ProjectBonuses.SingleOrDefault(pb => pb.ProjectID == id);
                if (item == null) return;
                db.ProjectBonuses.Remove(item);
                db.SaveChanges();
            }
        }

        public decimal ToTalBonusesOfEmployeeByTime(string employeeID, int month, int year)
        {
            using (var db = new CompanyManagementContext())
            {
                var query = from pb in db.ProjectBonuses 
                            where pb.EmployeeID == employeeID
                            && pb.ReceivedDate.Value.Month == month
                            && pb.ReceivedDate.Value.Year == year
                            select pb;
                return query.Sum(pb => pb.Amount).Value;
            }
        }

        public List<ProjectBonus> GetOfEmployeeByTime(string employeeID, int month, int year)
        {
            using (var db = new CompanyManagementContext())
            {
                var query = from pb in db.ProjectBonuses
                            where pb.EmployeeID == employeeID
                            && pb.ReceivedDate.Value.Month == month
                            && pb.ReceivedDate.Value.Year == year
                            select pb;
                return query.ToList();
            }
        }
    }
}
