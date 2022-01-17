namespace MailerService.Logic
{
	using System;
	using System.Threading.Tasks;
	using Google.Cloud.PubSub.V1;
	using MailerService.Contracts;

	/// <summary>
	///   Access google cloud Pub/Sub.
	/// </summary>
	public class PubSub : IPubSub
	{
		/// <summary>
		///   Access the application configuration.
		/// </summary>
		private readonly IMailerServiceConfiguration configuration;

		/// <summary>
		///   Access google cloud Pub/Sub.
		/// </summary>
		private PublisherClient client;

		/// <summary>
		///   Creates a new instance of <see cref="PubSub" />.
		/// </summary>
		/// <param name="configuration">Access the application configuration.</param>
		public PubSub(IMailerServiceConfiguration configuration)
		{
			this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
		}

		/// <summary>
		///   Publish a message to Pub/Sub that updates the status of a survey.
		/// </summary>
		/// <param name="surveyId">The id of the survey.</param>
		/// <param name="participantId">The id of the survey participant.</param>
		/// <param name="status">The new status of the survey.</param>
		/// <returns>A <see cref="Task" />.</returns>
		public async Task PublishUpdateAsync(string surveyId, string participantId, string status)
		{
			if (this.client == null)
			{
				var topic = TopicName.FromProjectTopic(this.configuration.ProjectId, this.configuration.TopicName);
				this.client = await PublisherClient.CreateAsync(topic);
			}

			var message = $"{{\"surveyId\":\"{surveyId}\",\"participantId\":\"{participantId}\",\"status\":\"{status}\"}}";
			Console.WriteLine(message);
			await this.client.PublishAsync(message);
		}
	}
}