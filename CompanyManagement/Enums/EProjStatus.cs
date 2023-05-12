using System.ComponentModel;

namespace CompanyManagement.Enums
{
    public enum EProjStatus
    {
        [Description("Đang triển khai")]
        InProcess = 1,
        [Description("Đã hoàn thành")]
        Completed = 2,
        [Description("Quá hạn")]
        Overdue = 3,
        [Description("Đang chờ thanh toán")]
        PendingPay = 4,
        [Description("Lên kế hoạch")]
        Planning = 5,
    }
}
