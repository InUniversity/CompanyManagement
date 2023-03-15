using System;
using System.Globalization;

namespace CompanyManagement;

public class Utils
{
    public const string INVALIDATE_EMAIL_MESSAGE = "Email không hợp lệ!!!";
    public const string INVALIDATE_PHONE_NUMBER_MESSAGE = "Số điện thoại không hợp lệ!!!";
    public const string INVALIDATE_BIRTHDAY_MESSAGE = "Ngày sinh không hợp lệ!!!";
    public const string INVALIDATE_IDENTIFY_CARD_MESSAGE = "CMND/CCCD không hợp lệ!!!";
    public const string INVALIDATE_EMPTY_MESSAGE = "Các thông tin không được để trống!!!";
    public const string EXIST_ID_MESSAGE = "ID đã tồn tại!!!";
    public const string EXIST_IDENTIFY_CARD_MESSAGE = "CMND/CCCD đã tồn tại!!!";
    public const string EXIST_PHONE_NUMBER_MESSAGE = "Số điện thoại đã tồn tại!!!";

    private const string FORMAT_DATE = "dd-MM-yyyy";
    
    public static string DateToString(DateTime dateTime)
    {
        return dateTime.ToString(FORMAT_DATE);
    }

    public static DateTime StringToDate(string dateOnly)
    {
        return DateTime.ParseExact(dateOnly, FORMAT_DATE, CultureInfo.InvariantCulture);
    }
}