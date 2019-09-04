using System;
using System.Text.RegularExpressions;

namespace BDSA2019.Lecture02
{
    public static class PostalCodeValidator
    {
        public static bool IsValid(string postalCode)
        {
            var pattern = @"^\d{3,4}$";

            return Regex.IsMatch(postalCode, pattern);
        }

        public static bool TryParse(string postalCodeAndLocality,
            out string postalCode,
            out string locality)
        {
            postalCode = null;
            locality = null;

            var pattern = @"<?postalCode>(^\d{3,4}) <?locality>(.+)$";

            var match = Regex.Match(postalCodeAndLocality, pattern);

            if (match.Success)
            {
                postalCode = match.Captures[1].Value;
                locality = match.Captures[2].Value;
            }

            return match.Success;
        }
    }
}
