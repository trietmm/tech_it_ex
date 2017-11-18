using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            Query_Question query = new Query_Question();
            var obj = new Entity.Mti_Question() { QuestionContent = "Question Content Test", QuestionName = "Question Name Test", QuestionType = CommonModel.QuestionType.Selection };
            query.QuestionInsert(obj);
            var questionSelected = query.QuestionSelect(obj.Id);
            Assert.IsTrue(questionSelected != null);
            query.QuestionDelete(obj.Id);
        }
    }
}