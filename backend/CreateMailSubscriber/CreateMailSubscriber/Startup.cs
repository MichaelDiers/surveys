namespace CreateMailSubscriber
{
    using CreateMailSubscriber.Contracts;
    using CreateMailSubscriber.Logic;
    using CreateMailSubscriber.Model;
    using Google.Cloud.Functions.Hosting;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Md.GoogleCloud.Base.Logic;
    using Md.GoogleCloudFirestore.Logic;
    using Md.GoogleCloudPubSub.Logic;
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

            services.AddScoped<IPubSubClientConfiguration>(
                _ => new PubSubClientConfiguration(configuration.ProjectId, configuration.SendMailTopicName));
            services.AddScoped<IPubSubClient, PubSubClient>();

            services.AddScoped<IDatabaseConfiguration>(
                _ => new DatabaseConfiguration(configuration.ProjectId, configuration.CollectionName));
            services.AddScoped<IReadOnlyDatabase, ReadonlyDatabase>();

            services.AddScoped<IPubSubProvider<ICreateMailMessage>, FunctionProvider>();
        }
    }
}
