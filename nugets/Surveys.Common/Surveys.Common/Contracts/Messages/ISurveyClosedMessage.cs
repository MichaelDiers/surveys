namespace Surveys.Common.Contracts.Messages
{
    using System.Collections.Generic;
    using Md.Common.Contracts.Messages;

    /// <summary>
    ///     Describes a closed survey message.
    /// </summary>
    public interface ISurveyClosedMessage : IMessage
    {
        /// <summary>
        ///     Gets the survey results.
        /// </summary>
        IEnumerable<ISurveyResult> Results { get; }

        /// <summary>
        ///     Gets the survey.
        /// </summary>
        ISurvey Survey { get; }
    }
}
