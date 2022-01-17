namespace UpdateSurveyStatusService.Logic
{
	using System;
	using System.Linq;
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
		///   Update the status of a participant.
		/// </summary>
		/// <param name="participantId">The id of the participant.</param>
		/// <param name="status">The new status for the participant.</param>
		/// <returns>A <see cref="Task" />.</returns>
		public async Task UpdateParticipant(string participantId, Status status)
		{
			var collection = this.database.Collection(this.configuration.SurveysCollectionName);
			var snapshot = await collection.WhereArrayContains("participantIds", participantId).Limit(1).GetSnapshotAsync();
			if (snapshot.Count == 1)
			{
				var doc = snapshot.Documents.Single();
				await doc.Reference.UpdateAsync(
					$"{participantId}.status",
					StatusJsonConverter.ConvertStatusToString(status));
			}
		}

		/// <summary>
		///   Update the status of a survey.
		/// </summary>
		/// <param name="surveyId">The id of the survey.</param>
		/// <param name="status">The new status of the survey.</param>
		/// <returns>A <see cref="Task" />.</returns>
		public async Task UpdateSurvey(string surveyId, Status status)
		{
			var collection = this.database.Collection(this.configuration.SurveysCollectionName);
			var docRef = collection.Document(surveyId);
			await docRef.UpdateAsync("status", StatusJsonConverter.ConvertStatusToString(status));
		}
	}
}