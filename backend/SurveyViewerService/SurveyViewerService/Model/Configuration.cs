namespace SurveyViewerService.Model
{
	using SurveyViewerService.Contracts;

	/// <summary>
	///   Describes the application configuration.
	/// </summary>
	public class Configuration : IConfiguration
	{
		/// <summary>
		///   Gets or sets the name of the collection of surveys.
		/// </summary>
		public string CollectionNameSurveys { get; set; }

		/// <summary>
		///   Gets or sets the name of the collection of survey result data.
		/// </summary>
		public string CollectionNameSurveysResult { get; set; }

		/// <summary>
		///   Gets or sets the name of the collection of survey status data.
		/// </summary>
		public string CollectionNameSurveysStatus { get; set; }

		/// <summary>
		///   Gets or sets the id of the project.
		/// </summary>
		public string ProjectId { get; set; }

		/// <summary>
		///   Gets or sets the Pub/Sub topic name.
		/// </summary>
		public string PubSubTopic { get; set; }
	}
}