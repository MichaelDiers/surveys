namespace SurveyViewerService.Model
{
	using System;
	using Google.Cloud.Firestore;
	using SurveyViewerService.Contracts;

	/// <summary>
	///   Describes status data of a survey.
	/// </summary>
	[FirestoreData]
	public class SurveyStatus : ISurveyStatus
	{
		/// <summary>
		///   Gets or sets the id of the survey participant.
		/// </summary>
		[FirestoreProperty("participantId")]
		public string ParticipantId { get; set; }

		/// <summary>
		///   Gets or sets the status of the survey.
		/// </summary>
		[FirestoreProperty("status")]
		public string Status { get; set; }

		/// <summary>
		///   Gets or sets the id of the survey.
		/// </summary>
		[FirestoreProperty("surveyId")]
		public string SurveyId { get; set; }

		/// <summary>
		///   Gets or sets the creation date and time of the status.
		/// </summary>
		[FirestoreProperty("timestamp")]
		public DateTime TimeStamp { get; set; }
	}
}