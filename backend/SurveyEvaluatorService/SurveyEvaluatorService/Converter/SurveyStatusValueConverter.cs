namespace SurveyEvaluatorService.Converter
{
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Linq;
	using Google.Cloud.Firestore;
	using SurveyEvaluatorService.Contracts;

	/// <summary>
	///   Firestore data converter for <see cref="SurveyStatusValue" />.
	/// </summary>
	public class SurveyStatusValueConverter : IFirestoreConverter<SurveyStatusValue>
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
		///   Invitation mail sent ok status.
		/// </summary>
		private const string InvitationMailOk = "INVITATION_MAIL_OK";

		/// <summary>
		///   Mapping of <see cref="string" /> to <see cref="SurveyStatusValue" />.
		/// </summary>
		private static readonly IReadOnlyDictionary<string, SurveyStatusValue> Map =
			new ReadOnlyDictionary<string, SurveyStatusValue>(
				new Dictionary<string, SurveyStatusValue>
				{
					{Created, SurveyStatusValue.Created},
					{InvitationMailOk, SurveyStatusValue.InvitationMailOk},
					{Closed, SurveyStatusValue.Closed}
				});

		/// <summary>
		///   Converts a firestore value to a <see cref="SurveyStatusValue" />.
		/// </summary>
		/// <param name="value">The firestore value to be converted.</param>
		/// <returns>The converted value.</returns>
		public SurveyStatusValue FromFirestore(object value)
		{
			return Map[value.ToString()];
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
	}
}