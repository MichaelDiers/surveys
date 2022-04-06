namespace EvaluateSurveySubscriber
{
    using EvaluateSurveySubscriber.Contracts;
    using EvaluateSurveySubscriber.Logic;
    using EvaluateSurveySubscriber.Model;
    using Google.Cloud.Functions.Hosting;
    using Md.Common.Contracts;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.GoogleCloudPubSub.Logic;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Surveys.Common.Contracts.Messages;
    using Surveys.Common.Firestore.Contracts;
    using Surveys.Common.Firestore.Models;
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

            services.AddScoped<IRuntimeEnvironment>(_ => configuration);
            services.AddScoped<ISurveyReadOnlyDatabase, SurveyReadOnlyDatabase>();
            services.AddScoped<ISurveyResultReadOnlyDatabase, SurveyResultReadOnlyDatabase>();
            services.AddScoped<ISurveyStatusReadOnlyDatabase, SurveyStatusReadOnlyDatabase>();

            services.AddScoped<ISaveSurveyStatusPubSubClient>(
                _ => new SaveSurveyStatusPubSubClient(
                    new PubSubClientEnvironment(
                        configuration.Environment,
                        configuration.ProjectId,
                        configuration.SaveSurveyStatusTopicName)));
            services.AddScoped<ISurveyClosedPubSubClient>(
                _ => new SurveyClosedPubSubClient(
                    new PubSubClientEnvironment(
                        configuration.Environment,
                        configuration.ProjectId,
                        configuration.SurveyClosedTopicName)));

            services.AddScoped<IPubSubProvider<IEvaluateSurveyMessage>, FunctionProvider>();
        }
    }
}
