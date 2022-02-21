namespace Surveys.Common.Models
{
    using System;
    using System.Collections.Generic;
    using Google.Cloud.Firestore;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;

    /// <summary>
    ///     Describes the data of a survey.
    /// </summary>
    [FirestoreData]
    public class Survey : Base, ISurvey
    {
        /// <summary>
        ///     Creates a new instance of <see cref="Base" />.
        /// </summary>
        /// <param name="id">The id of the object.</param>
        /// <param name="name">The name of the survey.</param>
        /// <param name="info">Additional info text describing the survey.</param>
        /// <param name="link">A link to a survey info page.</param>
        /// <param name="organizer">The organizer of the survey.</param>
        /// <param name="participants">The participants of the survey.</param>
        /// <param name="questions">The survey questions.</param>
        /// <exception cref="ArgumentException">Is thrown if <paramref name="id" /> is null or whitespace.</exception>
        /// <exception cref="ArgumentException">Is thrown if <paramref name="id" /> is not a guid.</exception>
        public Survey(
            string id,
            string name,
            string info,
            string link,
            Person organizer,
            IEnumerable<Participant> participants,
            IEnumerable<Question> questions
        )
            : base(id)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            }

            this.Info = info;
            this.Link = link;
            this.Name = name;
            this.Organizer = organizer;
            this.Participants = participants;
            this.Questions = questions;
        }

        /// <summary>
        ///     Gets an info text for the survey.
        /// </summary>
        [JsonProperty("info", Required = Required.Always, Order = 11)]
        [FirestoreProperty("info")]
        public string Info { get; }

        /// <summary>
        ///     Gets a link that provides additional survey info.
        /// </summary>
        [JsonProperty("link", Required = Required.Always, Order = 12)]
        [FirestoreProperty("link")]
        public string Link { get; }

        /// <summary>
        ///     Gets the name of the survey.
        /// </summary>
        [JsonProperty("name", Required = Required.Always, Order = 10)]
        [FirestoreProperty("name")]
        public string Name { get; }

        /// <summary>
        ///     Gets the organizer of the survey.
        /// </summary>
        [JsonProperty("organizer", Required = Required.Always, Order = 13)]
        public IPerson Organizer { get; }

        /// <summary>
        ///     Gets the participants of the survey.
        /// </summary>
        [JsonProperty("participants", Required = Required.Always, Order = 14)]
        public IEnumerable<IParticipant> Participants { get; }

        /// <summary>
        ///     Gets the questions of the survey.
        /// </summary>
        [JsonProperty("questions", Required = Required.Always, Order = 15)]
        public IEnumerable<IQuestion> Questions { get; }
    }
}
