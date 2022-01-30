namespace SurveyEvaluatorService
{
	using Google.Cloud.Functions.Hosting;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using SurveyEvaluatorService.Contracts;
	using SurveyEvaluatorService.Logic;
	using SurveyEvaluatorService.Model;

	/// <summary>
	///   Cloud functions startup.
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
			var configuration = new SurveyEvaluatorConfiguration();
			context.Configuration.Bind(configuration);

			services.AddScoped<ISurveyEvaluatorConfiguration>(_ => configuration);
			services.AddScoped<IDatabase, Database>();
			services.AddScoped<IPubSub, PubSub>();
			services.AddScoped<ISurveyEvaluatorProvider, SurveyEvaluatorProvider>();
		}
	}
}