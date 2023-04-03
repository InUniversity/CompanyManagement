using CompanyManagement.Models;

namespace CompanyManagement.Database.Interfaces
{
    public interface IAccountDao
    {
        void Add(Account account);
        void Delete(string employeeID);
        void Update(Account account);
        Account SearchByUsername(string userName);
    }
}
