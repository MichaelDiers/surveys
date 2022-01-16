namespace UpdateSurveyStatusService
{
	using Google.Cloud.Functions.Hosting;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using UpdateSurveyStatusService.Contracts;
	using UpdateSurveyStatusService.Logic;
	using UpdateSurveyStatusService.Model;
	using IConfiguration = UpdateSurveyStatusService.Contracts.IConfiguration;

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
			var configuration = new Configuration();
			context.Configuration.Bind(configuration);
			services.AddScoped<IConfiguration>(_ => configuration);

			services.AddScoped<IDatabase, Database>();
			services.AddScoped<IUpdateProvider, UpdateProvider>();
		}
	}
}