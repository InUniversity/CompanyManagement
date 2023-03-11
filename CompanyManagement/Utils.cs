using System;

namespace CompanyManagement;

public class Utils
{
    
    public static string DateToString(DateTime dateTime)
    {
        return dateTime.ToString("dd-MM-yyyy");
    }
}