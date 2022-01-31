namespace SurveyEvaluatorService.Model
{
	using System;
	using System.Collections.Generic;
	using Google.Cloud.Firestore;
	using Newtonsoft.Json;
	using SurveyEvaluatorService.Contracts;

	/// <summary>
	///   Describes an answer of a survey question.
	/// </summary>
	[FirestoreData]
	public class SurveyResultAnswer : ISurveyResultAnswer
	{
		/// <summary>
		///   Creates a new instance of <see cref="SurveyResultAnswer" />.
		/// </summary>
		public SurveyResultAnswer()
		{
		}

		/// <summary>
		///   Creates a new instance of <see cref="SurveyResultAnswer" />.
		/// </summary>
		/// <param name="values">The property values of the object.</param>
		public SurveyResultAnswer(IReadOnlyDictionary<string, object> values)
		{
			if (values == null)
			{
				throw new ArgumentNullException(nameof(values));
			}

			var answer = values.GetValueOrDefault("answer", null);
			if (answer != null)
			{
				this.AnswerValue = Convert.ToInt32(answer);
			}

			var questionId = values.GetValueOrDefault("questionId", null);
			if (questionId != null)
			{
				this.QuestionId = (string) questionId;
			}
		}

		/// <summary>
		///   Gets or sets the value of the answer.
		/// </summary>
		[JsonProperty("answer", Required = Required.Always)]
		[FirestoreProperty("answer")]
		public int AnswerValue { get; set; }

		/// <summary>
		///   Gets or sets the id of the question.
		/// </summary>
		[JsonProperty("questionId", Required = Required.Always)]
		[FirestoreProperty("questionId")]
		public string QuestionId { get; set; }
	}
}