﻿namespace UpdateSurveyService.Logic
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Newtonsoft.Json;
	using UpdateSurveyService.Contracts;
	using UpdateSurveyService.Model;

	/// <summary>
	///   Provider for handling survey updates.
	/// </summary>
	public class UpdateProvider : IUpdateProvider
	{
		/// <summary>
		///   Access to the firestore database.
		/// </summary>
		private readonly IDatabase database;

		/// <summary>
		///   Creates a new instance of <see cref="UpdateProvider" />.
		/// </summary>
		/// <param name="database">Access to the firestore database.</param>
		public UpdateProvider(IDatabase database)
		{
			this.database = database ?? throw new ArgumentNullException(nameof(database));
		}

		/// <summary>
		///   Update a survey.
		/// </summary>
		/// <param name="json">A <see cref="Message" /> in json format.</param>
		/// <returns>A <see cref="Task" />.</returns>
		public async Task Update(string json)
		{
			if (json == null)
			{
				throw new ArgumentNullException(nameof(json));
			}

			var message = JsonConvert.DeserializeObject<Message>(json);
			if (message == null)
			{
				throw new ArgumentException($"Cannot parse json message: {json}", nameof(json));
			}

			var updates = new Dictionary<string, object>();
			switch (message.Type)
			{
				case MessageType.Status:
					updates.Add("status", message.Status);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(message.Type), message.Type, "Cannot handle value.");
			}

			await this.database.Update(message.SurveyId, updates);
		}
	}
}