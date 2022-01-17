namespace MailerService.Test.Mocks
{
	using System.Collections.Generic;
	using MailerService.Contracts;
	using MimeKit;

	public class MessageConverterMock : IMessageConverter
	{
		public MimeMessage ToMimeMessage(IMessage message, IEnumerable<InternetAddress> from, string templateNewline)
		{
			return new MimeMessage();
		}
	}
}