using mti_tech_interview_examination.Lib.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mti_tech_interview_examination.Models.Entity;

namespace mti_tech_interview_examination.Lib.Execute
{
    public class Generate : IGenerate
    {

        private Interview_Examination_Context _Context;

        private Interview_Examination_Context Context
        {
            get
            {
                if(_Context== null)
                {
                    _Context = new Interview_Examination_Context();
                }
                return _Context;
            }
        }

        public List<Mti_Question> GenerateQuestion(ICandidate icandidate, int idCandidate)
        {
            Mti_Candidate candidate = icandidate.GetCandidate(idCandidate);
            int questionHard = 0;
            int questionNormal = 0;
            int questionEasy = 0;
            Common.GetNumberQuestionByLevel(candidate.level, out questionHard, out questionNormal, out questionEasy);

            var lstQuestionTmp = Context.Mti_Question.Select(m => new { m.Id, m.QuestionLevel }).ToList();
            var lstQuestionHard = lstQuestionTmp.Where(m => m.QuestionLevel == Models.CommonModel.QuestionLevel.Hard).Select(m => m.Id).ToList<int>().RandomList(questionHard);
            var lstQuestionNormal = lstQuestionTmp.Where(m => m.QuestionLevel == Models.CommonModel.QuestionLevel.Normal).Select(m => m.Id).ToList<int>().RandomList(questionNormal);
            var lstQuestionEasy = lstQuestionTmp.Where(m => m.QuestionLevel == Models.CommonModel.QuestionLevel.Easy).Select(m => m.Id).ToList<int>().RandomList(questionEasy);


            //TODO implement method GenerateQuestion
            return new List<Mti_Question>();

        }
    }
    
}