using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Entity.Database
{
    public class AccountsDao : BaseDao
    {
        public void Add(Account acc)
        {
            using (var db = new CompanyManagementContext())
            {
                db.Accounts.Add(acc);
                db.SaveChanges();
            }
        }

        public void Delete(string emplID)
        {
            using (var db = new CompanyManagementContext())
            {
                var acc = db.Accounts.SingleOrDefault(a => a.EmployeeID == emplID);
                if (acc == null) return;
                db.Accounts.Remove(acc);
                db.SaveChanges();
            }
        }

        public void Update(Account acc)
        {
            using (var db = new CompanyManagementContext())
            {
                db.Entry(acc).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public Account SearchByUsername(string username)
        {
            using (var db = new CompanyManagementContext())
            {
                return db.Accounts.SingleOrDefault(d => d.Username == username);
            }
        }
    }
}
