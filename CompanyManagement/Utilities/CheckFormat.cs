using System.Net.Mail;
using System.Text.RegularExpressions;

namespace CompanyManagement.Utilities
{
    public static class CheckFormat
    {
        private const int IDENTIFY_CARD_LENGTH = 12;
        private const int PASSWORD_MINIMUM_LENGTH = 8;

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
            return Regex.IsMatch(phoneNumber, pattern);
        }

        public static bool ValidateIdentifyCard(string identifyCard)
        {
            return identifyCard.Length == IDENTIFY_CARD_LENGTH;
        }

        public static bool ValidatePassword(string password)
        {
            return password.Length >= PASSWORD_MINIMUM_LENGTH;
        }
    }
}
