namespace UpdateSurveyService.Contracts
{
	using System.Threading.Tasks;
	using UpdateSurveyService.Model;

	/// <summary>
	///   Provider for handling survey updates.
	/// </summary>
	public interface IUpdateProvider
	{
		/// <summary>
		///   Update a survey.
		/// </summary>
		/// <param name="json">A <see cref="Message" /> in json format.</param>
		/// <returns>A <see cref="Task" />.</returns>
		Task Update(string json);
	}
}