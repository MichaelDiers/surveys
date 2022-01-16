namespace UpdateSurveyStatusService.Logic
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Newtonsoft.Json;
	using UpdateSurveyStatusService.Contracts;

	/// <summary>
	///   Convert <see cref="MessageType" /> values from and to json.
	/// </summary>
	public class MessageTypeJsonConverter : JsonConverter<MessageType>
	{
		/// <summary>
		///   A <see cref="Dictionary{TKey,TValue}" /> of valid conversions.
		/// </summary>
		private static readonly IDictionary<MessageType, string> MessageTypeToString = new Dictionary<MessageType, string>
		{
			{MessageType.Survey, "SURVEY"},
			{MessageType.Participant, "PARTICIPANT"}
		};

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
		public override MessageType ReadJson(
			JsonReader reader,
			Type objectType,
			MessageType existingValue,
			bool hasExistingValue,
			JsonSerializer serializer)
		{
			var value = (string) reader.Value;
			var (messageType, messageTypeString) = MessageTypeToString.FirstOrDefault(
				x => string.Equals(value, x.Value, StringComparison.InvariantCultureIgnoreCase));
			if (string.Equals(value, messageTypeString, StringComparison.InvariantCultureIgnoreCase))
			{
				return messageType;
			}

			throw new ArgumentOutOfRangeException(nameof(value), value, null);
		}

		/// <summary>
		///   Writes the JSON representation of the object.
		/// </summary>
		/// <param name="writer">The <see cref="JsonWriter" /> to write to.</param>
		/// <param name="value">The value.</param>
		/// <param name="serializer">The calling serializer.</param>
		public override void WriteJson(JsonWriter writer, MessageType value, JsonSerializer serializer)
		{
			writer.WriteValue(ConvertMessageTypeToString(value));
		}

		/// <summary>
		///   Convert a <see cref="MessageType" /> to <see cref="string" />.
		/// </summary>
		/// <param name="messageType">The message type to be converted.</param>
		/// <returns>A <see cref="string" />.</returns>
		private static string ConvertMessageTypeToString(MessageType messageType)
		{
			return MessageTypeToString[messageType];
		}
	}
}