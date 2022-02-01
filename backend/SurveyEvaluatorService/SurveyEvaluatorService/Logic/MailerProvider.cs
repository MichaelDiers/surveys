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
		///   Create a survey closed mail for a participant.
		/// </summary>
		/// <param name="survey">The survey for that the participant voted.</param>
		/// <param name="surveyResult">The last valid survey result of the participant.</param>
		/// <returns>A <see cref="Task" /> whose result is a request for pub/sub.</returns>
		public async Task<ISendMailRequest> CreateSorryClosedEmailAsync(ISurvey survey, ISurveyResult surveyResult)
		{
			if (survey == null)
			{
				throw new ArgumentNullException(nameof(survey));
			}

			var sendMailRequest = await this.CreateSendMailRequestAsync(survey, surveyResult);

			(string Question, string Answer)[] results =
				survey.Questions.Select(
					question =>
					{
						var answer = question.Choices.FirstOrDefault(
							qc => qc.Value == surveyResult?.Results?.First(sr => sr.QuestionId == question.Id).AnswerValue)?.Answer;
						return (question.Text,
							string.IsNullOrWhiteSpace(answer) ? this.configuration.TemplateNoAnswer : answer);
					}).ToArray();

			sendMailRequest.Body = new SendMailRequestBody
			{
				Html = string.Format(
					this.configuration.TemplateHtmlClosedSurvey,
					survey.Participants.First(p => p.Id == surveyResult.ParticipantId).Name,
					survey.Name,
					string.Join(
						"",
						results.Select(
							r => string.Format(this.configuration.TemplateHtmlClosedSurveyAnswer, r.Question, r.Answer))),
					survey.Organizer.Name),
				PlainText = string.Format(
					this.configuration.TemplatePlainClosedSurvey,
					survey.Participants.First(p => p.Id == surveyResult.ParticipantId).Name,
					survey.Name,
					string.Join(
						"",
						results.Select(
							r => string.Format(
								this.configuration.TemplatePlainClosedSurveyAnswer,
								r.Question,
								r.Answer,
								this.configuration.TemplatePlainNewline))),
					survey.Organizer.Name,
					this.configuration.TemplatePlainNewline)
			};

			sendMailRequest.StatusFailed = SurveyStatusValue.SurveyClosedMailFailed;
			sendMailRequest.StatusOk = SurveyStatusValue.SurveyClosedMailOk;
			sendMailRequest.Subject = string.Format(this.configuration.TemplateClosedSurveySubject, survey.Name);

			return sendMailRequest;
		}

		/// <summary>
		///   Create a thank you mail for a participant.
		/// </summary>
		/// <param name="survey">The survey for that the participant voted.</param>
		/// <param name="surveyResult">The survey result of the participant.</param>
		/// <returns>A <see cref="Task" /> whose result is a request for pub/sub.</returns>
		public async Task<ISendMailRequest> CreateThankYouEmailAsync(ISurvey survey, ISurveyResult surveyResult)
		{
			if (survey == null)
			{
				throw new ArgumentNullException(nameof(survey));
			}

			if (surveyResult == null)
			{
				throw new ArgumentNullException(nameof(surveyResult));
			}

			var sendMailRequest = await this.CreateSendMailRequestAsync(survey, surveyResult);

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

			sendMailRequest.Body = new SendMailRequestBody
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
			};

			sendMailRequest.StatusFailed = SurveyStatusValue.ThankYouMailFailed;
			sendMailRequest.StatusOk = SurveyStatusValue.ThankYouMailOk;
			sendMailRequest.Subject = string.Format(this.configuration.TemplateThankYouSubject, survey.Name);

			return sendMailRequest;
		}

		/// <summary>
		///   Creates a <see cref="SendMailRequest" /> from the given data. Does not set specifics like
		///   <see cref="SendMailRequest.Body" />.
		/// </summary>
		/// <param name="survey">The survey for that the request is created.</param>
		/// <param name="surveyResult">The survey result of a participant.</param>
		/// <returns>A <see cref="Task" /> whose result is a <see cref="SendMailRequest" />.</returns>
		private Task<SendMailRequest> CreateSendMailRequestAsync(ISurvey survey, ISurveyResult surveyResult)
		{
			if (survey == null)
			{
				throw new ArgumentNullException(nameof(survey));
			}

			var request = new SendMailRequest
			{
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
				SurveyId = survey.Id
			};

			return Task.FromResult(request);
		}
	}
}