using System;
using System.Globalization;

namespace CompanyManagement;

public class Utils
{
    
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