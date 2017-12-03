using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mti_tech_interview_examination.Models.Entity;

namespace mti_tech_interview_examination.Lib.Interface
{
    interface IGenerate
    {
       List<Mti_Question> GenerateQuestion (ICandidate icandidate, int idCandidate);
    }
}
