using System;
using System.Collections.Generic;
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
        
        private List<T> DataTableToList<T>(DataTable dataTable, Func<DataRow, T> converter)
        {
            List<T> list = new List<T>();
            foreach (DataRow row in dataTable.Rows)
                list.Add(converter(row));
            return list;
        }

        public List<T> GetList<T>(string sqlStr, Func<SqlDataReader, T> converter)
        {
            using (SqlDataReader reader = GetDataReader(sqlStr))
            {
                return ReaderToList(reader, converter);
            }
        }

        private SqlDataReader GetDataReader(string sqlStr)
        {
            SqlDataReader reader = null;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlStr, conn);
                reader = cmd.ExecuteReader();
                cmd.Dispose();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return reader;
        }
        
        private List<T> ReaderToList<T>(SqlDataReader reader, Func<SqlDataReader, T> converter)
        {
            List<T> list = new List<T>();
            while (reader.Read())
                list.Add(converter(reader));
            return list;
        }
    }  
}
