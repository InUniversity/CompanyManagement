using CompanyManagement.Database.Base;
using CompanyManagement.Models;
using CompanyManagement.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Database
{
    public class ProjectBonusesDao : BaseDao 
    {
        public void Add(ProjectBonus projectBonuses)
        {
            string sqlStr = $"INSERT INTO {projBonusTbl} ({projBonusID}, " +
                            $"{projBonusAmount}, {projBonusReceiveDate}, " +
                            $"{projBonusEmplID}, {projBonusProjID}) " +
                            $"VALUES ('{projectBonuses.ID}', '{projectBonuses.Amount}', " +
                            $"'{Utils.ToOnlyDateSQLFormat(projectBonuses.ReceivedDate)}', " +
                            $"'{projectBonuses.EmployeeID}', '{projectBonuses.ProjectID}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(string id)
        {
            string sqlStr = $"DELETE FROM {projBonusTbl} WHERE {projBonusID} = '{id}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Update(ProjectBonus projectBonuses)
        {
            string sqlStr = $"UPDATE {projBonusTbl} " +
                            $"SET {projBonusAmount} = '{projectBonuses.Amount}', " +
                            $"{projBonusReceiveDate} = '{Utils.ToOnlyDateSQLFormat(projectBonuses.ReceivedDate)}', " +
                            $"{projBonusEmplID} = '{projectBonuses.EmployeeID}', " +
                            $"{projBonusProjID} = '{projectBonuses.ProjectID}' " +
                            $"WHERE {projBonusID} = '{projectBonuses.ID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public List<ProjectBonus> SearchByID(string id)
        {
            string sqlStr = $"SELECT * FROM {projBonusTbl} WHERE {projBonusID} = '{id}'";
            return dbConnection.GetList<ProjectBonus>(sqlStr, reader => new ProjectBonus(reader));
        }

        public List<ProjectBonus> SearchByProjectID(string id)
        {
            string sqlStr = $"SELECT * FROM {projBonusTbl} WHERE {projBonusProjID} = '{id}'";
            return dbConnection.GetList<ProjectBonus>(sqlStr, reader => new ProjectBonus(reader));
        }

        public List<ProjectBonus> SearchByEmployeeID(string id)
        {
            string sqlStr = $"SELECT * FROM {projBonusTbl} WHERE {projBonusEmplID} = '{id}'";
            return dbConnection.GetList<ProjectBonus>(sqlStr, reader => new ProjectBonus(reader));
        }

        public void DeleteByProjectID(string id)
        {
            string sqlStr = $"DELETE FROM {projBonusTbl} WHERE {projBonusProjID} = '{id}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }
    }
}
