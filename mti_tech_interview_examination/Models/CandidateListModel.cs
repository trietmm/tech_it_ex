using mti_tech_interview_examination.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mti_tech_interview_examination.Models
{
    /// <summary>
    /// Candidate list
    /// </summary>
    public class CandidateListModel
    {
        /// <summary>
        /// Question list content
        /// </summary>
        public List<Mti_Candidate> Candidates { get; set; }

        /// <summary>
        /// Paging information
        /// </summary>
        public PagingModel Paging { get; set; }
    }
}