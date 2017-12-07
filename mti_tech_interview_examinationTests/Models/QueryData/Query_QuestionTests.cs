using Microsoft.VisualStudio.TestTools.UnitTesting;
using mti_tech_interview_examination.Lib.Execute;
using mti_tech_interview_examination.Models.QueryData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mti_tech_interview_examination.Models.QueryData.Tests
{
    [TestClass()]
    public class Query_QuestionTests
    {
        [TestMethod()]
        public void QuestionInsertTest()
        {
            RepoCandidate r = new RepoCandidate();
            r.testNlog();
        }
    }
}