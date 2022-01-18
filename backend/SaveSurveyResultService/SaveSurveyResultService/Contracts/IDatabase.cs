namespace SaveSurveyResultService.Contracts
{
	using System.Threading.Tasks;

	/// <summary>
	///   Specifies database operations on survey-result documents.
	/// </summary>
	public interface IDatabase
	{
		/// <summary>
		///   Inserts a new document into the survey-results collection.
		/// </summary>
		/// <param name="message">A survey result.</param>
		/// <returns>A <see cref="Task" />.</returns>
		Task InsertResult(IMessage message);
	}
}