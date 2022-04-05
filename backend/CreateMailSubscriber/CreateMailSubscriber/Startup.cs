namespace CreateMailSubscriber
{
    using CreateMailSubscriber.Contracts;
    using CreateMailSubscriber.Logic;
    using CreateMailSubscriber.Model;
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

            services.AddScoped<IPubSubClientEnvironment>(_ => configuration);
            services.AddScoped<ISendMailPubSubClient, SendMailPubSubClient>();

            services.AddScoped<IRuntimeEnvironment>(_ => configuration);
            services.AddScoped<IEmailTemplateReadOnlyDatabase, EmailTemplateReadOnlyDatabase>();
            services.AddScoped<ISurveyResultReadOnlyDatabase, SurveyResultReadOnlyDatabase>();
            services.AddScoped<ISurveyReadOnlyDatabase, SurveyReadOnlyDatabase>();

            services.AddScoped<IPubSubProvider<ICreateMailMessage>, FunctionProvider>();
        }
    }
}
