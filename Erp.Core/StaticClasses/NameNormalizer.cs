using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Core.StaticClasses
{
    public static class NameNormalizer
    {
        public static string Normalize(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return string.Empty;

            return name
                .Replace(" ", "")
                .ToLowerInvariant()
                .TrimEnd('.');
        }
    }
}
