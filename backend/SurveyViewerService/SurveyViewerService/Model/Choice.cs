namespace SurveyViewerService.Model
{
	using SurveyViewerService.Contracts;

	/// <summary>
	///   Specifies a possible answer of a survey question.
	/// </summary>
	public class Choice : IChoice
	{
		/// <summary>
		///   Gets or sets the answer of the question.
		/// </summary>
		public string Answer { get; set; }

		/// <summary>
		///   Gets or sets the value of the question.
		/// </summary>
		public string Value { get; set; }
	}
}