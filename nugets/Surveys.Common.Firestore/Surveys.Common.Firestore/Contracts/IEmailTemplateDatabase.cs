namespace Surveys.Common.Firestore.Contracts
{
    using System.Collections.Generic;
    using Md.GoogleCloud.Base.Contracts.Logic;

    /// <summary>
    ///     Database access for email templates.
    /// </summary>
    public interface IEmailTemplateDatabase : IDatabase<IDictionary<string, object>>
    {
    }
}
