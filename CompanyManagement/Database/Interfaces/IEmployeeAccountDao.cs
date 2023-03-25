using System.Collections.Generic;

namespace CompanyManagement.Database.Interfaces
{
    public interface IEmployeeAccountDao
    {
        void Add(EmployeeAccount employeeAccount);
        void Delete(string id);
        void Update(EmployeeAccount employeeAccount);
        List<EmployeeAccount> GetAll();
        EmployeeAccount SearchByID(string id);
        EmployeeAccount SearchByIdentifyCard(string identifyCard);
        EmployeeAccount SearchByPhoneNumber(string phoneNumber);
        EmployeeAccount SearchByUsername(string username);
        EmployeeAccount SearchByName(string fullName);
    }
}
