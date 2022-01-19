namespace SurveyViewerService.Model
{
	using SurveyViewerService.Contracts;

	/// <summary>
	///   Describes a survey participant.
	/// </summary>
	public class Participant : Address, IParticipant
	{
		/// <summary>
		///   Gets the id of the participant.
		/// </summary>
		public string Id { get; set; }
	}
}