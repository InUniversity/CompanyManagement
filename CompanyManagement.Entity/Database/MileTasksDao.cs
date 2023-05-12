using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Entity.Database
{
    public class MileTasksDao : BaseDao
    {
        public void Add(MileTask mileTsk)
        {
            using (var db = new CompanyManagementContext())
            {
                db.MileTasks.Add(mileTsk);
                db.SaveChanges();
            }
        }

        public void Delete(MileTask mileTsk)
        {
            using (var db = new CompanyManagementContext())
            {
                var mile = db.MileTasks.SingleOrDefault(m => m.ID == mileTsk.ID && m.TaskID == mileTsk.TaskID);
                if (mile == null) return;
                db.MileTasks.Remove(mile);
                db.SaveChanges();
            }
        }

        public void DeleteByMileID(string mileID)
        {
            using (var db = new CompanyManagementContext())
            {
                var mile = db.MileTasks.SingleOrDefault(m => m.ID == mileID);
                if (mile == null) return;
                db.MileTasks.Remove(mile);
                db.SaveChanges();
            }
        }

        public List<Task> SearchByMileID(string mileID)
        {
            using (var db = new CompanyManagementContext())
            {
                var mileTasks = from m in db.MileTasks where m.ID == mileID select m;
                var query = from t in db.Tasks 
                            join m in mileTasks on t.ID equals m.TaskID select t;
                return query.ToList();
            }
        }

        public List<Task> SearchTaskCompletedByMileID(string mileID)
        {
            using (var db = new CompanyManagementContext())
            {
                var mileTasks = from m in db.MileTasks where m.ID == mileID select m;
                var query = from t in db.Tasks
                            join mt in mileTasks on t.ID equals mt.TaskID
                            where t.Progress == completed
                            select t;
                return query.ToList();
            }
        }

        public List<Task> SearchTasksCanAddToMile(Milestone milestone)
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
    }
}
