namespace SurveyViewerService.Contracts
{
	/// <summary>
	///   Specifies a possible answer of a survey question.
	/// </summary>
	public interface IChoice
	{
		/// <summary>
		///   Gets the answer of the question.
		/// </summary>
		string Answer { get; }

		/// <summary>
		///   Gets the value of the question.
		/// </summary>
		string Value { get; }
	}
}