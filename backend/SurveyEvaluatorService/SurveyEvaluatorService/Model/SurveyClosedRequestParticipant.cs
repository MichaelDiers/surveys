namespace SurveyEvaluatorService.Model
{
	using System.Collections.Generic;
	using Newtonsoft.Json;

	/// <summary>
	///   Describes a participant.
	/// </summary>
	public class SurveyClosedRequestParticipant
	{
		/// <summary>
		///   Creates a new instance of <see cref="SurveyClosedRequestParticipant" />.
		/// </summary>
		public SurveyClosedRequestParticipant()
			: this(null, null)
		{
		}

		/// <summary>
		///   Creates a new instance of <see cref="SurveyClosedRequestParticipant" />.
		/// </summary>
		/// <param name="participant">Object is initialized from the given participant.</param>
		public SurveyClosedRequestParticipant(SurveyParticipant participant)
			: this(participant?.Name, participant?.Email)
		{
		}

		/// <summary>
		///   Creates a new instance of <see cref="SurveyClosedRequestParticipant" />.
		/// </summary>
		/// <param name="name">The name of the participant.</param>
		/// <param name="email">The email of the participant.</param>
		private SurveyClosedRequestParticipant(string name, string email)
		{
			this.Answers = new List<SurveyClosedRequestParticipantAnswer>();
			this.Name = name;
			this.Email = email;
		}

		/// <summary>
		///   Gets or sets the answers for the survey.
		/// </summary>
		[JsonProperty("answers", Order = 3)]
		public IList<SurveyClosedRequestParticipantAnswer> Answers { get; set; }

		/// <summary>
		///   Gets or set the email of the participant.
		/// </summary>
		[JsonProperty("email", Order = 2)]
		public string Email { get; set; }

		/// <summary>
		///   Gets or sets the name of the participant.
		/// </summary>
		[JsonProperty("name", Order = 1)]
		public string Name { get; set; }
	}
}