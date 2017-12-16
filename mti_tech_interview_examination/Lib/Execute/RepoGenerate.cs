using mti_tech_interview_examination.Lib.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mti_tech_interview_examination.Models.Entity;
using mti_tech_interview_examination.Models.Response;

namespace mti_tech_interview_examination.Lib.Execute
{
    public class RepoGenerate : IGenerate
    {

        public List<Response_Question> GenerateQuestion(ICandidate icandidate, int idCandidate)
        {
            List<Response_Question> lstQuestion = new List<Response_Question>();
            using (var Context = new Interview_Examination_Context())
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

                var lstTotalIds = new List<int>();
                lstTotalIds.AddRange(lstQuestionHard);
                lstTotalIds.AddRange(lstQuestionNormal);
                lstTotalIds.AddRange(lstQuestionEasy);

                SaveCandidateQuestion(Context, idCandidate, lstTotalIds);
                lstQuestion = Context.Mti_Question.Where(m => lstTotalIds.Contains(m.Id)).Select(m => new Response_Question() { QuestionId = m.Id, QuestionText = m.QuestionContent, QuestionType = m.QuestionType }).ToList();
                var lstAnswer = Context.Mti_Answer.Where(m => lstTotalIds.Contains(m.QuestionId)).Select(m => new Response_QuestionAnswer() { AnswerId = m.Id, Text = m.AnswerContent, QuestionId = m.QuestionId }).ToList();
                foreach (var question in lstQuestion)
                {
                    var lstAnswerByQuestion = lstAnswer.Where(m => m.QuestionId == question.QuestionId).ToList();
                    question.ListAnswer = lstAnswerByQuestion;

                }
            }
            //TODO implement method GenerateQuestion
            return lstQuestion;

        }

        private void SaveCandidateQuestion(Interview_Examination_Context context, int idCandidate, List<int> lstQuestionIds)
        {
            var lstQuestionDB = context.Mti_Question.Where(m => lstQuestionIds.Contains(m.Id)).Select(m => new { m.Id, m.QuestionType }).ToList();
            foreach (var questionId in lstQuestionIds)
            {
                Mti_Candidate_Question candidateQuestion = new Mti_Candidate_Question();
                candidateQuestion.CandidateId = idCandidate;
                candidateQuestion.QuestionId = questionId;
                var questionDB = lstQuestionDB.Where(m => m.Id == questionId).FirstOrDefault();
                if (questionDB != null && questionDB.QuestionType == Models.CommonModel.QuestionType.Text)
                {
                    candidateQuestion.IsRight = true;
                }
                else
                {
                    candidateQuestion.IsRight = false;
                }
                context.Mti_Candidate_Question.Add(candidateQuestion);
            }
            context.SaveChanges();
        }
    }

}