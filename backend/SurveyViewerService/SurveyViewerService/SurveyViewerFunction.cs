namespace SurveyViewerService
{
	using System;
	using System.Net;
	using System.Threading.Tasks;
	using Google.Cloud.Functions.Framework;
	using Google.Cloud.Functions.Hosting;
	using Microsoft.AspNetCore.Http;
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
				context.Response.StatusCode = (int) HttpStatusCode.OK;
				await context.Response.WriteAsync(DateTime.Now.ToString());
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
			}
		}
	}
}