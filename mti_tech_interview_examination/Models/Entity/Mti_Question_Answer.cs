using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using static mti_tech_interview_examination.Models.CommonModel;

namespace mti_tech_interview_examination.Models.Entity
{
    public class Mti_Question_Answer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public Mti_Question Question { get; set; }
        public int AnswerId { get; set; }
        [ForeignKey("AnswerId")]
        public Mti_Answer Answer { get; set; }
        public bool IsRight { get; set; }
    }
}