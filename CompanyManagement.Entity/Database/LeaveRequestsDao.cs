using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Entity.Database
{
    public class LeaveRequestsDao : BaseDao
    {
        public void Add(LeaveRequest request)
        {
            using (var db = new CompanyManagementContext())
            {
                db.LeaveRequests.Add(request);
                db.SaveChanges();
            }
        }

        public void Delete(string leaveID)
        {
            using (var db = new CompanyManagementContext())
            {
                var leave = db.LeaveRequests.SingleOrDefault(l => l.ID == leaveID);
                if (leave == null) return;
                db.LeaveRequests.Remove(leave);
                db.SaveChanges();
            }
        }

        public void Update(LeaveRequest request)
        {
            using (var db = new CompanyManagementContext())
            {
                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public LeaveRequest SearchByID(string id)
        {
            using (var db = new CompanyManagementContext())
            {
                return db.LeaveRequests.SingleOrDefault(l => l.ID == id);
            }
        }

        public List<LeaveRequest> GetMyRequests(string emplID)
        {
            using (var db = new CompanyManagementContext())
            {
                var query = from l in db.LeaveRequests where l.EmployeeID == emplID select l ;
                return query.ToList();
            }
        }

        public List<LeaveRequest> SearchByApproverID(string emplID)
        {
            using (var db = new CompanyManagementContext())
            {
                var query = from l in db.LeaveRequests where l.ApproverID == emplID select l;
                return query.ToList();
            }
        }
    }
}
