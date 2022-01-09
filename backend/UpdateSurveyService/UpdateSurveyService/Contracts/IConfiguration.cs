namespace UpdateSurveyService.Contracts
{
	/// <summary>
	///   The application configuration.
	/// </summary>
	public interface IConfiguration
	{
		/// <summary>
		///   Gets the project id.
		/// </summary>
		string ProjectId { get; }

		/// <summary>
		///   Gets the surveys collection name.
		/// </summary>
		string SurveysCollectionName { get; }
	}
}