using CompanyManagement.Enums;
using CompanyManagementEntity.Database.Base;
using CompanyManagementEntity.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CompanyManagementEntity.Database
{
    public class EmployeesDao : BaseDao<Employee>
    {
        public void Delete(string id)
        {
            NewDbContext(db =>
            {
                var empl = db.Employees.SingleOrDefault(e => e.ID == id);
                if (empl == null) return;
                db.Employees.Remove(empl);
                db.SaveChanges();
            });        
        }

        public List<Employee> SearchByCurrentID(string emplID)
        {
            var listItems = new List<Employee>();
            NewDbContext(db =>
            {
                var query = from e in db.Employees where e.ID != emplID select e;
                listItems = query.ToList();
            });
            return listItems;
        }

        public Employee SearchByID(string id)
        {
            var item = new Employee();
            NewDbContext(db =>
            {
                item = db.Employees.SingleOrDefault(e => e.ID == id);
            });
            return item;
        }

        public Employee SearchByIdentifyCard(string identCard)
        {
            var item = new Employee();
            NewDbContext(db =>
            {
                item = db.Employees.SingleOrDefault(e => e.IdentifyCard == identCard);
            });
            return item;    
        }

        public Employee SearchByPhoneNumber(string phoneNo)
        {
            var item = new Employee();
            NewDbContext(db =>
            {
                item = db.Employees.SingleOrDefault(e => e.PhoneNumber == phoneNo);
            });
            return item;
        }

        public List<Employee> GetManagers()
        {
            var listItems = new List<Employee>();
            NewDbContext(db =>
            {
                var query = from e in db.Employees where e.EmplRole.PermsID == (int)EPermission.Mgr select e;
                listItems = query.ToList();
            });
            return listItems;
        }
    }
}
