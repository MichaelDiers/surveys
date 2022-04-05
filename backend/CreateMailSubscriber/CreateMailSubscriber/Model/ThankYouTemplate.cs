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
        }
    }
}
