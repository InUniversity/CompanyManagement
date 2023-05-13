using CompanyManagement.Enums;
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
                    var listItems = from pa in db.ProjectAssignments where pa.ProjectID == projID select pa.DepartmentID;
                    var query = from e in db.Employees
                                where listItems.ToList().Contains(e.DepartmentID)
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
                    return result.ToList();
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
                    var listItems = from pa in db.ProjectAssignments
                                    join e in db.Employees on pa.DepartmentID equals e.DepartmentID
                                    where e.ID == emplID
                                    select pa.ProjectID;
                    var query = from p in db.Projects
                                where listItems.ToList().Contains(p.ID)
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
