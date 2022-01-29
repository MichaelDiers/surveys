namespace SurveyEvaluatorService.Contracts
{
	using System.Threading.Tasks;

	/// <summary>
	///   Evaluate a survey result.
	/// </summary>
	public interface ISurveyEvaluatorProvider
	{
		/// <summary>
		///   Evaluate the result of a survey for a participant.
		/// </summary>
		/// <param name="surveyResult">The result of the survey.</param>
		/// <returns>A <see cref="Task" />.</returns>
		Task Evaluate(ISurveyResult surveyResult);
	}
}