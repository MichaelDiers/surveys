namespace SurveyViewerService.Converter
{
	using System;
	using System.Collections.Generic;
	using Google.Cloud.Firestore;
	using SurveyViewerService.Contracts;
	using SurveyViewerService.Model;

	/// <summary>
	///   Converter for firestore data.
	/// </summary>
	public class AnswerConverter : IFirestoreConverter<IEnumerable<IAnswer>>
	{
		/// <summary>
		///   Converts firestore data to an answer object.
		/// </summary>
		/// <param name="value">The database value.</param>
		/// <returns>An <see cref="IEnumerable{T}" /> of <see cref="IAnswer" />.</returns>
		public IEnumerable<IAnswer> FromFirestore(object value)
		{
			if (!(value is IEnumerable<object> enumerable))
			{
				throw new ArgumentException($"Unexpected data: {value.GetType()}");
			}

			foreach (IDictionary<string, object> dictionary in enumerable)
			{
				yield return new Answer
				{
					QuestionId = (string) dictionary["questionId"],
					Value = Convert.ToInt32(dictionary["answer"])
				};
			}
		}

		/// <summary>
		///   Method is not implemented. Service is readonly.
		/// </summary>
		/// <param name="value">Method is not implemented. Service is readonly.</param>
		/// <returns>Method is not implemented. Service is readonly.</returns>
		public object ToFirestore(IEnumerable<IAnswer> value)
		{
			throw new NotImplementedException();
		}
	}
}