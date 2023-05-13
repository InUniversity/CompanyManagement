using CompanyManagement.Enums;
using CompanyManagementEntity.Database.Base;
using CompanyManagementEntity.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagementEntity.Database
{
    public class ProjectAssignmentsDao : BaseDao<ProjectAssignment>
    {
        public void Delete(ProjectAssignment assign)
        {
            NewDbContext(db =>
            {
                var item = db.ProjectAssignments.SingleOrDefault(i => i.ProjectID == assign.ProjectID && i.DepartmentID == assign.DepartmentID);
                if (item == null) return;
                db.ProjectAssignments.Remove(item);
                db.SaveChanges();
            });
        }

        public List<Department> GetAllDepartmentInProject(string projID)
        {
            var listItems = new List<Department>();
            NewDbContext(db =>
            {
                var query = from d in db.Departments
                            join pa in db.ProjectAssignments on d.ID equals pa.DepartmentID
                            where pa.ProjectID == projID
                            select d;
                listItems = query.ToList();
            });
            return listItems;
        }

        public List<Employee> GetEmployeesInProject(string projID)
        {
            var listItems = new List<Employee>();
            NewDbContext(db =>
            {
                var listDeptID = from pa in db.ProjectAssignments where pa.ProjectID == projID select pa.DepartmentID;
                var query = from e in db.Employees
                            where listDeptID.ToList().Contains(e.DepartmentID)
                            select e;
                listItems = query.ToList();
            });
            return listItems;
        }

        public List<Department> GetDepartmentsCanAssignWork(string projID, DateTime start, DateTime end)
        {
            var listItems = new List<Department>();
            NewDbContext(db =>
            {
                var query = from dept in db.Departments
                            where !(from projAssign in db.ProjectAssignments
                                    where (from proj in db.Projects
                                           where proj.ID != projID &&
                                                 proj.StatusID != (int)EProjStatus.Completed &&
                                                 proj.StartDate <= start &&
                                                 proj.EndDate >= end
                                           select proj.ID).Contains(projAssign.ProjectID)
                                    select projAssign.DepartmentID).Contains(dept.ID)
                            select dept;
                var exceptQuery = from dept in db.Departments
                                  join projAssign in db.ProjectAssignments on dept.ID equals projAssign.DepartmentID
                                  where projAssign.ProjectID == projID
                                  select dept;
                var result = query.Except(exceptQuery);
                listItems = result.ToList();
            });
            return listItems;
        }
        public List<Project> SearchProjectByEmployeeID(string emplID)
        {
            var listItems = new List<Project>();
            NewDbContext(db =>
            {
                var listDeptID = from pa in db.ProjectAssignments
                                join e in db.Employees on pa.DepartmentID equals e.DepartmentID
                                where e.ID == emplID
                                select pa.ProjectID;
                var query = from p in db.Projects
                            where listDeptID.ToList().Contains(p.ID)
                            select p;
                listItems = query.ToList();
            });
            return listItems;
        }

        public List<Project> SearchProjectByCreatorID(string mgrID)
        {
            var listItems = new List<Project>();
            NewDbContext(db =>
            {
                var query = from p in db.Projects
                            where p.OwnerID == mgrID
                            select p;
                listItems = query.ToList();
            });
            return listItems;
        }
    }
}
