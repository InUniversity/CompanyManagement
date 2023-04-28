using System.Collections.Generic;
using CompanyManagement.Database.Base;

namespace CompanyManagement.Strategies.UserControls.LeaveListView
{
    public class LeaveListStrategyFactory
    {
        private static readonly Dictionary<string, ILeaveListStrategy> strategies = new()
        {
            {BaseDao.MANAGER_POS_ID, new LeaveListForManager()},
            {BaseDao.DEPARTMENT_HEAD_POS_ID, new LeaveListForDepartmentHead()},
            {BaseDao.EMPLOYEE_POS_ID, new LeaveListForEmployee()}
        };

        public static ILeaveListStrategy Create(string positionID)
        {
            return strategies.TryGetValue(positionID, out var leaveListStrategy) ? leaveListStrategy : null;
        } 
    }
}