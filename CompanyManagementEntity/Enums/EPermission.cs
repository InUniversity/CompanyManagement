using System.ComponentModel;

namespace CompanyManagement.Enums
{
    public enum EPermission
    {
        [Description("Manager")]
        Mgr = 1,
        [Description("Department Head")]
        DepHead = 2, 
        [Description("Normal Employee")] 
        NorEmpl = 3,  
        [Description("Human Resource")] 
        HR = 4, 
        [Description("Not allow")] 
        NotAllow = 5
    }
}