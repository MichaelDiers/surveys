namespace Surveys.Common.Firestore.Contracts
{
    using System.Threading.Tasks;
    using Md.GoogleCloudFirestore.Contracts.Logic;
    using Surveys.Common.Contracts;

    /// <summary>
    ///     Database access for <see cref="ISurveyStatus" />.
    /// </summary>
    public interface ISurveyStatusDatabase : IDatabase<ISurveyStatus>, ISurveyStatusReadOnlyDatabase
    {
        Task<string?> InsertIfNotExistsAsync(ISurveyStatus surveyStatus);
    }
}
