namespace SurveyViewerService
{
	using Google.Cloud.Functions.Hosting;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using SurveyViewerService.Contracts;
	using SurveyViewerService.Logic;
	using SurveyViewerService.Model;
	using IConfiguration = SurveyViewerService.Contracts.IConfiguration;

	/// <summary>
	///   Initialize the google cloud function and its dependencies.
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
			var configuration = new Configuration();
			context.Configuration.Bind(configuration);
			services.AddScoped<IConfiguration>(_ => configuration);

			services.AddScoped<IDatabase, Database>();
			services.AddScoped<IPubSub, PubSub>();
			services.AddScoped<ISurveyViewerProvider, SurveyViewerProvider>();
		}
	}
}