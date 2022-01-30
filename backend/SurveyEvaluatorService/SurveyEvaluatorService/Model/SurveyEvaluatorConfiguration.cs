namespace SurveyEvaluatorService.Model
{
	using SurveyEvaluatorService.Contracts;

	/// <summary>
	///   Describes the app settings.
	/// </summary>
	public class SurveyEvaluatorConfiguration : ISurveyEvaluatorConfiguration
	{
		/// <summary>
		///   Gets or sets the name of the collection that contains survey status updates.
		/// </summary>
		public string CollectionNameStatus { get; set; }

		/// <summary>
		///   Gets or sets the id of the project.
		/// </summary>
		public string ProjectId { get; set; }
	}
}