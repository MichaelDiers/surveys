namespace MailerService.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using MailerService.Contracts;
    using MimeKit;
    using Surveys.Common.Contracts.Messages;

    /// <summary>
    ///     Converter for <see cref="ISendMailMessage" /> to <see cref="MimeMessage" />.
    /// </summary>
    public class MessageConverter : IMessageConverter
    {
        /// <summary>
        ///     Convert a <see cref="ISendMailMessage" /> to a <see cref="MimeMessage" />.
        /// </summary>
        /// <param name="request">The data that is used to create the <see cref="MimeMessage" />.</param>
        /// <param name="from">The sender data of the email.</param>
        /// <param name="templateNewline">Handle for newlines in templates.</param>
        /// <returns>An instance of <see cref="MimeMessage" />.</returns>
        public MimeMessage ToMimeMessage(
            ISendMailMessage request,
            IEnumerable<InternetAddress> from,
            string templateNewline
        )
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var builder = new BodyBuilder
            {
                TextBody = Regex.Unescape(request.Body.Plain.Replace(templateNewline, Environment.NewLine)),
                HtmlBody = request.Body.Html.Replace(templateNewline, Environment.NewLine)
            };

            var mimeMessage = new MimeMessage(
                from,
                request.Recipients.Select(r => new MailboxAddress(r.Name, r.Email)),
                request.Subject,
                builder.ToMessageBody());

            mimeMessage.ReplyTo.Add(new MailboxAddress(request.ReplyTo.Name, request.ReplyTo.Email));

            return mimeMessage;
        }
    }
}
