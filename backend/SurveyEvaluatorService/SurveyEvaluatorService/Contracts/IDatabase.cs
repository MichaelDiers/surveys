namespace SurveyEvaluatorService.Contracts
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using SurveyEvaluatorService.Model;

	/// <summary>
	///   Access to the firestore database.
	/// </summary>
	public interface IDatabase
	{
		/// <summary>
		///   Reads a survey by its id.
		/// </summary>
		/// <param name="surveyId">The id of the survey.</param>
		/// <returns>A <see cref="Task" /> whose result is a <see cref="Survey" />.</returns>
		Task<Survey> ReadSurveyAsync(string surveyId);

		/// <summary>
		///   Reads all status updates for a survey. The result is ordered by the timestamp.
		/// </summary>
		/// <param name="surveyId">The id of the survey.</param>
		/// <returns>The status updates.</returns>
		Task<IEnumerable<ISurveyStatus>> ReadSurveyStatusAsync(string surveyId);
	}
}