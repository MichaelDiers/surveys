﻿namespace UpdateSurveyStatusService.Tests.Logic
{
	using System;
	using Newtonsoft.Json;
	using UpdateSurveyStatusService.Logic;
	using UpdateSurveyStatusService.Tests.Mock;
	using Xunit;

	public class UpdateProviderTest
	{
		[Theory]
		[InlineData(
			"{\"surveyId\":\"a0341d33-8404-4763-bb7f-7ebe004eb3b9\",\"participantId\":\"participantId\",\"status\":\"INVITATION_MAIL_OK\"}")]
		[InlineData(
			"{\"surveyId\":\"a0341d33-8404-4763-bb7f-7ebe004eb3b9\",\"participantId\":\"participantId\",\"status\":\"INVITATION_MAIL_FAILED\"}")]
		[InlineData(
			"{\"surveyId\":\"a0341d33-8404-4763-bb7f-7ebe004eb3b9\",\"participantId\":null,\"status\":\"INVITATION_MAIL_OK\"}")]
		[InlineData(
			"{\"surveyId\":\"a0341d33-8404-4763-bb7f-7ebe004eb3b9\",\"participantId\":null,\"status\":\"INVITATION_MAIL_FAILED\"}")]
		public async void Update(string json)
		{
			await new UpdateProvider(new DatabaseMock()).Update(json);
		}

		[Theory]
		[InlineData(
			"{\"surveyId\":\"a0341d33-8404-4763-bb7f-7ebe004eb3b9\",\"participantId\":\"participantId\",\"status\":\"INVITATION_MAIL_OK1\"}")]
		[InlineData(
			"{\"surveyId\":\"a0341d33-8404-4763-bb7f-7ebe004eb3b9\",\"participantId\":\"participantId\",\"status\":\"INVITATION_MAIL_FAILED1\"}")]
		public async void UpdateThrowsArgumentOutOfRangeException(string json)
		{
			await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => new UpdateProvider(new DatabaseMock()).Update(json));
		}

		[Theory]
		[InlineData(
			"{\"surveyId1\":\"a0341d33-8404-4763-bb7f-7ebe004eb3b9\",\"participantId\":\"participantId\",\"status\":\"INVITATION_MAIL_OK\"}")]
		[InlineData(
			"{\"surveyId\":\"a0341d33-8404-4763-bb7f-7ebe004eb3b9\",\"participantId1\":\"participantId\",\"status\":\"INVITATION_MAIL_OK\"}")]
		[InlineData("{\"surveyId\":null,\"participantId\":\"participantId\",\"status\":\"INVITATION_MAIL_OK\"}")]
		[InlineData(
			"{\"surveyId\":\"a0341d33-8404-4763-bb7f-7ebe004eb3b9\",\"participantId\":\"participantId\",\"status\":null}")]
		public async void UpdateThrowsJsonSerializationException(string json)
		{
			await Assert.ThrowsAsync<JsonSerializationException>(() => new UpdateProvider(new DatabaseMock()).Update(json));
		}
	}
}