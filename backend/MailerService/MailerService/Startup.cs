namespace MailerService
{
	using Google.Cloud.Functions.Hosting;
	using MailerService.Contracts;
	using MailerService.Logic;
	using MailerService.Model;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///   Entry point of the google cloud function at startup.
	/// </summary>
	public class Startup : FunctionsStartup
	{
		/// <summary>
		///   Initialize dependencies.
		/// </summary>
		/// <param name="context">The <see cref="WebHostBuilderContext" />.</param>
		/// <param name="services">The <see cref="IServiceCollection" />.</param>
		public override void ConfigureServices(WebHostBuilderContext context, IServiceCollection services)
		{
			var mailerServiceConfiguration = new MailerServiceConfiguration();
			context.Configuration.Bind(mailerServiceConfiguration);
			services.AddScoped<IMailerServiceConfiguration>(_ => mailerServiceConfiguration);

			services.AddScoped<IMailerSmtpClient, MailerSmtpClient>();
			services.AddScoped<IMessageConverter, MessageConverter>();
			services.AddScoped<IMailerProvider, MailerProvider>();
		}
	}
}