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
	public class ParticipantConverter : IFirestoreConverter<IEnumerable<IParticipant>>
	{
		/// <summary>
		///   Converts firestore data to an enumerable of participants.
		/// </summary>
		/// <param name="value">The database value.</param>
		/// <returns>An <see cref="IEnumerable{T}" /> of <see cref="IParticipant" />.</returns>
		public IEnumerable<IParticipant> FromFirestore(object value)
		{
			if (!(value is IEnumerable<object> enumerable))
			{
				throw new ArgumentException($"Unexpected data: {value.GetType()}");
			}

			foreach (IDictionary<string, object> dictionary in enumerable)
			{
				yield return new Participant
				{
					Email = (string) dictionary["email"],
					Id = (string) dictionary["id"],
					Name = (string) dictionary["name"]
				};
			}
		}

		/// <summary>
		///   Method is not implemented. Service is readonly.
		/// </summary>
		/// <param name="value">Method is not implemented. Service is readonly.</param>
		/// <returns>Method is not implemented. Service is readonly.</returns>
		public object ToFirestore(IEnumerable<IParticipant> value)
		{
			throw new NotImplementedException();
		}
	}
}