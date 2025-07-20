
using Microsoft.AspNetCore.Components;
using System.Globalization;
using System.Text;

namespace ValuBakery.Web.Helpers
{
    public static class StringHelper
    {
        public static MarkupString GetShortOrFull(string? content, bool expanded, int maxLength, out bool isTruncated)
        {
            isTruncated = false;

            if (string.IsNullOrWhiteSpace(content))
                return (MarkupString)"";

            if (expanded || content.Length <= maxLength)
                return (MarkupString)content;

            isTruncated = true;
            return (MarkupString)(content.Substring(0, maxLength) + "...");
        }

        public static string RemoveDiacritics(string text)
        {
            var normalized = text.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (var c in normalized)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
