namespace SurveyViewerService.Contracts
{
	using System.Collections.Generic;

	/// <summary>
	///   Describes a question of a survey.
	/// </summary>
	public interface IQuestion
	{
		/// <summary>
		///   Gets the answer choices for the question.
		/// </summary>
		IEnumerable<IChoice> Choices { get; }

		/// <summary>
		///   Gets the id of the question.
		/// </summary>
		string Id { get; }

		/// <summary>
		///   Gets the text of the question.
		/// </summary>
		string Text { get; }
	}
}