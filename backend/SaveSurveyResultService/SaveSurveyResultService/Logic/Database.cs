namespace SaveSurveyResultService.Logic
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using Google.Cloud.Firestore;
	using SaveSurveyResultService.Contracts;

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
		///   Inserts a new document into the survey-results collection.
		/// </summary>
		/// <param name="message">A survey result.</param>
		/// <returns>A <see cref="Task" />.</returns>
		public async Task InsertResult(IMessage message)
		{
			var results = message.Results.Select(
				result => new Dictionary<string, object>
				{
					{"questionId", result.QuestionId},
					{"answer", result.Answer}
				});
			var document = new Dictionary<string, object>
			{
				{"surveyId", message.SurveyId},
				{"participantId", message.ParticipantId},
				{"results", results},
				{"timestamp", FieldValue.ServerTimestamp}
			};

			var docReference = this.database.Collection(this.configuration.CollectionName).Document();
			await docReference.SetAsync(document);
		}
	}
}