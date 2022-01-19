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
	public class AddressConverter : IFirestoreConverter<IAddress>
	{
		/// <summary>
		///   Converts firestore data to an address object.
		/// </summary>
		/// <param name="value">The database value.</param>
		/// <returns>An <see cref="IAddress" />.</returns>
		public IAddress FromFirestore(object value)
		{
			if (!(value is IDictionary<string, object> data))
			{
				throw new ArgumentException($"Unexpected data: {value.GetType()}");
			}

			return new Address
			{
				Email = (string) data["email"],
				Name = (string) data["name"]
			};
		}

		/// <summary>
		///   Method is not implemented. Service is readonly.
		/// </summary>
		/// <param name="value">Method is not implemented. Service is readonly.</param>
		/// <returns>Method is not implemented. Service is readonly.</returns>
		public object ToFirestore(IAddress value)
		{
			throw new NotImplementedException();
		}
	}
}