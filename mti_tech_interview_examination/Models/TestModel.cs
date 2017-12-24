using mti_tech_interview_examination.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mti_tech_interview_examination.Models
{
    /// <summary>
    /// Model for Test form
    /// </summary>
    public class TestModel
    {
        /// <summary>
        /// List of questions
        /// </summary>
        public List<Response_Question> Questions { get; set; }

        /// <summary>
        /// Current question index
        /// </summary>
        public int CurrentQuestionIndex { get; set; }
    }
}