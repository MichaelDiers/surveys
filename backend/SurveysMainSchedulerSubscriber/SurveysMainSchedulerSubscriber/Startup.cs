namespace SurveysMainSchedulerSubscriber
{
    using System.Collections.Generic;
    using System.Linq;
    using Google.Cloud.Functions.Hosting;
    using Md.Common.Contracts.Messages;
    using Md.GoogleCloudFunctions.Contracts.Logic;
    using Md.GoogleCloudPubSub.Model;
    using Md.Tga.Common.PubSub.Contracts.Logic;
    using Md.Tga.Common.PubSub.Logic;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

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
            services.AddOptions<FunctionConfiguration>().Bind(context.Configuration).ValidateDataAnnotations();

            services.AddScoped<IEnumerable<ISchedulerPubSubClient>>(
                provider =>
                {
                    var configuration = provider.GetService<IOptions<FunctionConfiguration>>().Value;
                    return configuration.TopicNames.Select(
                            topic => new SchedulerPubSubClient(
                                new PubSubClientEnvironment(configuration.Environment, configuration.ProjectId, topic)))
                        .ToArray();
                });

            services.AddScoped<IPubSubProvider<IMessage>, FunctionProvider>();
        }
    }
}
