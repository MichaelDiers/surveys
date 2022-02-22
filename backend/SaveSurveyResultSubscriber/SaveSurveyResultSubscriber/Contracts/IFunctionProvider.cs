namespace SaveSurveyResultSubscriber.Contracts
{
    using System.Threading.Tasks;
    using Surveys.Common.Contracts;

    /// <summary>
    ///     Provider that handles the business logic of the cloud function.
    /// </summary>
    public interface IFunctionProvider
    {
        /// <summary>
        ///     Handle an incoming message from google cloud pub/sub.
        /// </summary>
        /// <param name="message">The incoming message from pub/sub.</param>
        /// <returns>A <see cref="Task" /> without a result.</returns>
        Task HandleAsync(ISaveSurveyResultMessage message);
    }
}
