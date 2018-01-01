using mti_tech_interview_examination.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mti_tech_interview_examination.Models
{
    /// <summary>
    /// Question list
    /// </summary>
    public class QuestionListModel
    {
        /// <summary>
        /// Question list content
        /// </summary>
        public List<Mti_Question> Questions { get; set; }

        /// <summary>
        /// Paging information
        /// </summary>
        public PagingModel Paging { get; set; }
    }
}