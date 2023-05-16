using System.ComponentModel;

namespace CompanyManagement.Enums
{
    public enum ELeavStatus
    {
        [Description("Chấp nhận")]
        Approved = 1,
        [Description("Chưa giải quyết")]
        Unapproved = 2,
        [Description("Từ chối")]
        Denied = 3
    }
}