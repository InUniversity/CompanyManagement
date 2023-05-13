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
    public class AccountsDao : BaseDao<Account>
    {
        public void Delete(string emplID)
        {
            NewDbContext(db =>
            {
                var acc = db.Accounts.SingleOrDefault(a => a.EmployeeID == emplID);
                if (acc == null) return;
                db.Accounts.Remove(acc);
                db.SaveChanges();
            });   
        }

        public Account SearchByUsername(string username)
        {
            var acc = new Account();
            NewDbContext(db =>
            {
                acc = db.Accounts.SingleOrDefault(d => d.Username == username);
            });
            return acc;
        }
    }
}
