namespace CreateMailSubscriber.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///     Base for email templates.
    /// </summary>
    public abstract class BaseTemplate
    {
        /// <summary>
        ///     The template data.
        /// </summary>
        private readonly IDictionary<string, object> template;

        /// <summary>
        ///     Creates a new instance of <see cref="BaseTemplate" />.
        /// </summary>
        /// <param name="template">The template data.</param>
        protected BaseTemplate(IDictionary<string, object> template)
        {
            this.template = template ?? throw new ArgumentNullException(nameof(template));

            this.SubjectTemplate = this.GetEntry("subject");
            this.BodyHtmlTemplate = this.GetEntry("body", "html");
            this.BodyPlainTemplate = this.GetEntry("body", "plain");
        }

        /// <summary>
        ///     Gets the template for the html body.
        /// </summary>
        protected string BodyHtmlTemplate { get; }

        /// <summary>
        ///     Gets the template for the plain text body.
        /// </summary>
        protected string BodyPlainTemplate { get; }

        /// <summary>
        ///     Gets the template for the subject.
        /// </summary>
        protected string SubjectTemplate { get; }


        /// <summary>
        ///     Reads an entry for the given <paramref name="key" /> in the template data.
        /// </summary>
        /// <param name="key">The template key.</param>
        /// <returns>The entry for the key.</returns>
        protected string GetEntry(string key)
        {
            return GetEntry<string>(this.template, key);
        }

        /// <summary>
        ///     Reads an entry for the given <paramref name="key" /> in the template data.
        /// </summary>
        /// <param name="objectKey">The key of the template object.</param>
        /// <param name="key">The template key.</param>
        /// <returns>The entry for the key.</returns>
        protected string GetEntry(string objectKey, string key)
        {
            var dictionary = GetEntry<IDictionary<string, object>>(this.template, objectKey);
            return GetEntry<string>(dictionary, key);
        }

        /// <summary>
        ///     Reads an entry for the given <paramref name="key" /> in the template data.
        /// </summary>
        /// <param name="dictionary">Dictionary that contains the template strings.</param>
        /// <param name="key">The template key.</param>
        /// <returns>The entry for the key.</returns>
        private static T GetEntry<T>(IDictionary<string, object> dictionary, string key)
        {
            if (dictionary.TryGetValue(key, out var value))
            {
                return (T) value;
            }

            throw new ArgumentException($"Unknown template key {key}", nameof(key));
        }
    }
}
