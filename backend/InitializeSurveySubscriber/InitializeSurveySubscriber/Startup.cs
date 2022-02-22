namespace InitializeSurveySubscriber
{
    using Google.Cloud.Functions.Hosting;
    using InitializeSurveySubscriber.Contracts;
    using InitializeSurveySubscriber.Logic;
    using InitializeSurveySubscriber.Model;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
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

            services.AddScoped<IFunctionProvider>(
                _ => new FunctionProvider(
                    configuration,
                    new PubSub(new PubSubConfiguration(configuration.ProjectId, configuration.SaveSurveyTopicName)),
                    new PubSub(
                        new PubSubConfiguration(configuration.ProjectId, configuration.SaveSurveyResultTopicName)),
                    new PubSub(
                        new PubSubConfiguration(configuration.ProjectId, configuration.SaveSurveyStatusTopicName))));
        }
    }
}
