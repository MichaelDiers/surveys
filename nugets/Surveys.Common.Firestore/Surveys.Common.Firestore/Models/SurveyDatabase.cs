namespace Surveys.Common.Firestore.Models
{
    using Md.Common.Contracts;
    using Md.GoogleCloudFirestore.Logic;
    using Surveys.Common.Contracts;
    using Surveys.Common.Firestore.Contracts;
    using Surveys.Common.Models;

    /// <summary>
    ///     Database access for <see cref="ISurvey" />.
    /// </summary>
    public class SurveyDatabase : Database<ISurvey>, ISurveyDatabase
    {
        /// <summary>
        ///     The base name of the collection.
        /// </summary>
        public const string CollectionNameBase = "surveys";

        /// <summary>
        ///     Creates a new instance of <see cref="SurveyDatabase" />.
        /// </summary>
        /// <param name="runtimeEnvironment">The runtime environment.</param>
        public SurveyDatabase(IRuntimeEnvironment runtimeEnvironment)
            : base(runtimeEnvironment, SurveyDatabase.CollectionNameBase, Survey.FromDictionary)
        {
        }
    }
}
