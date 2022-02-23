namespace Surveys.Common.Contracts.Mails
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    ///     Process templates and input to create an email.
    /// </summary>
    public interface ITemplateProcessor
    {
        /// <summary>
        ///     Process the input data and create an email.
        /// </summary>
        /// <typeparam name="T">The type of the input data.</typeparam>
        /// <param name="templates">The email template data.</param>
        /// <param name="input">The email content.</param>
        /// <returns>A <see cref="Task" />.</returns>
        Task ProcessAsync<T>(IDictionary<string, string> templates, T input) where T : class;
    }
}
