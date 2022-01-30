namespace SurveyEvaluatorService.Model
{
	using Google.Cloud.Firestore;

	/// <summary>
	///   Describes a survey participant.
	/// </summary>
	[FirestoreData]
	public class SurveyParticipant
	{
		/// <summary>
		///   Gets or sets the email.
		/// </summary>
		[FirestoreProperty("email")]
		public string Email { get; set; }

		/// <summary>
		///   Gets or sets the id.
		/// </summary>
		[FirestoreProperty("id")]
		public string Id { get; set; }

		/// <summary>
		///   Gets or sets the name.
		/// </summary>
		[FirestoreProperty("name")]
		public string Name { get; set; }
	}
}