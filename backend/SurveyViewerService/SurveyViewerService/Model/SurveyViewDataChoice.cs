namespace SurveyViewerService.Model
{
	/// <summary>
	///   Describes a choice of a survey question.
	/// </summary>
	public class SurveyViewDataChoice
	{
		/// <summary>
		///   Gets or sets a value that indicates if the value is selected.
		/// </summary>
		public bool IsSelected { get; set; }

		/// <summary>
		///   Gets or sets the text of the choice.
		/// </summary>
		public string Text { get; set; }

		/// <summary>
		///   Gets the value of the choice.
		/// </summary>
		public string Value { get; set; }
	}
}