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
    }
}