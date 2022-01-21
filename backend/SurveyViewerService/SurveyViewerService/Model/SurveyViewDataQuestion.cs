namespace SurveyViewerService.Model
{
	using System.Collections.Generic;
	using Newtonsoft.Json;

	/// <summary>
	///   Describes a survey question.
	/// </summary>
	public class SurveyViewDataQuestion
	{
		/// <summary>
		///   Gets or sets the choices of the question.
		/// </summary>
		[JsonProperty("choices")]
		public IEnumerable<SurveyViewDataChoice> Choices { get; set; }

		/// <summary>
		///   Gets or sets the id of the question.
		/// </summary>
		[JsonProperty("id")]
		public string Id { get; set; }

		/// <summary>
		///   Gets or sets the text of the question.
		/// </summary>
		[JsonProperty("text")]
		public string Text { get; set; }
	}
}