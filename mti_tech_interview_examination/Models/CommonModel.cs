using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mti_tech_interview_examination.Models
{
    public class CommonModel
    {
        public enum QuestionType
        {
            Selection = 0,
            MultiSelection = 1,
            Text = 2
        }

        public enum QuestionLevel
        {
            Easy = 0,
            Normal = 1,
            Hard = 2
        }

        public enum LevelCandidate
        {
            Junior = 0,
            Middle = 1,
            Senior = 2
        }
    }
}