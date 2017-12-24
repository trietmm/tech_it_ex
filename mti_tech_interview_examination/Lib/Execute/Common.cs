using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static mti_tech_interview_examination.Models.CommonModel;

namespace mti_tech_interview_examination.Lib.Execute
{
    public static class Common
    {

        public static int GetNumberQuestionByLevel(LevelCandidate lev, out int questionHard, out int questionNormal, out int questionEasy)
        {
            switch (lev)
            {
                case LevelCandidate.Junior:
                    {
                        questionHard = 0;
                        questionNormal = 5;
                        questionEasy = 15;
                    }; break;
                case LevelCandidate.Middle:
                    {
                        questionHard = 2;
                        questionNormal = 8;
                        questionEasy = 10;
                    }; break;
                case LevelCandidate.Senior:
                    {
                        questionHard = 5;
                        questionNormal = 10;
                        questionEasy = 5;
                    }; break;
                default:
                    {
                        questionHard = 0;
                        questionNormal = 5;
                        questionEasy = 15;
                    }; break;
                    
            }
            //return total question
            return questionEasy + questionNormal + questionHard;
        }
    }
}