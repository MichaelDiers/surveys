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
		/// <returns>A <see cref="Task" />.</returns>
		Task SendMailAsync();
	}
}