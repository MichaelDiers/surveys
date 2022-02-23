namespace Surveys.Common.Firestore.Logic
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Surveys.Common.Contracts.Databases;
    using Surveys.Common.Contracts.Messages;
    using Surveys.Common.Firestore.Contracts;

    /// <summary>
    ///     Read email templates from the database.
    /// </summary>
    public class DatabaseTemplateReader : DatabaseBase, ITemplateReader
    {
        /// <summary>
        ///     Creates a new instance of <see cref="DatabaseTemplateReader" />.
        /// </summary>
        /// <param name="databaseConfiguration">The configuration of the database.</param>
        public DatabaseTemplateReader(IDatabaseConfiguration databaseConfiguration)
            : base(databaseConfiguration)
        {
        }

        /// <summary>
        ///     Read the templates for the given <paramref name="mailType" />.
        /// </summary>
        /// <param name="mailType">Specifies the email type..</param>
        /// <returns>A <see cref="Task" /> whose result is a <see cref="IDictionary{TKey,TValue}" /> that contains the templates.</returns>
        public async Task<IDictionary<string, string>> ReadTemplate(MailType mailType)
        {
            if (!Enum.IsDefined(typeof(MailType), mailType) || mailType == MailType.Undefined)
            {
                throw new InvalidEnumArgumentException(nameof(mailType), (int)mailType, typeof(MailType));
            }

            var documentSnapshot = await this.Collection().Document(mailType.ToString()).GetSnapshotAsync();
            if (documentSnapshot.Exists)
            {
                return new Dictionary<string, string>(
                    documentSnapshot.ToDictionary().Select(
                        keyValuePair => new KeyValuePair<string, string>(
                            keyValuePair.Key,
                            (string)keyValuePair.Value)));
            }

            return new Dictionary<string, string>();
        }
    }
}
