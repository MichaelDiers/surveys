namespace SurveyViewerService.Model
{
	using Google.Cloud.Firestore;
	using SurveyViewerService.Contracts;

	/// <summary>
	///   Describes the answer of a question.
	/// </summary>
	[FirestoreData]
	public class Answer : IAnswer
	{
		/// <summary>
		///   Gets or sets the id of the question.
		/// </summary>
		[FirestoreProperty("questionId")]
		public string QuestionId { get; set; }

		/// <summary>
		///   Gets or sets the value of the answer.
		/// </summary>
		[FirestoreProperty("answer")]
		public int Value { get; set; }
	}
}