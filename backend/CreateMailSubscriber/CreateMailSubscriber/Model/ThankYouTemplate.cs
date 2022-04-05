namespace CreateMailSubscriber.Model
{
    using System.Collections.Generic;

    /// <summary>
    ///     The template for thank you mails.
    /// </summary>
    public class ThankYouTemplate : SurveyBaseTemplate
    {
        /// <summary>
        ///     Creates a new instance of <see cref="ThankYouTemplate" />.
        /// </summary>
        /// <param name="template">The template data.</param>
        public ThankYouTemplate(IDictionary<string, object> template)
            : base(template)
        {
            this.ResultPlainTemplate = this.GetEntry("results", "resultPlain");
            this.ResultListPlainTemplate = this.GetEntry("results", "resultListPlain");
            this.ResultHtmlTemplate = this.GetEntry("results", "resultHtml");
            this.ResultListHtmlTemplate = this.GetEntry("results", "resultListHtml");
        }

        /// <summary>
        ///     The template for a single result in html format.
        /// </summary>
        protected string ResultHtmlTemplate { get; }

        /// <summary>
        ///     The template for a result list in html format.
        /// </summary>
        protected string ResultListHtmlTemplate { get; }

        /// <summary>
        ///     The template for a single result in text plain format.
        /// </summary>
        protected string ResultListPlainTemplate { get; }

        /// <summary>
        ///     The template for a single result in text plain format.
        /// </summary>
        protected string ResultPlainTemplate { get; }

        /// <summary>
        ///     Build the html body of the email.
        /// </summary>
        /// <param name="participantName">The name of the participant.</param>
        /// <param name="surveyName">The name of the survey.</param>
        /// <param name="link">The link to the survey,</param>
        /// <param name="organizerName">The name of the organizer.</param>
        /// <param name="results">The results of the survey.</param>
        /// <returns>The html body of the email.</returns>
        public string BodyHtml(
            string participantName,
            string surveyName,
            string link,
            string organizerName,
            string results
        )
        {
            return string.Format(
                this.BodyHtmlTemplate,
                participantName,
                surveyName,
                link,
                organizerName,
                results);
        }

        /// <summary>
        ///     Build the html body of the email.
        /// </summary>
        /// <param name="participantName">The name of the participant.</param>
        /// <param name="surveyName">The name of the survey.</param>
        /// <param name="link">The link to the survey,</param>
        /// <param name="organizerName">The name of the organizer.</param>
        /// <param name="results">The results of the survey.</param>
        /// <returns>The html body of the email.</returns>
        public string BodyPlain(
            string participantName,
            string surveyName,
            string link,
            string organizerName,
            string results
        )
        {
            return string.Format(
                this.BodyPlainTemplate,
                participantName,
                surveyName,
                link,
                organizerName,
                results);
        }

        public string ResultHtml(string question, string answer)
        {
            return string.Format(this.ResultHtmlTemplate, question, answer);
        }

        public string ResultListHtml(IEnumerable<string> results)
        {
            return string.Format(this.ResultListHtmlTemplate, string.Join(string.Empty, results));
        }

        public string ResultListPlain(IEnumerable<string> results)
        {
            return string.Format(this.ResultListPlainTemplate, string.Join(string.Empty, results));
        }

        public string ResultPlain(string question, string answer)
        {
            return string.Format(this.ResultPlainTemplate, question, answer);
        }
    }
}
