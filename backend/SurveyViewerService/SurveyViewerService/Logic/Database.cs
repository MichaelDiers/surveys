namespace SurveyViewerService.Logic
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using Google.Cloud.Firestore;
	using SurveyViewerService.Contracts;
	using SurveyViewerService.Model;

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
		///   Create a new instance of <see cref="Database" />.
		/// </summary>
		/// <param name="configuration">Access the application configuration.</param>
		public Database(IConfiguration configuration)
		{
			this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
			this.database = FirestoreDb.Create(configuration.ProjectId);
		}

		/// <summary>
		///   Read survey data by the id of a participant.
		/// </summary>
		/// <param name="participantId">The id of the survey participant.</param>
		/// <returns>An <see cref="ISurvey" />.</returns>
		public async Task<ISurvey> ReadSurvey(string participantId)
		{
			var snapshot = await this.database.Collection(this.configuration.CollectionNameSurveys)
				.WhereArrayContains("participantIds", participantId).Limit(1).GetSnapshotAsync();
			if (snapshot.Count != 1)
			{
				return null;
			}

			var documentSnapshot = snapshot.Single();
			if (!documentSnapshot.Exists)
			{
				return null;
			}

			var survey = documentSnapshot.ConvertTo<Survey>();
			survey.SurveyId = documentSnapshot.Id;
			return survey;
		}

		/// <summary>
		///   Read all status updates for a given survey id.
		/// </summary>
		/// <param name="surveyId">The id of the survey.</param>
		/// <returns>An <see cref="IEnumerable{T}" /> of <see cref="ISurveyStatus" />.</returns>
		public async Task<IEnumerable<ISurveyStatus>> ReadSurveyStatus(string surveyId)
		{
			var snapshot = await this.database.Collection(this.configuration.CollectionNameSurveysStatus)
				.WhereEqualTo("surveyId", surveyId).GetSnapshotAsync();
			var result = new List<ISurveyStatus>();
			if (snapshot.Any())
			{
				result.AddRange(snapshot.Where(doc => doc.Exists).Select(doc => doc.ConvertTo<SurveyStatus>()));
			}

			return result;
		}
	}
}