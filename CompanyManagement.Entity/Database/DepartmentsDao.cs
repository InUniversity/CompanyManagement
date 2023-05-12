using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Entity.Database
{
    public class DepartmentsDao : BaseDao
    {
        public void Add(Department dept)
        {
            //TODO
            using (var db = new CompanyManagementContext())
            {
                db.Departments.Add(dept);
                db.SaveChanges();
            }    
        }

        public void Delete(string id)
        {
            using (var db = new CompanyManagementContext())
            {
                var dept = db.Departments.SingleOrDefault(d => d.ID == id);
                if (dept == null) return;
                db.Departments.Remove(dept);
                db.SaveChanges();
            }
        }

        public void Update(Department dept)
        {
            using (var db = new CompanyManagementContext())
            {
                db.Entry(dept).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public List<Department> GetAll()
        {
            using (var db = new CompanyManagementContext())
            {
                return db.Departments.ToList();
            }
        }

        public Department SearchByID(string dptID)
        {
            using (var db = new CompanyManagementContext())
            {
                return db.Departments.SingleOrDefault(d => d.ID == dptID);
            }
        }
    }
}
