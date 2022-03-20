namespace EvaluateSurveySubscriber.Logic
{
    using EvaluateSurveySubscriber.Contracts;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.GoogleCloudFirestore.Logic;

    /// <summary>
    ///     Describes a database for reading surveys.
    /// </summary>
    public class SurveyDatabase : ReadonlyDatabase, ISurveyDatabase
    {
        /// <summary>
        ///     Creates a new instance of <see cref="SurveyDatabase" />.
        /// </summary>
        /// <param name="databaseConfiguration">The configuration of the database.</param>
        public SurveyDatabase(IDatabaseConfiguration databaseConfiguration)
            : base(databaseConfiguration)
        {
        }
    }
}
