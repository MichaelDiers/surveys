namespace Surveys.Common.Firestore.Contracts
{
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Surveys.Common.Contracts;

    /// <summary>
    ///     Database access for <see cref="ISurveyResult" />.
    /// </summary>
    public interface ISurveyResultReadOnlyDatabase : IReadOnlyDatabase<ISurveyResult>
    {
    }
}
