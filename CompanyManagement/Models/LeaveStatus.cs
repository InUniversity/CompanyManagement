using CompanyManagement.Database.Base;
using CompanyManagement.Utilities;
using System;
using System.Data;

namespace CompanyManagement.Models
{
    public class LeaveStatus
    {
        private string id = "";
        private string statusName = "";

        public string ID => id;
        public string StatusName => statusName;

        public LeaveStatus(IDataRecord reader)
        {
            try
            {
                id = Utils.GetString(reader, BaseDao.leavStasID);
                statusName = Utils.GetString(reader, BaseDao.leavStasName);
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(Account), "CAST ERROR: " + ex.Message);
            }
        }
    }
}
