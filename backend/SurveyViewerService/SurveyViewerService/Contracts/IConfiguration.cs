namespace SurveyViewerService.Contracts
{
	/// <summary>
	///   Describes the application configuration.
	/// </summary>
	public interface IConfiguration
	{
		/// <summary>
		///   Gets the name of the collection of surveys.
		/// </summary>
		string CollectionNameSurveys { get; }

		/// <summary>
		///   Gets the name of the collection of survey result data.
		/// </summary>
		string CollectionNameSurveysResult { get; }

		/// <summary>
		///   Gets the name of the collection of survey status data.
		/// </summary>
		string CollectionNameSurveysStatus { get; }

		/// <summary>
		///   Gets the id of the project.
		/// </summary>
		string ProjectId { get; }

		/// <summary>
		///   Gets the Pub/Sub topic name.
		/// </summary>
		string PubSubTopic { get; }
	}
}