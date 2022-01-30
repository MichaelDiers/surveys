namespace SurveyEvaluatorService.Contracts
{
	using System.Collections.Generic;
	using System.Threading.Tasks;

	/// <summary>
	///   Access to the firestore database.
	/// </summary>
	public interface IDatabase
	{
		/// <summary>
		///   Reads all status updates for a survey. The result is ordered by the timestamp.
		/// </summary>
		/// <param name="surveyId">The id of the survey.</param>
		/// <returns>The status updates.</returns>
		Task<IEnumerable<ISurveyStatus>> ReadSurveyStatusAsync(string surveyId);
	}
}