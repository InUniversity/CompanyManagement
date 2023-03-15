using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CompanyManagement
{
    public static class CheckFormat
    {
        public static bool ValidateEmail(string email)
        {
            try
            {
                MailAddress checkmail = new MailAddress(email);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public static bool ValidatePhoneNumber(string phoneNumber)
        {
            string pattern = @"^0[0-9]{9}$";
            if (!Regex.IsMatch(phoneNumber, pattern))
                return false;
            return true;
        }
        public static bool ValidateIdentifyCard(string identifyCard)
        {
            if (identifyCard.Length == 12)
                return true;
            return false;
        }
    }
}
