using System.ComponentModel;

namespace CompanyManagement.Enums
{
    public enum EPermission
    {
        [Description("Manager")]
        Mgr,
        [Description("Department Head")]
        DepHead, 
        [Description("Normal Employee")] 
        NorEmpl,  
        [Description("Human Resource")] 
        HR, 
        [Description("Not allow")] 
        NotAllow
    }
}