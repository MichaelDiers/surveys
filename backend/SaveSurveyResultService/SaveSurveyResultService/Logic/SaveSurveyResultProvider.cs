namespace SaveSurveyResultService.Logic
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using Newtonsoft.Json;
	using SaveSurveyResultService.Contracts;
	using SaveSurveyResultService.Model;

	/// <summary>
	///   Provider for handling survey results.
	/// </summary>
	public class SaveSurveyResultProvider : ISaveSurveyResultProvider
	{
		/// <summary>
		///   Access to the firestore database.
		/// </summary>
		private readonly IDatabase database;

		/// <summary>
		///   Creates a new instance of <see cref="SaveSurveyResultProvider" />.
		/// </summary>
		/// <param name="database">Access to the firestore database.</param>
		public SaveSurveyResultProvider(IDatabase database)
		{
			this.database = database ?? throw new ArgumentNullException(nameof(database));
		}

		/// <summary>
		///   Insert a survey result into firestore.
		/// </summary>
		/// <param name="json">A <see cref="Message" /> in json format.</param>
		/// <returns>A <see cref="Task" />.</returns>
		public async Task InsertSurveyResult(string json)
		{
			if (string.IsNullOrWhiteSpace(json))
			{
				throw new ArgumentNullException(nameof(json));
			}

			// parse and validate the message
			var message = JsonConvert.DeserializeObject<Message>(json);
			if (string.IsNullOrWhiteSpace(message?.SurveyId)
			    || !Guid.TryParse(message.SurveyId, out var surveyId)
			    || surveyId == Guid.Empty
			    || string.IsNullOrWhiteSpace(message.ParticipantId)
			    || !Guid.TryParse(message.ParticipantId, out var participantId)
			    || participantId == Guid.Empty
			    || surveyId == participantId
			    || message.Results?.Any(
				    result => string.IsNullOrWhiteSpace(result.Answer)
				              || !int.TryParse(result.Answer, out _)
				              || string.IsNullOrWhiteSpace(result.QuestionId)
				              || !Guid.TryParse(result.QuestionId, out var questionId)
				              || questionId == Guid.Empty)
			    != false)
			{
				throw new ArgumentException($"Cannot parse json message: {json}", nameof(json));
			}

			await this.database.InsertResult(message);
		}
	}
}