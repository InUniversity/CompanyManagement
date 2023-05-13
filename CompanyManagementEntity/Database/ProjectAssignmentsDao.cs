using CompanyManagementEntity.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagementEntity.Database
{
    public class ProjectAssignmentsDao
    {
        public void Add(ProjectAssignment assign)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    db.ProjectAssignments.Add(assign);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
            }        
        }

        public void Delete(ProjectAssignment assign)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    var item = db.ProjectAssignments.SingleOrDefault(i => i.ProjectID == assign.ProjectID && i.DepartmentID == assign.DepartmentID);
                    if (item == null) return;
                    db.ProjectAssignments.Remove(item);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
            }       
        }

        public List<Department> GetAllDepartmentInProject(string projID)
        {
            try
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
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
                return null;
            }     
        }

        public List<Employee> GetEmployeesInProject(string projID)
        {
            try
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
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
                return null;
            }           
        }

        public List<Department> GetDepartmentsCanAssignWork(string projID, DateTime start, DateTime end)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    //TODO
                    return db.Departments.ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
                return null;
            }       
        }
        public List<Project> SearchProjectByEmployeeID(string emplID)
        {
            try
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
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
                return null;
            }         
        }

        public List<Project> SearchProjectByCreatorID(string mgrID)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    var query = from p in db.Projects
                                where p.OwnerID == mgrID
                                select p;
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
                return null;
            }           
        }
    }
}
