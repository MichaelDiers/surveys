namespace SurveyEvaluatorService.Converter
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Google.Cloud.Firestore;
	using SurveyEvaluatorService.Contracts;
	using SurveyEvaluatorService.Model;

	/// <summary>
	///   Converter for firestore objects to <see cref="IEnumerable{T}" /> of <see cref="ISurveyResultAnswer" />.
	/// </summary>
	public class SurveyResultAnswerConverter : IFirestoreConverter<IEnumerable<ISurveyResultAnswer>>
	{
		/// <summary>
		///   Convert data from to firestore.
		/// </summary>
		/// <param name="value">The firestore value.</param>
		/// <returns>An <see cref="IEnumerable{T}" /> of <see cref="ISurveyResultAnswer" />.</returns>
		public IEnumerable<ISurveyResultAnswer> FromFirestore(object value)
		{
			if (!(value is IEnumerable<object> entries))
			{
				throw new ArgumentException("Unable to parse value.", nameof(value));
			}

			return entries.Select(entry => new SurveyResultAnswer((IReadOnlyDictionary<string, object>) entry)).ToArray();
		}

		/// <summary>
		///   Not implemented.
		/// </summary>
		/// <param name="value">Not implemented.</param>
		/// <returns>Not implemented.</returns>
		public object ToFirestore(IEnumerable<ISurveyResultAnswer> value)
		{
			throw new NotImplementedException();
		}
	}
}