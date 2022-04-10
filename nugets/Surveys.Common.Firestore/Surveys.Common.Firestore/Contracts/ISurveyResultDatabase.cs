namespace Surveys.Common.Firestore.Contracts
{
    using Md.GoogleCloudFirestore.Contracts.Logic;
    using Surveys.Common.Contracts;

    /// <summary>
    ///     Database access for <see cref="ISurveyResult" />.
    /// </summary>
    public interface ISurveyResultDatabase : IDatabase<ISurveyResult>, ISurveyResultReadOnlyDatabase
    {
    }
}
