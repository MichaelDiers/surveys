namespace EvaluateSurveySubscriber
{
    using System.ComponentModel.DataAnnotations;
    using Md.Common.DataAnnotations;
    using Md.Common.Model;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public class FunctionConfiguration : RuntimeEnvironment
    {
        /// <summary>
        ///     Gets the pub/sub topic name.
        /// </summary>
        [Required]
        [TopicName]
        public string SaveSurveyStatusTopicName { get; set; } = string.Empty;
    }
}
