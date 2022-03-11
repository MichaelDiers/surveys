namespace CreateMailSubscriber.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Md.GoogleCloud.Base.Contracts.Logic;

    internal class DatabaseMock : IReadOnlyDatabase
    {
        public Task<IDictionary<string, object>?> ReadByDocumentIdAsync(string documentId)
        {
            return Task.FromResult<IDictionary<string, object>?>(
                new Dictionary<string, object>
                {
                    {"subject", "Neue Umfrage '{0}'"},
                    {
                        "body",
                        new Dictionary<string, object>
                        {
                            {
                                "html",
                                "<html><body><h1>Hej {0}!</h1><p>Eine neue Umfrage <a href='{2}'>{1}</a> steht für Dich bereit!</p><p>Viele Grüße,<br><br>{3}</p></body></html>"
                            },
                            {
                                "plain",
                                "Hej {0},\\n\\neine neue Umfrage '{1}' steht für dich bereit:\\n\\n{2}\\n\\nViele Grüße,\\n\\n{3}"
                            }
                        }
                    }
                });
        }

        public Task<IEnumerable<IDictionary<string, object>>> ReadManyAsync(string fieldPath, object value)
        {
            throw new NotImplementedException();
        }

        public Task<IDictionary<string, object>?> ReadOneAsync(string fieldPath, object value)
        {
            throw new NotImplementedException();
        }
    }
}
