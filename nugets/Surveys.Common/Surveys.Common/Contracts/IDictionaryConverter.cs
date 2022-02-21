namespace Surveys.Common.Contracts
{
    using System.Collections.Generic;

    /// <summary>
    ///     Convert the values of an object to a dictionary.
    /// </summary>
    public interface IDictionaryConverter
    {
        /// <summary>
        ///     Add the object to a dictionary.
        /// </summary>
        /// <param name="document">The data is added to the given dictionary.</param>
        void AddToDictionary(Dictionary<string, object> document);

        /// <summary>
        ///     Convert the object values to a dictionary.
        /// </summary>
        /// <returns>A <see cref="Dictionary{TKey,TValue}" />.</returns>
        Dictionary<string, object> ToDictionary();
    }
}
