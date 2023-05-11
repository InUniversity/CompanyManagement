using System.ComponentModel;

namespace CompanyManagement.Models
{
    public enum Permission
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