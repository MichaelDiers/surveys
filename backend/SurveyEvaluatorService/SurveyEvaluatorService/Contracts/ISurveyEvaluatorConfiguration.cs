namespace SurveyEvaluatorService.Contracts
{
	/// <summary>
	///   Describes the app settings.
	/// </summary>
	public interface ISurveyEvaluatorConfiguration
	{
		/// <summary>
		///   Gets the name of the collection that contains survey results.
		/// </summary>
		string CollectionNameResults { get; }

		/// <summary>
		///   Gets the name of the collection that contains survey status updates.
		/// </summary>
		string CollectionNameStatus { get; }

		/// <summary>
		///   Gets the name of the collection that contains surveys.
		/// </summary>
		string CollectionNameSurveys { get; }

		/// <summary>
		///   Gets the id of the project.
		/// </summary>
		string ProjectId { get; }

		/// <summary>
		///   Gets the url of the survey viewer.
		/// </summary>
		string SurveyViewerUrl { get; }

		/// <summary>
		///   Gets the html template for thank you mails.
		/// </summary>
		string TemplateHtmlThankYou { get; }

		/// <summary>
		///   Gets the html template for the answer part of thank you mails.
		/// </summary>
		string TemplateHtmlThankYouAnswer { get; }

		/// <summary>
		///   Gets the newline replacement for text emails.
		/// </summary>
		string TemplatePlainNewline { get; }

		/// <summary>
		///   Gets the text template for thank you mails.
		/// </summary>
		string TemplatePlainThankYou { get; }

		/// <summary>
		///   Gets the text template for the answer part of thank you mails.
		/// </summary>
		string TemplatePlainThankYouAnswer { get; }

		/// <summary>
		///   Gets the subject for thank you mails.
		/// </summary>
		string TemplateThankYouSubject { get; }

		/// <summary>
		///   Gets the name of the pub/sub topic for sending emails.
		/// </summary>
		string TopicNameSendMail { get; }

		/// <summary>
		///   Gets the name of the pub/sub topic for sending status updates of surveys.
		/// </summary>
		string TopicNameStatusUpdate { get; }
	}
}