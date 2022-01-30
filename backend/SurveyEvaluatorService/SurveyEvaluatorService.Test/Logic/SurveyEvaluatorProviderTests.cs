namespace SurveyEvaluatorService.Test.Logic
{
	using System;
	using System.Linq;
	using SurveyEvaluatorService.Contracts;
	using SurveyEvaluatorService.Logic;
	using SurveyEvaluatorService.Model;
	using SurveyEvaluatorService.Test.Mocks;
	using Xunit;

	public class SurveyEvaluatorProviderTests
	{
		private static readonly ISurvey Survey;

		static SurveyEvaluatorProviderTests()
		{
			var participantIds = Enumerable.Range(1, 5).Select(id => Guid.NewGuid().ToString()).ToArray();
			Survey = new Survey
			{
				Id = Guid.NewGuid().ToString(),
				Name = nameof(Survey.Name),
				Organizer = new SurveyOrganizer
				{
					Email = "Organizer.Email",
					Name = "Organizer.Name"
				},
				ParticipantIds = participantIds,
				Participants = participantIds.Select(
					id => new SurveyParticipant
					{
						Email = $"{nameof(SurveyParticipant)}-Email-{id}",
						Id = id,
						Name = $"{nameof(SurveyParticipant)}-Name-{id}"
					}),
				Questions = Enumerable.Range(1, 6).Select(
					i => new SurveyQuestion
					{
						Choices = Enumerable.Range(0, i).Select(
							j => new SurveyQuestionChoice
							{
								Answer = $"{i}-{j}-choice",
								Value = i * j + j
							}).ToArray()
					}),
				Timestamp = DateTime.Now
			};
		}


		[Theory]
		[InlineData("surveyId")]
		public async void Evaluate(string surveyId)
		{
			var surveyResult = new SurveyResult
			{
				SurveyId = surveyId
			};

			await new SurveyEvaluatorProvider(
				new LoggerMock<SurveyEvaluatorProvider>(),
				new DatabaseMock(surveyId, Survey, Enumerable.Empty<ISurveyStatus>()),
				new PubSubMock(),
				new MailerProvider()).Evaluate(surveyResult);
		}

		[Fact]
		public async void EvaluateThrowsArgumentNullExceptionForNull()
		{
			await Assert.ThrowsAsync<ArgumentNullException>(
				() => new SurveyEvaluatorProvider(
					new LoggerMock<SurveyEvaluatorProvider>(),
					new DatabaseMock(),
					new PubSubMock(),
					new MailerProvider()).Evaluate(null));
		}
	}
}