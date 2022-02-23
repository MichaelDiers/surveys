namespace Surveys.Common.Contracts.Databases
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Surveys.Common.Contracts.Messages;

    /// <summary>
    ///     Database access for reading email templates.
    /// </summary>
    public interface ITemplateReader
    {
        /// <summary>
        ///     Read the templates for the given <paramref name="mailType" />.
        /// </summary>
        /// <param name="mailType">Specifies the email type..</param>
        /// <returns>A <see cref="Task" /> whose result is a <see cref="IDictionary{TKey,TValue}" /> that contains the templates.</returns>
        Task<IDictionary<string, string>> ReadTemplate(MailType mailType);
    }
}
