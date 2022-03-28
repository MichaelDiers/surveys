namespace SaveSurveyStatusSubscriber
{
    using Google.Cloud.Functions.Hosting;
    using Md.Common.Contracts;
    using Md.GoogleCloud.Base.Contracts.Logic;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using SaveSurveyStatusSubscriber.Logic;
    using SaveSurveyStatusSubscriber.Model;
    using Surveys.Common.Contracts;
    using Surveys.Common.Firestore.Contracts;
    using Surveys.Common.Firestore.Models;

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

            services.AddScoped<IRuntimeEnvironment>(_ => configuration);
            services.AddScoped<ISurveyStatusDatabase, SurveyStatusDatabase>();

            services.AddScoped<IPubSubProvider<ISaveSurveyStatusMessage>, FunctionProvider>();
        }
    }
}
