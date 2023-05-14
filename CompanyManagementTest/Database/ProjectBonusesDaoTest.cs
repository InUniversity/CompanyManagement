using System;
using System.Collections.Generic;
using CompanyManagement.Database;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;
using NUnit.Framework;

namespace CompanyManagementTest.Database
{
    [TestFixture]
    public class ProjectBonusesDaoTest
    {
        private ProjectBonusesDao myDao;

        [SetUp]
        public void SetUp()
        {
            myDao = new ProjectBonusesDao();
        }
        
        [Test]
        public void Add_Delete_Successs()
        {
            var prjbns = new ProjectBonus("ABC", 545454546545, new DateTime(2023, 07, 1 , 9 , 0 , 0), "EM009", "PRJ0001");
            
            //add
            myDao.Add(prjbns);
            var added = Search(prjbns);
            
            //update
            var prjbnsupdate = new ProjectBonus(prjbns.ID, 99999999, prjbns.ReceivedDate, prjbns.EmployeeID,
                prjbns.ProjectID);
            myDao.Update(prjbnsupdate);
            var updated = Search(prjbnsupdate);
            
            //delete
            myDao.Delete(prjbns.ID);
            var deleted = Search(prjbns);
            
            //assert add
            AssertObject(prjbns, added);
            
            //assert update
            AssertObject(prjbnsupdate, updated);
            
            //assert delete
            Assert.IsNull(deleted);
        }

        [Test]
        public void SearchByProjID_Found()
        {
            var prjbns = new ProjectBonus("123", 545454546545, new DateTime(2023, 07, 1 , 9 , 0 , 0), "EM009", "PRJ0002");
            myDao.Add(prjbns);

            var listdao = myDao.SearchByProjectID("PRJ0002");
            var listtest = SearchByProjectID("PRJ0002");
            Assert.AreEqual(listdao.Count, listtest.Count);
        }

        [Test]
        public void SearchByEmplojID_Found()
        {
            var prjbns = new ProjectBonus("123", 545454546545, new DateTime(2023, 07, 1 , 9 , 0 , 0), "EM009", "PRJ0002");
            myDao.Add(prjbns);

            var listdao = myDao.SearchByEmployeeID("EM009");
            var listtest = SearchByEmployeeID("EM009");
            Assert.AreEqual(listdao.Count, listtest.Count);
        }

        public ProjectBonus Search(ProjectBonus projectBonus)
        {
            DBConnection dbConnection = new DBConnection();
            string sqlStr = $"Select * From ProjectBonuses Where ID = '{projectBonus.ID}'";
            return (ProjectBonus)dbConnection.GetSingleObject(sqlStr, reader => new ProjectBonus(reader));
        }

        public List<ProjectBonus> SearchByProjectID(string prjectID)
        {
            DBConnection dbConnection = new DBConnection();
            string sqlStr = $"Select * From ProjectBonuses Where ProjectID = '{prjectID}'";
            return dbConnection.GetList(sqlStr, reader => new ProjectBonus(reader));
        }
        public List<ProjectBonus> SearchByEmployeeID(string employeeID)
        {
            DBConnection dbConnection = new DBConnection();
            string sqlStr = $"Select * From ProjectBonuses Where EmployeeID = '{employeeID}'";
            return dbConnection.GetList(sqlStr, reader => new ProjectBonus(reader));
        }
        public void AssertObject(ProjectBonus expected, ProjectBonus actual)
        {
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.ID,actual.ID);
            Assert.AreEqual(expected.Amount,actual.Amount);
            Assert.AreEqual(expected.ReceivedDate,actual.ReceivedDate);
            Assert.AreEqual(expected.EmployeeID,actual.EmployeeID);
            Assert.AreEqual(expected.ProjectID,actual.ProjectID);
        }
    }
}