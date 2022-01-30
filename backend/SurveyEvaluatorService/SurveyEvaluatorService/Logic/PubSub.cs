namespace SurveyEvaluatorService.Logic
{
	using System;
	using System.Threading.Tasks;
	using Google.Cloud.PubSub.V1;
	using Newtonsoft.Json;
	using SurveyEvaluatorService.Contracts;

	/// <summary>
	///   Access to google cloud Pub/Sub.
	/// </summary>
	public class PubSub : IPubSub
	{
		/// <summary>
		///   Access to the application settings.
		/// </summary>
		private readonly ISurveyEvaluatorConfiguration configuration;

		/// <summary>
		///   Access google cloud Pub/Sub.
		/// </summary>
		private PublisherClient client;

		/// <summary>
		///   Create a new instance of <see cref="PubSub" />.
		/// </summary>
		/// <param name="configuration">Access to the application settings.</param>
		public PubSub(ISurveyEvaluatorConfiguration configuration)
		{
			this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
		}

		/// <summary>
		///   Send a mail request to pub/sub.
		/// </summary>
		/// <param name="request">The request data.</param>
		/// <returns>A <see cref="Task" />.</returns>
		public async Task SendMailAsync(ISendMailRequest request)
		{
			if (this.client == null)
			{
				var topic = TopicName.FromProjectTopic(this.configuration.ProjectId, this.configuration.TopicNameSendMail);
				this.client = await PublisherClient.CreateAsync(topic);
			}

			var json = JsonConvert.SerializeObject(request);
			await this.client.PublishAsync(json);
		}
	}
}