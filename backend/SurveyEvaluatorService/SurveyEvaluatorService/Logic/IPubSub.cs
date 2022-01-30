namespace SurveyEvaluatorService.Logic
{
	using System.Threading.Tasks;
	using SurveyEvaluatorService.Contracts;

	/// <summary>
	///   Access to google cloud Pub/Sub.
	/// </summary>
	public class PubSub : IPubSub
	{
		/// <summary>
		///   Send a mail request to pub/sub.
		/// </summary>
		/// <returns>A <see cref="Task" />.</returns>
		public Task SendMailAsync()
		{
			return Task.CompletedTask;
		}
	}
}