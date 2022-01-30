namespace SurveyEvaluatorService.Model
{
	using System;
	using Google.Cloud.Firestore;
	using SurveyEvaluatorService.Contracts;
	using SurveyEvaluatorService.Converter;

	/// <summary>
	///   Describes a survey status update.
	/// </summary>
	public class SurveyStatus : ISurveyStatus
	{
		/// <summary>
		///   Gets or sets the id of the participant.
		/// </summary>
		[FirestoreProperty("participantId")]
		public string ParticipantId { get; set; }

		/// <summary>
		///   Gets or sets the status of the survey.
		/// </summary>
		[FirestoreProperty("status", ConverterType = typeof(SurveyStatusValueConverter))]
		public SurveyStatusValue Status { get; set; }

		/// <summary>
		///   Gets or sets the id of the survey.
		/// </summary>
		[FirestoreProperty("surveyId")]
		public string SurveyId { get; set; }

		/// <summary>
		///   Gets or sets the creation data of the status.
		/// </summary>
		[FirestoreProperty("timestamp")]
		public DateTime Timestamp { get; set; }
	}
}