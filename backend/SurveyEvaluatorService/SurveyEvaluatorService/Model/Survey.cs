namespace SurveyEvaluatorService.Model
{
	using System;
	using System.Collections.Generic;
	using Google.Cloud.Firestore;
	using SurveyEvaluatorService.Contracts;

	/// <summary>
	///   Describes the data of a survey.
	/// </summary>
	[FirestoreData]
	public class Survey : ISurvey
	{
		/// <summary>
		///   Gets or sets the id of the survey-
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		///   Gets or sets the name of the survey.
		/// </summary>
		[FirestoreProperty("name")]
		public string Name { get; set; }

		/// <summary>
		///   Gets or sets the organizer of the survey.
		/// </summary>
		[FirestoreProperty("organizer")]
		public SurveyOrganizer Organizer { get; set; }

		/// <summary>
		///   Gets or sets the id of the survey participants.
		/// </summary>
		[FirestoreProperty("participantIds")]
		public IEnumerable<string> ParticipantIds { get; set; }

		/// <summary>
		///   Gets or sets the participants of the survey.
		/// </summary>
		[FirestoreProperty("participants")]
		public IEnumerable<SurveyParticipant> Participants { get; set; }

		/// <summary>
		///   Gets or sets the questions.
		/// </summary>
		[FirestoreProperty("questions")]
		public IEnumerable<SurveyQuestion> Questions { get; set; }

		/// <summary>
		///   Gets or set the creation date.
		/// </summary>
		[FirestoreProperty("timestamp")]
		public DateTime Timestamp { get; set; }
	}
}