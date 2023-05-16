using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CompanyManagement.Utilities;

namespace CompanyManagement.Database.Base
{
    public class DbConnection
    {
        private SqlConnection conn;

        public DbConnection()
        {
            conn = new SqlConnection(Properties.Settings.Default.connStr);
        }

        public bool ExecuteNonQuery(string command)
        {
            bool success = false;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(command, conn);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    success = true;
                    Log.Ins.Information(nameof(DbConnection), "Completed");
                }
            }
            catch (Exception ex)
            {
                Log.Ins.Error(nameof(DbConnection), ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return success;
        }

        public object? GetSingleObject<T>(string sqlStr, Func<SqlDataReader, T> converter)
        {
            List<T> list = GetList(sqlStr, converter);
            return list.Count == 0 ? null : list[0];
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
                Log.Ins.Error(nameof(DbConnection), ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return list;
        }

        public decimal GetDecimal(string sqlStr)
        {
            decimal variable = 0;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlStr, conn);
                variable = Convert.ToDecimal(cmd.ExecuteScalar());
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                Log.Ins.Error(nameof(DbConnection), ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return variable;
        }
    }
}
