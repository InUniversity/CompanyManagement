using CompanyManagement.Database.Base;
using CompanyManagement.Utilities;
using System;
using System.Data;

namespace CompanyManagement.Models
{
    public class LeaveStatus
    {
        private string id = "";
        private string name = "";

        public string ID => id;
        public string Name => name;

        public LeaveStatus(IDataRecord reader)
        {
            try
            {
                id = Utils.GetString(reader, BaseDao.leavStasID);
                name = Utils.GetString(reader, BaseDao.leavStasName);
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(Account), "CAST ERROR: " + ex.Message);
            }
        }
    }
}
