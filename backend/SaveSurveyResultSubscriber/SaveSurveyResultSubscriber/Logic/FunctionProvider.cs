namespace SaveSurveyResultSubscriber.Logic
{
    using System;
    using System.Threading.Tasks;
    using SaveSurveyResultSubscriber.Contracts;
    using Surveys.Common.Contracts;
    using Surveys.Common.Firestore.Contracts;

    /// <summary>
    ///     Provider that handles the business logic of the cloud function.
    /// </summary>
    public class FunctionProvider : IFunctionProvider
    {
        /// <summary>
        ///     Access to the survey database.
        /// </summary>
        private readonly IDatabase database;

        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="database">Access to the survey database.</param>
        public FunctionProvider(IDatabase database)
        {
            this.database = database ?? throw new ArgumentNullException(nameof(database));
        }

        /// <summary>
        ///     Handle an incoming message from google cloud pub/sub.
        /// </summary>
        /// <param name="message">The incoming message from pub/sub.</param>
        /// <returns>A <see cref="Task" /> without a result.</returns>
        public async Task HandleAsync(ISaveSurveyResultMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            await this.database.InsertAsync(message.SurveyResult);
        }
    }
}
