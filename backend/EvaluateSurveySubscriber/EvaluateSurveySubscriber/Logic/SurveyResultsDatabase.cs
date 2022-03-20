namespace EvaluateSurveySubscriber.Logic
{
    using EvaluateSurveySubscriber.Contracts;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.GoogleCloudFirestore.Logic;

    /// <summary>
    ///     Describes a database for reading survey results.
    /// </summary>
    public class SurveyResultsDatabase : ReadonlyDatabase, ISurveyResultsDatabase
    {
        /// <summary>
        ///     Creates a new instance of <see cref="SurveyResultsDatabase" />.
        /// </summary>
        /// <param name="databaseConfiguration">The configuration of the database.</param>
        public SurveyResultsDatabase(IDatabaseConfiguration databaseConfiguration)
            : base(databaseConfiguration)
        {
        }
    }
}
