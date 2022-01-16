namespace UpdateSurveyStatusService.Contracts
{
	using System.Threading.Tasks;

	/// <summary>
	///   Provider for handling survey updates.
	/// </summary>
	public interface IUpdateProvider
	{
		/// <summary>
		///   Update a survey.
		/// </summary>
		/// <param name="json">The incoming message in json format.</param>
		/// <returns>A <see cref="Task" />.</returns>
		Task Update(string json);
	}
}