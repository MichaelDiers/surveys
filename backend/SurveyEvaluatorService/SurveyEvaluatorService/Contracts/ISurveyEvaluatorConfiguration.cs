﻿namespace SurveyEvaluatorService.Contracts
{
	/// <summary>
	///   Describes the app settings.
	/// </summary>
	public interface ISurveyEvaluatorConfiguration
	{
		/// <summary>
		///   Gets the name of the collection that contains survey status updates.
		/// </summary>
		string CollectionNameStatus { get; }

		/// <summary>
		///   Gets the name of the collection that contains surveys.
		/// </summary>
		string CollectionNameSurveys { get; }

		/// <summary>
		///   Gets the id of the project.
		/// </summary>
		string ProjectId { get; }

		/// <summary>
		///   Gets the name of the pub/sub topic for sending emails.
		/// </summary>
		string TopicNameSendMail { get; }
	}
}