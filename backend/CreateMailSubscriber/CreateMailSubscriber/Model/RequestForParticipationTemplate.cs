namespace CreateMailSubscriber.Model
{
    using System.Collections.Generic;

    /// <summary>
    ///     The template the survey invitation email.
    /// </summary>
    public class RequestForParticipationTemplate : SurveyBaseTemplate
    {
        /// <summary>
        ///     Creates a new instance of <see cref="RequestForParticipationTemplate" />.
        /// </summary>
        /// <param name="template">The template data.</param>
        public RequestForParticipationTemplate(IDictionary<string, object> template)
            : base(template)
        {
        }

        /// <summary>
        ///     Build the html body of the email.
        /// </summary>
        /// <param name="participantName">The name of the participant.</param>
        /// <param name="surveyName">The name of the survey.</param>
        /// <param name="link">The link to the survey,</param>
        /// <param name="organizerName">The name of the organizer.</param>
        /// <returns>The html body of the email.</returns>
        public string BodyHtml(
            string participantName,
            string surveyName,
            string link,
            string organizerName
        )
        {
            return string.Format(
                this.BodyHtmlTemplate,
                participantName,
                surveyName,
                link,
                organizerName);
        }

        /// <summary>
        ///     Build the html body of the email.
        /// </summary>
        /// <param name="participantName">The name of the participant.</param>
        /// <param name="surveyName">The name of the survey.</param>
        /// <param name="link">The link to the survey,</param>
        /// <param name="organizerName">The name of the organizer.</param>
        /// <returns>The html body of the email.</returns>
        public string BodyPlain(
            string participantName,
            string surveyName,
            string link,
            string organizerName
        )
        {
            return string.Format(
                this.BodyPlainTemplate,
                participantName,
                surveyName,
                link,
                organizerName);
        }
    }
}
