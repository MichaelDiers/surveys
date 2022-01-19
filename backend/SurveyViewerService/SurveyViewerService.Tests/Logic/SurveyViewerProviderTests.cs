namespace SurveyViewerService.Tests.Logic
{
	using System;
	using SurveyViewerService.Contracts;
	using SurveyViewerService.Logic;
	using SurveyViewerService.Model;
	using SurveyViewerService.Tests.Mocks;
	using Xunit;

	public class SurveyViewerProviderTests
	{
		[Theory]
		[InlineData("8a4455ac-85e7-40fc-8302-2e7a4e509656")]
		public async void ReadSurveyData(string participantId)
		{
			await InitSurveyViewerProvider().ReadSurveyData(participantId);
		}

		[Theory]
		[InlineData("no guid")]
		public async void ReadSurveyDataThrowsArgumentException(string participantId)
		{
			await Assert.ThrowsAsync<ArgumentException>(() => InitSurveyViewerProvider().ReadSurveyData(participantId));
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public async void ReadSurveyDataThrowsArgumentNullException(string participantId)
		{
			await Assert.ThrowsAsync<ArgumentNullException>(() => InitSurveyViewerProvider().ReadSurveyData(participantId));
		}

		private static ISurveyViewerProvider InitSurveyViewerProvider()
		{
			return new SurveyViewerProvider(
				new Configuration
				{
					ProjectId = "project_id"
				},
				new DatabaseMock());
		}
	}
}