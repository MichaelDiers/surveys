namespace SurveyViewerService.Logic
{
	using System;
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
	}
}