using mti_tech_interview_examination.Lib.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mti_tech_interview_examination.Models.Entity;
using mti_tech_interview_examination.Models.Response;
using System.Linq.Expressions;
using mti_tech_interview_examination.Models;

namespace mti_tech_interview_examination.Lib.Execute
{
    public class RepoQuestion : IQuestion
    {
        /// <summary>
        /// Use these func/action to set delete/check if the question is deleted or not. Later, if we change logic, we only change this place
        /// </summary>
        public static Func<Mti_Question, bool> IsNotDeleted = (q) => q.QuestionName != "#";
        public static Action<Mti_Question> Delete = (q) => q.QuestionName = "#";

        public void CreateQuestion(Mti_Question question, List<Mti_Answer> lstAnswer)
        {
            UpdateQuestion(question, lstAnswer);
        }

        //NOTE: temporarily we do not consume this method because of two reasons:
        //1. Technical: Mti_Candidate_Question table is refer to Mti_Question table
        //2. Logic: we should keep answer result for ex-candidates athough some questions are removed
        // => We will use IsDeleted flag instead
        //public void DeleteQuestion(int idQuestion)
        //{
        //    using (var Context = new Interview_Examination_Context())
        //    {

        //        var question = Context.Mti_Question.Where(m => m.Id == idQuestion).FirstOrDefault();
        //        if (question != null)
        //        {
        //            var lstAnswer = Context.Mti_Answer.Where(m => m.QuestionId == idQuestion).ToList();
        //            Context.Mti_Answer.RemoveRange(lstAnswer);
        //            Context.Mti_Question.Remove(question);

        //            Context.SaveChanges();
        //        }
        //    }
        //}
        
        /// <summary>
        /// This method is copied from above method with a litle update. Temporarily delete quesion by set its name to '#'
        /// </summary>
        /// <param name="idQuestion"></param>
        public void DeleteQuestion(int idQuestion)
        {
            using (var Context = new Interview_Examination_Context())
            {
                var question = Context.Mti_Question.Where(m => m.Id == idQuestion).FirstOrDefault();
                if (question != null)
                {
                    Delete(question);
                    Context.SaveChanges();
                }
            }
        }
        

        public List<Mti_Question> ListQuestion()
        {
            using (var Context = new Interview_Examination_Context())
            {
                var questions = Context.Mti_Question.Include("Answers").Where(IsNotDeleted).OrderByDescending(q => q.Id).ToList();
                return questions;
            }
        }

        public List<Mti_Question> ListQuestion(int Page, out int TotalNumber)
        {
            List<Mti_Question> ListResult = new List<Mti_Question>();
            using (var Context = new Interview_Examination_Context())
            {
                var query = Context.Mti_Question.Include("Answers").Where(IsNotDeleted);
                var page = query.OrderByDescending(p => p.Id)
                                .Skip((Page - 1) * CommonModel.PageNumber).Take(CommonModel.PageNumber)
                                .GroupBy(p => new { Total = query.Count() })
                                .First();
                TotalNumber = page.Key.Total;
                ListResult = page.Select(p => p).ToList();
            }
            return ListResult;
        }


        public void UpdateQuestion(Mti_Question question, List<Mti_Answer> lstAnswer)
        {
            using (var Context = new Interview_Examination_Context())
            {
                var QuestionInDB = Context.Mti_Question.Where(IsNotDeleted).Where(m => m.Id == question.Id).FirstOrDefault();
                if (QuestionInDB != null)
                {
                    //update
                    var lstProperty = typeof(Mti_Question).GetProperties();
                    foreach (var property in lstProperty)
                    {
                        if (property.Name == "Id" || property.Name == "Answers")
                            continue;
                        property.SetValue(QuestionInDB, property.GetValue(question));
                    }
                }
                else
                {
                    //create
                    Context.Mti_Question.Add(question);
                }
                var lstAnswerIds = lstAnswer.Select(m => m.Id).ToList();
                var lstAnswerInDB = Context.Mti_Answer.Where(m => lstAnswerIds.Contains(m.Id)).ToList();
                foreach (var answer in lstAnswer)
                {
                    var answerDB = lstAnswerInDB.Where(m => m.Id == answer.Id).FirstOrDefault();
                    if (answerDB != null)
                    {
                        //update
                        var lstProperty = typeof(Mti_Answer).GetProperties();
                        foreach (var property in lstProperty)
                        {
                            if (property.Name == "Id" || property.Name.Contains("Question"))
                                continue;
                            property.SetValue(answerDB, property.GetValue(answer));
                        }
                    }
                    else
                    {
                        //create
                        Context.Mti_Answer.Add(answer);
                    }
                }
                Context.SaveChanges();
            }
        }

        public Mti_Question ViewQuestion(int id)
        {
            using (var Context = new Interview_Examination_Context())
            {
                var questions = Context.Mti_Question.Include("Answers").Where(IsNotDeleted).Where(q => q.Id == id).FirstOrDefault();
                return questions;
            }

        }
    }

}