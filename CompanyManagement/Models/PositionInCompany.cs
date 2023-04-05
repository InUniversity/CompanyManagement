using System;
using System.Data.SqlClient;
using CompanyManagement.Database.Base;
using CompanyManagement.Utilities;

namespace CompanyManagement.Models
{
    public class PositionInCompany
    {
        
        private string id;
        private string name;

        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public PositionInCompany() { }

        public PositionInCompany(string id, string name)
        {
            this.id = id;
            this.name = name;
        }
        
        public PositionInCompany(SqlDataReader reader)
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