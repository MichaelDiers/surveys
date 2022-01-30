namespace SurveyEvaluatorService.Contracts
{
	/// <summary>
	///   Describes an email body.
	/// </summary>
	public interface ISendMailRequestBody
	{
		/// <summary>
		///   Gets the html formatted email body.
		/// </summary>
		string Html { get; }

		/// <summary>
		///   Gets the plain text formatted email body.
		/// </summary>
		string PlainText { get; }
	}
}