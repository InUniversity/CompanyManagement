using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;

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
                MessageBox.Show("Error! " + ex);
            }
            finally
            {
                conn.Close();
            }
        }

        public List<T> GetList<T>(string sqlStr, Func<SqlDataReader, T> converter)
        {
            List<T> list = new List<T>();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlStr, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    list.Add(converter(reader));
                cmd.Dispose();
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return list;
        }
    }  
}
