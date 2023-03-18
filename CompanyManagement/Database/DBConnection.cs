using System;
using System.Data.SqlClient;
using System.Windows;
using System.Data;

namespace CompanyManagement.Database
{
    public class DBConnection
    {

        private SqlConnection conn;

        public DBConnection()
        {
            conn = new SqlConnection(Properties.Settings.Default.connStr);
        }

        public void ExecuteNonQuery(string command)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(command, conn);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Completed! ");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erorr! " + ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable GetDataTable(string sqlStr)
        {
            DataTable tableData = new DataTable();
            try
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sqlStr, conn);
                adapter.Fill(tableData);

                conn.Close();
            }
            catch (Exception exe)
            {
                throw exe;
            }
            finally
            {
                conn.Close();
            }
            return tableData;
        }
    }
}
