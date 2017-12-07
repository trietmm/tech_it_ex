using mti_tech_interview_examination.Lib.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mti_tech_interview_examination.Models.Entity;
using mti_tech_interview_examination.Models.Response;
using mti_tech_interview_examination.Models.QueryData;
using NLog;

namespace mti_tech_interview_examination.Lib.Execute
{
    public class RepoCandidate : BaseClass, ICandidate 
    {

        public Mti_Candidate GetCandidate(int id)
        {
            Mti_Candidate ObjResult = null;
            using (var context = new Interview_Examination_Context())
            {
                ObjResult = context.Mti_Candidate.Where(m => m.Id == id).FirstOrDefault();
            }
            return ObjResult;
        }

        public List<Mti_Candidate> lstCandidate(Query_Candidate query)
        {
            List<Mti_Candidate> ObjResult = null;
            try
            {
                using (var context = new Interview_Examination_Context())
                {
                    var queryContext = context.Mti_Candidate.AsQueryable();
                    if (!string.IsNullOrEmpty(query.SearchName))
                    {
                        queryContext = queryContext.Where(m => m.CanidateName.Contains(query.SearchName));
                    }
                    if (query.SearchDateFrom != null && query.SearchDateTo != null)
                    {
                        queryContext = queryContext.Where(m => m.DateMakeExam >= query.SearchDateFrom && m.DateMakeExam <= query.SearchDateTo);

                    }
                    ObjResult = queryContext.ToList();
                }
            }
            catch (Exception e)
            {
                Log.Error(e.InnerException);
                throw new Exception("Get List Candidate Fails");
            }
            return ObjResult;
        }

        public void Register(Mti_Candidate candidate)
        {
            Update(candidate);
        }

        public void Update(Mti_Candidate candidate)
        {
            try
            {
                using (var context = new Interview_Examination_Context())
                {
                    var candidateDB = context.Mti_Candidate.Where(m => m.Id == candidate.Id);
                        
                    if(candidateDB!= null)
                    {
                        var lstProperty = typeof(Mti_Candidate).GetProperties();
                        foreach (var property in lstProperty)
                        {
                            property.SetValue(candidateDB, property.GetValue(candidate));
                        }
                    }
                    else
                    {
                        context.Mti_Candidate.Add(candidate);
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception e) 
            {
                Log.Error(e.InnerException);
                throw new Exception("Update Fails");
            }
        }
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public void testNlog()
        {
            logger.Trace("Sample trace message");
            logger.Debug("Sample debug message");
            logger.Info("Sample informational message");
            logger.Warn("Sample warning message");
            logger.Error("Sample error message");
            logger.Fatal("Sample fatal error message");

            // alternatively you can call the Log() method
            // and pass log level as the parameter.
            logger.Log(LogLevel.Info, "Sample informational message");
        }
    }

}