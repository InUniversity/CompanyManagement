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
    public class LeaveStatusDao : BaseDao
    {
        public List<LeaveStatus> GetAll()
        {
            string sqlStr = $"SELECT * FROM {LEAVE_STATUS_TABLE}";
            return dbConnection.GetList(sqlStr, reader => new LeaveStatus(reader));
        }
    }
}
