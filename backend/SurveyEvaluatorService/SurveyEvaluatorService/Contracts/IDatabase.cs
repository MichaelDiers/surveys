﻿namespace SurveyEvaluatorService.Contracts
{
	using System.Collections.Generic;
	using System.Threading.Tasks;

	/// <summary>
	///   Access to the firestore database.
	/// </summary>
	public interface IDatabase
	{
		/// <summary>
		///   Reads a survey by its id.
		/// </summary>
		/// <param name="surveyId">The id of the survey.</param>
		/// <returns>A <see cref="Task" /> whose result is a <see cref="ISurvey" />.</returns>
		Task<ISurvey> ReadSurveyAsync(string surveyId);

		/// <summary>
		///   Read all survey results for a given survey.
		/// </summary>
		/// <param name="surveyId">The id the survey.</param>
		/// <returns>A <see cref="Task" /> whose result is an <see cref="IEnumerable{T}" /> of <see cref="ISurveyResult" />.</returns>
		Task<IEnumerable<ISurveyResult>> ReadSurveyResultsAsync(string surveyId);

		/// <summary>
		///   Reads all status updates for a survey. The result is ordered by the timestamp.
		/// </summary>
		/// <param name="surveyId">The id of the survey.</param>
		/// <returns>The status updates.</returns>
		Task<IEnumerable<ISurveyStatus>> ReadSurveyStatusAsync(string surveyId);
	}
}