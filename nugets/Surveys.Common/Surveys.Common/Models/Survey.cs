namespace Surveys.Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Md.Common.Database;
    using Md.Common.Extensions;
    using Newtonsoft.Json;
    using Surveys.Common.Contracts;

    /// <summary>
    ///     Describes the data of a survey.
    /// </summary>
    public class Survey : DatabaseObject, ISurvey
    {
        /// <summary>
        ///     Json name of property <see cref="Info" />
        /// </summary>
        private const string InfoName = "info";

        /// <summary>
        ///     Json name of property <see cref="Link" />
        /// </summary>
        private const string LinkName = "link";

        /// <summary>
        ///     Json name of property <see cref="Name" />
        /// </summary>
        private const string NameName = "name";

        /// <summary>
        ///     Json name of property <see cref="Organizer" />
        /// </summary>
        private const string OrganizerName = "organizer";

        /// <summary>
        ///     Json name of property <see cref="Participants" />
        /// </summary>
        private const string ParticipantsName = "participants";

        /// <summary>
        ///     Json name of property <see cref="Questions" />
        /// </summary>
        private const string QuestionsName = "questions";

        /// <summary>
        ///     Creates a new instance of <see cref="Survey" />.
        /// </summary>
        /// <param name="documentId">The id of the document.</param>
        /// <param name="created">The creation time of the object.</param>
        /// <param name="parentDocumentId">The id of the parent document.</param>
        /// <param name="name">The name of the survey.</param>
        /// <param name="info">Additional info text describing the survey.</param>
        /// <param name="link">A link to a survey info page.</param>
        /// <param name="organizer">The organizer of the survey.</param>
        /// <param name="participants">The participants of the survey.</param>
        /// <param name="questions">The survey questions.</param>
        [JsonConstructor]
        public Survey(
            string? documentId,
            DateTime? created,
            string? parentDocumentId,
            string name,
            string info,
            string link,
            Person organizer,
            IEnumerable<Participant> participants,
            IEnumerable<Question> questions
        )
            : this(
                documentId,
                created,
                parentDocumentId,
                name,
                info,
                link,
                organizer,
                participants.Select(p => p as IParticipant).ToArray(),
                questions.Select(q => q as IQuestion).ToArray())
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="Survey" />.
        /// </summary>
        /// <param name="documentId">The id of the document.</param>
        /// <param name="created">The creation time of the object.</param>
        /// <param name="parentDocumentId">The id of the parent document.</param>
        /// <param name="name">The name of the survey.</param>
        /// <param name="info">Additional info text describing the survey.</param>
        /// <param name="link">A link to a survey info page.</param>
        /// <param name="organizer">The organizer of the survey.</param>
        /// <param name="participants">The participants of the survey.</param>
        /// <param name="questions">The survey questions.</param>
        public Survey(
            string? documentId,
            DateTime? created,
            string? parentDocumentId,
            string name,
            string info,
            string link,
            IPerson organizer,
            IEnumerable<IParticipant> participants,
            IEnumerable<IQuestion> questions
        )
            : base(documentId, created, parentDocumentId)
        {
            this.Info = info.ValidateIsNotNullOrWhitespace(nameof(info));
            this.Link = link.ValidateIsNotNullOrWhitespace(nameof(link));
            this.Name = name.ValidateIsNotNullOrWhitespace(nameof(name));
            this.Organizer = organizer;
            this.Participants = participants;
            this.Questions = questions;
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

        /// <summary>
        ///     Add the property values to a dictionary.
        /// </summary>
        /// <param name="dictionary">The values are added to the given dictionary.</param>
        /// <returns>The given <paramref name="dictionary" />.</returns>
        public override IDictionary<string, object> AddToDictionary(IDictionary<string, object> dictionary)
        {
            base.AddToDictionary(dictionary);
            dictionary.Add(Survey.InfoName, this.Info);
            dictionary.Add(Survey.LinkName, this.Link);
            dictionary.Add(Survey.NameName, this.Name);

            dictionary.Add(Survey.OrganizerName, this.Organizer.ToDictionary());
            dictionary.Add(Survey.ParticipantsName, this.Participants.Select(p => p.ToDictionary()));
            dictionary.Add(Survey.QuestionsName, this.Questions.Select(q => q.ToDictionary()));
            return dictionary;
        }

        /// <summary>
        ///     Create a new <see cref="Survey" /> from dictionary data.
        /// </summary>
        /// <param name="dictionary">The initial values of the object.</param>
        /// <returns>A <see cref="Survey" />.</returns>
        public new static Survey FromDictionary(IDictionary<string, object> dictionary)
        {
            var baseObject = DatabaseObject.FromDictionary(dictionary);
            var name = dictionary.GetString(Survey.NameName);
            var info = dictionary.GetString(Survey.InfoName);
            var link = dictionary.GetString(Survey.LinkName);
            var organizer = Person.FromDictionary(dictionary.GetDictionary(Survey.OrganizerName));
            var participants = dictionary.GetDictionaries(Survey.ParticipantsName)
                .Select(Participant.FromDictionary)
                .ToArray();
            var questions = dictionary.GetDictionaries(Survey.QuestionsName).Select(Question.FromDictionary).ToArray();
            return new Survey(
                baseObject.DocumentId,
                baseObject.Created,
                baseObject.ParentDocumentId,
                name,
                info,
                link,
                organizer,
                participants,
                questions);
        }
    }
}
