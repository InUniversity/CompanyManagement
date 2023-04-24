using System;
using System.Data;
using CompanyManagement.Database.Base;
using CompanyManagement.Utilities;

namespace CompanyManagement.Models
{
    public class PositionInCompany
    {
        private string id;
        private string name;

        public string ID => id;
        public string Name => name;

        public PositionInCompany(IDataRecord reader)
        {
            try
            {
                id = (string)reader[BaseDao.POSITION_ID];
                name = (string)reader[BaseDao.POSITION_NAME];
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(PositionInCompany), "CAST ERROR: " + ex.Message);
            }
        }
    }
}