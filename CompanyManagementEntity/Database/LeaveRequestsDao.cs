using CompanyManagementEntity.Database.Base;
using CompanyManagementEntity.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagementEntity.Database
{
    public class LeaveRequestsDao : BaseDao<LeaveRequest>
    {
        public void Delete(string leaveID)
        {
            NewDbContext(db =>
            {
                var leave = db.LeaveRequests.SingleOrDefault(l => l.ID == leaveID);
                if (leave == null) return;
                db.LeaveRequests.Remove(leave);
                db.SaveChanges();
            });
        }

        public LeaveRequest SearchByID(string id)
        {
            var item = new LeaveRequest();
            NewDbContext(db =>
            {
                item = db.LeaveRequests.SingleOrDefault(l => l.ID == id);
            });
            return item; 
        }

        public List<LeaveRequest> GetMyRequests(string emplID)
        {
            var listItems = new List<LeaveRequest>();
            NewDbContext(db =>
            {
                var query = from l in db.LeaveRequests where l.EmployeeID == emplID select l;
                listItems = query.ToList();
            });
            return listItems;
        }

        public List<LeaveRequest> SearchByApproverID(string emplID)
        {
            var listItems = new List<LeaveRequest>();
            NewDbContext(db =>
            {
                var query = from l in db.LeaveRequests where l.ApproverID == emplID select l;
                listItems = query.ToList();
            });
            return listItems;
        }
    }
}
