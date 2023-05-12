using System.Collections.Generic;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;
using CompanyManagement.Utilities;

namespace CompanyManagement.Database
{
    public class MilestonesDao : BaseDao
    {
        public void Add(Milestone mile)
        {
            string sqlStr = $"INSERT INTO {mileTbl} ({mileID}, {mileTitle}, {mileExplanation}, {mileStart}, " +
                            $"{mileEnd}, {mileCompleted}, {mileOwnerID}, {mileProjID}) " +
                            $"VALUES ('{mile.ID}', N'{mile.Title}', N'{mile.Explanation}', " +
                            $"'{Utils.ToSQLFormat(mile.Start)}', '{Utils.ToSQLFormat(mile.End)}', " +
                            $"'{Utils.ToSQLFormat(mile.Completed)}', '{mile.OwnerID}', '{mile.ProjID}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }
        
        public void Delete(string id)
        {
            string sqlStr = $"DELETE FROM {mileTbl} WHERE {mileID} = '{id}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }
        
        public void Update(Milestone mile)
        {
            string sqlStr = $"UPDATE {mileTbl} SET {mileTitle}=N'{mile.Title}', " +
                            $"{mileExplanation}=N'{mile.Explanation}', {mileStart}='{Utils.ToSQLFormat(mile.Start)}', " +
                            $"{mileEnd}='{Utils.ToSQLFormat(mile.End)}', {mileCompleted}='{Utils.ToSQLFormat(mile.Completed)}', " +
                            $"{mileOwnerID}='{mile.OwnerID}' WHERE {mileID} = '{mile.ID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void DeleteProjID(string projID)
        {
            string sqlStr = $"DELETE FROM {mileTbl} WHERE {mileProjID}='{projID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public Milestone SearchByID(string id)
        {
            string sqlStr = $"SELECT * FROM {mileTbl} WHERE {mileID} = '{id}'";
            return (Milestone)dbConnection.GetSingleObject(sqlStr, reader => new Milestone(reader));
        }
        public List<Milestone> SearchByProjectID(string projID)
        {
            string sqlStr = $"SELECT * FROM {mileTbl} WHERE {mileProjID} = '{projID}'";
            return dbConnection.GetList(sqlStr, reader => new Milestone(reader));
        }
    }
}