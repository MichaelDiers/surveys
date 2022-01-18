namespace SaveSurveyResultService.Tests.Logic
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using SaveSurveyResultService.Contracts;
	using Xunit;

	public class DatabaseMock : IDatabase
	{
		private readonly IMessage expectedMessage;

		public DatabaseMock(IMessage expectedMessage)
		{
			this.expectedMessage = expectedMessage ?? throw new ArgumentNullException(nameof(expectedMessage));
		}

		public Task InsertResult(IMessage message)
		{
			Assert.Equal(this.expectedMessage.SurveyId, message.SurveyId);
			Assert.Equal(this.expectedMessage.ParticipantId, message.ParticipantId);
			Assert.Equal(this.expectedMessage.Results.Count(), message.Results.Count());
			foreach (var expectedMessageResult in this.expectedMessage.Results)
			{
				Assert.Contains(
					message.Results,
					result => result.Answer == expectedMessageResult.Answer
					          && result.QuestionId == expectedMessageResult.QuestionId);
			}

			return Task.CompletedTask;
		}
	}
}