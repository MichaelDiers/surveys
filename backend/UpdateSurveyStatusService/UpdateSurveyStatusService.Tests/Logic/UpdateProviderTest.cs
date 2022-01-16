namespace UpdateSurveyStatusService.Tests.Logic
{
	using UpdateSurveyStatusService.Contracts;
	using UpdateSurveyStatusService.Logic;
	using UpdateSurveyStatusService.Tests.Mock;
	using Xunit;

	public class UpdateProviderTest
	{
		[Theory]
		[InlineData(
			"{\"id\":\"the_id\",\"type\":\"SURVEY\",\"status\":\"INVITATION_MAILS_REQUEST_OK\"}",
			"the_id",
			MessageType.Survey,
			Status.InvitationMailsRequestOk)]
		public async void Update(
			string json,
			string id,
			MessageType messageType,
			Status status)
		{
			await new UpdateProvider(new DatabaseMock(id, messageType, status)).Update(json);
		}
	}
}