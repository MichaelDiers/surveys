namespace EvaluateSurveySubscriber.Logic
{
    using EvaluateSurveySubscriber.Contracts;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.GoogleCloudFirestore.Logic;

    /// <summary>
    ///     Describes a database for reading survey status.
    /// </summary>
    public class SurveyStatusDatabase : ReadonlyDatabase, ISurveyStatusDatabase
    {
        /// <summary>
        ///     Creates a new instance of <see cref="SurveyStatusDatabase" />.
        /// </summary>
        /// <param name="databaseConfiguration">The configuration of the database.</param>
        public SurveyStatusDatabase(IDatabaseConfiguration databaseConfiguration)
            : base(databaseConfiguration)
        {
        }
    }
}
