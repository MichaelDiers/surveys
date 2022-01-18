namespace SaveSurveyResultService.Contracts
{
	/// <summary>
	///   The application configuration.
	/// </summary>
	public interface IConfiguration
	{
		/// <summary>
		///   Gets the collection name.
		/// </summary>
		string CollectionName { get; }

		/// <summary>
		///   Gets the project id.
		/// </summary>
		string ProjectId { get; }
	}
}