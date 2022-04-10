namespace Surveys.Common.Firestore.Contracts
{
    using System.Collections.Generic;
    using Md.GoogleCloudFirestore.Contracts.Logic;

    /// <summary>
    ///     Database access for email templates.
    /// </summary>
    public interface IEmailTemplateReadOnlyDatabase : IReadOnlyDatabase<IDictionary<string, object>>
    {
    }
}
