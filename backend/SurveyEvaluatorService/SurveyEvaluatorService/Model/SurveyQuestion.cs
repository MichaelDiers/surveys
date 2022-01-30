namespace SurveyEvaluatorService.Model
{
	using System.Collections.Generic;
	using Google.Cloud.Firestore;

	/// <summary>
	///   Describes a survey question.
	/// </summary>
	[FirestoreData]
	public class SurveyQuestion
	{
		/// <summary>
		///   Gets or sets the choices of the question.
		/// </summary>
		[FirestoreProperty("choices")]
		public IEnumerable<SurveyQuestionChoice> Choices { get; set; }

		/// <summary>
		///   Gets or sets the id of the question.
		/// </summary>
		[FirestoreProperty("guid")]
		public string Id { get; set; }

		/// <summary>
		///   Gets or sets the question text.
		/// </summary>
		[FirestoreProperty("question")]
		public string Text { get; set; }
	}
}