using CompanyManagementEntity.Database.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagementEntity.Database
{
    public class EmployeesDao : BaseDao
    {
        public void Add(Employee empl)
        {
            using (var db = new CompanyManagementContext())
            {
                db.Employees.Add(empl);
                db.SaveChanges();
            }
        }

        public void Delete(string id)
        {
            using (var db = new CompanyManagementContext())
            {
                var empl = db.Employees.SingleOrDefault(e => e.ID == id);
                if (empl == null) return;
                db.Employees.Remove(empl);
                db.SaveChanges();
            }
        }

        public void Update(Employee empl)
        {
            using (var db = new CompanyManagementContext())
            {
                db.Entry(empl).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public List<Employee> SearchByCurrentID(string emplID)
        {
            using (var db = new CompanyManagementContext())
            {
                var query = from e in db.Employees where e.ID != emplID select e;
                return query.ToList();
            }
        }

        public Employee SearchByID(string id)
        {
            using (var db = new CompanyManagementContext())
            {
                return db.Employees.SingleOrDefault(e => e.ID == id);
            }
        }

        public Employee SearchByIdentifyCard(string identCard)
        {
            using (var db = new CompanyManagementContext())
            {
                return db.Employees.SingleOrDefault(e => e.IdentifyCard == identCard);
            }
        }

        public Employee SearchByPhoneNumber(string phoneNo)
        {
            using (var db = new CompanyManagementContext())
            {
                return db.Employees.SingleOrDefault(e => e.PhoneNumber == phoneNo);
            }
        }

        public List<Employee> GetManagers()
        {
            using (var db = new CompanyManagementContext())
            {
                var query = from e in db.Employees where e.RoleID == managerRole select e;
                return query.ToList();
            }
        }
    }
}
