namespace SaveSurveyResultService.Contracts
{
	public interface IResult
	{
		/// <summary>
		///   Gets the answer of a survey question.
		/// </summary>
		int Answer { get; }

		/// <summary>
		///   Gets the id of the question.
		/// </summary>
		string QuestionId { get; }
	}
}