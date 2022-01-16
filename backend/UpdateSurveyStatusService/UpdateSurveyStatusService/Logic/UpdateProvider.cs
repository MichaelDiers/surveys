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
			if (json == null)
			{
				throw new ArgumentNullException(nameof(json));
			}

			var message = JsonConvert.DeserializeObject<Message>(json);
			if (message == null || !message.IsValid())
			{
				throw new ArgumentException($"Cannot parse json message: {json}", nameof(json));
			}

			switch (message.Type)
			{
				case MessageType.Survey:
					await this.database.UpdateSurvey(message.Id, message.Status);
					break;
				case MessageType.Participant:
					await this.database.UpdateParticipant(message.Id, message.Status);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(message.Type), message.Type, null);
			}
		}
	}
}