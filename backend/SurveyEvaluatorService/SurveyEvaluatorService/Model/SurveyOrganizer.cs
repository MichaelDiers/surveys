namespace SurveyEvaluatorService.Model
{
	using Google.Cloud.Firestore;

	/// <summary>
	///   Describes the organizer of a survey.
	/// </summary>
	[FirestoreData]
	public class SurveyOrganizer
	{
		/// <summary>
		///   Gets or sets the email.
		/// </summary>
		[FirestoreProperty("email")]
		public string Email { get; set; }

		/// <summary>
		///   Gets or sets the name.
		/// </summary>
		[FirestoreProperty("name")]
		public string Name { get; set; }
	}
}