namespace SaveSurveyResultService.Model
{
	using System.Collections.Generic;
	using Newtonsoft.Json;
	using SaveSurveyResultService.Contracts;

	/// <summary>
	///   Specifies the incoming messages of the cloud function.
	/// </summary>
	public class Message : IMessage
	{
		/// <summary>
		///   Creates a new instance of <see cref="Message" />.
		/// </summary>
		/// <param name="results">The results of the survey.</param>
		[JsonConstructor]
		public Message(IEnumerable<Result> results)
		{
			this.Results = results;
		}

		/// <summary>
		///   Gets or sets the id of the participant.
		/// </summary>
		[JsonProperty("participantId", Required = Required.Always)]
		public string ParticipantId { get; set; }

		/// <summary>
		///   Gets or sets the survey results.
		/// </summary>
		[JsonProperty("results", Required = Required.Always)]
		public IEnumerable<IResult> Results { get; set; }

		/// <summary>
		///   Gets or sets the id of the survey.
		/// </summary>
		[JsonProperty("surveyId", Required = Required.Always)]
		public string SurveyId { get; set; }
	}
}