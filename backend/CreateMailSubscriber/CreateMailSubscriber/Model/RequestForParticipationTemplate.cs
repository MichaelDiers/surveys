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
    }
}
