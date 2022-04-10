namespace Surveys.Common.Firestore.Models
{
    using System.Collections.Generic;
    using Md.Common.Contracts.Model;
    using Md.GoogleCloudFirestore.Logic;
    using Surveys.Common.Firestore.Contracts;

    /// <summary>
    ///     Database access for email templates.
    /// </summary>
    public class EmailTemplateReadOnlyDatabase
        : ReadonlyDatabase<IDictionary<string, object>>, IEmailTemplateReadOnlyDatabase
    {
        /// <summary>
        ///     The collection base name.
        /// </summary>
        public const string CollectionNameBase = "email-templates";

        /// <summary>
        ///     Creates a new instance of <see cref="EmailTemplateReadOnlyDatabase" />.
        /// </summary>
        /// <param name="runtimeEnvironment">The runtime environment.</param>
        public EmailTemplateReadOnlyDatabase(IRuntimeEnvironment runtimeEnvironment)
            : base(runtimeEnvironment, EmailTemplateReadOnlyDatabase.CollectionNameBase, x => x)
        {
        }
    }
}
