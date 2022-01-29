namespace SurveyEvaluatorService.Contracts
{
	/// <summary>
	///   Describes the app settings.
	/// </summary>
	public interface ISurveyEvaluatorConfiguration
	{
		/// <summary>
		///   Gets the id of the project.
		/// </summary>
		string ProjectId { get; }
	}
}