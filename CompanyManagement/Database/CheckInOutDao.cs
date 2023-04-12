using System.Collections.Generic;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;

namespace CompanyManagement.Database
{
    public class CheckInOutDao : BaseDao
    {

        public void Add(CheckInOut checkInOut)
        {
            string sqlStr = $"";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(string id)
        {
            string sqlStr = $"";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Update(CheckInOut checkInOut)
        {
            string sqlStr = $"";
            dbConnection.ExecuteNonQuery(sqlStr); 
        }

        public List<CheckInOut> GetAll()
        {
            string sqlStr = $"SELECT * FROM {CHECK_IN_OUT_TABLE}";
            return dbConnection.GetList(sqlStr, reader => new CheckInOut(reader)); 
        }

        public CheckInOut SearchByID(string id)
        {
            string sqlStr = $"SELECT * FROM {CHECK_IN_OUT_TABLE} C WHERE C.{CHECK_IN_OUT_ID}='{id}'";
            return (CheckInOut)dbConnection.GetSingleObject(sqlStr, reader => new CheckInOut(reader));
        }
    }
}
