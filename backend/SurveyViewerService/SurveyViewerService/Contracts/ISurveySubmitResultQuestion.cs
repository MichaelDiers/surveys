namespace SurveyViewerService.Contracts
{
	/// <summary>
	///   Describes the questions and answers of a survey result.
	/// </summary>
	public interface ISurveySubmitResultQuestion
	{
		/// <summary>
		///   Gets the id of the question.
		/// </summary>
		string QuestionId { get; }

		/// <summary>
		///   Gets the value of the answer.
		/// </summary>
		string Value { get; }
	}
}