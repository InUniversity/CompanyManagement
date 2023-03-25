using System.Collections.Generic;

namespace CompanyManagement.Database.Interfaces
{
    public interface IEmployeeDao
    {
        void Add(Employee employee);
        void Delete(string id);
        void Update(Employee employee);
        List<Employee> GetAll();
        Employee SearchByID(string id);
        Employee SearchByIdentifyCard(string identifyCard);
        Employee SearchByPhoneNumber(string phoneNumber);
        Employee SearchByUsername(string username);
        Employee SearchByName(string fullName);
    }
}
