namespace Surveys.Common.Firestore.Models
{
    using Md.Common.Contracts.Model;
    using Md.GoogleCloudFirestore.Logic;
    using Surveys.Common.Contracts;
    using Surveys.Common.Firestore.Contracts;
    using Surveys.Common.Models;

    /// <summary>
    ///     Database access for <see cref="ISurveyResult" />.
    /// </summary>
    public class SurveyResultDatabase : Database<ISurveyResult>, ISurveyResultDatabase
    {
        /// <summary>
        ///     The base name of the collection.
        /// </summary>
        public const string CollectionNameBase = "survey-results";

        /// <summary>
        ///     Creates a new instance of <see cref="SurveyResultDatabase" />.
        /// </summary>
        /// <param name="runtimeEnvironment">The runtime environment.</param>
        public SurveyResultDatabase(IRuntimeEnvironment runtimeEnvironment)
            : base(runtimeEnvironment, SurveyResultDatabase.CollectionNameBase, SurveyResult.FromDictionary)
        {
        }
    }
}
