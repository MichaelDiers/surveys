namespace SurveyEvaluatorService.Model
{
	using SurveyEvaluatorService.Contracts;

	/// <summary>
	///   Describes the app settings.
	/// </summary>
	public class SurveyEvaluatorConfiguration : ISurveyEvaluatorConfiguration
	{
		/// <summary>
		///   Gets or sets the name of the collection that contains survey results.
		/// </summary>
		public string CollectionNameResults { get; set; }

		/// <summary>
		///   Gets or sets the name of the collection that contains survey status updates.
		/// </summary>
		public string CollectionNameStatus { get; set; }

		/// <summary>
		///   Gets the name of the collection that contains surveys.
		/// </summary>
		public string CollectionNameSurveys { get; set; }

		/// <summary>
		///   Gets or sets the id of the project.
		/// </summary>
		public string ProjectId { get; set; }

		/// <summary>
		///   Gets the url of the survey viewer.
		/// </summary>
		public string SurveyViewerUrl { get; set; }

		/// <summary>
		///   Gets or sets template for the subject of closed survey emails.
		/// </summary>
		public string TemplateClosedSurveySubject { get; set; }

		/// <summary>
		///   Gets or sets the html template for closed survey emails.
		/// </summary>
		public string TemplateHtmlClosedSurvey { get; set; }

		/// <summary>
		///   Gets or sets the html template for the answer part of closed survey emails.
		/// </summary>
		public string TemplateHtmlClosedSurveyAnswer { get; set; }

		/// <summary>
		///   Gets or sets the html template for thank you mails.
		/// </summary>
		public string TemplateHtmlThankYou { get; set; }

		/// <summary>
		///   Gets or sets the html template for the answer part of thank you mails .
		/// </summary>
		public string TemplateHtmlThankYouAnswer { get; set; }

		/// <summary>
		///   Gets or sets  the text for survey questions without an answer.
		/// </summary>
		public string TemplateNoAnswer { get; set; }

		/// <summary>
		///   Gets or sets the plain text template for closed survey emails.
		/// </summary>
		public string TemplatePlainClosedSurvey { get; set; }

		/// <summary>
		///   Gets or sets the plain text template for the answer part of closed survey emails.
		/// </summary>
		public string TemplatePlainClosedSurveyAnswer { get; set; }

		/// <summary>
		///   Gets or sets the newline replacement for text emails.
		/// </summary>
		public string TemplatePlainNewline { get; set; }

		/// <summary>
		///   Gets or sets the text template for thank you mails.
		/// </summary>
		public string TemplatePlainThankYou { get; set; }

		/// <summary>
		///   Gets or sets the text template for the answer part of thank you mails.
		/// </summary>
		public string TemplatePlainThankYouAnswer { get; set; }

		/// <summary>
		///   Gets or sets the subject for thank you mails.
		/// </summary>
		public string TemplateThankYouSubject { get; set; }

		/// <summary>
		///   Gets or sets the name of the pub/sub topic for sending emails.
		/// </summary>
		public string TopicNameSendMail { get; set; }

		/// <summary>
		///   Gets or sets the name of the pub/sub topic for sending status updates of surveys.
		/// </summary>
		public string TopicNameStatusUpdate { get; set; }
	}
}