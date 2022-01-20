namespace SurveyViewerService.Model
{
	using System;
	using System.Collections.Generic;
	using Google.Cloud.Firestore;
	using SurveyViewerService.Contracts;
	using SurveyViewerService.Converter;

	/// <summary>
	///   Describes a survey result.
	/// </summary>
	[FirestoreData]
	public class SurveyResult : ISurveyResult
	{
		/// <summary>
		///   Gets or sets the answers of the participant.
		/// </summary>
		[FirestoreProperty(ConverterType = typeof(AnswerConverter), Name = "results")]
		public IEnumerable<IAnswer> Answers { get; set; }

		/// <summary>
		///   Gets or sets the id of the participant.
		/// </summary>
		[FirestoreProperty("participantId")]
		public string ParticipantId { get; set; }

		/// <summary>
		///   Gets or sets the id of the survey.
		/// </summary>
		[FirestoreProperty("surveyId")]
		public string SurveyId { get; set; }

		/// <summary>
		///   Gets the creation data of the data.
		/// </summary>
		[FirestoreProperty("timestamp")]
		public DateTime TimeStamp { get; set; }
	}
}