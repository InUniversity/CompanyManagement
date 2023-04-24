using System.Collections.Generic;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;

namespace CompanyManagement.Database
{
    public class PositionDao : BaseDao
    {
        public List<PositionInCompany> GetAll()
        {
            string sqlStr = $"SELECT * FROM {POSITION_TABLE}";
            return dbConnection.GetList(sqlStr, reader => new PositionInCompany(reader));
        }
    }
}
