namespace SaveSurveySubscriber.Logic
{
    using System;
    using System.Threading.Tasks;
    using Google.Cloud.Firestore;
    using SaveSurveySubscriber.Contracts;
    using Surveys.Common.Contracts;

    /// <summary>
    ///     Access the survey database.
    /// </summary>
    public class Database : IDatabase
    {
        /// <summary>
        ///     The application configuration.
        /// </summary>
        private readonly IFunctionConfiguration configuration;

        /// <summary>
        ///     Access to the database implementation.
        /// </summary>
        private readonly FirestoreDb database;

        /// <summary>
        ///     Creates a new instance of <see cref="Database" />.
        /// </summary>
        /// <param name="configuration">The application configuration.</param>
        /// <exception cref="ArgumentNullException">Is thrown if the configuration is null.</exception>
        public Database(IFunctionConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.database = FirestoreDb.Create(configuration.ProjectId);
        }

        /// <summary>
        ///     Insert a new survey.
        /// </summary>
        /// <param name="message">The survey data.</param>
        /// <returns>A <see cref="Task" />.</returns>
        public async Task Insert(ISaveSurveyMessage message)
        {
            var documentReference = this.database.Collection(this.configuration.CollectionName)
                .Document(message.InternalSurveyId);
            var document = message.Survey.ToDictionary();
            document.Add("created", FieldValue.ServerTimestamp);
            await documentReference.CreateAsync(document);
        }
    }
}
