namespace SurveyEvaluatorService.Model
{
	using SurveyEvaluatorService.Contracts;

	/// <summary>
	///   Describes the app settings.
	/// </summary>
	public class SurveyEvaluatorConfiguration : ISurveyEvaluatorConfiguration
	{
		/// <summary>
		///   Gets or sets the id of the project.
		/// </summary>
		public string ProjectId { get; set; }
	}
}