namespace SurveyViewerService.Contracts
{
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
	}
}