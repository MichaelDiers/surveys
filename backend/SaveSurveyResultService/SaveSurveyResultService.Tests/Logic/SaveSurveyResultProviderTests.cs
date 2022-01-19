namespace SaveSurveyResultService.Tests.Logic
{
	using Newtonsoft.Json.Linq;
	using SaveSurveyResultService.Logic;
	using SaveSurveyResultService.Model;
	using Xunit;

	public class SaveSurveyResultProviderTests
	{
		[Theory]
		[InlineData(
			"8a4455ac-85e7-40fc-8302-2e7a4e509656",
			"f47cebae-c5ca-4cdc-8017-77c942b8e9fe",
			"8a4455ac-85e7-40fc-8302-2e7a4e509656",
			1,
			"f47cebae-c5ca-4cdc-8017-77c942b8e9fe",
			7)]
		public async void InsertSurveyResult(
			string surveyId,
			string participantId,
			string questionId1,
			int answer1,
			string questionId2,
			int answer2)
		{
			dynamic result1 = new JObject();
			result1.question = questionId1;
			result1.answer = answer1;

			dynamic result2 = new JObject();
			result2.question = questionId2;
			result2.answer = answer2;

			dynamic json = new JObject();
			json.surveyId = surveyId;
			json.participantId = participantId;
			json.results = new JArray(result1, result2);

			var message = new Message(
				new[]
				{
					new Result
					{
						Answer = answer1,
						QuestionId = questionId1
					},
					new Result
					{
						Answer = answer2,
						QuestionId = questionId2
					}
				})
			{
				SurveyId = surveyId,
				ParticipantId = participantId
			};

			await new SaveSurveyResultProvider(new DatabaseMock(message)).InsertSurveyResult(json.ToString());
		}
	}
}