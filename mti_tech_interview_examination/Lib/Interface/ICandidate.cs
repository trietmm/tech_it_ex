﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mti_tech_interview_examination.Models.Entity;

namespace mti_tech_interview_examination.Lib.Interface
{
    public interface ICandidate
    {
        Mti_Candidate Register(Mti_Candidate candidate);
        Mti_Candidate Update(Mti_Candidate candidate);
        Mti_Candidate GetCandidate(int id);
    }
}
