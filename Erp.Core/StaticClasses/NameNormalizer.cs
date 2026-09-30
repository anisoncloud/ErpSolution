using System;
using System.Collections.Generic;
using System.Text;

namespace Erp.Core.StaticClasses
{
    public static class NameNormalizer
    {
        public static string Normalize(string? name)
        {
            /*if (string.IsNullOrWhiteSpace(name))
                return string.Empty;

            return name
                .Replace(" ", "")
                .ToLowerInvariant()
                .TrimEnd('.');*/
            if (string.IsNullOrWhiteSpace(name))
                return string.Empty;

            var sb = new StringBuilder(name.Length);

            foreach (var c in name)
            {
                if (char.IsWhiteSpace(c) || c == '.' || c == '·')
                    continue;

                sb.Append(char.ToLowerInvariant(c));
            }

            return sb.ToString();
        }
    }
}
