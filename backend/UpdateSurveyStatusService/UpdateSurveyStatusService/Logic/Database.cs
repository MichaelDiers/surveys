namespace UpdateSurveyStatusService.Logic
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Google.Cloud.Firestore;
	using UpdateSurveyStatusService.Contracts;

	/// <summary>
	///   Specifies operations on firestore database.
	/// </summary>
	public class Database : IDatabase
	{
		/// <summary>
		///   Access the application configuration.
		/// </summary>
		private readonly IConfiguration configuration;

		/// <summary>
		///   A client for the database.
		/// </summary>
		private readonly FirestoreDb database;

		/// <summary>
		///   Creates a new instance of <see cref="Database" />.
		/// </summary>
		/// <param name="configuration">The application configuration.</param>
		public Database(IConfiguration configuration)
		{
			this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
			this.database = FirestoreDb.Create(configuration.ProjectId);
		}

		/// <summary>
		///   Inserts a new document into the status collection.
		/// </summary>
		/// <param name="surveyId">The id of the survey.</param>
		/// <param name="participantId">The id of the participant.</param>
		/// <param name="status">The new status.</param>
		/// <returns>A <see cref="Task" />.</returns>
		public async Task InsertStatus(string surveyId, string participantId, string status)
		{
			var document = new Dictionary<string, object>
			{
				{"surveyId", surveyId},
				{"participantId", participantId},
				{"status", status}
			};

			var docReference = this.database.Collection(this.configuration.CollectionName).Document();
			await docReference.SetAsync(document);
		}
	}
}