namespace MailerService
{
    using Google.Cloud.Functions.Hosting;
    using MailerService.Contracts;
    using MailerService.Logic;
    using MailerService.Model;
    using Md.GoogleCloudFunctions.Contracts.Logic;
    using Md.GoogleCloudSecrets.Contracts.Logic;
    using Md.GoogleCloudSecrets.Logic;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Surveys.Common.Contracts.Messages;

    /// <summary>
    ///     Entry point of the google cloud function at startup.
    /// </summary>
    public class Startup : FunctionsStartup
    {
        /// <summary>
        ///     Initialize dependencies.
        /// </summary>
        /// <param name="context">The <see cref="WebHostBuilderContext" />.</param>
        /// <param name="services">The <see cref="IServiceCollection" />.</param>
        public override void ConfigureServices(WebHostBuilderContext context, IServiceCollection services)
        {
            var mailerServiceConfiguration = new MailerServiceConfiguration();
            context.Configuration.Bind(mailerServiceConfiguration);
            services.AddScoped<IMailerServiceConfiguration>(_ => mailerServiceConfiguration);

            services.AddScoped<ISecretManagerEnvironment>(_ => mailerServiceConfiguration);
            services.AddScoped<ISecretManager, SecretManager>();

            services.AddScoped<IMailerSmtpClient, MailerSmtpClient>();
            services.AddScoped<IMessageConverter, MessageConverter>();
            services.AddScoped<IPubSubProvider<ISendMailMessage>, MailerProvider>();
        }
    }
}
