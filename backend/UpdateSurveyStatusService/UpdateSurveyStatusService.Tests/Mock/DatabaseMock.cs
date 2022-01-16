namespace UpdateSurveyStatusService.Tests.Mock
{
	using System;
	using System.Threading.Tasks;
	using UpdateSurveyStatusService.Contracts;
	using Xunit;

	public class DatabaseMock : IDatabase
	{
		private readonly Status expectedStatus;
		private readonly string id;
		private readonly MessageType messageType;

		public DatabaseMock(string id, MessageType messageType, Status expectedStatus)
		{
			this.id = id;
			this.messageType = messageType;
			this.expectedStatus = expectedStatus;
		}

		public Task UpdateParticipant(string participantId, Status status)
		{
			Assert.Equal(this.id, participantId);

			if (this.messageType == MessageType.Participant)
			{
				Assert.Equal(this.expectedStatus, status);
				return Task.CompletedTask;
			}

			throw new InvalidOperationException();
		}

		public Task UpdateSurvey(string surveyId, Status status)
		{
			Assert.Equal(this.id, surveyId);

			if (this.messageType == MessageType.Survey)
			{
				Assert.Equal(this.expectedStatus, status);
				return Task.CompletedTask;
			}

			throw new InvalidOperationException();
		}
	}
}