namespace SurveyEvaluatorService.Model
{
	using Newtonsoft.Json;

	/// <summary>
	///   Describes an organizer.
	/// </summary>
	public class SurveyClosedRequestOrganizer
	{
		/// <summary>
		///   Creates a new instance of <see cref="SurveyClosedRequestOrganizer" />.
		/// </summary>
		public SurveyClosedRequestOrganizer()
			: this(null, null)
		{
		}

		/// <summary>
		///   Creates a new instance of <see cref="SurveyClosedRequestOrganizer" />.
		/// </summary>
		/// <param name="organizer">Object is initialized from the given organizer.</param>
		public SurveyClosedRequestOrganizer(SurveyOrganizer organizer)
			: this(organizer?.Name, organizer?.Email)
		{
		}

		/// <summary>
		///   Creates a new instance of <see cref="SurveyClosedRequestOrganizer" />.
		/// </summary>
		/// <param name="name">The name of the organizer.</param>
		/// <param name="email">The email of the organizer.</param>
		private SurveyClosedRequestOrganizer(string name, string email)
		{
			this.Name = name;
			this.Email = email;
		}

		/// <summary>
		///   Gets or set the email of the organizer.
		/// </summary>
		[JsonProperty("email", Order = 2)]
		public string Email { get; set; }

		/// <summary>
		///   Gets or sets the name of the organizer.
		/// </summary>
		[JsonProperty("name", Order = 1)]
		public string Name { get; set; }
	}
}