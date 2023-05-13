using CompanyManagementEntity.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CompanyManagementEntity.Database
{
    public class ProjectBonusesDao
    {
        public void Add(ProjectBonus projectBonuses)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    db.ProjectBonuses.Add(projectBonuses);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
            }         
        }

        public void Delete(string id)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    var item = db.ProjectBonuses.SingleOrDefault(pb => pb.ID == id);
                    if (item == null) return;
                    db.ProjectBonuses.Remove(item);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
            }        
        }

        public void Update(ProjectBonus projectBonuses)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    db.Entry(projectBonuses).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
            }        
        }

        public ProjectBonus SearchByID(string id)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    return db.ProjectBonuses.SingleOrDefault(pb => pb.ID == id);
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
                return  null;
            }            
        }

        public List<ProjectBonus> SearchByProjectID(string id)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    var query = from pb in db.ProjectBonuses where pb.ProjectID == id select pb;
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
                return null;
            }       
        }

        public List<ProjectBonus> SearchByEmployeeID(string id)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    var query = from pb in db.ProjectBonuses where pb.EmployeeID == id select pb;
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
                return null;
            }          
        }

        public void DeleteByProjectID(string id)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    var item = db.ProjectBonuses.SingleOrDefault(pb => pb.ProjectID == id);
                    if (item == null) return;
                    db.ProjectBonuses.Remove(item);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);               
            }           
        }

        public decimal ToTalBonusesOfEmployeeByTime(string employeeID, int month, int year)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    var query = from pb in db.ProjectBonuses
                                where pb.EmployeeID == employeeID
                                && pb.ReceivedDate.Value.Month == month
                                && pb.ReceivedDate.Value.Year == year
                                select pb;
                    return query.Sum(pb => pb.Amount).Value;
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
                return 0;
            }           
        }

        public List<ProjectBonus> GetOfEmployeeByTime(string employeeID, int month, int year)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    var query = from pb in db.ProjectBonuses
                                where pb.EmployeeID == employeeID
                                && pb.ReceivedDate.Value.Month == month
                                && pb.ReceivedDate.Value.Year == year
                                select pb;
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
