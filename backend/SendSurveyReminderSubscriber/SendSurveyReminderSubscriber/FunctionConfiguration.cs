namespace SendSurveyReminderSubscriber
{
    using Md.Common.Contracts.Model;
    using Md.GoogleCloudPubSub.Model;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public class FunctionConfiguration : PubSubClientEnvironment, IRuntimeEnvironment
    {
    }
}
