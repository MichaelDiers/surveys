namespace SaveSurveySubscriber.Contracts
{
    using System.Threading.Tasks;
    using Surveys.Common.Contracts;

    /// <summary>
    ///     Access to the survey database.
    /// </summary>
    public interface IDatabase
    {
        /// <summary>
        ///     Insert a new survey.
        /// </summary>
        /// <param name="message">The survey data.</param>
        /// <returns>A <see cref="Task" />.</returns>
        Task Insert(ISaveSurveyMessage message);
    }
}
