namespace EvaluateSurveySubscriber.Contracts
{
    using Md.GoogleCloud.Base.Contracts.Logic;

    /// <summary>
    ///     Describes a database for reading survey status.
    /// </summary>
    public interface ISurveyStatusDatabase : IReadOnlyDatabase
    {
    }
}
