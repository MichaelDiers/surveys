namespace SurveyViewerService.Tests.Logic
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Newtonsoft.Json.Linq;
	using SurveyViewerService.Contracts;
	using SurveyViewerService.Logic;
	using SurveyViewerService.Model;
	using SurveyViewerService.Tests.Mocks;
	using Xunit;

	public class SurveyViewerProviderTests
	{
		[Theory]
		[InlineData(
			"participantId",
			"questionId1",
			"questionsValue1",
			"questionId2",
			"questionsValue2")]
		public async void HandleSurveySubmitResult(
			string participantId,
			string questionId1,
			string questionsValue1,
			string questionId2,
			string questionsValue2)
		{
			var data = new JObject
			{
				["participantId"] = participantId,
				["questions"] = new JArray(
					new JObject
					{
						["questionId"] = questionId1,
						["value"] = questionsValue1
					},
					new JObject
					{
						["questionId"] = questionId2,
						["value"] = questionsValue2
					})
			};

			await InitSurveyViewerProvider().HandleSurveySubmitResult(data.ToString());
		}

		[Theory]
		[InlineData("{\"foo\":\"bar\"}")]
		public async void HandleSurveySubmitResultThrowsArgumentException(string json)
		{
			await Assert.ThrowsAsync<ArgumentException>(() => InitSurveyViewerProvider().HandleSurveySubmitResult(json));
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public async void HandleSurveySubmitResultThrowsArgumentNullException(string json)
		{
			await Assert.ThrowsAsync<ArgumentNullException>(() => InitSurveyViewerProvider().HandleSurveySubmitResult(json));
		}

		[Fact]
		public async void ReadSurveyReturnNullIfNoSurveyExists()
		{
			Assert.Null(await InitSurveyViewerProvider(null, null, null).ReadSurveyData(Guid.NewGuid().ToString()));
		}

		[Fact]
		public async void ReadSurveyReturnSurveyWithoutStatusWithoutResults()
		{
			var (survey, results, status) = InitSurveyData();
			var participantId = survey.Participants.First().Id;
			var surveyView = await InitSurveyViewerProvider(survey, null, null).ReadSurveyData(participantId);

			this.Check(
				survey,
				surveyView,
				participantId,
				false);

			foreach (var surveyViewDataQuestion in surveyView.Questions)
			{
				Assert.Single(
					surveyViewDataQuestion.Choices,
					choice => choice.IsSelected && string.IsNullOrWhiteSpace(choice.Value));
			}
		}

		[Fact]
		public async void ReadSurveyReturnSurveyWithoutStatusWithResults()
		{
			var (survey, results, status) = InitSurveyData();
			var participantId = survey.Participants.First().Id;
			var newResults = results.ToArray();
			var surveyView = await InitSurveyViewerProvider(survey, newResults, null).ReadSurveyData(participantId);

			this.Check(
				survey,
				surveyView,
				participantId,
				false);

			foreach (var surveyResult in newResults)
			{
				foreach (var surveyResultAnswer in surveyResult.Answers)
				{
					Assert.Single(
						surveyView.Questions.Where(
							question => question.Id == surveyResultAnswer.QuestionId
							            && question.Choices.Count(
								            choice => choice.IsSelected && choice.Value == surveyResultAnswer.Value.ToString())
							            == 1));
				}
			}
		}

		[Fact]
		public async void ReadSurveyReturnSurveyWithStatusClosedWithResults()
		{
			var (survey, results, status) = InitSurveyData();
			var participantId = survey.Participants.First().Id;
			var newStatusList = new List<ISurveyStatus>(status)
			{
				new SurveyStatus
				{
					SurveyId = survey.SurveyId,
					TimeStamp = DateTime.Now,
					Status = "CLOSED"
				}
			};
			var newResults = new List<ISurveyResult>(results);

			var surveyView = await InitSurveyViewerProvider(survey, newResults, newStatusList).ReadSurveyData(participantId);

			this.Check(
				survey,
				surveyView,
				participantId,
				true);

			foreach (var surveyResult in newResults.Where(r => r.ParticipantId == participantId))
			{
				foreach (var surveyResultAnswer in surveyResult.Answers)
				{
					Assert.Single(
						surveyView.Questions.Where(
							question => question.Id == surveyResultAnswer.QuestionId
							            && question.Choices.Count(
								            choice => choice.IsSelected && choice.Value == surveyResultAnswer.Value.ToString())
							            == 1));
				}
			}
		}

		[Fact]
		public async void ReadSurveyReturnSurveyWithStatusWithMultipleResults()
		{
			var (survey, results, status) = InitSurveyData();
			var participantId = survey.Participants.First().Id;
			var newResults = new List<ISurveyResult>(results);
			var lastResult = newResults.Where(x => x.ParticipantId == participantId).OrderBy(x => x.TimeStamp).Last();
			newResults.Add(
				new SurveyResult
				{
					ParticipantId = participantId,
					SurveyId = survey.SurveyId,
					TimeStamp = lastResult.TimeStamp.AddMinutes(-1),
					Answers = lastResult.Answers.Select(
						a => new Answer
						{
							QuestionId = a.QuestionId,
							Value = a.Value - 1
						}).ToArray()
				});

			var surveyView = await InitSurveyViewerProvider(survey, newResults, status).ReadSurveyData(participantId);

			this.Check(
				survey,
				surveyView,
				participantId,
				false);

			foreach (var surveyQuestion in survey.Questions)
			{
				Assert.Contains(
					surveyView.Questions,
					surveyViewQuestion =>
					{
						var result = surveyQuestion.Id == surveyViewQuestion.Id && surveyQuestion.Text == surveyViewQuestion.Text;
						if (result)
						{
							Assert.Equal(surveyQuestion.Choices.Count(), surveyViewQuestion.Choices.Count());
							foreach (var surveyChoice in surveyQuestion.Choices)
							{
								Assert.Contains(
									surveyViewQuestion.Choices,
									surveyViewChoice => surveyChoice.Value == surveyViewChoice.Value
									                    && surveyChoice.Answer == surveyViewChoice.Text
									                    && surveyViewChoice.IsSelected
									                    == (surveyQuestion.Choices.Last().Value == surveyChoice.Value));
							}
						}

						return result;
					});
			}
		}

		[Fact]
		public async void ReadSurveyReturnSurveyWithStatusWithResults()
		{
			var (survey, results, status) = InitSurveyData();
			var participantId = survey.Participants.First().Id;
			var newResults = new List<ISurveyResult>(results);
			var surveyView = await InitSurveyViewerProvider(survey, newResults, status).ReadSurveyData(participantId);

			this.Check(
				survey,
				surveyView,
				participantId,
				false);

			foreach (var surveyResult in newResults.Where(r => r.ParticipantId == participantId))
			{
				foreach (var surveyResultAnswer in surveyResult.Answers)
				{
					Assert.Single(
						surveyView.Questions.Where(
							question => question.Id == surveyResultAnswer.QuestionId
							            && question.Choices.Count(
								            choice => choice.IsSelected && choice.Value == surveyResultAnswer.Value.ToString())
							            == 1));
				}
			}
		}

		[Theory]
		[InlineData("sdfgh")]
		[InlineData("8a4455ac-85e7-40fc-8302-2e7a4e50965")]
		[InlineData("00000000-0000-0000-0000-000000000000")]
		public async void ReadSurveyWithInvalidGuidAsParticipantId(string participantId)
		{
			await Assert.ThrowsAsync<ArgumentException>(() => InitSurveyViewerProvider().ReadSurveyData(participantId));
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public async void ReadSurveyWithNullOrEmptyParticipantId(string participantId)
		{
			await Assert.ThrowsAsync<ArgumentNullException>(() => InitSurveyViewerProvider().ReadSurveyData(participantId));
		}

		private void Check(
			ISurvey expected,
			SurveyViewData actual,
			string participantId,
			bool isClosed)
		{
			Assert.NotNull(expected);
			Assert.NotNull(actual);

			Assert.Equal(expected.Name, actual.SurveyName);
			Assert.Equal(expected.Participants.First(p => p.Id == participantId).Name, actual.ParticipantName);
			Assert.Equal(participantId, actual.ParticipantId);
			Assert.Equal(isClosed, actual.IsClosed);

			Assert.Equal(expected.Questions.Count(), actual.Questions.Count());
			foreach (var expectedQuestion in expected.Questions)
			{
				Assert.Contains(
					actual.Questions,
					actualQuestion =>
					{
						var result = actualQuestion.Id == expectedQuestion.Id
						             && actualQuestion.Text == expectedQuestion.Text
						             && actualQuestion.Choices.Count() == expectedQuestion.Choices.Count();
						if (result)
						{
							foreach (var expectedQuestionChoice in expectedQuestion.Choices)
							{
								Assert.Contains(
									actualQuestion.Choices,
									actualQuestionChoice => actualQuestionChoice.Value == expectedQuestionChoice.Value
									                        && actualQuestionChoice.Text == expectedQuestionChoice.Answer);
							}
						}

						return result;
					});
			}
		}


		private static (ISurvey, IEnumerable<ISurveyResult>, IEnumerable<ISurveyStatus>) InitSurveyData()
		{
			var survey = new Survey
			{
				Name = $"{nameof(Survey)}@{nameof(Survey.Name)}",
				Organizer = new Address
				{
					Email = $"{nameof(Survey.Organizer)}@{nameof(Address.Email)}",
					Name = $"{nameof(Survey.Organizer)}@{nameof(Address.Name)}"
				},
				Participants = Enumerable.Range(1, 5).Select(
					i => new Participant
					{
						Email = $"{nameof(Participant)}@{nameof(Participant.Email)}-{i}",
						Id = Guid.NewGuid().ToString(),
						Name = $"{nameof(Participant)}@{nameof(Participant.Name)}-{i}"
					}).ToArray(),
				Questions = Enumerable.Range(1, 5).Select(
					i => new Question
					{
						Choices = Enumerable.Range(0, 10).Select(
							j => new Choice
							{
								Answer = $"{nameof(Choice)}@{nameof(Choice.Answer)}-{j}",
								Value = j == 0 ? null : j.ToString()
							}).ToArray(),
						Id = Guid.NewGuid().ToString(),
						Text = $"{nameof(Question)}@{nameof(Question.Text)}-{i}"
					}).ToArray(),
				SurveyId = Guid.NewGuid().ToString()
			};

			var results = survey.Participants.Select(
				participant => new SurveyResult
				{
					SurveyId = survey.SurveyId,
					ParticipantId = participant.Id,
					TimeStamp = DateTime.Now,
					Answers = survey.Questions.Select(
						question => new Answer
						{
							Value = int.Parse(question.Choices.Last().Value),
							QuestionId = question.Id
						}).ToArray()
				}).ToArray();

			var status = new[]
			{
				new SurveyStatus
				{
					Status = "CREATED",
					SurveyId = survey.SurveyId,
					TimeStamp = DateTime.Now
				}
			};

			return (survey, results, status);
		}

		private static ISurveyViewerProvider InitSurveyViewerProvider()
		{
			var (survey, results, status) = InitSurveyData();
			return InitSurveyViewerProvider(survey, results, status);
		}

		private static ISurveyViewerProvider InitSurveyViewerProvider(
			ISurvey survey,
			IEnumerable<ISurveyResult> results,
			IEnumerable<ISurveyStatus> status)
		{
			return new SurveyViewerProvider(new DatabaseMock(survey, results, status), new PubSubMock());
		}
	}
}