using System.ComponentModel;

namespace CompanyManagement.Enums
{
    public enum ETaskStatus
    {
        [Description("Đang thực hiện")]
        InProcess = 1,
        [Description("Đã hoàn thành")]
        Completed = 2,
        [Description("Quá hạn")]
        Overdue = 3,
        [Description("Đang xem xét")]
        Reviewing = 4,
        [Description("Hủy")]
        Cancelled = 5
    }
}