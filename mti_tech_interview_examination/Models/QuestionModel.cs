using mti_tech_interview_examination.Models.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static mti_tech_interview_examination.Models.CommonModel;

namespace mti_tech_interview_examination.Models
{
    /// <summary>
    /// Question model is used for view
    /// </summary>
    public class QuestionModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of question
        /// </summary>
        [Display(Name = "Name")]
        [Required]
        public string QuestionName { get; set; }

        /// <summary>
        /// Content of question
        /// </summary>
        [Display(Name = "Content")]
        [Required]
        public string QuestionContent { get; set; }

        /// <summary>
        /// Question type
        /// </summary>
        [Display(Name = "Type")]
        [Required]
        public QuestionType QuestionType { get; set; }

        /// <summary>
        /// Question level
        /// </summary>
        [Display(Name = "Level")]
        [Required]
        public QuestionLevel QuestionLevel { get; set; }

        /// <summary>
        /// Answers
        /// </summary>        
        [MaxLength(500)]
        public List<string> AnswerContents { get; set; }

        /// <summary>
        /// Correct answer indexes
        /// </summary>
        public List<int> CorrectAnswerIndexes { get; set; }
    }
}