namespace MailerService.Test.Mocks
{
	using System.Collections.Generic;
	using MailerService.Contracts;
	using MimeKit;

	public class MessageConverterMock : IMessageConverter
	{
		public MimeMessage ToMimeMessage(IMailerServiceRequest message, IEnumerable<InternetAddress> from)
		{
			return new MimeMessage();
		}
	}
}