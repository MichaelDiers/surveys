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
		///   Read survey data by the id of a participant.
		/// </summary>
		/// <param name="participantId">The id of the participant.</param>
		/// <returns>A <see cref="SurveyViewData" /> instance containing the survey data.</returns>
		Task<SurveyViewData> ReadSurveyData(string participantId);
	}
}