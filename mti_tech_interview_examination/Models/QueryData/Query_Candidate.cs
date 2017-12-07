using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mti_tech_interview_examination.Models.Entity;

namespace mti_tech_interview_examination.Models.QueryData
{
    public class Query_Candidate
    {
        public string SearchName { get; set; }
        public DateTime SearchDateFrom { get; set; }
        public DateTime SearchDateTo { get; set; }

    }
}