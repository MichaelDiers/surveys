namespace MailerService.Logic
{
    using System;
    using System.Collections.Generic;
    using System.IO;
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
        /// <returns>An instance of <see cref="MimeMessage" />.</returns>
        public MimeMessage ToMimeMessage(ISendMailMessage request, IEnumerable<InternetAddress> from)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var fromInternetAddresses = from.ToArray();

            var builder = new BodyBuilder {TextBody = Regex.Unescape(request.Body.Plain), HtmlBody = request.Body.Html};

            var multipart = new Multipart("mixed");
            if (request.Attachments.Any())
            {
                foreach (var requestAttachment in request.Attachments)
                {
                    var memoryStream = new MemoryStream();
                    memoryStream.Write(requestAttachment.Data, 0, requestAttachment.Data.Length);
                    var attachment = new MimePart("application", "octet-stream")
                    {
                        Content = new MimeContent(memoryStream),
                        ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                        ContentTransferEncoding = ContentEncoding.Base64,
                        FileName = requestAttachment.Name
                    };

                    multipart.Add(attachment);
                }
            }

            multipart.Add(builder.ToMessageBody());

            var mimeMessage = new MimeMessage(
                fromInternetAddresses,
                request.Recipients.Select(r => new MailboxAddress(r.Name, r.Email)),
                request.Subject,
                multipart);

            mimeMessage.ReplyTo.Add(new MailboxAddress(request.ReplyTo.Name, request.ReplyTo.Email));
            mimeMessage.Bcc.AddRange(fromInternetAddresses);
            return mimeMessage;
        }
    }
}
