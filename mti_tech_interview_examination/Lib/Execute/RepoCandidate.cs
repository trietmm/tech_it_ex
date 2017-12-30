using mti_tech_interview_examination.Lib.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mti_tech_interview_examination.Models.Entity;
using mti_tech_interview_examination.Models.Response;
using mti_tech_interview_examination.Models.QueryData;
using NLog;

namespace mti_tech_interview_examination.Lib.Execute
{
    public class RepoCandidate : BaseClass, ICandidate
    {
        public void CandidateAnswer(List<Mti_Candidate_Question> lstCandidateAnswer)
        {
            if (lstCandidateAnswer == null || lstCandidateAnswer.Count == 0)
                return;
            var candidateID = lstCandidateAnswer.Select(m => m.CandidateId).FirstOrDefault();
            if (candidateID == 0)
                return;
            using (var context = new Interview_Examination_Context())
            {

                var lstCandiateQuestion_DB = context.Mti_Candidate_Question.Where(m => m.CandidateId == candidateID).ToList();
                var lstQuestionIds = lstCandidateAnswer.Select(m => m.QuestionId).Distinct().ToList();
                //this is just the question TEXT
                var lstQuestionTextIdDB = context.Mti_Question.Where(m => lstQuestionIds.Contains(m.Id) && m.QuestionType == Models.CommonModel.QuestionType.Text).Select(m => m.Id).ToList();
                var lstAnswerInDB = context.Mti_Answer.Where(m => lstQuestionIds.Contains(m.QuestionId)).ToList();

                foreach (var canAnswer in lstCandidateAnswer)
                {
                    if (lstQuestionTextIdDB.Contains(canAnswer.QuestionId))
                    {
                        canAnswer.IsRight = null;
                        canAnswer.IsText = true;
                    }
                    else
                    {
                        canAnswer.IsText = false;
                        var lstAnswerRightforQuestion = lstAnswerInDB.Where(m => m.QuestionId == canAnswer.QuestionId && m.IsRight == true).ToList();
                        var lstAnswerId_of_candidate = canAnswer.CandidateAnswer.Split(new char[] { ',', ';', '|' }, StringSplitOptions.RemoveEmptyEntries).Select(m => int.Parse(m)).ToList();
                        if(lstAnswerRightforQuestion.Count!= lstAnswerId_of_candidate.Count)
                        {
                            canAnswer.IsRight = false;
                        }
                        else
                        {
                            canAnswer.IsRight = true;
                            foreach (var Id_of_candidate in lstAnswerId_of_candidate)
                            {
                                if(!lstAnswerRightforQuestion.Any(m=>m.Id== Id_of_candidate))
                                {
                                    canAnswer.IsRight = false;
                                    break;
                                }
                            }
                        }
                    }

                    var canAnswerDB = lstCandiateQuestion_DB.Where(m => m.QuestionId == canAnswer.QuestionId).FirstOrDefault();
                    if (canAnswerDB != null)
                    {
                        //update
                        var lstProperty = typeof(Mti_Candidate_Question).GetProperties();
                        foreach (var property in lstProperty)
                        {
                            property.SetValue(canAnswerDB, property.GetValue(canAnswer));
                        }
                    }
                    else
                    {
                        context.Mti_Candidate_Question.Add(canAnswer);
                    }
                }
                context.SaveChanges();
            }
        }

        public Mti_Candidate GetCandidate(int id)
        {
            Mti_Candidate ObjResult = null;
            using (var context = new Interview_Examination_Context())
            {
                ObjResult = context.Mti_Candidate.Where(m => m.Id == id).FirstOrDefault();
            }
            return ObjResult;
        }

        public List<Mti_Candidate> lstCandidate(Query_Candidate query)
        {
            List<Mti_Candidate> ObjResult = null;
            try
            {
                using (var context = new Interview_Examination_Context())
                {
                    var queryContext = context.Mti_Candidate.AsQueryable();
                    if (!string.IsNullOrEmpty(query.SearchName))
                    {
                        queryContext = queryContext.Where(m => m.CanidateName.Contains(query.SearchName));
                    }
                    if (query.SearchDateFrom != null && query.SearchDateTo != null)
                    {
                        queryContext = queryContext.Where(m => m.DateMakeExam >= query.SearchDateFrom && m.DateMakeExam <= query.SearchDateTo);

                    }
                    ObjResult = queryContext.ToList();
                }
            }
            catch (Exception e)
            {
                Log.Error(e.InnerException);
                throw new Exception("Get List Candidate Fails");
            }
            return ObjResult;
        }

        public int Register(Mti_Candidate candidate)
        {
           return Update(candidate);
        }

        public int Update(Mti_Candidate candidate)
        {
            try
            {
                using (var context = new Interview_Examination_Context())
                {
                    var candidateDB = context.Mti_Candidate.Where(m => m.Id == candidate.Id).FirstOrDefault();

                    if (candidateDB != null)
                    {
                        var lstProperty = typeof(Mti_Candidate).GetProperties();
                        foreach (var property in lstProperty)
                        {
                            property.SetValue(candidateDB, property.GetValue(candidate));
                        }
                    }
                    else
                    {
                        context.Mti_Candidate.Add(candidate);
                    }
                    context.SaveChanges();
                }
                return candidate.Id;
            }
            catch (Exception e)
            {
                Log.Error(e.InnerException);
                throw new Exception("Update Fails");
            }
        }
    }

}