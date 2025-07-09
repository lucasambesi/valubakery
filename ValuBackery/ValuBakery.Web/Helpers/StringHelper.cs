
using Microsoft.AspNetCore.Components;

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
    }
}
