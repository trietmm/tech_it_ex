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
        /// Go to Finish screen
        /// </summary>
        /// <returns></returns>
        public ActionResult Finish()
        {
            return View();
        }

    }
}