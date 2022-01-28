namespace SurveyViewerService
{
	using System;
	using System.IO;
	using System.Net;
	using System.Threading.Tasks;
	using Google.Cloud.Functions.Framework;
	using Google.Cloud.Functions.Hosting;
	using Microsoft.AspNetCore.Http;
	using Newtonsoft.Json;
	using SurveyViewerService.Contracts;

	/// <summary>
	///   Google cloud function for reading and updating survey data.
	/// </summary>
	[FunctionsStartup(typeof(Startup))]
	public class SurveyViewerFunction : IHttpFunction
	{
		private readonly ISurveyViewerProvider surveyViewerProvider;

		/// <summary>
		///   Creates a new instance of <see cref="SurveyViewerFunction" />.
		/// </summary>
		/// <param name="surveyViewerProvider">Provider for reading and updating survey data.</param>
		public SurveyViewerFunction(ISurveyViewerProvider surveyViewerProvider)
		{
			this.surveyViewerProvider = surveyViewerProvider ?? throw new ArgumentNullException(nameof(surveyViewerProvider));
		}

		/// <summary>
		///   Handle incoming function requests.
		/// </summary>
		/// <param name="context">The context of the current function execution.</param>
		/// <returns>A <see cref="Task" />.</returns>
		public async Task HandleAsync(HttpContext context)
		{
			switch (context.Request.Method)
			{
				case "GET":
					await this.HandleGetAsync(context);
					break;
				case "POST":
					await this.HandlePostAsync(context);
					break;
				default:
					context.Response.StatusCode = (int) HttpStatusCode.NotFound;
					break;
			}
		}

		/// <summary>
		///   Handle http get requests.
		/// </summary>
		/// <param name="context">The context of the current function execution.</param>
		/// <returns>A <see cref="Task" />.</returns>
		private async Task HandleGetAsync(HttpContext context)
		{
			var participantId = context.Request?.Path.HasValue == true ? context.Request.Path.Value[1..] : null;
			if (string.IsNullOrWhiteSpace(participantId))
			{
				context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
				return;
			}

			try
			{
				var data = await this.surveyViewerProvider.ReadSurveyData(participantId);
				var json = JsonConvert.SerializeObject(data);
				context.Response.StatusCode = (int) HttpStatusCode.OK;
				context.Response.ContentType = "application/json";
				await context.Response.WriteAsync(json);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
			}
		}

		/// <summary>
		///   Handle http post requests.
		/// </summary>
		/// <param name="context">The context of the current function execution.</param>
		/// <returns>A <see cref="Task" />.</returns>
		private async Task HandlePostAsync(HttpContext context)
		{
			try
			{
				using TextReader reader = new StreamReader(context.Request.Body);
				var json = await reader.ReadToEndAsync();
				var result = await this.surveyViewerProvider.HandleSurveySubmitResult(json);
				context.Response.StatusCode = result ? 200 : 404;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				context.Response.StatusCode = 500;
			}
		}
	}
}