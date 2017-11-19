using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using static mti_tech_interview_examination.Models.CommonModel;

namespace mti_tech_interview_examination.Models.Entity
{
    public class Mti_Candidate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(100)]
        public string CanidateName { get; set; }
        public int CandidateBirthYear { get; set; }
        public DateTime DateMakeExam { get; set; }
        public string ImgURL { get; set; }
    }
}