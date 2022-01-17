namespace MailerService.Contracts
{
	using System.Threading.Tasks;

	/// <summary>
	///   Access google cloud Pub/Sub.
	/// </summary>
	public interface IPubSub
	{
		/// <summary>
		///   Publish a message to Pub/Sub that updates the status of a survey.
		/// </summary>
		/// <param name="surveyId">The id of the survey.</param>
		/// <param name="participantId">The id of the survey participant.</param>
		/// <param name="status">The new status of the survey.</param>
		/// <returns>A <see cref="Task" />.</returns>
		Task PublishUpdateAsync(string surveyId, string participantId, string status);
	}
}