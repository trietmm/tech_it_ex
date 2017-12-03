using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mti_tech_interview_examination.Models.Entity;

namespace mti_tech_interview_examination.Lib.Interface
{
    interface IAnswer
    {
        void CreateAnswer(List<Mti_Answer> lstAnswer);
        void UpdateAnswer(List<Mti_Answer> lstAnswer);
        void DeleteAnswerByQuestion(List<int> questionIds);
        void DeleteAnswerById(List<int> answerIds);
        List<Mti_Question> ListAnswer(int questionIds);
        Mti_Question ViewAnswer(int id);
    }
}
