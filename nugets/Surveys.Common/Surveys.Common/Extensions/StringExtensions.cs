namespace Surveys.Common.Extensions
{
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

            return $"{s.Substring(0, 1)}{s.Substring(1)}";
        }
    }
}
