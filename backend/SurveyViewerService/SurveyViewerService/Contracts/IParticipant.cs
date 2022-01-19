namespace SurveyViewerService.Contracts
{
	/// <summary>
	///   Describes a survey participant.
	/// </summary>
	public interface IParticipant : IAddress
	{
		/// <summary>
		///   Gets the id of the participant.
		/// </summary>
		string Id { get; }
	}
}