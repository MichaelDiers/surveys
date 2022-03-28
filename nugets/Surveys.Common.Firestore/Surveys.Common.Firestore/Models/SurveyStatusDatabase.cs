namespace Surveys.Common.Firestore.Models
{
    using Md.Common.Contracts;
    using Md.GoogleCloudFirestore.Logic;
    using Surveys.Common.Contracts;
    using Surveys.Common.Firestore.Contracts;
    using Surveys.Common.Models;

    /// <summary>
    ///     Database access for <see cref="ISurveyStatus" />.
    /// </summary>
    public class SurveyStatusDatabase : Database<ISurveyStatus>, ISurveyStatusDatabase
    {
        /// <summary>
        ///     The base name of the collection.
        /// </summary>
        public const string CollectionNameBase = "survey-status";

        /// <summary>
        ///     Creates a new instance of <see cref="SurveyStatusDatabase" />.
        /// </summary>
        /// <param name="runtimeEnvironment">The runtime environment.</param>
        public SurveyStatusDatabase(IRuntimeEnvironment runtimeEnvironment)
            : base(runtimeEnvironment, SurveyStatusDatabase.CollectionNameBase, SurveyStatus.FromDictionary)
        {
        }
    }
}
