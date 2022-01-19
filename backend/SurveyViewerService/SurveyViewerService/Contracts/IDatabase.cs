namespace SurveyViewerService.Contracts
{
	using System.Collections.Generic;
	using System.Threading.Tasks;

	/// <summary>
	///   Access to firestore database.
	/// </summary>
	public interface IDatabase
	{
		/// <summary>
		///   Read survey data by the id of a participant.
		/// </summary>
		/// <param name="participantId">The id of the survey participant.</param>
		/// <returns>An <see cref="ISurvey" />.</returns>
		Task<ISurvey> ReadSurvey(string participantId);

		/// <summary>
		///   Read all results for a given survey id.
		/// </summary>
		/// <param name="surveyId">The id of the survey.</param>
		/// <returns>An <see cref="IEnumerable{T}" /> of <see cref="ISurveyResult" />.</returns>
		Task<IEnumerable<ISurveyResult>> ReadSurveyResults(string surveyId);

		/// <summary>
		///   Read all status updates for a given survey id.
		/// </summary>
		/// <param name="surveyId">The id of the survey.</param>
		/// <returns>An <see cref="IEnumerable{T}" /> of <see cref="ISurveyStatus" />.</returns>
		Task<IEnumerable<ISurveyStatus>> ReadSurveyStatus(string surveyId);
	}
}