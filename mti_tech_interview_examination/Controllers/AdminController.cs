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
            {
                if (HttpContext.User.Identity.Name == "admin")
                    return RedirectToAction("CreateQuestion");
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

                return RedirectToAction("ListCandidates");
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
        /// Update question
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UpdateQuestion(int id)
        {
            IQuestion repoCandidate = new RepoQuestion();

            //Get question
            Mti_Question question = repoCandidate.ViewQuestion(id);
            string[] answerContents = (question.Answers??new List<Mti_Answer>()).Select(a => a.AnswerContent).ToArray();
            int[] answerIds = (question.Answers ?? new List<Mti_Answer>()).Select(a => a.Id).ToArray();
            int[] correctAnswerIndexes = (question.Answers ?? new List<Mti_Answer>()).Where(a => a.IsRight == true).Select(a => a.Id).ToArray();

            if (question != null)
            {
                //Create question model
                QuestionModel model = new QuestionModel
                {
                    Id = question.Id,
                    QuestionContent = question.QuestionContent,
                    QuestionLevel = question.QuestionLevel,
                    QuestionName = question.QuestionName,
                    QuestionType = question.QuestionType,
                    AnswerContents = answerContents,
                    AnswerIds = answerIds,
                    CorrectAnswerIndexes = correctAnswerIndexes
                };
                return View(model);
            }

            return View();
        }

        /// <summary>
        /// Submit form to register new candidate
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateQuestion(QuestionModel model)
        {
            //Check if model is valid
            if (ModelState.IsValid)
            {
                IQuestion repoCandidate = new RepoQuestion();

                //create new question
                Mti_Question question = new Mti_Question
                {
                    Id = model.Id,
                    QuestionName = model.QuestionName,
                    QuestionContent = model.QuestionContent,
                    QuestionLevel = model.QuestionLevel,
                    QuestionType = model.QuestionType
                };

                //Create list of anwser
                List<Mti_Answer> answers = new List<Mti_Answer>();
                for (int i = 0; i < model.AnswerContents.Length; i++)
                {
                    Mti_Answer ans = new Mti_Answer
                    {
                        Id = model.AnswerIds[i],
                        AnswerContent = model.AnswerContents[i],
                        IsRight = model.CorrectAnswerIndexes.Contains(i),
                        QuestionId = model.Id
                    };
                    answers.Add(ans);

                }

                repoCandidate.UpdateQuestion(question, answers);

                //Redirect to start page
                return RedirectToAction("UpdateQuestionSuccess");

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

        /// <summary>
        /// List all the questions
        /// </summary>
        /// <returns></returns>
        public ActionResult ListQuestions(int? pageIndex)
        {
            pageIndex = pageIndex ?? 0;
            IQuestion repoQuestion = new RepoQuestion();
            QuestionListModel model = null;

            //TODO: temporarily get whole list, not care about performance
            List <Mti_Question> questions = repoQuestion.ListQuestion();
            if(questions != null)
            {
                model = new QuestionListModel
                {
                    Questions = questions.Skip(pageIndex.Value * Config.QuestionPageSize).Take(Config.QuestionPageSize).ToList(),
                    Paging = new PagingModel {
                        PageSize = Config.QuestionPageSize,
                        PageIndex = pageIndex.Value,
                        PageCount = (questions.Count / Config.QuestionPageSize) + (questions.Count % Config.QuestionPageSize != 0 ? 1 : 0),
                        BaseUrl = "ListQuestions"
                    }                    
                };    
            }
            
            return View(model);
        }

        /// <summary>
        /// List all the candidates
        /// </summary>
        /// <returns></returns>
        public ActionResult ListCandidates(int? pageIndex)
        {
            pageIndex = pageIndex ?? 0;
            ICandidate repoCandidate = new RepoCandidate();
            CandidateListModel model = null;

            //TODO: temporarily get whole list, not care about performance
            //TODO: the criteria searching will be handled later
            List<Mti_Candidate> candidates = repoCandidate.lstCandidate();

            if (candidates != null)
            {
                model = new CandidateListModel
                {
                    Candidates = candidates.Skip(pageIndex.Value * Config.CandidatePageSize).Take(Config.CandidatePageSize).ToList(),
                    Paging = new PagingModel
                    {
                        PageSize = Config.CandidatePageSize,
                        PageIndex = pageIndex.Value,
                        PageCount = (candidates.Count / Config.CandidatePageSize) + (candidates.Count % Config.CandidatePageSize != 0? 1: 0),
                        BaseUrl = "ListCandidates"
                    }
                };
            }

            return View(model);
        }
    }
}