namespace SurveysMainSchedulerSubscriber
{
    using System.Collections.Generic;
    using Md.Common.Model;

    /// <summary>
    ///     Access the application settings.
    /// </summary>
    public class FunctionConfiguration : RuntimeEnvironment
    {
        /// <summary>
        ///     Gets the topic names that are triggered by the scheduler.
        /// </summary>
        public IEnumerable<string> TopicNames { get; set; }
    }
}
