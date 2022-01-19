namespace SurveyViewerService.Contracts
{
	/// <summary>
	///   Describes the answer of a question.
	/// </summary>
	public interface IAnswer
	{
		/// <summary>
		///   Gets the id of the question.
		/// </summary>
		string QuestionId { get; }

		/// <summary>
		///   Gets the value of the answer.
		/// </summary>
		int Value { get; }
	}
}