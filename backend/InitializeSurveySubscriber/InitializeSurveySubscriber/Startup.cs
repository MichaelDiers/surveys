namespace InitializeSurveySubscriber
{
    using Google.Cloud.Functions.Hosting;
    using InitializeSurveySubscriber.Contracts;
    using InitializeSurveySubscriber.Logic;
    using InitializeSurveySubscriber.Model;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.GoogleCloudPubSub.Logic;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Surveys.Common.Contracts;
    using Surveys.Common.PubSub.Contracts.Logic;
    using Surveys.Common.PubSub.Logic;

    /// <summary>
    ///     Initialize the function.
    /// </summary>
    public class Startup : FunctionsStartup
    {
        /// <summary>
        ///     This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="context">The builder context.</param>
        /// <param name="services">Add services to this collection used in dependency injection context.</param>
        public override void ConfigureServices(WebHostBuilderContext context, IServiceCollection services)
        {
            var configuration = new FunctionConfiguration();
            context.Configuration.Bind(configuration);

            services.AddScoped<IFunctionConfiguration>(_ => configuration);

            services.AddSingleton<ISaveSurveyPubSubClient>(
                _ => new SaveSurveyPubSubClient(
                    new PubSubClientEnvironment(
                        configuration.Environment,
                        configuration.ProjectId,
                        configuration.SaveSurveyTopicName)));
            services.AddSingleton<ISaveSurveyResultPubSubClient>(
                _ => new SaveSurveyResultPubSubClient(
                    new PubSubClientEnvironment(
                        configuration.Environment,
                        configuration.ProjectId,
                        configuration.SaveSurveyResultTopicName)));
            services.AddSingleton<ISaveSurveyStatusPubSubClient>(
                _ => new SaveSurveyStatusPubSubClient(
                    new PubSubClientEnvironment(
                        configuration.Environment,
                        configuration.ProjectId,
                        configuration.SaveSurveyStatusTopicName)));
            services.AddSingleton<ICreateMailPubSubClient>(
                _ => new CreateMailPubSubClient(
                    new PubSubClientEnvironment(
                        configuration.Environment,
                        configuration.ProjectId,
                        configuration.CreateMailTopicName)));
            services.AddScoped<IPubSubProvider<IInitializeSurveyMessage>, FunctionProvider>();
        }
    }
}
