using CompanyManagement.Enums;
using CompanyManagementEntity.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyManagementEntity.Database
{
    public class MileTasksDao
    {
        public void Add(MileTask mileTsk)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    db.MileTasks.Add(mileTsk);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
            }         
        }

        public void Delete(MileTask mileTsk)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    var mile = db.MileTasks.SingleOrDefault(m => m.MileID == mileTsk.MileID && m.TaskID == mileTsk.TaskID);
                    if (mile == null) return;
                    db.MileTasks.Remove(mile);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
            }  
        }

        public void DeleteByMileID(string mileID)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    var mile = db.MileTasks.SingleOrDefault(m => m.MileID == mileID);
                    if (mile == null) return;
                    db.MileTasks.Remove(mile);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
            }        
        }

        public List<Task> SearchByMileID(string mileID)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    var mileTasks = from m in db.MileTasks where m.MileID == mileID select m;
                    var query = from t in db.Tasks
                                join m in mileTasks on t.ID equals m.TaskID
                                select t;
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
                return null;
            }          
        }

        public List<Task> SearchTaskCompletedByMileID(string mileID)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    var mileTasks = from m in db.MileTasks where m.MileID == mileID select m;
                    var query = from t in db.Tasks
                                join mt in mileTasks on t.ID equals mt.TaskID
                                where t.StatusID == (int)ETaskStatus.Completed
                                select t;
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
                return null;
            }        
        }

        public List<Task> SearchTasksCanAddToMile(Milestone milestone)
        {
            try
            {
                //TODO
                using (var db = new CompanyManagementContext())
                {
                    //var miles = from m in db.Milestones where m.ProjectID == milestone.ProjectID select m;
                    //var mileTasks = from mt in db.MileTasks
                    //                join m in miles on mt.ID equals m.ID
                    //                select mt;
                    //var query = from t in db.Tasks
                    //            join mt in mileTasks on t.ID equals mt.TaskID
                    //            where t.ProjectID == milestone.ProjectID
                    //            && t.Deadline <= milestone.EndDate
                    //            && t.StartDate >= milestone.StartDate
                    //            select t;
                    //return query.ToList();
                    return db.Tasks.ToList();
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
