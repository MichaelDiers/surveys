namespace SurveyViewerService.Model
{
	using System.Collections.Generic;
	using SurveyViewerService.Contracts;

	/// <summary>
	///   Describes a question of a survey.
	/// </summary>
	public class Question : IQuestion
	{
		/// <summary>
		///   Gets or sets the answer choices for the question.
		/// </summary>
		public IEnumerable<IChoice> Choices { get; set; }

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