namespace MailerService.Test.Mocks
{
    using System.Collections.Generic;
    using MailerService.Contracts;
    using MimeKit;
    using Surveys.Common.Contracts.Messages;

    public class MessageConverterMock : IMessageConverter
    {
        public MimeMessage ToMimeMessage(ISendMailMessage message, IEnumerable<InternetAddress> from)
        {
            return new MimeMessage();
        }
    }
}
