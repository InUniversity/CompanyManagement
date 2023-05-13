using CompanyManagement.Enums;
using CompanyManagementEntity.Database.Base;
using CompanyManagementEntity.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;

namespace CompanyManagementEntity.Database
{
    public class MileTasksDao : BaseDao<MileTask>
    {
        public void Delete(MileTask mileTsk)
        {
            NewDbContext(db =>
            {
                var mile = db.MileTasks.SingleOrDefault(m => m.MileID == mileTsk.MileID && m.TaskID == mileTsk.TaskID);
                if (mile == null) return;
                db.MileTasks.Remove(mile);
                db.SaveChanges();
            });
        }

        public void DeleteByMileID(string mileID)
        {
            NewDbContext(db =>
            {
                var mile = db.MileTasks.SingleOrDefault(m => m.MileID == mileID);
                if (mile == null) return;
                db.MileTasks.Remove(mile);
                db.SaveChanges();
            });
        }

        public List<Task> SearchByMileID(string mileID)
        {
            var listItems = new List<Task>();
            NewDbContext(db =>
            {
                var listTaskID = from m in db.MileTasks where m.MileID == mileID select m.TaskID;
                var query = from t in db.Tasks
                            where listTaskID.ToList().Contains(t.ID)
                            select t;
                listItems = query.ToList();
            });
            return listItems;
        }

        public List<Task> SearchTaskCompletedByMileID(string mileID)
        {
            var listItems = new List<Task>();
            NewDbContext(db =>
            {
                var listTaskID = from m in db.MileTasks where m.MileID == mileID select m.TaskID;
                var query = from t in db.Tasks
                            where listTaskID.ToList().Contains(t.ID)
                            && t.StatusID == (int)ETaskStatus.Completed
                            select t;
                listItems = query.ToList();
            });
            return listItems;
        }

        public List<Task> SearchTasksCanAddToMile(Milestone milestone)
        {
            var listItems = new List<Task>();
            NewDbContext(db =>
            {
                var miles = from m in db.Milestones where m.ProjectID == milestone.ProjectID select m;
                var listTaskID = from mt in db.MileTasks
                                join m in miles on mt.MileID equals m.ID
                                select mt.TaskID;
                var query = from t in db.Tasks
                            where !listTaskID.ToList().Contains(t.ID)
                            && t.ProjectID == milestone.ProjectID
                            && t.Deadline <= milestone.EndDate
                            && t.StartDate >= milestone.StartDate
                            select t;
                listItems = query.ToList();
            });
            return listItems;
        }
    }
}
