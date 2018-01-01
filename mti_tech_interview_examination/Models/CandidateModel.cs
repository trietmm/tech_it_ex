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
    /// Candidate model is used for view
    /// </summary>
    public class CandidateModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of candidate
        /// </summary>
        [Display(Name = "Name")]
        [Required]
        public string CandidateName { get; set; }

        /// <summary>
        /// Birthyear
        /// </summary>
        [Display(Name = "Birth year")]
        [Required]
        public int CandidateBirthYear { get; set; }

        /// <summary>
        /// CV's url
        /// </summary>
        [Display(Name = "CV URL")]
        public string CvUrl { get; set; }

        /// <summary>
        /// Candidate's level
        /// </summary>
        [Display(Name = "Level")]
        [Required]
        public LevelCandidate Level { get; set; }

        /// <summary>
        /// List of question
        /// </summary>
        public List<Response_Question> Questions { get; set; }

        /// <summary>
        /// Started time
        /// </summary>
        public DateTime StartedTime { get; set; }

        /// <summary>
        /// Question index
        /// </summary>
        public int QuestionIndex { get; set; }
    }
}