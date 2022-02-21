namespace Surveys.Common.PubSub.Logic
{
    using System;
    using Surveys.Common.PubSub.Contracts;

    /// <summary>
    ///     The configuration for sending messages to pub/sub.
    /// </summary>
    public class PubSubConfiguration : IPubSubConfiguration
    {
        /// <summary>
        ///     Creates a new instance of <see cref="PubSubConfiguration" />.
        /// </summary>
        /// <param name="projectId">The id of the google cloud project.</param>
        /// <param name="topicName">The name of the pub/sub topic.</param>
        public PubSubConfiguration(string projectId, string topicName)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(projectId));
            }

            if (string.IsNullOrWhiteSpace(topicName))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(topicName));
            }

            this.ProjectId = projectId;
            this.TopicName = topicName;
        }

        /// <summary>
        ///     Gets the id of the google cloud project.
        /// </summary>
        public string ProjectId { get; }

        /// <summary>
        ///     Gets the name of the pub/sub topic.
        /// </summary>
        public string TopicName { get; }
    }
}
