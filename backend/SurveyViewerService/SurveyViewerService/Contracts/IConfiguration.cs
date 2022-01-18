﻿namespace SurveyViewerService.Contracts
{
	/// <summary>
	///   Describes the application configuration.
	/// </summary>
	public interface IConfiguration
	{
		/// <summary>
		///   Gets the id of the project.
		/// </summary>
		string ProjectId { get; }
	}
}