namespace SurveyEvaluatorService.Contracts
{
	/// <summary>
	///   Describes an answer of a survey question.
	/// </summary>
	public interface ISurveyResultAnswer
	{
		/// <summary>
		///   Gets the value of the answer.
		/// </summary>
		int AnswerValue { get; }

		/// <summary>
		///   Gets the id of the question.
		/// </summary>
		string QuestionId { get; }
	}
}