using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mti_tech_interview_examination.Models.Entity;

namespace mti_tech_interview_examination.Models.QueryData
{
    public class Query_Question
    {
        public void QuestionInsert(Mti_Question question)
        {
            using (var context = new Interview_Examination_Context())
            {
                context.Mti_Question.Add(question);
                context.SaveChanges();
            }
        }
        public void QuestionInsert(List<Mti_Question> questions)
        {
            using (var context = new Interview_Examination_Context())
            {
                context.Mti_Question.AddRange(questions);
                context.SaveChanges();
            }
        }

        public void QuestionUpdate(Mti_Question question)
        {
            using (var context = new Interview_Examination_Context())
            {
                var updateEntity = context.Mti_Question.Where(m => m.Id == question.Id).FirstOrDefault();
                if (updateEntity != null)
                {
                    var lstProperties = typeof(Mti_Question).GetProperties();
                    foreach (var property in lstProperties)
                    {
                        property.SetValue(updateEntity, property.GetValue(question));
                    }
                }
                else
                {
                    context.Mti_Question.Add(question);
                }
                context.SaveChanges();
            }
        }
        public void QuestionDelete(int id)
        {
            using (var context = new Interview_Examination_Context())
            {
                var deleteEntity = context.Mti_Question.Where(m => m.Id == id).FirstOrDefault();
                if (deleteEntity != null)
                {
                    context.Mti_Question.Remove(deleteEntity);
                    context.SaveChanges();
                }
            }
        }
        public void QuestionDelete(List<int> ids)
        {
            using (var context = new Interview_Examination_Context())
            {
                var deleteEntities = context.Mti_Question.Where(m => ids.Contains(m.Id)).ToList();
                if (deleteEntities != null && deleteEntities.Count > 0)
                {
                    context.Mti_Question.RemoveRange(deleteEntities);
                    context.SaveChanges();
                }
            }
        }

        public Mti_Question QuestionSelect(int id)
        {
            Mti_Question result = null;
            using (var context = new Interview_Examination_Context())
            {
                result = context.Mti_Question.Where(m => m.Id == id).FirstOrDefault();
            }
            return result;
        }

    }
}