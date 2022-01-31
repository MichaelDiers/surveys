namespace SurveyEvaluatorService.Logic
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using SurveyEvaluatorService.Contracts;
	using SurveyEvaluatorService.Model;

	/// <summary>
	///   Describes provider for creating emails.
	/// </summary>
	public class MailerProvider : IMailerProvider
	{
		/// <summary>
		///   Access the application settings.
		/// </summary>
		private readonly ISurveyEvaluatorConfiguration configuration;

		/// <summary>
		///   Creates a new instance of <see cref="MailerProvider" />.
		/// </summary>
		/// <param name="configuration">Access the application settings.</param>
		public MailerProvider(ISurveyEvaluatorConfiguration configuration)
		{
			this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
		}

		/// <summary>
		///   Create a thank you mail for a participant.
		/// </summary>
		/// <param name="survey">The survey for that the participant voted.</param>
		/// <param name="surveyResult">The survey result of the participant.</param>
		/// <returns>A <see cref="Task" /> whose result is a request for pub/sub.</returns>
		public Task<ISendMailRequest> CreateThankYouEmailAsync(ISurvey survey, ISurveyResult surveyResult)
		{
			if (survey == null)
			{
				throw new ArgumentNullException(nameof(survey));
			}

			if (surveyResult == null)
			{
				throw new ArgumentNullException(nameof(surveyResult));
			}

			var results = surveyResult.Results.Select(
				sr =>
				{
					var question = survey.Questions.First(q => q.Id == sr.QuestionId);
					var answer = question.Choices.First(c => c.Value == sr.AnswerValue).Answer;
					return new
					{
						Question = question.Text,
						Answer = answer
					};
				}).ToArray();

			var request = new SendMailRequest
			{
				Body = new SendMailRequestBody
				{
					Html = string.Format(
						this.configuration.TemplateHtmlThankYou,
						survey.Participants.First(p => p.Id == surveyResult.ParticipantId).Name,
						survey.Name,
						string.Join(
							"",
							results.Select(r => string.Format(this.configuration.TemplateHtmlThankYouAnswer, r.Question, r.Answer))),
						survey.Organizer.Name,
						$"{this.configuration.SurveyViewerUrl}{surveyResult.ParticipantId}"),
					PlainText = string.Format(
						this.configuration.TemplatePlainThankYou,
						survey.Participants.First(p => p.Id == surveyResult.ParticipantId).Name,
						survey.Name,
						string.Join(
							"",
							results.Select(
								r => string.Format(
									this.configuration.TemplatePlainThankYouAnswer,
									r.Question,
									r.Answer,
									this.configuration.TemplatePlainNewline))),
						survey.Organizer.Name,
						this.configuration.TemplatePlainNewline,
						$"{this.configuration.SurveyViewerUrl}{surveyResult.ParticipantId}")
				},
				Organizer = new SendMailRequestRecipient
				{
					Email = survey.Organizer.Email,
					Name = survey.Organizer.Name
				},
				ParticipantIds = new[]
				{
					surveyResult.ParticipantId
				},
				Recipients = survey.Participants.Where(p => p.Id == surveyResult.ParticipantId).Select(
					p => new SendMailRequestRecipient
					{
						Email = p.Email,
						Name = p.Name
					}).ToArray(),
				StatusFailed = SurveyStatusValue.ThankYouMailFailed,
				StatusOk = SurveyStatusValue.ThankYouMailOk,
				Subject = string.Format(this.configuration.TemplateThankYouSubject, survey.Name),
				SurveyId = survey.Id
			};

			return Task.FromResult<ISendMailRequest>(request);
		}
	}
}