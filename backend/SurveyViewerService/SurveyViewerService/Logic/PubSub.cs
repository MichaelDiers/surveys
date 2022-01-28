namespace SurveyViewerService.Logic
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using Google.Cloud.PubSub.V1;
	using Newtonsoft.Json;
	using SurveyViewerService.Contracts;
	using SurveyViewerService.Model.SaveSurveyResult;

	/// <summary>
	///   Access google cloud Pub/Sub.
	/// </summary>
	public class PubSub : IPubSub
	{
		private readonly IConfiguration configuration;

		/// <summary>
		///   Access google cloud Pub/Sub.
		/// </summary>
		private PublisherClient client;

		/// <summary>
		///   Creates a new instance of <see cref="PubSub" />.
		/// </summary>
		/// <param name="configuration">Access the application configuration.</param>
		public PubSub(IConfiguration configuration)
		{
			this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
		}

		/// <summary>
		///   Publish a message to Pub/Sub to save the survey result.
		/// </summary>
		/// <param name="surveyId">The id of the survey.</param>
		/// <param name="surveySubmitResult">The result of the survey.</param>
		/// <returns>A <see cref="Task" />.</returns>
		public async Task PublishMessageAsync(string surveyId, ISurveySubmitResult surveySubmitResult)
		{
			if (this.client == null)
			{
				var topic = TopicName.FromProjectTopic(this.configuration.ProjectId, this.configuration.PubSubTopic);
				this.client = await PublisherClient.CreateAsync(topic);
			}

			var request = new SaveSurveyResultRequest(
				surveySubmitResult.Questions.Select(
					q => new SurveyResult
					{
						QuestionId = q.QuestionId,
						QuestionValue = int.Parse(q.Value)
					}))
			{
				ParticipantId = surveySubmitResult.ParticipantId,
				SurveyId = surveyId
			};

			var json = JsonConvert.SerializeObject(request);

			await this.client.PublishAsync(json);
		}
	}
}