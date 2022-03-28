﻿namespace EvaluateSurveySubscriber.Model
{
    using EvaluateSurveySubscriber.Contracts;
    using Md.Common.Model;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public class FunctionConfiguration : RuntimeEnvironment, IFunctionConfiguration
    {
        /// <summary>
        ///     Gets the pub/sub topic name.
        /// </summary>
        public string SaveSurveyStatusTopicName { get; set; } = "";

        /// <summary>
        ///     Gets the pub/sub topic name.
        /// </summary>
        public string SurveyClosedTopicName { get; set; } = "";
    }
}
