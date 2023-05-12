using CompanyManagement.Database.Base;
using CompanyManagement.Models;
using CompanyManagement.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

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
                            $"'{Utils.ToSQLFormat(projectBonuses.ReceivedDate)}', " +
                            $"'{projectBonuses.EmployeeID}', '{projectBonuses.ProjectID}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(string id)
        {
            string sqlStr = $"DELETE FROM {projBonusTbl} WHERE {projBonusID} = '{id}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }
        
        public void DeleteProjID(string projID)
        {
            string sqlStr = $"DELETE FROM {projBonusTbl} WHERE {projBonusProjID}='{projID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Update(ProjectBonus projectBonuses)
        {
            string sqlStr = $"UPDATE {projBonusTbl} " +
                            $"SET {projBonusAmount} = '{projectBonuses.Amount}', " +
                            $"{projBonusReceiveDate} = '{Utils.ToSQLFormat(projectBonuses.ReceivedDate)}', " +
                            $"{projBonusEmplID} = '{projectBonuses.EmployeeID}', " +
                            $"{projBonusProjID} = '{projectBonuses.ProjectID}' " +
                            $"WHERE {projBonusID} = '{projectBonuses.ID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public ProjectBonus SearchByID(string id)
        {
            string sqlStr = $"SELECT * FROM {projBonusTbl} WHERE {projBonusID} = '{id}'";
            return (ProjectBonus)dbConnection.GetSingleObject(sqlStr, reader => new ProjectBonus(reader));
        }

        public List<ProjectBonus> SearchByProjectID(string id)
        {
            string sqlStr = $"SELECT * FROM {projBonusTbl} WHERE {projBonusProjID} = '{id}'";
            return dbConnection.GetList(sqlStr, reader => new ProjectBonus(reader));
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

        public decimal ToTalBonusesOfEmployeeByTime(string employeeID, int month, int year)
        {
            string sqlStr = $"SELECT SUM({projBonusAmount}) FROM {projBonusTbl} WHERE {projBonusEmplID} = '{employeeID}' " +
                            $"AND MONTH({projBonusReceiveDate}) = '{month}' " +
                            $"AND YEAR({projBonusReceiveDate}) = '{year}'";
            return dbConnection.GetDecimal(sqlStr);
        }


        public List<ProjectBonus> GetOfEmployeeByTime(string employeeID, int month, int year)
        {
            string sqlStr = $"SELECT * FROM {projBonusTbl} WHERE {projBonusEmplID} = '{employeeID}'" +
                            $"AND MONTH({projBonusReceiveDate}) = '{month}' " +
                            $"AND YEAR({projBonusReceiveDate}) = '{year}'";
            return dbConnection.GetList<ProjectBonus>(sqlStr, reader => new ProjectBonus(reader));
        }
    }
}
