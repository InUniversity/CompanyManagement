using System.Collections.Generic;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Database
{
    public class LeaveStatusesDao : BaseDao
    {
        public List<LeaveStatus> GetAll()
        {
            string sqlStr = $"SELECT * FROM {leavStasTbl}";
            return dbConnection.GetList(sqlStr, reader => new LeaveStatus(reader));
        }

        public LeaveStatus SearchByID(string statusID)
        {
            string sqlStr = $"SELECT * FROM {leavStasTbl} WHERE {leavStatusID}='{statusID}'";
            return (LeaveStatus)dbConnection.GetSingleObject(sqlStr, reader => new LeaveStatus(reader));
        }
    }
}
