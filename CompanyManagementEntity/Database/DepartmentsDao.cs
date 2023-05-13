using CompanyManagementEntity.Database.Base;
using CompanyManagementEntity.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagementEntity.Database
{
    public class DepartmentsDao : BaseDao<Department>
    {
        public void Delete(string id)
        {
            NewDbContext(db =>
            {
                var dept = db.Departments.SingleOrDefault(d => d.ID == id);
                if (dept == null) return;
                db.Departments.Remove(dept);
                db.SaveChanges();
            });
        }

        public List<Department> GetAll()
        {
            var listItems = new List<Department>();
            NewDbContext(db =>
            {
                listItems = db.Departments.ToList();
            });
            return listItems;
        }

        public Department SearchByID(string dptID)
        {
            var item = new Department();
            NewDbContext(db =>
            {
                item = db.Departments.SingleOrDefault(d => d.ID == dptID);
            });
            return item;
        }
    }
}
