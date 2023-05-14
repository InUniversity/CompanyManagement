using System;
using System.Collections.Generic;
using CompanyManagement.Database;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompanyManagementMSTest.Database
{
    [TestClass]
    public class SalaryRecordsDaoTest
    {
        private SalaryRecordsDao myDao;

        [TestInitialize]
        public void SetUp()
        {
            myDao = new SalaryRecordsDao();
        }

        [TestMethod]
        public void Add_Delete_Success()
        {
            var slrRcd = new SalaryRecord("ABC", "EM99", new DateTime(2023, 07, 01), 18, 99999, 77777);
            
            //add
            myDao.Add(slrRcd);
            var added = Search(slrRcd);
            
            //delete
            myDao.Delete(slrRcd.ID);
            var deleted = Search(slrRcd);
            
            //assert add
            AssertObject(slrRcd, added);
            
            //assert delete
            Assert.IsNull(deleted);
        }

        [TestMethod]
        public void SearchByID_Found()
        {
            var slrRcd = myDao.SearchByID("SR00001");
            Assert.IsNotNull(slrRcd);
        }

        [TestMethod]
        public void GetByTime_Found()
        {
            var slrRcd = new SalaryRecord("ABC", "EM99", new DateTime(2023, 07, 01), 18, 99999, 77777);
            myDao.Add(slrRcd);

            var list = myDao.GetByTime(slrRcd.MonthYear.Month, slrRcd.MonthYear.Year);
            var listTest = SearchByMonthYear(slrRcd.MonthYear.Month, slrRcd.MonthYear.Year);
            Assert.AreEqual(list.Count, listTest.Count);
        }

        [TestMethod]
        public void GetByDepartment_Fount()
        {
            var list = myDao.GetByDepartmentID("DPM001",03, 2023);
            Assert.AreEqual(1, list.Count);
        }

        [TestMethod]
        public void GetByEmployeeID_Fount()
        {
            var list = myDao.GetByEmployeeID("EM006");
            Assert.AreEqual(1, list.Count);
        }

        public SalaryRecord Search(SalaryRecord salaryRecord)
        {
            DBConnection dbConnection = new DBConnection();
            string sqlStr = $"Select * From SalaryRecords where ID = '{salaryRecord.ID}'";
            return (SalaryRecord)dbConnection.GetSingleObject(sqlStr, reader => new SalaryRecord(reader));
        }
        public List<SalaryRecord> SearchByMonthYear(int month, int year)
        {
            DBConnection dbConnection = new DBConnection();
            string sqlStr = $"Select * From SalaryRecords where Month(MonthYear) = '{month}' AND Year(MonthYear) = '{year}'";
            return dbConnection.GetList(sqlStr, reader => new SalaryRecord(reader));
        }

        public void AssertObject(SalaryRecord expected, SalaryRecord actual)
        {
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.EmployeeID, actual.EmployeeID);
            Assert.AreEqual(expected.MonthYear, actual.MonthYear);
            Assert.AreEqual(expected.TotalWorkDays, actual.TotalWorkDays);
            Assert.AreEqual(expected.TotalBonuses, actual.TotalBonuses);
            Assert.AreEqual(expected.Income, actual.Income);
        }

    }
}