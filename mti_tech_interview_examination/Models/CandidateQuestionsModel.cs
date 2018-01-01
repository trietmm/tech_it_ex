using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mti_tech_interview_examination.Models.Entity;

namespace mti_tech_interview_examination.Models
{
    /// <summary>
    /// Candidate's questions model
    /// </summary>
    public class CandidateQuestionsModel
    {
        /// <summary>
        /// Candidate
        /// </summary>
        public Mti_Candidate Candidate { get; set; }

        /// <summary>
        /// Questions
        /// </summary>
        public List<Mti_Question> Questions { get; set; }

        /// <summary>
        /// CandidateAnswer
        /// </summary>
        public List<CandidateAnswerModel> CandidateAnswers { get; set; }

        /// <summary>
        /// Correct text answers from candidate
        /// </summary>
        public int[] CorrectTextAnswers { get; set; }
    }

    /// <summary>
    /// Candidate answer model
    /// </summary>
    public class CandidateAnswerModel
    {
        /// <summary>
        /// QuestionId
        /// </summary>
        public int QuestionId { get; set; }

        /// <summary>
        /// AnwserIds
        /// </summary>
        public List<int> AnwserIds { get; set; }

        /// <summary>
        /// Is correct answer
        /// </summary>
        public bool? IsRight { get; set; }

        /// <summary>
        /// Answer text
        /// </summary>
        public string AnswerText { get; set; }
    }
}