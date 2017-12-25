using mti_tech_interview_examination.Common;
using mti_tech_interview_examination.Lib.Execute;
using mti_tech_interview_examination.Lib.Interface;
using mti_tech_interview_examination.Models;
using mti_tech_interview_examination.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace mti_tech_interview_examination.Controllers
{
    /// <summary>
    /// Admin region
    /// </summary>
    [CustomAuthentication("admin")]
    public class AdminController : Controller
    {
        /// <summary>
        /// Login form
        /// </summary>
        /// <returns></returns>    
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("CreateQuestion");
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

                return RedirectToAction("CreateQuestion");
            }

            return View(model);
        }

        /// <summary>
        /// Create question form
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateQuestion()
        {
            return View();
        }

        /// <summary>
        /// Submit form to register new candidate
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateQuestion(QuestionModel model)
        {
            //Check if model is valid
            if (ModelState.IsValid)
            {
                
                IQuestion repoCandidate = new RepoQuestion();

                //create new question
                Mti_Question question = new Mti_Question
                {
                    QuestionName = model.QuestionName,
                    QuestionContent = model.QuestionContent,
                    QuestionLevel = model.QuestionLevel,
                    QuestionType = model.QuestionType
                };

                //Create list of anwser
                List<Mti_Answer> answers = new List<Mti_Answer>();
                for(int i = 0; i < model.AnswerContents.Length; i++)
                {
                    Mti_Answer ans = new Mti_Answer
                    {
                        AnswerContent = model.AnswerContents[i],
                        IsRight = model.CorrectAnswerIndexes.Contains(i),
                         Question = question
                    };
                    answers.Add(ans);

                }
                repoCandidate.CreateQuestion(question, answers);

                //Redirect to start page
                return RedirectToAction("CreateQuestionSuccess");

            }
            return View(model);
        }

        /// <summary>
        /// CreateQuestionSuccess
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateQuestionSuccess()
        {
            return View("Notification", 
                new NotificationModel {
                    Message = "Added question successfully!",
                    ButtonText = "Continue to add",
                    ButtonLink = "/Admin/CreateQuestion"
                });
        }

        // <summary>
        /// UpdateQuestionSuccess
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateQuestionSuccess()
        {
            return View("Notification",
                new NotificationModel
                {
                    Message = "Updated question successfully!",
                    ButtonText = "Back to list",
                    ButtonLink = "/Admin/ListQuestions"
                });
        }
    }
}