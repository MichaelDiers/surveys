namespace SaveSurveySubscriber.Logic
{
    using System;
    using System.Threading.Tasks;
    using SaveSurveySubscriber.Contracts;

    /// <summary>
    ///     Provider that handles the business logic of the cloud function.
    /// </summary>
    public class FunctionProvider : IFunctionProvider
    {
        /// <summary>
        ///     Access the application settings.
        /// </summary>
        private readonly IFunctionConfiguration configuration;

        /// <summary>
        ///     Creates a new instance of <see cref="FunctionProvider" />.
        /// </summary>
        /// <param name="configuration">Access to the application settings.</param>
        public FunctionProvider(IFunctionConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        ///     Handle an incoming message from google cloud pub/sub.
        /// </summary>
        /// <param name="message">The incoming message from pub/sub.</param>
        /// <returns>A <see cref="Task" /> without a result.</returns>
        public Task HandleAsync(IMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            return Task.CompletedTask;
        }
    }
}
