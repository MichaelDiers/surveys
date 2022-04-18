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
