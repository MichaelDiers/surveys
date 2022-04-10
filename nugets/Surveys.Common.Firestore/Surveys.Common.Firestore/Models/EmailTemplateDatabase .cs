namespace Surveys.Common.Firestore.Models
{
    using System.Collections.Generic;
    using Md.Common.Contracts.Model;
    using Md.GoogleCloudFirestore.Logic;
    using Surveys.Common.Firestore.Contracts;

    /// <summary>
    ///     Database access for email templates.
    /// </summary>
    public class EmailTemplateDatabase : Database<IDictionary<string, object>>, IEmailTemplateDatabase
    {
        /// <summary>
        ///     Creates a new instance of <see cref="EmailTemplateDatabase" />.
        /// </summary>
        /// <param name="runtimeEnvironment">The runtime environment.</param>
        public EmailTemplateDatabase(IRuntimeEnvironment runtimeEnvironment)
            : base(runtimeEnvironment, EmailTemplateReadOnlyDatabase.CollectionNameBase, x => x)
        {
        }
    }
}
