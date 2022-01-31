namespace SurveyEvaluatorService.Model
{
	using System;
	using System.Collections.Generic;
	using Google.Cloud.Firestore;
	using Newtonsoft.Json;
	using SurveyEvaluatorService.Contracts;
	using SurveyEvaluatorService.Converter;

	/// <summary>
	///   Describes a survey result.
	/// </summary>
	[FirestoreData]
	public class SurveyResult : ISurveyResult
	{
		/// <summary>
		///   Creates a new instance of <see cref="SurveyResult" />.
		/// </summary>
		public SurveyResult()
		{
		}

		/// <summary>
		///   Creates a new instance of <see cref="SurveyResult" />.
		/// </summary>
		/// <param name="results">The results of a survey for a participant.</param>
		[JsonConstructor]
		public SurveyResult(IEnumerable<SurveyResultAnswer> results)
		{
			this.Results = results;
		}

		/// <summary>
		///   Gets or sets the id of a participant.
		/// </summary>
		[JsonProperty("participantId")]
		[FirestoreProperty("participantId")]
		public string ParticipantId { get; set; }

		/// <summary>
		///   Gets or sets the results of the survey for the participant.
		/// </summary>
		[JsonProperty("results", Required = Required.Always)]
		[FirestoreProperty("results", ConverterType = typeof(SurveyResultAnswerConverter))]
		public IEnumerable<ISurveyResultAnswer> Results { get; set; }

		/// <summary>
		///   Gets or sets the id of the survey.
		/// </summary>
		[JsonProperty("surveyId", Required = Required.Always)]
		[FirestoreProperty("surveyId")]
		public string SurveyId { get; set; }

		/// <summary>
		///   Gets or sets the creation timestamp of the result.
		/// </summary>
		[JsonProperty("timestamp", Required = Required.Always)]
		[FirestoreProperty("timestamp")]
		public DateTime Timestamp { get; set; }
	}
}