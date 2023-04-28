using System;

namespace CompanyManagement.Utilities
{
    public class Utils
    {
        public const string INVALIDATE_USERNAME_PASSWORD_MESSAGE = "Tên đăng nhập hoặc mật khẩu không đúng!";
        public const string INVALIDATE_EMAIL_MESSAGE = "Email không hợp lệ!!!";
        public const string INVALIDATE_PHONE_NUMBER_MESSAGE = "Số điện thoại không hợp lệ!!!";
        public const string INVALIDATE_BIRTHDAY_MESSAGE = "Ngày sinh không hợp lệ!!!";
        public const string INVALIDATE_IDENTIFY_CARD_MESSAGE = "CMND/CCCD không hợp lệ!!!";
        public const string INVALIDATE_EMPTY_MESSAGE = "Các thông tin có dấu (*) không được để trống!!!";
        public const string INVALIDATE_TIMELINE = "Ngày bắt đầu phải lớn hơn ngày hiện tại và thời gian kết thúc phải lớn hơn ngày bắt đầu!!!";
        public const string EXIST_ID_MESSAGE = "ID đã tồn tại!!!";
        public const string EXIST_IDENTIFY_CARD_MESSAGE = "CMND/CCCD đã tồn tại!!!";
        public const string EXIST_PHONE_NUMBER_MESSAGE = "Số điện thoại đã tồn tại!!!";
        public const string INVALIDATE_TASK_CHECK_IN = "Chưa chọn nhiệm vụ để điểm danh!";

        public static readonly DateTime EMPTY_DATETIME = new DateTime(2000, 1, 1, 0, 0, 0);
        private const string FORMAT_DATETIME = "yyyy-MM-dd hh:mm:ss";

        public static string ToFormatSQLServer(DateTime dateTime)
        {
            return dateTime.ToString(FORMAT_DATETIME);
        }
    }
}