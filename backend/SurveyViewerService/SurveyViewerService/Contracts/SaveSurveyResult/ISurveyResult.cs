namespace SurveyViewerService.Contracts.SaveSurveyResult
{
	/// <summary>
	///   Describes the answer of a survey question.
	/// </summary>
	public interface ISurveyResult
	{
		/// <summary>
		///   Gets the id of the question.
		/// </summary>
		string QuestionId { get; }

		/// <summary>
		///   Gets the answer of a survey question.
		/// </summary>
		int QuestionValue { get; }
	}
}