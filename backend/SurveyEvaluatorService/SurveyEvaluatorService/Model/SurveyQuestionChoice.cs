namespace SurveyEvaluatorService.Model
{
	using Google.Cloud.Firestore;

	/// <summary>
	///   Describes an answer of a question.
	/// </summary>
	[FirestoreData]
	public class SurveyQuestionChoice
	{
		/// <summary>
		///   Gets or sets the answer of the question.
		/// </summary>
		[FirestoreProperty("answer")]
		public string Answer { get; set; }

		/// <summary>
		///   Gets or sets the value of the
		/// </summary>
		[FirestoreProperty("value")]
		public int Value { get; set; }
	}
}