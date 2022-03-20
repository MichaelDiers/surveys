namespace EvaluateSurveySubscriber
{
    using EvaluateSurveySubscriber.Contracts;
    using EvaluateSurveySubscriber.Logic;
    using EvaluateSurveySubscriber.Model;
    using Google.Cloud.Functions.Hosting;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.GoogleCloud.Base.Logic;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Surveys.Common.Contracts.Messages;

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

            services.AddScoped<ISurveyDatabase>(
                _ => new SurveyDatabase(
                    new DatabaseConfiguration(configuration.ProjectId, configuration.SurveysCollectionName)));
            services.AddScoped<ISurveyResultsDatabase>(
                _ => new SurveyResultsDatabase(
                    new DatabaseConfiguration(configuration.ProjectId, configuration.SurveyResultsCollectionName)));
            services.AddScoped<ISurveyStatusDatabase>(
                _ => new SurveyStatusDatabase(
                    new DatabaseConfiguration(configuration.ProjectId, configuration.SurveyStatusCollectionName)));

            services.AddScoped<ISaveSurveyStatusPubSubClient>(
                _ => new SaveSurveyStatusPubSubClient(
                    new PubSubClientConfiguration(configuration.ProjectId, string.Empty)));
            services.AddScoped<ISurveyClosedPubSubClient>(
                _ => new SurveyClosedPubSubClient(
                    new PubSubClientConfiguration(configuration.ProjectId, string.Empty)));

            services.AddScoped<IPubSubProvider<IEvaluateSurveyMessage>, FunctionProvider>();
        }
    }
}
