using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mti_tech_interview_examination.Models.Entity;

namespace mti_tech_interview_examination.Lib.Interface
{
    interface IQuestion
    {
        void CreateQuestion(Mti_Question question, List<Mti_Answer> lstAnswer);
        void UpdateQuestion(Mti_Question question, List<Mti_Answer> lstAnswer);
        void DeleteQuestion(int idQuestion);
        List<Mti_Question> ListQuestion();
        Mti_Question ViewQuestion(int id);
    }
}
