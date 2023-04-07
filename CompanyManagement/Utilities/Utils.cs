using System;
using System.Diagnostics;
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
        public const string INVALIDATE_EMPTY_MESSAGE = "Các thông tin không được để trống!!!";
        public const string EXIST_ID_MESSAGE = "ID đã tồn tại!!!";
        public const string EXIST_IDENTIFY_CARD_MESSAGE = "CMND/CCCD đã tồn tại!!!";
        public const string EXIST_PHONE_NUMBER_MESSAGE = "Số điện thoại đã tồn tại!!!";

        public static readonly DateTime EMPTY_DATETIME = new DateTime(2000, 1, 1, 0, 0, 0);
        private const string FORMAT_DATEONLY = "dd-MM-yyyy";
        private const string FORMAT_TIMEONLY = "hh:mm tt";
        private const string FORMAT_DATETIME = "dd-MM-yyyy hh:mm tt";

        public const string POSITION_ID_EMPLOYEE = "3";

        public static DateOnly DateTimeToDateOnly(DateTime dateTime)
        {
            return new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);
        }

        public static TimeOnly TimeSpanToTimeOnly(TimeSpan timeSpan)
        {
            DateTime time = DateTime.Now.Add(timeSpan);
            return StringToTimeOnly(DateTimeToString(time));
        }

        private static TimeOnly StringToTimeOnly(string timeStr)
        {
            TimeOnly result;
            bool canParse = TimeOnly.TryParseExact(timeStr, FORMAT_TIMEONLY, CultureInfo.CurrentCulture, DateTimeStyles.None, out result);
            return canParse ? result : TimeOnly.MinValue;
        }
        
        private static string DateTimeToString(DateTime dateTime)
        {
            return dateTime.ToString(FORMAT_DATETIME);
        }
    }
}