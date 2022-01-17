namespace UpdateSurveyStatusService.Model
{
	using UpdateSurveyStatusService.Contracts;

	/// <summary>
	///   The application configuration.
	/// </summary>
	public class Configuration : IConfiguration
	{
		/// <summary>
		///   Gets or sets the collection name.
		/// </summary>
		public string CollectionName { get; set; }

		/// <summary>
		///   Gets or sets the project id.
		/// </summary>
		public string ProjectId { get; set; }
	}
}