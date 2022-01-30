namespace SurveyEvaluatorService.Contracts
{
	/// <summary>
	///   Describes an email recipient.
	/// </summary>
	public interface ISendMailRequestRecipient
	{
		/// <summary>
		///   Gets the email address.
		/// </summary>
		string Email { get; }

		/// <summary>
		///   Gets the name of the recipient.
		/// </summary>
		string Name { get; }
	}
}