using System.Globalization;

namespace ValuBakery.Data.Helpers
{
    public static class StringHelper
    {
        public static readonly string OnlyDatePattern = "yyyy-MM-dd";

        public static DateTime ToDate(this string input)
        {
            return DateTime.TryParseExact(
                input,
                OnlyDatePattern,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime convertedInput) ? convertedInput : default;
        }

        public static long ToLong(this string input)
        {
            return long.TryParse(input, out long convertedInput) ? convertedInput : default;
        }

        public static int ToInt(this string input)
        {
            return int.TryParse(input, NumberStyles.Integer, CultureInfo.InvariantCulture, out int convertedInput) ? convertedInput : default;
        }

        public static decimal ToDecimal(this string input)
        {
            return decimal.TryParse(input, NumberStyles.Currency, CultureInfo.InvariantCulture, out decimal exchangerate) ? exchangerate : default;
        }

        public static Guid ToGuid(this string input)
        {
            return Guid.TryParse(input, out Guid convertedInput) ? convertedInput : default;
        }

        public static int? ToIntNulleable(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }

            int? result = input.ToInt();

            return result != default ? result : null;
        }

        public static DateTime? ToDateNulleable(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }

            DateTime.TryParseExact(input, OnlyDatePattern, null, DateTimeStyles.None, out DateTime dateTimeValue);

            return dateTimeValue;
        }

        public static Guid? ToGuidNulleable(this string input)
        {
            Guid? result = null;

            if (!string.IsNullOrEmpty(input))
            {
                result = ToGuid(input);
            }

            return result != Guid.Empty ? result : null;
        }

        public static bool IsInt(string numb)
        {
            return int.TryParse(numb, out int _);
        }

        public static bool IsDate(string date)
        {
            return DateTime.TryParseExact(
                date,
                OnlyDatePattern,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime _);
        }

        public static bool IsGuid(string guid)
        {
            return Guid.TryParse(guid, out Guid _);
        }

        public static Guid? ToNullableGuidOrEmpty(this string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (Guid.TryParse(input, out Guid result))
                {
                    return result;
                }
                else
                {
                    return Guid.Empty;
                }
            }

            return default;
        }
    }
}
