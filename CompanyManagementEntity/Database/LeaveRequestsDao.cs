using CompanyManagementEntity.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagementEntity.Database
{
    public class LeaveRequestsDao
    {
        public void Add(LeaveRequest request)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    db.LeaveRequests.Add(request);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
            }    
        }

        public void Delete(string leaveID)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    var leave = db.LeaveRequests.SingleOrDefault(l => l.ID == leaveID);
                    if (leave == null) return;
                    db.LeaveRequests.Remove(leave);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
            }          
        }

        public void Update(LeaveRequest request)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    db.Entry(request).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
            }            
        }

        public LeaveRequest SearchByID(string id)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    return db.LeaveRequests.SingleOrDefault(l => l.ID == id);
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
                return null;
            }           
        }

        public List<LeaveRequest> GetMyRequests(string emplID)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    var query = from l in db.LeaveRequests where l.EmployeeID == emplID select l;
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(EmployeesDao), ex.Message);
                return null;
            }          
        }

        public List<LeaveRequest> SearchByApproverID(string emplID)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    var query = from l in db.LeaveRequests where l.ApproverID == emplID select l;
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
