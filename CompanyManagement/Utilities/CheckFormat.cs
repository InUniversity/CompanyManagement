using System;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace CompanyManagement.Utilities
{
    public class CheckFormat
    {
        private const int IDENTIFY_CARD_LENGTH = 12;
        private const int PASSWORD_MINIMUM_LENGTH = 8;
        private const int AGE_MINIMUM = 18;

        public bool ValidateEmail(string email)
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

        public bool ValidatePhoneNumber(string phoneNumber)
        {
            string pattern = @"^0[0-9]{9}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }

        public bool ValidateIdentifyCard(string identifyCard)
        {
            return identifyCard.Length == IDENTIFY_CARD_LENGTH;
        }

        public bool ValidatePassword(string password)
        {
            return password.Length >= PASSWORD_MINIMUM_LENGTH;
        }

        public bool ValidateBirthday(DateTime birthday)
        {
            DateTime today = DateTime.Now;
            int age = today.Year - birthday.Year;
            if (age < AGE_MINIMUM)
                return false;
            if (age > AGE_MINIMUM)
                return true;
            if (birthday.Month > today.Month)
                return false;
            if (birthday.Month < today.Month)
                return true;
            if (birthday.Day > today.Day)
                return false;
            return true;
        }

        public bool ValidateTimeline(DateTime start, DateTime end)
        {
            return start <= end;
        }
    }
}
