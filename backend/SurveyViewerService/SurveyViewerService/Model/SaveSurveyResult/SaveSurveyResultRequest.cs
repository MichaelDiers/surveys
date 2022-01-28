namespace SurveyViewerService.Model.SaveSurveyResult
{
	using System.Collections.Generic;
	using Newtonsoft.Json;
	using SurveyViewerService.Contracts.SaveSurveyResult;

	/// <summary>
	///   Describes a survey result.
	/// </summary>
	public class SaveSurveyResultRequest : ISaveSurveyResultRequest
	{
		/// <summary>
		///   Creates a new instance of <see cref="SaveSurveyResultRequest" />.
		/// </summary>
		/// <param name="results">The results of the survey.</param>
		[JsonConstructor]
		public SaveSurveyResultRequest(IEnumerable<ISurveyResult> results)
		{
			this.Results = results;
		}

		/// <summary>
		///   Gets or sets the id of the participant.
		/// </summary>
		[JsonProperty("participantId", Required = Required.Always, Order = 2)]
		public string ParticipantId { get; set; }

		/// <summary>
		///   Gets or sets the survey results.
		/// </summary>
		[JsonProperty("results", Required = Required.Always, Order = 3)]
		public IEnumerable<ISurveyResult> Results { get; set; }

		/// <summary>
		///   Gets or sets the id of the survey.
		/// </summary>
		[JsonProperty("surveyId", Required = Required.Always, Order = 1)]
		public string SurveyId { get; set; }
	}
}