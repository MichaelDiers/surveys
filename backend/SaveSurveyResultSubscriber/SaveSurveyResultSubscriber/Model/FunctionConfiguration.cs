﻿namespace SaveSurveyResultSubscriber.Model
{
    using Md.Common.Model;
    using SaveSurveyResultSubscriber.Contracts;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public class FunctionConfiguration : RuntimeEnvironment, IFunctionConfiguration
    {
        /// <summary>
        ///     Gets the pub/sub topic name for creating thank you emails.
        /// </summary>
        public string CreateMailTopicName { get; set; }

        /// <summary>
        ///     Gets the pub/sub topic name for evaluating surveys.
        /// </summary>
        public string EvaluateSurveyTopicName { get; set; }
    }
}
