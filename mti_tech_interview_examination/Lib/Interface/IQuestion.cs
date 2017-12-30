using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mti_tech_interview_examination.Models.Entity;
using System.Linq.Expressions;

namespace mti_tech_interview_examination.Lib.Interface
{
    interface IQuestion
    {
        void CreateQuestion(Mti_Question question, List<Mti_Answer> lstAnswer);
        void UpdateQuestion(Mti_Question question, List<Mti_Answer> lstAnswer);
        void DeleteQuestion(int idQuestion);
        List<Mti_Question> ListQuestion(Expression<Func<Mti_Question, bool>> express);
        Mti_Question ViewQuestion(int id);
    }
}
