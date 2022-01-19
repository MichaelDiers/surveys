namespace SurveyViewerService.Converter
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Google.Cloud.Firestore;
	using SurveyViewerService.Contracts;
	using SurveyViewerService.Model;

	/// <summary>
	///   Converter for firestore data.
	/// </summary>
	public class QuestionConverter : IFirestoreConverter<IEnumerable<IQuestion>>
	{
		/// <summary>
		///   Converts firestore data to an enumerable of questions.
		/// </summary>
		/// <param name="value">The database value.</param>
		/// <returns>An <see cref="IEnumerable{T}" /> of <see cref="IQuestion" />.</returns>
		public IEnumerable<IQuestion> FromFirestore(object value)
		{
			if (!(value is IEnumerable<object> enumerable))
			{
				throw new ArgumentException($"Unexpected data: {value.GetType()}");
			}

			foreach (IDictionary<string, object> dictionary in enumerable)
			{
				yield return new Question
				{
					Id = dictionary.ContainsKey("guid") ? dictionary["guid"] as string : null,
					Text = dictionary.ContainsKey("question") ? dictionary["question"] as string : null,
					Choices = ((IEnumerable<object>) dictionary["choices"])
						.Select(choice => choice as IDictionary<string, object>).Where(choice => choice != null).Select(
							choice => new Choice
							{
								Answer = choice.ContainsKey("answer") ? (string) choice["answer"] : null,
								Value = choice.ContainsKey("value") ? choice["value"].ToString() : null
							})
				};
			}
		}

		/// <summary>
		///   Method is not implemented. Service is readonly.
		/// </summary>
		/// <param name="value">Method is not implemented. Service is readonly.</param>
		/// <returns>Method is not implemented. Service is readonly.</returns>
		public object ToFirestore(IEnumerable<IQuestion> value)
		{
			throw new NotImplementedException();
		}
	}
}