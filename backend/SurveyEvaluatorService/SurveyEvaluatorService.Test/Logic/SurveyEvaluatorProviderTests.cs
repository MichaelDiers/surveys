﻿namespace SurveyEvaluatorService.Test.Logic
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

		[Fact(Skip = "Integration only")]
		public async void EvaluateIntegration()
		{
			var configuration = new SurveyEvaluatorConfiguration
			{
				CollectionNameSurveys = "surveys",
				TopicNameSendMail = "SEND_MAIL",
				CollectionNameStatus = "surveys-status",
				ProjectId = "surveys-services-test"
			};

			var provider = new SurveyEvaluatorProvider(
				new LoggerMock<SurveyEvaluatorProvider>(),
				new Database(configuration),
				new PubSub(configuration),
				new MailerProvider());

			await provider.Evaluate(
				new SurveyResult
				{
					ParticipantId = "ba893854-0ed4-4d13-8aec-341b4d445849",
					SurveyId = "e5ade11a-093a-4bc0-b01b-7b6971f9e9bf",
					Results = new[]
					{
						new SurveyResultAnswer
						{
							AnswerValue = "1",
							QuestionId = "f3df32c0-3104-4596-a864-4b76b852b97b"
						},
						new SurveyResultAnswer
						{
							AnswerValue = "4",
							QuestionId = "c6efd18d-3049-4451-a1cd-6bd5b1cde759"
						}
					},
					Timestamp = DateTime.Now
				});
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