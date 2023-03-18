using System;
using System.Data.SqlClient;
using System.Windows;
using System.Data;
using System.Collections.Generic;
using Dapper;
using System.Linq;
using System.Reflection;

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

        public List<T> GetDataList<T>(string sqlStr) where T : class, new()
        {
            List<T> list = new List<T>();
            try
            {
                conn.Open();
               using(SqlDataReader reader = (new SqlCommand(sqlStr, conn)).ExecuteReader())
                {
                    T obj = new T();

                    while(reader.Read())
                    {
                        var columnNames = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();

                        foreach(var columnName in columnNames)
                        {
                            PropertyInfo property = typeof(T).GetProperty(columnName)
                        }    
                    }    
                }    
            }

              

            List<T> objs = conn.Query<T>(sqlStr).ToList<T>();
            return objs;
        }
    }  
}
