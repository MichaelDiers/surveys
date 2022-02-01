namespace SurveyEvaluatorService.Converter
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Linq;
	using Google.Cloud.Firestore;
	using Newtonsoft.Json;
	using SurveyEvaluatorService.Contracts;

	/// <summary>
	///   Firestore data converter for <see cref="SurveyStatusValue" />.
	///   In addition converts from and to json.
	/// </summary>
	public class SurveyStatusValueConverter : JsonConverter, IFirestoreConverter<SurveyStatusValue>
	{
		/// <summary>
		///   Survey closed status.
		/// </summary>
		private const string Closed = "CLOSED";

		/// <summary>
		///   Created state.
		/// </summary>
		private const string Created = "CREATED";

		/// <summary>
		///   Invitation mail sent failed status.
		/// </summary>
		private const string InvitationMailFailed = "INVITATION_MAIL_FAILED";

		/// <summary>
		///   Invitation mail sent ok status.
		/// </summary>
		private const string InvitationMailOk = "INVITATION_MAIL_OK";

		/// <summary>
		///   Survey closed mail sent failed status.
		/// </summary>
		private const string SurveyClosedMailFailed = "SURVEY_CLOSED_MAIL_FAILED";

		/// <summary>
		///   Survey closed mail sent ok status.
		/// </summary>
		private const string SurveyClosedMailOk = "SURVEY_CLOSED_MAIL_OK";

		/// <summary>
		///   Thank you mail sent failed status.
		/// </summary>
		private const string ThankYouMailFailed = "THANK_YOU_MAIL_FAILED";

		/// <summary>
		///   Thank you mail sent ok status.
		/// </summary>
		private const string ThankYouMailOk = "THANK_YOU_MAIL_OK";

		/// <summary>
		///   Mapping of <see cref="string" /> to <see cref="SurveyStatusValue" />.
		/// </summary>
		private static readonly IReadOnlyDictionary<string, SurveyStatusValue> Map =
			new ReadOnlyDictionary<string, SurveyStatusValue>(
				new Dictionary<string, SurveyStatusValue>
				{
					{Created, SurveyStatusValue.Created},
					{InvitationMailOk, SurveyStatusValue.InvitationMailOk},
					{Closed, SurveyStatusValue.Closed},
					{ThankYouMailOk, SurveyStatusValue.ThankYouMailOk},
					{ThankYouMailFailed, SurveyStatusValue.ThankYouMailFailed},
					{InvitationMailFailed, SurveyStatusValue.InvitationMailFailed},
					{SurveyClosedMailOk, SurveyStatusValue.SurveyClosedMailOk},
					{SurveyClosedMailFailed, SurveyStatusValue.SurveyClosedMailFailed}
				});

		/// <summary>
		///   Converts a firestore value to a <see cref="SurveyStatusValue" />.
		/// </summary>
		/// <param name="value">The firestore value to be converted.</param>
		/// <returns>The converted value.</returns>
		public SurveyStatusValue FromFirestore(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException(nameof(value));
			}

			return Map[(string) value];
		}

		/// <summary>
		///   Convert a <see cref="SurveyStatusValue" /> to its firestore value.
		/// </summary>
		/// <param name="value">The value to be converted.</param>
		/// <returns>The value as it is stored in firestore.</returns>
		public object ToFirestore(SurveyStatusValue value)
		{
			return Map.First(item => item.Value == value).Key;
		}

		/// <summary>
		///   Check if the type can be converted.
		/// </summary>
		/// <param name="objectType">The type to checked.</param>
		/// <returns>True if the value can be converted and false otherwise.</returns>
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(string);
		}

		/// <summary>
		///   Convert the given string to a <see cref="SurveyStatusValue" />.
		/// </summary>
		/// <param name="reader">The used reader.</param>
		/// <param name="objectType">The type of the object.</param>
		/// <param name="existingValue">The value to be converted.</param>
		/// <param name="serializer">The used serializer.</param>
		/// <returns>A <see cref="SurveyStatusValue" /> as an <see cref="object" />.</returns>
		public override object ReadJson(
			JsonReader reader,
			Type objectType,
			object existingValue,
			JsonSerializer serializer)
		{
			if (existingValue == null)
			{
				throw new ArgumentNullException(nameof(existingValue));
			}

			return this.FromFirestore(existingValue);
		}

		/// <summary>
		///   Serialize a <see cref="SurveyStatusValue" /> to <see cref="string" />.
		/// </summary>
		/// <param name="writer">The result writer.</param>
		/// <param name="value">The value to be converted.</param>
		/// <param name="serializer">The used serializer.</param>
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				throw new ArgumentNullException(nameof(value));
			}

			writer.WriteValue(this.ToFirestore((SurveyStatusValue) value));
		}
	}
}