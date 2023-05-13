using CompanyManagementEntity.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagementEntity.Database
{
    public class AccountsDao 
    {
        public void Add(Account acc)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    db.Accounts.Add(acc);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
            }           
        }

        public void Delete(string emplID)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    var acc = db.Accounts.SingleOrDefault(a => a.EmployeeID == emplID);
                    if (acc == null) return;
                    db.Accounts.Remove(acc);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
            }           
        }

        public void Update(Account acc)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    db.Entry(acc).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
            }         
        }

        public Account SearchByUsername(string username)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    return db.Accounts.SingleOrDefault(d => d.Username == username);
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
