using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mti_tech_interview_examination.Models.Response
{
    public class Response_Question
    {
        public int QuestionId { get; set; }
        public CommonModel.QuestionType QuestionType { get; set; }

        public string QuestionText { get; set; }

        public List<Response_QuestionAnswer> ListAnswer { get; set; }

        /// <summary>
        /// Just for question type = Text
        /// </summary>
        public string AnswerText { get; set; }

    }

    public class Response_QuestionAnswer
    {
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public string Text { get; set; }

        //Is selected by candidate? Used if candidate accidently close the Test tab, he can restore his work then
        public bool? IsSelected { get; set; }

    }
}