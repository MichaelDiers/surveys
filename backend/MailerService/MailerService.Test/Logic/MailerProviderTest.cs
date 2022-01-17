namespace MailerService.Test.Logic
{
	using System;
	using MailerService.Logic;
	using MailerService.Model;
	using MailerService.Test.Mocks;
	using Newtonsoft.Json;
	using Xunit;

	public class MailerProviderTest
	{
		[Theory]
		[InlineData(
			"{\"recipients\":[{\"email\":\"RecipientEmail\",\"name\":\"RecipientName\"}],\"replyTo\":{\"email\":\"ReplyToEmail\",\"name\":\"ReplyToName\"},\"subject\":\"subject\",\"text\":{\"html\":\"html body\",\"plain\":\"plain body\"}}")]
		public async void SendAsyncDeserializeObjectSucceeds(string json)
		{
			await new MailerProvider(
					new MessageConverterMock(),
					new MailerSmtpClientMock(),
					new MailerServiceConfiguration
					{
						MailboxAddressFrom = new MailboxAddressFromConfiguration
						{
							Email = "foo@bar",
							Name = "foo"
						}
					})
				.SendAsync(json);
		}

		[Theory]
		[InlineData("cool")]
		public async void SendAsyncDeserializeObjectThrowsJsonReaderException(string json)
		{
			await Assert.ThrowsAsync<JsonReaderException>(
				() =>
					new MailerProvider(new MessageConverterMock(), new MailerSmtpClientMock(), new MailerServiceConfiguration())
						.SendAsync(json));
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public async void SendAsyncThrowsArgumentNullException(string json)
		{
			await Assert.ThrowsAsync<ArgumentNullException>(
				() =>
					new MailerProvider(new MessageConverterMock(), new MailerSmtpClientMock(), new MailerServiceConfiguration())
						.SendAsync(json));
		}
	}
}