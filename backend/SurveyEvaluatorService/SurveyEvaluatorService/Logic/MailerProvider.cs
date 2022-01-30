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

			var request = new SendMailRequest
			{
				Body = new SendMailRequestBody
				{
					Html = "<html><body><h1>Thank you!</h1></body></html>",
					PlainText = "Thank you!"
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
				Subject = $"{survey.Name}: Thanks for your Vote!",
				SurveyId = survey.Id
			};

			return Task.FromResult<ISendMailRequest>(request);
		}
	}
}