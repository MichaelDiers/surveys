﻿namespace Surveys.Common.PubSub.Contracts.Logic
{
    using System.Threading.Tasks;
    using Surveys.Common.Contracts;

    /// <summary>
    ///     Google pub/sub client for publishing an <see cref="ISaveSurveyStatusMessage" />.
    /// </summary>
    public interface ISaveSurveyStatusPubSubClient
    {
        /// <summary>
        ///     Publish a <see cref="ISaveSurveyStatusMessage" /> message.
        /// </summary>
        /// <param name="message">The message to publish.</param>
        /// <returns>A <see cref="Task" /> that indicates completion.</returns>
        // ReSharper disable once UnusedMember.Global
        Task PublishAsync(ISaveSurveyStatusMessage message);
    }
}
