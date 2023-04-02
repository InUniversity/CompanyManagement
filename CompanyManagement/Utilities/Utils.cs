using System;
using System.Globalization;

namespace CompanyManagement.Utilities
{
    public class Utils
    {
        public const string INVALIDATE_USERNAME_PASSWORD_MESSAGE = "Tên đăng nhập hoặc mật khẩu không đúng!";
        public const string INVALIDATE_EMAIL_MESSAGE = "Email không hợp lệ!!!";
        public const string INVALIDATE_PHONE_NUMBER_MESSAGE = "Số điện thoại không hợp lệ!!!";
        public const string INVALIDATE_BIRTHDAY_MESSAGE = "Ngày sinh không hợp lệ!!!";
        public const string INVALIDATE_IDENTIFY_CARD_MESSAGE = "CMND/CCCD không hợp lệ!!!";
        public const string INVALIDATE_PASSWORK_MESSAGE = "Mật khẩu không hợp lệ!!!";
        public const string INVALIDATE_EMPTY_MESSAGE = "Các thông tin không được để trống!!!";
        public const string EXIST_ID_MESSAGE = "ID đã tồn tại!!!";
        public const string EXIST_IDENTIFY_CARD_MESSAGE = "CMND/CCCD đã tồn tại!!!";
        public const string EXIST_PHONE_NUMBER_MESSAGE = "Số điện thoại đã tồn tại!!!";

        private const string FORMAT_DATEONLY = "dd-MM-yyyy";
        private const string FORMAT_TIMEONLY = "hh:mm tt";
        private const string FORMAT_DATETIME = "dd-MM-yyyy hh:mm tt";
        
        public static string DateToString(DateTime dateOnly)
        {
            return dateOnly.ToString(FORMAT_DATEONLY);
        }

        public static DateTime StringToDate(string dateOnlyStr)
        {
            DateTime result;
            bool canParse = DateTime.TryParseExact(dateOnlyStr, FORMAT_DATEONLY, CultureInfo.CurrentCulture, DateTimeStyles.None, out result);
            return canParse ? result : DateTime.Now;
        }
        
        public static string DateTimeToString(DateTime dateTime)
        {
            return dateTime.ToString(FORMAT_DATETIME);
        }

        public static DateTime StringToDateTime(string dateTimeStr)
        {
            DateTime result;
            bool canParse = DateTime.TryParseExact(dateTimeStr, FORMAT_DATETIME, CultureInfo.CurrentCulture, DateTimeStyles.None, out result);
            return canParse ? result : DateTime.Now;
        }

        public static string TimeToString(DateTime dateTime)
        {
            return dateTime.ToString(FORMAT_TIMEONLY, CultureInfo.CurrentCulture);
        }
        
        public static DateTime StringToTime(string timeStr)
        {
            DateTime result;
            bool canParse = DateTime.TryParseExact(timeStr, FORMAT_TIMEONLY, CultureInfo.CurrentCulture, DateTimeStyles.None, out result);
            return canParse ? result : DateTime.Now;
        }
    }
}