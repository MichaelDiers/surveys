namespace Surveys.Common.Firestore.Contracts
{
    using Md.GoogleCloudFirestore.Contracts.Logic;
    using Surveys.Common.Contracts;

    /// <summary>
    ///     Database access for <see cref="ISurveyStatus" />.
    /// </summary>
    public interface ISurveyStatusReadOnlyDatabase : IReadOnlyDatabase<ISurveyStatus>
    {
    }
}
