namespace SurveyViewerService.Logic
{
	using System;
	using System.Threading.Tasks;
	using Newtonsoft.Json;
	using SurveyViewerService.Contracts;
	using SurveyViewerService.Model;

	/// <summary>
	///   Provider for reading and updating survey data.
	/// </summary>
	public class SurveyViewerProvider : ISurveyViewerProvider
	{
		/// <summary>
		///   Access the application configuration.
		/// </summary>
		private readonly IConfiguration configuration;

		/// <summary>
		///   Access to the firestore database.
		/// </summary>
		private readonly IDatabase database;

		/// <summary>
		///   Creates a new instance of <see cref="SurveyViewerProvider" />.
		/// </summary>
		/// <param name="configuration">Access to the application configuration.</param>
		/// <param name="database">Access to the firestore database.</param>
		public SurveyViewerProvider(IConfiguration configuration, IDatabase database)
		{
			this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
			this.database = database ?? throw new ArgumentNullException(nameof(database));
		}

		/// <summary>
		///   Read survey data by the id of a participant.
		/// </summary>
		/// <param name="participantId">The id of the participant.</param>
		/// <returns>A <see cref="SurveyViewData" /> instance containing the survey data.</returns>
		public async Task<SurveyViewData> ReadSurveyData(string participantId)
		{
			if (string.IsNullOrWhiteSpace(participantId))
			{
				throw new ArgumentNullException(nameof(participantId), "Value cannot be null or whitespace.");
			}

			if (!Guid.TryParse(participantId, out var guid) || guid == Guid.Empty)
			{
				throw new ArgumentException($"Invalid participantId: '{participantId}'", nameof(participantId));
			}

			var survey = await this.database.ReadSurvey(participantId);
			var json = JsonConvert.SerializeObject(survey);
			Console.WriteLine(json);
			return new SurveyViewData();
		}
	}
}