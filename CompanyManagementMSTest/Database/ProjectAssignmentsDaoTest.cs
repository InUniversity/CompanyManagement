using System;
using CompanyManagement.Database;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompanyManagementMSTest.Database
{
    [TestClass]
    public class ProjectAssignmentsDaoTest
    {
        private ProjectAssignmentsDao myDao;
        
        [TestInitialize]
        public void SetUp()
        {
            myDao = new ProjectAssignmentsDao();
        }

        [TestMethod]
        public void Add_Delete_Success()
        {
            var assign = new ProjectAssignment("PROJ1231", "DMP12314");  
            
            // add
            myDao.Add(assign); 
            var added = Search(assign);
            
            // add
            myDao.Delete(assign);
            var afterDelete = Search(assign);
            
            // assert added
            Assert.IsNotNull(added);
            AssertObject(assign, added);
            
            // assert deleted
            Assert.IsNull(afterDelete); 
        }
        
        [TestMethod]
        public void GetAllDepartmentInProject_Found()
        {
            var list = myDao.GetAllDepartmentInProject("PRJ001");
            Assert.AreEqual(1, list.Count);
        }
        
        [TestMethod]
        public void GetEmployeesInProject_Found()
        {
            var list = myDao.GetEmployeesInProject("PRJ001");
            Assert.AreEqual(18, list.Count);
        }
        
        [TestMethod]
        public void GetDepartmentsCanAssignWork_Found()
        {
            var list = myDao.GetDepartmentsCanAssignWork("", 
                new DateTime(2024, 9, 1, 12, 0, 0), 
                new DateTime(2024, 10, 1, 12, 0, 0));
            Assert.AreEqual(5, list.Count);
        }
        
        [TestMethod]
        public void SearchProjectByEmployeeID_Found()
        {
            var list = myDao.SearchProjectByEmployeeID("EM025");
            Assert.AreEqual(1, list.Count);
        }

        [TestMethod]
        public void SearchProjectByCreatorID_Found()
        {
            var list = myDao.SearchProjectByCreatorID("EM001");
            Assert.AreEqual(1, list.Count);
        }
        
        private ProjectAssignment Search(ProjectAssignment assign)
        {
            DbConnection dbConnection = new DbConnection();
            string sql = $"SELECT * FROM ProjectAssignments WHERE ProjectID='{assign.ProjID}' AND DepartmentID='{assign.DeptID}'";
            return (ProjectAssignment)dbConnection.GetSingleObject(sql, reader => new ProjectAssignment(reader)); 
        }
        
        private void AssertObject(ProjectAssignment expected, ProjectAssignment actual)
        {
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.ProjID, actual.ProjID);
            Assert.AreEqual(expected.DeptID, actual.DeptID);
        }
    }
}