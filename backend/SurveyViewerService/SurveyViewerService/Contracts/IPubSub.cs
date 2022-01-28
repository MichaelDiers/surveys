namespace SurveyViewerService.Contracts
{
	using System.Threading.Tasks;

	/// <summary>
	///   Access google cloud Pub/Sub.
	/// </summary>
	public interface IPubSub
	{
		/// <summary>
		///   Publish a message to Pub/Sub to save the survey result.
		/// </summary>
		/// <param name="surveyId">The id of the survey.</param>
		/// <param name="surveySubmitResult">The result of the survey.</param>
		/// <returns>A <see cref="Task" />.</returns>
		Task PublishMessageAsync(string surveyId, ISurveySubmitResult surveySubmitResult);
	}
}