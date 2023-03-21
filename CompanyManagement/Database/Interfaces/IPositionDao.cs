using System.Collections.Generic;
using CompanyManagement.Models;

namespace CompanyManagement.Database.Interfaces
{
    public interface IPositionDao
    {
        void Add(PositionInCompany pos);
        void Delete(string id);
        void Update(Department dep);
        List<PositionInCompany> GetAll();
    }
}
