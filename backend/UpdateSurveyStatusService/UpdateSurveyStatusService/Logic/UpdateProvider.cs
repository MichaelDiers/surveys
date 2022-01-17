namespace UpdateSurveyStatusService.Logic
{
	using System;
	using System.Threading.Tasks;
	using Newtonsoft.Json;
	using UpdateSurveyStatusService.Contracts;
	using UpdateSurveyStatusService.Model;

	/// <summary>
	///   Provider for handling survey updates.
	/// </summary>
	public class UpdateProvider : IUpdateProvider
	{
		/// <summary>
		///   Access to the firestore database.
		/// </summary>
		private readonly IDatabase database;

		/// <summary>
		///   Creates a new instance of <see cref="UpdateProvider" />.
		/// </summary>
		/// <param name="database">Access to the firestore database.</param>
		public UpdateProvider(IDatabase database)
		{
			this.database = database ?? throw new ArgumentNullException(nameof(database));
		}

		/// <summary>
		///   Update a survey.
		/// </summary>
		/// <param name="json">A <see cref="Message" /> in json format.</param>
		/// <returns>A <see cref="Task" />.</returns>
		public async Task Update(string json)
		{
			if (string.IsNullOrWhiteSpace(json))
			{
				throw new ArgumentNullException(nameof(json));
			}

			var message = JsonConvert.DeserializeObject<Message>(json);
			if (message == null || message.Status == Status.None)
			{
				throw new ArgumentException($"Cannot parse json message: {json}", nameof(json));
			}

			await this.database.InsertStatus(
				message.SurveyId,
				message.ParticipantId,
				StatusJsonConverter.ConvertStatusToString(message.Status));
		}
	}
}