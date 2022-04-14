namespace SaveSurveyResultSubscriber
{
    using Google.Cloud.Functions.Hosting;
    using Md.Common.Contracts.Model;
    using Md.GoogleCloudFunctions.Contracts.Logic;
    using Md.GoogleCloudPubSub.Model;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using SaveSurveyResultSubscriber.Contracts;
    using SaveSurveyResultSubscriber.Logic;
    using SaveSurveyResultSubscriber.Model;
    using Surveys.Common.Contracts;
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
            services.AddScoped<ISurveyResultDatabase, SurveyResultDatabase>();
            services.AddScoped<ISurveyReadOnlyDatabase, SurveyReadOnlyDatabase>();

            services.AddScoped<IEvaluateSurveyPubSubClient>(
                _ => new EvaluateSurveyPubSubClient(
                    new PubSubClientEnvironment(
                        configuration.Environment,
                        configuration.ProjectId,
                        configuration.EvaluateSurveyTopicName)));
            services.AddScoped<ICreateMailPubSubClient>(
                _ => new CreateMailPubSubClient(
                    new PubSubClientEnvironment(
                        configuration.Environment,
                        configuration.ProjectId,
                        configuration.CreateMailTopicName)));

            services.AddScoped<IPubSubProvider<ISaveSurveyResultMessage>, FunctionProvider>();
        }
    }
}
