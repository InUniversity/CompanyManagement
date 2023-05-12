using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Entity.Database
{
    public class ProjectAssignmentsDao : BaseDao
    {
        public void Add(ProjectAssignment assign)
        {
            using (var db = new CompanyManagementContext())
            {
                db.ProjectAssignments.Add(assign);
                db.SaveChanges();
            }
        }

        public void Delete(ProjectAssignment assign)
        {
            using (var db = new CompanyManagementContext())
            {
                var item = db.ProjectAssignments.SingleOrDefault(i => i.ProjectID == assign.ProjectID && i.DepartmentID == assign.DepartmentID);
                if (item == null) return;
                db.ProjectAssignments.Remove(item);
                db.SaveChanges();
            }
        }

        public List<Department> GetAllDepartmentInProject(string projID)
        {
            using (var db = new CompanyManagementContext())
            {
                var query = from d in db.Departments
                            join pa in db.ProjectAssignments on d.ID equals pa.DepartmentID
                            where pa.ProjectID == projID
                            select d;
                return query.ToList();
            }
        }

        public List<Employee> GetEmployeesInProject(string projID)
        {
            using (var db = new CompanyManagementContext())
            {
                var query = from e in db.Employees
                            join pa in db.ProjectAssignments on e.DepartmentID equals pa.DepartmentID
                            where pa.ProjectID == projID
                            select e;
                return query.ToList();
            }
        }

        public List<Department> GetDepartmentsCanAssignWork(string projID, DateTime start, DateTime end)
        {
            using (var db = new CompanyManagementContext())
            {
                //TODO
                return db.Departments.ToList();
            }
        }
        public List<Project> SearchProjectByEmployeeID(string emplID)
        {
                using (var db = new CompanyManagementContext())
                {
                    var query = from p in db.Projects 
                                join pa in db.ProjectAssignments on p.ID equals pa.ProjectID
                                join e in db.Employees on pa.DepartmentID equals e.DepartmentID
                                where e.ID == emplID
                                select p;
                    return query.ToList();
                }
        }

        public List<Project> SearchProjectByCreatorID(string mgrID)
        {
            using (var db = new CompanyManagementContext())
            {
                var query = from p in db.Projects
                            where p.OwnerID == mgrID
                            select p;
                return query.ToList();
            }
        }
    }
}
