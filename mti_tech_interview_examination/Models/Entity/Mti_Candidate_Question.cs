using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using static mti_tech_interview_examination.Models.CommonModel;

namespace mti_tech_interview_examination.Models.Entity
{
    public class Mti_Candidate_Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CandidateId { get; set; }
        [ForeignKey("CandidateId")]
        public Mti_Candidate Candidate { get; set; }
        public int QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public Mti_Question Question { get; set; }
        public string CandidateAnswer { get; set; }

        /// <summary>
        /// if the answer for question is the text this is the type of question
        /// </summary>
        public bool IsText { get; set; }

        /// <summary>
        /// this is will automatic for check the answer of candidate
        /// it also change by teacher if the question is the text
        /// it maybe not finish by teacher so it maybe nullable
        /// </summary>
        public bool? IsRight { get; set; }
    }
}