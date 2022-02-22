namespace Surveys.Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;
    using Surveys.Common.Extensions;

    /// <summary>
    ///     Describes the data of a survey.
    /// </summary>
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
        ///     Add the object values to a dictionary.
        /// </summary>
        /// <param name="document">The data is added to the given dictionary.</param>
        /// <returns>A <see cref="Dictionary{TKey,TValue}" />.</returns>
        public override void AddToDictionary(Dictionary<string, object?> document)
        {
            base.AddToDictionary(document);
            document.Add(nameof(this.Info).FirstCharacterToLower(), this.Info);
            document.Add(nameof(this.Link).FirstCharacterToLower(), this.Link);
            document.Add(nameof(this.Name).FirstCharacterToLower(), this.Name);

            document.Add(nameof(this.Organizer).FirstCharacterToLower(), this.Organizer.ToDictionary());
            document.Add(
                nameof(this.Participants).FirstCharacterToLower(),
                this.Participants.Select(p => p.ToDictionary()));
            document.Add(nameof(this.Questions).FirstCharacterToLower(), this.Questions.Select(q => q.ToDictionary()));
        }

        /// <summary>
        ///     Gets an info text for the survey.
        /// </summary>
        [JsonProperty("info", Required = Required.Always, Order = 11)]
        public string Info { get; }

        /// <summary>
        ///     Gets a link that provides additional survey info.
        /// </summary>
        [JsonProperty("link", Required = Required.Always, Order = 12)]
        public string Link { get; }

        /// <summary>
        ///     Gets the name of the survey.
        /// </summary>
        [JsonProperty("name", Required = Required.Always, Order = 10)]
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
