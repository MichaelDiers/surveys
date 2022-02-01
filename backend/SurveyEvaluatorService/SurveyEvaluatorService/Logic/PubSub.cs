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
		///   Access google cloud Pub/Sub for sending emails.
		/// </summary>
		private PublisherClient sendMailClient;

		/// <summary>
		///   Access google cloud Pub/Sub for updaing the status of a survey.
		/// </summary>
		private PublisherClient statusUpdateClient;

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
			if (this.sendMailClient == null)
			{
				var topic = TopicName.FromProjectTopic(this.configuration.ProjectId, this.configuration.TopicNameSendMail);
				this.sendMailClient = await PublisherClient.CreateAsync(topic);
			}

			var json = JsonConvert.SerializeObject(request);
			await this.sendMailClient.PublishAsync(json);
		}

		/// <summary>
		///   Send a status update request for a survey.
		/// </summary>
		/// <param name="request">The request data.</param>
		/// <returns>A <see cref="Task" />.</returns>
		public async Task SendStatusUpdateAsync(ISurveyStatusUpdateRequest request)
		{
			if (this.statusUpdateClient == null)
			{
				var topic = TopicName.FromProjectTopic(this.configuration.ProjectId, this.configuration.TopicNameStatusUpdate);
				this.statusUpdateClient = await PublisherClient.CreateAsync(topic);
			}

			var json = JsonConvert.SerializeObject(request);
			await this.statusUpdateClient.PublishAsync(json);
		}

		/// <summary>
		///   Sends the result of the survey to Pub/Sub.
		/// </summary>
		/// <param name="request">The survey result data.</param>
		/// <returns>A <see cref="Task" />.</returns>
		public Task SendSurveyClosed(ISurveyClosedRequest request)
		{
			var json = JsonConvert.SerializeObject(request);
			return Task.CompletedTask;
		}
	}
}