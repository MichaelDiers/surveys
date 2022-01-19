namespace SurveyViewerService.Model
{
	using System.Collections.Generic;
	using Google.Cloud.Firestore;
	using SurveyViewerService.Contracts;
	using SurveyViewerService.Converter;

	/// <summary>
	///   Describes a survey.
	/// </summary>
	[FirestoreData]
	public class Survey : ISurvey
	{
		/// <summary>
		///   Gets or sets the name of the survey.
		/// </summary>
		[FirestoreProperty("name")]
		public string Name { get; set; }

		/// <summary>
		///   Gets or sets the organizer of a survey.
		/// </summary>
		[FirestoreProperty(ConverterType = typeof(AddressConverter), Name = "organizer")]
		public IAddress Organizer { get; set; }

		/// <summary>
		///   Gets or sets the participants of the survey.
		/// </summary>
		[FirestoreProperty(ConverterType = typeof(ParticipantConverter), Name = "participants")]
		public IEnumerable<IParticipant> Participants { get; set; }


		/// <summary>
		///   Gets or sets the questions of the survey.
		/// </summary>
		[FirestoreProperty(ConverterType = typeof(QuestionConverter), Name = "questions")]
		public IEnumerable<IQuestion> Questions { get; set; }

		/// <summary>
		///   Gets or sets the id of the survey.
		/// </summary>
		public string SurveyId { get; set; }
	}
}