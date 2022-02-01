namespace SurveyEvaluatorService.Logic
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using Microsoft.Extensions.Logging;
	using SurveyEvaluatorService.Contracts;
	using SurveyEvaluatorService.Model;

	/// <summary>
	///   Evaluate a survey result.
	/// </summary>
	public class SurveyEvaluatorProvider : ISurveyEvaluatorProvider
	{
		/// <summary>
		///   Access the firestore database.
		/// </summary>
		private readonly IDatabase database;

		/// <summary>
		///   Log errors.
		/// </summary>
		private readonly ILogger<SurveyEvaluatorProvider> logger;

		/// <summary>
		///   Provider for creating emails.
		/// </summary>
		private readonly IMailerProvider mailerProvider;

		/// <summary>
		///   Access to google cloud Pub/Sub.
		/// </summary>
		private readonly IPubSub pubSub;

		/// <summary>
		///   Creates a new instance of <see cref="SurveyEvaluatorProvider" />.
		/// </summary>
		/// <param name="logger">Access the error logger.</param>
		/// <param name="database">Access the firestore database.</param>
		/// <param name="pubSub">Access to google cloud Pub/Sub.</param>
		/// <param name="mailerProvider">Provider for creating emails.</param>
		public SurveyEvaluatorProvider(
			ILogger<SurveyEvaluatorProvider> logger,
			IDatabase database,
			IPubSub pubSub,
			IMailerProvider mailerProvider)
		{
			this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
			this.database = database ?? throw new ArgumentNullException(nameof(database));
			this.pubSub = pubSub ?? throw new ArgumentNullException(nameof(pubSub));
			this.mailerProvider = mailerProvider ?? throw new ArgumentNullException(nameof(mailerProvider));
		}

		/// <summary>
		///   Evaluate the result of a survey for a participant.
		/// </summary>
		/// <param name="surveyResult">The result of the survey.</param>
		/// <returns>A <see cref="Task" />.</returns>
		public async Task Evaluate(ISurveyResult surveyResult)
		{
			if (surveyResult == null)
			{
				throw new ArgumentNullException(nameof(surveyResult));
			}

			var survey = await this.database.ReadSurveyAsync(surveyResult.SurveyId);
			var surveyResults = (await this.database.ReadSurveyResultsAsync(surveyResult.SurveyId)).ToArray();
			var surveyStatus = (await this.database.ReadSurveyStatusAsync(surveyResult.SurveyId)).ToArray();

			var closedStatus = surveyStatus.FirstOrDefault(status => status.Status == SurveyStatusValue.Closed);
			if (closedStatus != null)
			{
				// find last valid vote
				var lastValidVote = surveyResults
					.Where(
						result => result.ParticipantId == surveyResult.ParticipantId && result.Timestamp < closedStatus.Timestamp)
					.OrderByDescending(status => status.Timestamp).First();
				var email = await this.mailerProvider.CreateSorryClosedEmailAsync(survey, lastValidVote);
				await this.pubSub.SendMailAsync(email);
			}
			else
			{
				var email = await this.mailerProvider.CreateThankYouEmailAsync(survey, surveyResult);
				await this.pubSub.SendMailAsync(email);
			}

			// all participants voted
			if (closedStatus == null && survey.ParticipantIds.All(pid => surveyResults.Any(sr => sr.ParticipantId == pid)))
			{
				await this.pubSub.SendStatusUpdateAsync(
					new SurveyStatusUpdateRequest
					{
						ParticipantId = surveyResult.ParticipantId,
						SurveyId = surveyResult.SurveyId,
						Status = SurveyStatusValue.Closed
					});
			}
		}
	}
}