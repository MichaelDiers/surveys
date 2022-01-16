namespace UpdateSurveyStatusService.Contracts
{
	using System.Threading.Tasks;

	/// <summary>
	///   Specifies database operations on survey documents.
	/// </summary>
	public interface IDatabase
	{
		/// <summary>
		///   Updates a participant entry of a survey document.
		/// </summary>
		/// <param name="participantId">The id of the participant.</param>
		/// <param name="status">The new status set for the participant.</param>
		/// <returns>A <see cref="Task" />.</returns>
		Task UpdateParticipant(string participantId, Status status);

		/// <summary>
		///   Update the status of a survey.
		/// </summary>
		/// <param name="surveyId">The id of the survey.</param>
		/// <param name="status">The new status of the survey.</param>
		/// <returns>A <see cref="Task" />.</returns>
		Task UpdateSurvey(string surveyId, Status status);
	}
}