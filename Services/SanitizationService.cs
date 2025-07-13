using System.Text.RegularExpressions;

namespace SafeVault.Services
{
    public class SanitizationService
    {
        public string SanitizeInput(string input)
        {
            // Identify possibly harmful characters with a regular expression, and remove them.
            var sanitized = Regex.Replace(input, @"[<>""';]", string.Empty);
            return sanitized.Trim();
        }

        public bool ValidEmail(string input)
        {
            var emailRegex = @"^[^\s@]+@[^\s@]+\.[^\s@]+$";
            return Regex.IsMatch(input, emailRegex);
        }

        public bool ValidUsername(string input)
        {
            var userRegex = @"^[a-zA-Z0-9]+$";
            return Regex.IsMatch(input, userRegex);
        }
    }
}