namespace SurveyViewerService.Model
{
	using Newtonsoft.Json;

	/// <summary>
	///   Describes a choice of a survey question.
	/// </summary>
	public class SurveyViewDataChoice
	{
		/// <summary>
		///   Gets or sets a value that indicates if the value is selected.
		/// </summary>
		[JsonProperty("isSelected")]
		public bool IsSelected { get; set; }

		/// <summary>
		///   Gets or sets the text of the choice.
		/// </summary>
		[JsonProperty("text")]
		public string Text { get; set; }

		/// <summary>
		///   Gets the value of the choice.
		/// </summary>
		[JsonProperty("value")]
		public string Value { get; set; }
	}
}