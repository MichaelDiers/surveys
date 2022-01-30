﻿namespace SurveyEvaluatorService.Test.Mocks
{
	using System.Threading.Tasks;
	using SurveyEvaluatorService.Contracts;

	public class PubSubMock : IPubSub
	{
		public Task SendMailAsync(ISendMailRequest request)
		{
			return Task.CompletedTask;
		}
	}
}