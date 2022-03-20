namespace EvaluateSurveySubscriber.Contracts
{
    using Md.GoogleCloud.Base.Contracts.Logic;

    /// <summary>
    ///     Describes a database for reading survey results.
    /// </summary>
    public interface ISurveyResultsDatabase : IReadOnlyDatabase
    {
    }
}
