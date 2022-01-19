namespace SurveyViewerService.Contracts
{
	/// <summary>
	///   Describes an address.
	/// </summary>
	public interface IAddress
	{
		/// <summary>
		///   Gets the email.
		/// </summary>
		string Email { get; }

		/// <summary>
		///   Gets the name.
		/// </summary>
		string Name { get; }
	}
}