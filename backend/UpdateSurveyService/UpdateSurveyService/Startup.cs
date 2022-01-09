namespace UpdateSurveyService
{
	using Google.Cloud.Functions.Hosting;
	using Microsoft.AspNetCore.Hosting;
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
		}
	}
}