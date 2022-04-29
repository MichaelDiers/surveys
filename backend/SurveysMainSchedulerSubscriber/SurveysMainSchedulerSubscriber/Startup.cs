namespace SurveysMainSchedulerSubscriber
{
    using Google.Cloud.Functions.Hosting;
    using Md.Common.Contracts.Messages;
    using Md.GoogleCloudFunctions.Contracts.Logic;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;

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
            services.AddScoped<IPubSubProvider<IMessage>, FunctionProvider>();
        }
    }
}
