using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static mti_tech_interview_examination.Models.CommonModel;

namespace mti_tech_interview_examination.Lib.Execute
{
    public class BaseClass
    {
        protected Logger Log { get; private set; }
        protected BaseClass()
        {
            Log = LogManager.GetLogger(GetType().FullName);
        }
    }
}