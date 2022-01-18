namespace SaveSurveyResultService.Contracts
{
	using System.Threading.Tasks;

	/// <summary>
	///   Provider for handling survey results.
	/// </summary>
	public interface ISaveSurveyResultProvider
	{
		/// <summary>
		///   InsertSurveyResult a survey.
		/// </summary>
		/// <param name="json">The incoming message in json format.</param>
		/// <returns>A <see cref="Task" />.</returns>
		Task InsertSurveyResult(string json);
	}
}