namespace SurveyViewerService.Model
{
	using System.Collections.Generic;

	/// <summary>
	///   Describes a survey question.
	/// </summary>
	public class SurveyViewDataQuestion
	{
		/// <summary>
		///   Gets or sets the choices of the question.
		/// </summary>
		public IEnumerable<SurveyViewDataChoice> Choices { get; set; }

		/// <summary>
		///   Gets or sets the id of the question.
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		///   Gets or sets the text of the question.
		/// </summary>
		public string Text { get; set; }
	}
}