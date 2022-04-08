namespace Surveys.Common.Firestore.Contracts
{
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Surveys.Common.Contracts;

    /// <summary>
    ///     Database access for <see cref="ISurvey" />.
    /// </summary>
    public interface ISurveyDatabase : IDatabase<ISurvey>, ISurveyReadOnlyDatabase
    {
    }
}
