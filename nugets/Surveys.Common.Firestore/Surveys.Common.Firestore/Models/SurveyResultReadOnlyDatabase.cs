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
    public class SurveyResultReadOnlyDatabase : ReadonlyDatabase<ISurveyResult>, ISurveyResultReadOnlyDatabase
    {
        /// <summary>
        ///     Creates a new instance of <see cref="SurveyResultReadOnlyDatabase" />.
        /// </summary>
        /// <param name="runtimeEnvironment">The runtime environment.</param>
        public SurveyResultReadOnlyDatabase(IRuntimeEnvironment runtimeEnvironment)
            : base(runtimeEnvironment, SurveyResultDatabase.CollectionNameBase, SurveyResult.FromDictionary)
        {
        }
    }
}
