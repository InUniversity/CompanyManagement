using System;
using System.Data;
using System.Data.SqlClient;

namespace CompanyManagement.Utilities
{
    public class Utils
    {
        public const string invalidAccMess = "Tên đăng nhập hoặc mật khẩu không đúng!";
        public const string invalidEmailMess = "Email không hợp lệ!!!";
        public const string invalidPhoneNoMess = "Số điện thoại không hợp lệ!!!";
        public const string invalidBirthdayMess = "Ngày sinh không hợp lệ!!!";
        public const string invalidIdentCardMess = "CMND/CCCD không hợp lệ!!!";
        public const string invalidEmptyMess = "Các thông tin có dấu không được để trống!!!";
        public const string invalidTimeline = "Ngày bắt đầu phải lớn hơn ngày hiện tại và thời gian kết thúc phải lớn hơn ngày bắt đầu!!!";
        public const string invalidIDMess = "ID đã tồn tại!!!";
        public const string invalidCheckInMess = "Chưa chọn nhiệm vụ để điểm danh!";

        public static readonly DateTime emptyDate = new DateTime(2000, 1, 1, 7, 0, 0);
        private const string formatDateTime = "yyyy-MM-dd hh:mm:ss";
        private const string formatDate = "yyyy-MM-dd";

        public static string ToSQLFormat(DateTime dt)
        {
            return dt.ToString(formatDateTime);
        }

        public static int GetInt(IDataRecord record, string colName)
        {
            return GetValueOrDefault(record, colName, 0);
        }

        public static string GetString(IDataRecord record, string colName)
        {
            return GetValueOrDefault(record, colName, string.Empty);
        }
        
        public static DateTime GetDateTime(IDataRecord record, string colName)
        {
            return GetValueOrDefault(record, colName, emptyDate);
        }

        public static decimal GetDecimal(IDataRecord record, string colName)
        {
            return GetValueOrDefault(record, colName, (decimal)0);
        }

        private static T GetValueOrDefault<T>(IDataRecord record, string colName, T defaultVal)
        {
            return record[colName] == DBNull.Value ? defaultVal : (T)record[colName];
        }
    }
}