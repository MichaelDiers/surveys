namespace Surveys.Common.PubSub.Logic
{
    using Md.GoogleCloudPubSub.Contracts.Model;
    using Md.GoogleCloudPubSub.Logic;
    using Surveys.Common.Contracts.Messages;
    using Surveys.Common.PubSub.Contracts.Logic;

    /// <summary>
    ///     Google pub/sub client for publishing an <see cref="ISendMailMessage" />.
    /// </summary>
    public class SendMailPubSubClient : AbstractPubSubClient<ISendMailMessage>, ISendMailPubSubClient
    {
        /// <summary>
        ///     Creates a new instance of <see cref="CreateMailPubSubClient" />.
        /// </summary>
        /// <param name="environment"></param>
        public SendMailPubSubClient(IPubSubClientEnvironment environment)
            : base(environment)
        {
        }
    }
}
