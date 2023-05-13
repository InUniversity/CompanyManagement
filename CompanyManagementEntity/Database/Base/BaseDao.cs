using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagementEntity.Database.Base
{
    public abstract class BaseDao
    {
        public const string managerRole = "ER01";
        public const string deptHeadRole = "ER02";
        public const string hrRole = "ER03";
        public const string regularEmplRole = "ER04";

        public const string completed = "100";
        public const string leavRequestUpapproved = "LS2";
        public const string leavRequesApproved = "LS1";
        public const string leavRequesDenied = "LS3";
        public const string projRunningID = "PST1";
        public const string projCompletedID = "PST2";
        public const string projOverdueID = "PST3";
        public const string projPendingPayID = "PST4";
        public const string projInProgressID = "PST5";
    }
}
