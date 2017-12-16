using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mti_tech_interview_examination.Models.Entity;
using mti_tech_interview_examination.Models.QueryData;
using mti_tech_interview_examination.Models.Response;

namespace mti_tech_interview_examination.Lib.Interface
{
    public interface ICandidate
    {
        void Register(Mti_Candidate candidate);
        void Update(Mti_Candidate candidate);
        Mti_Candidate GetCandidate(int id);
        List<Mti_Candidate> lstCandidate(Query_Candidate query);

        void CandidateAnswer(List<Mti_Candidate_Question> candidateAnswer);
    }
}
