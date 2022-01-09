namespace UpdateSurveyService.Contracts
{
	using System.Collections.Generic;
	using System.Threading.Tasks;

	/// <summary>
	///   Specifies operations on the database.
	/// </summary>
	public interface IDatabase
	{
		/// <summary>
		///   Updates the specifies survey by the given values.
		/// </summary>
		/// <param name="surveyId">The survey to update.</param>
		/// <param name="updates">The new values of the survey.</param>
		/// <returns>A <see cref="Task" />.</returns>
		Task Update(string surveyId, IDictionary<string, object> updates);
	}
}