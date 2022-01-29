namespace SurveyEvaluatorService.Test.Mocks
{
	using System;
	using Microsoft.Extensions.Logging;

	public class LoggerMock<T> : ILogger<T>
	{
		public IDisposable BeginScope<TState>(TState state)
		{
			return new DisposableMock();
		}

		public bool IsEnabled(LogLevel logLevel)
		{
			return true;
		}

		public void Log<TState>(
			LogLevel logLevel,
			EventId eventId,
			TState state,
			Exception? exception,
			Func<TState, Exception?, string> formatter)
		{
		}
	}
}