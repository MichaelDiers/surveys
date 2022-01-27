namespace SurveyViewerService.Contracts
{
	using System.Threading.Tasks;
	using SurveyViewerService.Model;

	/// <summary>
	///   Provider for reading and updating survey data.
	/// </summary>
	public interface ISurveyViewerProvider
	{
		/// <summary>
		///   Process the survey result of a survey participant.
		/// </summary>
		/// <param name="json">The json formatted survey result.</param>
		/// <returns>A <see cref="Task" />.</returns>
		Task HandleSurveySubmitResult(string json);

		/// <summary>
		///   Read survey data by the id of a participant.
		/// </summary>
		/// <param name="participantId">The id of the participant.</param>
		/// <returns>A <see cref="SurveyViewData" /> instance containing the survey data.</returns>
		Task<SurveyViewData> ReadSurveyData(string participantId);
	}
}