﻿namespace Surveys.Common.PubSub.Contracts.Logic
{
    using System.Threading.Tasks;
    using Surveys.Common.Contracts;

    /// <summary>
    ///     Google pub/sub client for publishing an <see cref="IInitializeSurveyMessage" />.
    /// </summary>
    public interface IInitializeSurveyPubSubClient
    {
        /// <summary>
        ///     Publish a <see cref="IInitializeSurveyMessage" /> message.
        /// </summary>
        /// <param name="message">The message to publish.</param>
        /// <returns>A <see cref="Task" /> that indicates completion.</returns>
        // ReSharper disable once UnusedMember.Global
        Task PublishAsync(IInitializeSurveyMessage message);
    }
}
