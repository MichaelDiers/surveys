namespace SaveSurveyResultSubscriber
{
    using Google.Cloud.Functions.Hosting;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.GoogleCloud.Base.Logic;
    using Md.GoogleCloudFirestore.Logic;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using SaveSurveyResultSubscriber.Contracts;
    using SaveSurveyResultSubscriber.Logic;
    using SaveSurveyResultSubscriber.Model;
    using Surveys.Common.Contracts;

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


            services.AddScoped<IDatabaseConfiguration>(
                _ => new DatabaseConfiguration(configuration.ProjectId, configuration.CollectionName));
            services.AddScoped<IDatabase, Database>();

            services.AddScoped<IPubSubClientConfiguration>(
                _ => new PubSubClientConfiguration(configuration.ProjectId, configuration.PubSubTopicName));
            services.AddScoped<IPubSubClient, IPubSubClient>();

            services.AddScoped<IPubSubProvider<ISaveSurveyResultMessage>, FunctionProvider>();
        }
    }
}
