namespace SurveyEvaluatorService.Logic
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using Google.Cloud.Firestore;
	using SurveyEvaluatorService.Contracts;
	using SurveyEvaluatorService.Model;

	/// <summary>
	///   Access to the firestore database.
	/// </summary>
	public class Database : IDatabase
	{
		/// <summary>
		///   Access the application settings.
		/// </summary>
		private readonly ISurveyEvaluatorConfiguration configuration;

		/// <summary>
		///   A client for the database.
		/// </summary>
		private readonly FirestoreDb database;

		/// <summary>
		///   Creates a new instance of <see cref="Database" />.
		/// </summary>
		/// <param name="configuration">Access the application settings.</param>
		public Database(ISurveyEvaluatorConfiguration configuration)
		{
			this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
			this.database = FirestoreDb.Create(configuration.ProjectId);
		}

		/// <summary>
		///   Reads a survey by its id.
		/// </summary>
		/// <param name="surveyId">The id of the survey.</param>
		/// <returns>A <see cref="Task" /> whose result is a <see cref="Survey" />.</returns>
		public async Task<Survey> ReadSurveyAsync(string surveyId)
		{
			if (string.IsNullOrWhiteSpace(surveyId))
			{
				throw new ArgumentNullException(nameof(surveyId));
			}

			var snapshot = await this.database.Collection(this.configuration.CollectionNameSurveys)
				.WhereEqualTo("surveyId", surveyId).Limit(1)
				.GetSnapshotAsync();
			if (snapshot.Count > 0)
			{
				return snapshot.Documents.Single(doc => doc.Exists).ConvertTo<Survey>();
			}

			return null;
		}

		/// <summary>
		///   Reads all status updates for a survey. The result is ordered by the timestamp.
		/// </summary>
		/// <param name="surveyId">The id of the survey.</param>
		/// <returns>The status updates.</returns>
		public async Task<IEnumerable<ISurveyStatus>> ReadSurveyStatusAsync(string surveyId)
		{
			if (string.IsNullOrWhiteSpace(surveyId))
			{
				throw new ArgumentNullException(nameof(surveyId));
			}

			var snapshot = await this.database.Collection(this.configuration.CollectionNameStatus)
				.WhereEqualTo("surveyId", surveyId)
				.OrderBy("timestamp").GetSnapshotAsync();
			if (snapshot.Count > 0)
			{
				return snapshot.Documents.Where(doc => doc.Exists).Select(doc => doc.ConvertTo<ISurveyStatus>());
			}

			return Enumerable.Empty<ISurveyStatus>();
		}
	}
}