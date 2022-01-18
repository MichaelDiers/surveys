namespace SurveyViewerService.Tests.Logic
{
	using System;
	using SurveyViewerService.Contracts;
	using SurveyViewerService.Logic;
	using SurveyViewerService.Model;
	using Xunit;

	public class SurveyViewerProviderTests
	{
		private static readonly IConfiguration Configuration = new Configuration
		{
			ProjectId = "project_id"
		};

		[Theory]
		[InlineData("8a4455ac-85e7-40fc-8302-2e7a4e509656")]
		public async void ReadSurveyData(string participantId)
		{
			await new SurveyViewerProvider(Configuration).ReadSurveyData(participantId);
		}

		[Theory]
		[InlineData("no guid")]
		public async void ReadSurveyDataThrowsArgumentException(string participantId)
		{
			await Assert.ThrowsAsync<ArgumentException>(
				() => new SurveyViewerProvider(Configuration).ReadSurveyData(participantId));
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public async void ReadSurveyDataThrowsArgumentNullException(string participantId)
		{
			await Assert.ThrowsAsync<ArgumentNullException>(
				() => new SurveyViewerProvider(Configuration).ReadSurveyData(participantId));
		}
	}
}