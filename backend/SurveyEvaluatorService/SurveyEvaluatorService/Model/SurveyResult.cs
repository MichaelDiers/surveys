namespace SurveyEvaluatorService.Model
{
	using System;
	using System.Collections.Generic;
	using Newtonsoft.Json;
	using SurveyEvaluatorService.Contracts;

	/// <summary>
	///   Describes a survey result.
	/// </summary>
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
		public string ParticipantId { get; set; }

		/// <summary>
		///   Gets or sets the results of the survey for the participant.
		/// </summary>
		[JsonProperty("results", Required = Required.Always)]
		public IEnumerable<ISurveyResultAnswer> Results { get; set; }

		/// <summary>
		///   Gets or sets the id of the survey.
		/// </summary>
		[JsonProperty("surveyId", Required = Required.Always)]
		public string SurveyId { get; set; }

		/// <summary>
		///   Gets or sets the creation timestamp of the result.
		/// </summary>
		[JsonProperty("timestamp", Required = Required.Always)]
		public DateTime Timestamp { get; set; }
	}
}