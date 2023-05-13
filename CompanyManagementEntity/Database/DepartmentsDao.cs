using CompanyManagementEntity.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagementEntity.Database
{
    public class DepartmentsDao 
    {
        public void Add(Department dept)
        {
            try
            {
                //TODO
                using (var db = new CompanyManagementContext())
                {
                    db.Departments.Add(dept);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
            }           
        }

        public void Delete(string id)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    var dept = db.Departments.SingleOrDefault(d => d.ID == id);
                    if (dept == null) return;
                    db.Departments.Remove(dept);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
            }         
        }

        public void Update(Department dept)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    db.Entry(dept).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
            }         
        }

        public List<Department> GetAll()
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    return db.Departments.ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
                return null;
            }    
        }

        public Department SearchByID(string dptID)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    return db.Departments.SingleOrDefault(d => d.ID == dptID);
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
                return null;
            }        
        }
    }
}
