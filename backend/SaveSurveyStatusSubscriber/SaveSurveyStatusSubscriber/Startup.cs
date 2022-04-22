namespace SaveSurveyStatusSubscriber
{
    using Google.Cloud.Functions.Hosting;
    using Md.Common.Contracts.Model;
    using Md.GoogleCloudFunctions.Contracts.Logic;
    using Md.GoogleCloudPubSub.Contracts.Model;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
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
            services.AddOptions<FunctionConfiguration>().Bind(context.Configuration).ValidateDataAnnotations();

            services.AddScoped<IRuntimeEnvironment>(
                provider => provider.GetService<IOptions<FunctionConfiguration>>().Value);
            services.AddScoped<ISurveyStatusDatabase, SurveyStatusDatabase>();

            services.AddScoped<IPubSubClientEnvironment>(
                provider => provider.GetService<IOptions<FunctionConfiguration>>().Value);
            services.AddScoped<ISurveyClosedPubSubClient, SurveyClosedPubSubClient>();

            services.AddScoped<IPubSubProvider<ISaveSurveyStatusMessage>, FunctionProvider>();
        }
    }
}
