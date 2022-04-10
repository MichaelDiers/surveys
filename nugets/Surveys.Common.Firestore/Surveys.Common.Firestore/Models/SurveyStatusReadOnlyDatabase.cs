namespace Surveys.Common.Firestore.Models
{
    using Md.Common.Contracts.Model;
    using Md.GoogleCloudFirestore.Logic;
    using Surveys.Common.Contracts;
    using Surveys.Common.Firestore.Contracts;
    using Surveys.Common.Models;

    /// <summary>
    ///     Database access for <see cref="ISurveyStatus" />.
    /// </summary>
    public class SurveyStatusReadOnlyDatabase : ReadonlyDatabase<ISurveyStatus>, ISurveyStatusReadOnlyDatabase
    {
        /// <summary>
        ///     Creates a new instance of <see cref="SurveyStatusReadOnlyDatabase" />.
        /// </summary>
        /// <param name="runtimeEnvironment">The runtime environment.</param>
        public SurveyStatusReadOnlyDatabase(IRuntimeEnvironment runtimeEnvironment)
            : base(runtimeEnvironment, SurveyStatusDatabase.CollectionNameBase, SurveyStatus.FromDictionary)
        {
        }
    }
}
