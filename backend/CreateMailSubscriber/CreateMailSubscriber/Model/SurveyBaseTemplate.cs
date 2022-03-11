namespace CreateMailSubscriber.Model
{
    using System.Collections.Generic;

    /// <summary>
    ///     Base for survey email templates.
    /// </summary>
    public abstract class SurveyBaseTemplate : BaseTemplate
    {
        /// <summary>
        ///     Creates a new instance of <see cref="SurveyBaseTemplate" />.
        /// </summary>
        /// <param name="template">The template data.</param>
        protected SurveyBaseTemplate(IDictionary<string, object> template)
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

        /// <summary>
        ///     Build the subject of the email.
        /// </summary>
        /// <param name="surveyName">The name of the survey.</param>
        /// <returns>The subject of the email.</returns>
        public string Subject(string surveyName)
        {
            return string.Format(this.SubjectTemplate, surveyName);
        }
    }
}
