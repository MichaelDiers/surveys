namespace Surveys.Common.Firestore.Models
{
    using Md.Common.Contracts.Model;
    using Md.GoogleCloudFirestore.Logic;
    using Surveys.Common.Contracts;
    using Surveys.Common.Firestore.Contracts;
    using Surveys.Common.Models;

    /// <summary>
    ///     Database access for <see cref="ISurvey" />.
    /// </summary>
    public class SurveyReadOnlyDatabase : ReadonlyDatabase<ISurvey>, ISurveyReadOnlyDatabase
    {
        /// <summary>
        ///     Creates a new instance of <see cref="SurveyReadOnlyDatabase" />.
        /// </summary>
        /// <param name="runtimeEnvironment">The runtime environment.</param>
        public SurveyReadOnlyDatabase(IRuntimeEnvironment runtimeEnvironment)
            : base(runtimeEnvironment, SurveyDatabase.CollectionNameBase, Survey.FromDictionary)
        {
        }
    }
}
