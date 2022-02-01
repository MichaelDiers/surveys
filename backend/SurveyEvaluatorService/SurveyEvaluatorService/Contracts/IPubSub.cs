namespace SurveyEvaluatorService.Contracts
{
	using System.Threading.Tasks;

	/// <summary>
	///   Access to google cloud Pub/Sub.
	/// </summary>
	public interface IPubSub
	{
		/// <summary>
		///   Send a mail request to pub/sub.
		/// </summary>
		/// <param name="request">The request data.</param>
		/// <returns>A <see cref="Task" />.</returns>
		Task SendMailAsync(ISendMailRequest request);

		/// <summary>
		///   Send a status update request for a survey.
		/// </summary>
		/// <param name="request">The request data.</param>
		/// <returns>A <see cref="Task" />.</returns>
		Task SendStatusUpdateAsync(ISurveyStatusUpdateRequest request);

		/// <summary>
		///   Sends the result of the survey to Pub/Sub.
		/// </summary>
		/// <param name="request">The survey result data.</param>
		/// <returns>A <see cref="Task" />.</returns>
		Task SendSurveyClosed(ISurveyClosedRequest request);
	}
}