using System.Collections.Generic;
using CompanyManagement.Database.Interfaces;
using CompanyManagement.Models;

namespace CompanyManagement.Database.Implementations
{
    public class PositionDao : BaseDao, IPositionDao
    {
        public void Add(PositionInCompany pos)
        {
            string sqlStr =
                $"INSERT INTO {POSITION_TABLE} ({POSITION_ID}, {POSITION_NAME}) VALUES ('{pos.ID}', N'{pos.Name}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(string id)
        {
            string sqlStr = $"DELETE FROM {POSITION_TABLE} WHERE {POSITION_ID} = '{id}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Update(Department dep)
        {
            string sqlStr =
                $"UPDATE {POSITION_TABLE} SET {POSITION_NAME}=N'{dep.Name}' WHERE {POSITION_ID}='{dep.ID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public List<PositionInCompany> GetAll()
        {
            string sqlStr = $"SELECT * FROM {POSITION_TABLE}";
            return dbConnection.GetList(sqlStr, reader => new PositionInCompany(reader));
        }
    }
}
