using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows;
using System.Data;

namespace CompanyManagement
{
    public class DBConnection
    {
        SqlConnection conn = new SqlConnection(Properties.Settings.Default.connStr);

        public void ExecuteNonQuery(string sqlstr)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, conn);
                if (cmd.ExecuteNonQuery() > 0)
                    MessageBox.Show("Completed!");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
            finally
            {
                conn.Close();
            }
        }
        public DataTable GetDataTable(string sqlStr)
        {
            DataTable databasetable = new DataTable();
            try
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sqlStr, conn);
                adapter.Fill(databasetable);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
            }
            finally
            {
                conn.Close();
            }
            return databasetable;
        }
    }
}
