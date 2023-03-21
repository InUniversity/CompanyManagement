using CompanyManagement.Models;

namespace CompanyManagement.Database.Interfaces
{
    public interface IAccountDao
    {
        void Add(Account account);
        public void Delete(string employeeID);
        public void Update(Account account);
        public Account SearchByEmployeeID(string employeeID);
        public Account SearchByUsername(string username);
    }
}
