using CompanyManagement.Enums;
using CompanyManagementEntity.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CompanyManagementEntity.Database
{
    public class EmployeesDao
    {
        public void Add(Employee empl)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    db.Employees.Add(empl);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao),ex.Message);
            }
           
        }

        public void Delete(string id)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    var empl = db.Employees.SingleOrDefault(e => e.ID == id);
                    if (empl == null) return;
                    db.Employees.Remove(empl);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
            }          
        }

        public void Update(Employee empl)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    db.Entry(empl).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
            }          
        }

        public List<Employee> SearchByCurrentID(string emplID)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    var query = from e in db.Employees where e.ID != emplID select e;
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
                return null;
            }           
        }

        public Employee SearchByID(string id)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    return db.Employees.SingleOrDefault(e => e.ID == id);
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
                return null;
            }     
        }

        public Employee SearchByIdentifyCard(string identCard)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    return db.Employees.SingleOrDefault(e => e.IdentifyCard == identCard);
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
                return null;
            }          
        }

        public Employee SearchByPhoneNumber(string phoneNo)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    return db.Employees.SingleOrDefault(e => e.PhoneNumber == phoneNo);
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
                return null;
            }   
        }

        public List<Employee> GetManagers()
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    var query = from e in db.Employees where e.EmplRole.PermsID == (int)EPermission.Mgr  select e;
                    return query.ToList();
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
