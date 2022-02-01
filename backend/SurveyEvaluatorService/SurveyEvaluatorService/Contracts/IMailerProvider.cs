namespace SurveyEvaluatorService.Contracts
{
	using System.Threading.Tasks;

	/// <summary>
	///   Describes provider for creating emails.
	/// </summary>
	public interface IMailerProvider
	{
		/// <summary>
		///   Create a survey closed mail for a participant.
		/// </summary>
		/// <param name="survey">The survey for that the participant voted.</param>
		/// <param name="surveyResult">The last valid survey result of the participant.</param>
		/// <returns>A <see cref="Task" /> whose result is a request for pub/sub.</returns>
		Task<ISendMailRequest> CreateSorryClosedEmailAsync(ISurvey survey, ISurveyResult surveyResult);

		/// <summary>
		///   Create a thank you mail for a participant.
		/// </summary>
		/// <param name="survey">The survey for that the participant voted.</param>
		/// <param name="surveyResult">The survey result of the participant.</param>
		/// <returns>A <see cref="Task" /> whose result is a request for pub/sub.</returns>
		Task<ISendMailRequest> CreateThankYouEmailAsync(ISurvey survey, ISurveyResult surveyResult);
	}
}