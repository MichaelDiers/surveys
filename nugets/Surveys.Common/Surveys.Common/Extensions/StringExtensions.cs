namespace Surveys.Common.Extensions
{
    using System;

    /// <summary>
    ///     Extensions for <see cref="string" />.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        ///     Convert first character of the given string to lower.
        /// </summary>
        /// <param name="s">The string to process.</param>
        /// <returns>A new <see cref="string" />.</returns>
        public static string FirstCharacterToLower(this string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return s;
            }

            return $"{s.Substring(0, 1).ToLower()}{s.Substring(1)}";
        }

        /// <summary>
        ///     Throws a <see cref="ArgumentException" /> if <paramref name="s" /> is not a valid guid.
        /// </summary>
        /// <param name="s">The string to check for a valid guid.</param>
        public static void ThrowExceptionIfGuidIsInvalid(this string s)
        {
            if (string.IsNullOrWhiteSpace(s) || !Guid.TryParse(s, out var guid) || guid == Guid.Empty)
            {
                throw new ArgumentException($"Value is not a valid guid: {s}", nameof(s));
            }
        }
    }
}
