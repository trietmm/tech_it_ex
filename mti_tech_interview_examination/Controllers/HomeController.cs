using mti_tech_interview_examination.Common;
using mti_tech_interview_examination.Lib.Execute;
using mti_tech_interview_examination.Lib.Interface;
using mti_tech_interview_examination.Models;
using mti_tech_interview_examination.Models.Entity;
using mti_tech_interview_examination.Models.Response;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace mti_tech_interview_examination.Controllers
{
    [CustomAuthentication]
    public class HomeController : Controller
    {
        /// <summary>
        /// Login form
        /// </summary>
        /// <returns></returns>    
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                if(HttpContext.User.Identity.Name != "admin")
                    return RedirectToAction("Candidate");
                else 
                    FormsAuthentication.SignOut();
            }
            return View();
        }

        /// <summary>
        /// Login form
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model)
        {            
            if (ModelState.IsValid && FormsAuthentication.Authenticate(model.UserName, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.UserName, false);

                //save UserName to session
                Session[SessionKey.UserName] = model.UserName;

                return RedirectToAction("Candidate");
            }
            
            return View(model);
        }

        /// <summary>
        /// Log off
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LogOff()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        /// <summary>
        /// Form to add new candidate
        /// </summary>
        /// <returns></returns>
        public ActionResult Candidate()
        {
            Mti_Candidate candidate = Session[SessionKey.Candidate] as Mti_Candidate;

            //In case session is still remaining, we will redirect to Test page
            if(candidate != null)
            {
                //We return list of response questions for candidate
                IGenerate repoGenerate = new RepoGenerate();
                List<Response_Question> questions = repoGenerate.GenerateQuestion(candidate.Id);

                return View("Test", questions);
            }

            return View();
        }

        /// <summary>
        /// Submit form to register new candidate
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Candidate(CandidateModel model)
        {
            //Check if model is valid
            if (ModelState.IsValid)
            {
                //Save candidate information
                ICandidate repoCandidate = new RepoCandidate();

                //create new candidate
                Mti_Candidate can = new Mti_Candidate
                {
                    DateMakeExam = DateTime.Now,
                    CanidateName = model.CandidateName,
                    CandidateBirthYear = model.CandidateBirthYear,
                    ImgURL = model.CvUrl,
                    level = model.Level
                };
                
                int candidateId = repoCandidate.Register(can);
                model.Id = candidateId;

                //save candidate to session
                Session[SessionKey.Candidate] = model;
                
                //Redirect to start page
                return RedirectToAction("Start");

            }
            return View(model);
        }

        /// <summary>
        /// Start screen: show some rules to candidate
        /// </summary>
        /// <returns></returns>
        public ActionResult Start()
        {
            var model = Session[SessionKey.Candidate] as CandidateModel;
            return View(model);
        }

        /// <summary>
        /// Start screen submit: they decide to start the form
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Start(FormCollection form)
        {
            CandidateModel candidate = Session[SessionKey.Candidate] as CandidateModel;

            //In case session is still remaining, we will redirect to Test page
            if (candidate != null)
            {
                //We return list of response questions for candidate
                IGenerate repoGenerate = new RepoGenerate();
                List<Response_Question> questions = repoGenerate.GenerateQuestion(candidate.Id);
                candidate.Questions = questions;
                candidate.StartedTime = DateTime.Now;

                return RedirectToAction("Test");
            }

            return RedirectToAction("Candidate");
        }

        /// <summary>
        /// Go to Test screen
        /// </summary>
        /// <returns></returns>
        public ActionResult Test()
        {
            var model = Session[SessionKey.Candidate] as CandidateModel;
            return View(model);
        }

        /// <summary>
        /// Save answer
        /// </summary>
        /// <param name="questionId"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public JsonResult SaveAnswer(int questionId, string value, int gotoQuestionIndex)
        {
            try
            {
                CandidateModel candidate = Session[SessionKey.Candidate] as CandidateModel;
                if (candidate != null && candidate.Questions != null)
                {                
                    var question = candidate.Questions.FirstOrDefault(q => q.QuestionId == questionId);
                    if (question != null)
                    {
                        //Save candidate index for unexpected tab closing
                        candidate.QuestionIndex = gotoQuestionIndex < 0 ? 0 : (gotoQuestionIndex >= candidate.Questions.Count ? candidate.Questions.Count - 1 : gotoQuestionIndex);                        
                        question.AnswerText = value;

                        if(question.QuestionType == CommonModel.QuestionType.Selection)
                        {
                            //Reset all selection
                            question.ListAnswer.ForEach(i => i.IsSelected = false);

                            //Check the selected value
                            var ans = question.ListAnswer.Where(a => a.AnswerId == int.Parse(value.Trim())).FirstOrDefault();
                            if (ans != null)
                                ans.IsSelected = true;
                        }
                        else if (question.QuestionType == CommonModel.QuestionType.MultiSelection)
                        {
                            //Reset all selection
                            question.ListAnswer.ForEach(i => i.IsSelected = false);

                            //Split value to multi values
                            int[] values = value.Split(',').Select(s => int.Parse(s.Trim())).ToArray();

                            //Check the selected values
                            var ansList = question.ListAnswer.Where(a => values.Contains(a.AnswerId)).ToList();
                            if (ansList != null)
                                ansList.ForEach(a => a.IsSelected = true);
                        }

                        //Finish state
                        if(gotoQuestionIndex >= candidate.Questions.Count)
                        {
                            List<Mti_Candidate_Question> candiateAnswerList = new List<Mti_Candidate_Question>();
                            foreach(var ques in candidate.Questions)
                            {
                                var candidateAns = new Mti_Candidate_Question
                                {
                                    CandidateId = candidate.Id,
                                    QuestionId = ques.QuestionId,
                                    CandidateAnswer = ques.AnswerText
                                };
                                candiateAnswerList.Add(candidateAns);
                            }
                            RepoCandidate repo = new RepoCandidate();                            
                            repo.CandidateAnswer(candiateAnswerList);

                            //Return the flag to redirect to finish page
                            return Json("done", JsonRequestBehavior.AllowGet);
                        }
                    }
                    
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Go to Finish screen
        /// </summary>
        /// <returns></returns>
        public ActionResult Finish()
        {
            return View();
        }

    }
}