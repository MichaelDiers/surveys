namespace UpdateSurveyStatusService.Contracts
{
	using System.Threading.Tasks;

	/// <summary>
	///   Specifies database operations on survey documents.
	/// </summary>
	public interface IDatabase
	{
		/// <summary>
		///   Inserts a new document into the status collection.
		/// </summary>
		/// <param name="surveyId">The id of the survey.</param>
		/// <param name="participantId">The id of the participant.</param>
		/// <param name="status">The new status.</param>
		/// <returns>A <see cref="Task" />.</returns>
		Task InsertStatus(string surveyId, string participantId, string status);
	}
}