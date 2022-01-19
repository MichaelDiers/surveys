namespace SurveyViewerService.Model
{
	using SurveyViewerService.Contracts;

	/// <summary>
	///   Describes an address.
	/// </summary>
	public class Address : IAddress
	{
		/// <summary>
		///   Gets or sets the email.
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		///   Gets or sets the name.
		/// </summary>
		public string Name { get; set; }
	}
}