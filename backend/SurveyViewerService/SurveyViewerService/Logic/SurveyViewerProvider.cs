namespace SurveyViewerService.Logic
{
	using System;
	using System.Linq;
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
		///   Access to the firestore database.
		/// </summary>
		private readonly IDatabase database;

		/// <summary>
		///   Creates a new instance of <see cref="SurveyViewerProvider" />.
		/// </summary>
		/// <param name="database">Access to the firestore database.</param>
		public SurveyViewerProvider(IDatabase database)
		{
			this.database = database ?? throw new ArgumentNullException(nameof(database));
		}

		/// <summary>
		///   Process the survey result of a survey participant.
		/// </summary>
		/// <param name="json">The json formatted survey result.</param>
		/// <returns>A <see cref="Task" />.</returns>
		public Task HandleSurveySubmitResult(string json)
		{
			if (string.IsNullOrWhiteSpace(json))
			{
				throw new ArgumentNullException(nameof(json));
			}

			if (!(JsonConvert.DeserializeObject<SurveySubmitResult>(json) is ISurveySubmitResult submitResult)
			    || string.IsNullOrWhiteSpace(submitResult.ParticipantId)
			    || submitResult.Questions?.Any() != true)
			{
				throw new ArgumentException($"Unable to parse survey result: {json}");
			}

			return Task.CompletedTask;
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
			if (survey == null)
			{
				return null;
			}

			var surveyStatus = await this.database.ReadSurveyStatus(survey.SurveyId);
			var surveyResults = await this.database.ReadSurveyResults(survey.SurveyId);

			var result = new SurveyViewData
			{
				IsClosed = surveyStatus.Any(status => status.Status == "CLOSED"),
				SurveyName = survey.Name,
				ParticipantId = participantId,
				ParticipantName = survey.Participants.FirstOrDefault(p => p.Id == participantId)?.Name,
				Questions = survey.Questions.Select(
					question => new SurveyViewDataQuestion
					{
						Id = question.Id,
						Text = question.Text,
						Choices = question.Choices.Select(
							choice => new SurveyViewDataChoice
							{
								Text = choice.Answer,
								Value = choice.Value
							}).ToArray()
					}).ToArray()
			};

			var lastResult = surveyResults.Where(sr => sr.ParticipantId == participantId)
				.OrderByDescending(sr => sr.TimeStamp).FirstOrDefault();

			foreach (var surveyViewDataQuestion in result.Questions)
			{
				foreach (var surveyViewDataChoice in surveyViewDataQuestion.Choices)
				{
					surveyViewDataChoice.IsSelected = lastResult == null && string.IsNullOrWhiteSpace(surveyViewDataChoice.Value)
					                                  || lastResult?.Answers.Any(
						                                  answer => answer.QuestionId == surveyViewDataQuestion.Id
						                                            && answer.Value.ToString() == surveyViewDataChoice.Value)
					                                  == true;
				}
			}

			return result;
		}
	}
}