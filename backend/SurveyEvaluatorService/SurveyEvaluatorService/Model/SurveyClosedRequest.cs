namespace SurveyEvaluatorService.Model
{
	using System.Collections.Generic;
	using Newtonsoft.Json;
	using SurveyEvaluatorService.Contracts;

	/// <summary>
	///   Describes a survey closed request.
	/// </summary>
	public class SurveyClosedRequest : ISurveyClosedRequest
	{
		/// <summary>
		///   Creates a new instance of <see cref="SurveyClosedRequest" />.
		/// </summary>
		public SurveyClosedRequest()
		{
			this.Participants = new List<SurveyClosedRequestParticipant>();
		}

		/// <summary>
		///   Gets or sets the name of the survey.
		/// </summary>
		[JsonProperty("surveyName", Order = 1)]
		public string Name { get; set; }

		/// <summary>
		///   Gets or sets the organizer of the survey.
		/// </summary>
		[JsonProperty("organizer", Order = 3)]
		public SurveyClosedRequestOrganizer Organizer { get; set; }

		/// <summary>
		///   Gets or sets the participants of the survey.
		/// </summary>
		[JsonProperty("participants", Order = 2)]
		public IList<SurveyClosedRequestParticipant> Participants { get; set; }
	}
}