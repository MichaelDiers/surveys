namespace Surveys.Common.PubSub.Logic
{
    using Md.GoogleCloudPubSub.Logic;
    using Surveys.Common.Contracts.Messages;
    using Surveys.Common.PubSub.Contracts.Logic;

    /// <summary>
    ///     Google pub/sub client for publishing an <see cref="ICreateMailMessage" />.
    /// </summary>
    public class CreateMailPubSubClient : AbstractPubSubClient<ICreateMailMessage>, ICreateMailPubSubClient
    {
        /// <summary>
        ///     Creates a new instance of <see cref="CreateMailPubSubClient" />.
        /// </summary>
        /// <param name="environment"></param>
        public CreateMailPubSubClient(IPubSubClientEnvironment environment)
            : base(environment)
        {
        }
    }
}
