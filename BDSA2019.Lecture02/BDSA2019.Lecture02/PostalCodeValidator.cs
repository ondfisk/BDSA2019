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
            var pattern = @"^(?<postalCode>\d{3,4}) (?<locality>.+)$";

            var match = Regex.Match(postalCodeAndLocality, pattern);

            if (match.Success)
            {
                var groups = match.Groups;

                postalCode = groups["postalCode"].Value;
                locality = groups["locality"].Value;
            }
            else
            {
                postalCode = null;
                locality = null;
            }

            return match.Success;
        }
    }
}
