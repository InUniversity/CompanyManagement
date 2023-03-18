﻿using System;
using System.Data.SqlClient;
using System.Windows;
using System.Data;
using System.Collections.Generic;
using Dapper;
using System.Linq;
using System.Reflection;
using Microsoft.VisualBasic;

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

        public SqlDataReader GetDataReader(string sqlStr)
        {
            SqlDataReader reader = null;
            try
            {
                conn.Open();
                using (reader = (new SqlCommand(sqlStr, conn)).ExecuteReader())
                {
                    return reader;
                }    
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
    }  
}
