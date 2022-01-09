namespace UpdateSurveyService.Logic
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using Google.Cloud.Firestore;
	using UpdateSurveyService.Contracts;

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
		///   Updates the specifies survey by the given values.
		/// </summary>
		/// <param name="surveyId">The survey to update.</param>
		/// <param name="updates">The new values of the survey.</param>
		/// <returns>A <see cref="Task" />.</returns>
		public async Task Update(string surveyId, IDictionary<string, object> updates)
		{
			var collection = this.database.Collection(this.configuration.SurveysCollectionName);
			var docRef = collection.Document(surveyId);
			await docRef.UpdateAsync(
				new Dictionary<FieldPath, object>(
					updates.Select(x => new KeyValuePair<FieldPath, object>(new FieldPath(x.Key), x.Value))));
		}
	}
}