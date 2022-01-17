namespace UpdateSurveyStatusService.Logic
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Newtonsoft.Json;
	using UpdateSurveyStatusService.Contracts;

	/// <summary>
	///   Convert <see cref="Status" /> values from and to json.
	/// </summary>
	public class StatusJsonConverter : JsonConverter<Status>
	{
		/// <summary>
		///   A <see cref="Dictionary{TKey,TValue}" /> of valid conversions.
		/// </summary>
		private static readonly IDictionary<Status, string> StatusToString = new Dictionary<Status, string>
		{
			{Status.Created, "CREATED"},
			{Status.InvitationMailOk, "INVITATION_MAIL_OK"},
			{Status.InvitationMailFailed, "INVITATION_MAIL_FAILED"}
		};

		/// <summary>
		///   Convert a <see cref="Status" /> to <see cref="string" />.
		/// </summary>
		/// <param name="status">The status to be converted.</param>
		/// <returns>A <see cref="string" />.</returns>
		public static string ConvertStatusToString(Status status)
		{
			return StatusToString[status];
		}

		/// <summary>
		///   Reads the JSON representation of the object.
		/// </summary>
		/// <param name="reader">The <see cref="JsonReader" /> to read from.</param>
		/// <param name="objectType">Type of the object.</param>
		/// <param name="existingValue">
		///   The existing value of object being read. If there is no existing value then <c>null</c>
		///   will be used.
		/// </param>
		/// <param name="hasExistingValue">The existing value has a value.</param>
		/// <param name="serializer">The calling serializer.</param>
		/// <returns>The object value.</returns>
		public override Status ReadJson(
			JsonReader reader,
			Type objectType,
			Status existingValue,
			bool hasExistingValue,
			JsonSerializer serializer)
		{
			var value = (string) reader.Value;
			var (status, statusString) = StatusToString.FirstOrDefault(
				x => string.Equals(value, x.Value, StringComparison.InvariantCultureIgnoreCase));
			if (string.Equals(value, statusString, StringComparison.InvariantCultureIgnoreCase))
			{
				return status;
			}

			throw new ArgumentOutOfRangeException(nameof(value), value, null);
		}

		/// <summary>
		///   Writes the JSON representation of the object.
		/// </summary>
		/// <param name="writer">The <see cref="JsonWriter" /> to write to.</param>
		/// <param name="value">The value.</param>
		/// <param name="serializer">The calling serializer.</param>
		public override void WriteJson(JsonWriter writer, Status value, JsonSerializer serializer)
		{
			writer.WriteValue(ConvertStatusToString(value));
		}
	}
}